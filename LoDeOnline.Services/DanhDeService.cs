using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using Microsoft.AspNet.Identity;
using MyERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class DanhDeService : Service<DanhDe>
    {
        public DanhDeService(IRepository<DanhDe> repository)
            : base(repository)
        {
        }

        public void Compute(DanhDe self)
        {
            var lineObj = DependencyResolver.Current.GetService<DanhDeLineService>();
            lineObj.Compute(self.Lines);
            _ComputeAmount(self);
        }

        public void Create(DanhDe self)
        {
            Compute(self);
            Insert(self);
        }

        private void _ComputeAmount(DanhDe self)
        {
            self.AmountTotal = self.Lines.Sum(x => x.PriceSubtotal);
            //var loaiDe = self.LoaiDe;
            //var numbers = self.SoDanh.Split(new string[] { " - " }, StringSplitOptions.None);
            //var total_number = numbers.Length;
            //var currency = self.Company.Currency;
            //var multi = loaiDe.Multi ?? 1;
            //var pay_number = loaiDe.ThanhToan1K ?? 0;
            //if (multi == 0)
            //{
            //    total_number = 1;
            //}

            //self.Quantity = total_number;
            //var amount_total = currency.Round(self.PriceUnit * pay_number * total_number);
            //self.AmountTotal = amount_total;
        }

        private void _CreateSequence()
        {
            var seqObj = DependencyResolver.Current.GetService<IRSequenceService>();
            var seq = new IRSequence
            {
                Name = "Đánh đề",
                Prefix = "BĐ",
                Code = "danh.de",
                NumberNext = 1,
                NumberIncrement = 1,
                Padding = 4,
            };
            seqObj.Insert(seq);
        }

        public void ActionInvoiceOpen(IEnumerable<long> ids)
        {
            var invoices = Search(x => ids.Contains(x.Id)).ToList();
            ActionInvoiceOpen(invoices);
        }

        public void ActionInvoiceOpen(IEnumerable<DanhDe> self)
        {
            //lots of duplicate calls to action_invoice_open, so we remove those already open
            var to_open_invoices = self.Where(x => x.State != "open");
            if (to_open_invoices.Any(x => x.State != "draft"))
                throw new Exception("Biên đề trạng thái nháp mới có thể xác nhận");

            ActionMoveCreate(to_open_invoices);

            InvoiceValidate(to_open_invoices);

            SaveChanges();
        }

        private void InvoiceValidate(IEnumerable<DanhDe> self)
        {
            foreach (var invoice in self)
            {
                invoice.State = "open";
            }
        }

        private void ActionMoveCreate(IEnumerable<DanhDe> self)
        {
            //Creates invoice related analytics and financial move lines
            var accountMoveObj = DependencyResolver.Current.GetService<AccountMoveService>();
            var accountObj = DependencyResolver.Current.GetService<AccountService>();
            var journalObj = DependencyResolver.Current.GetService<AccountJournalService>();
            var journal = journalObj.Search(x => x.Type == "sale").FirstOrDefault();
            foreach (var invoice in self)
            {
                if (journal.Sequence == null)
                    throw new Exception("Vui lòng định nghĩa trình tự cho sổ nhật ký");

                if (invoice.Move != null)
                    return;

                var company = invoice.Company;
                var amls = new List<AccountMoveLine>();

                //tạo account move: 1 dòng trong nhật ký
                var move = new AccountMove()
                {
                    Ref = invoice.Name,
                    JournalId = journal.Id,
                    Journal = journal,
                    Date = invoice.Date,
                    CompanyId = invoice.CompanyId,
                    Company = invoice.Company,
                    PartnerId = invoice.PartnerId,
                };

                var cur = invoice.Company.Currency;
                var income_account = invoice.Company.AccountIncome;
                if (income_account == null)
                    throw new Exception("Vui lòng cấu hình tài khoản doanh thu cho công ty");
                var order_account = invoice.Company.AccountReceivable;
                if (order_account == null)
                    throw new Exception("Vui lòng cấu hình tài khoản phải thu của khách hàng cho công ty");
                foreach (var line in invoice.Lines)
                {
                    var name = line.LoaiDe.Name;
                    if (!string.IsNullOrEmpty(line.SoDanh))
                        name = name + " (" + line.SoDanh + ")";
                    var amount = line.PriceSubtotal;
                    var aml = new AccountMoveLine
                    {
                        Name = name,
                        Quantity = line.Quantity,
                        AccountId = income_account.Id,
                        Account = income_account,
                        Credit = amount,
                        Debit = 0,
                        PartnerId = line.DanhDe.PartnerId,
                        Move = move,
                    };

                    move.MoveLines.Add(aml);
                }

                var counter_part_line = new AccountMoveLine
                {
                    Name = !string.IsNullOrEmpty(invoice.Name) ? invoice.Name : "/",
                    AccountId = order_account.Id,
                    Account = order_account,
                    Credit = 0,
                    Debit = invoice.AmountTotal,
                    PartnerId = invoice.PartnerId,
                    Move = move,
                };
                move.MoveLines.Add(counter_part_line);

                accountMoveObj.Create(new List<AccountMove>() { move });

                accountMoveObj.Post(new List<AccountMove>() { move }, danhde: invoice);

                invoice.MoveId = move.Id;
                invoice.Move = move;
                invoice.MoveName = move.Name;
                invoice.Number = move.Name;
            }
        }

        public void Unlink(DanhDe self)
        {
            if (self.State != "draft" && self.State != "cancel")
                throw new Exception("Chỉ có thể xóa biên đề ở trạng thái nháp hoặc hủy bỏ");
            Delete(self);
        }

        private AccountMoveLineItem _DanhDeMoveLineGet(DanhDe self)
        {
            //var name = string.Format("Đánh {0} {1} {2}K", self.LoaiDe.Name, self.SoDanh, self.PriceUnit);
            var modelDataObj = DependencyResolver.Current.GetService<IRModelDataService>();
            return new AccountMoveLineItem
            {
                Type = "src",
                //Name = name,
                //PriceUnit = self.PriceUnit,
                //Quantity = self.Quantity,
                Price = self.AmountTotal,
                InvoiceId = self.Id,
            };
        }

        public override Expression<Func<DanhDe, bool>> RuleDomainGet(IRRule rule)
        {
            var userObj = DependencyResolver.Current.GetService<ApplicationUserManager>();
            var user = userObj.FindById(UserId);
            switch (rule.Code)
            {
                case "sale.danh_de_personal_rule":
                    return x => x.PartnerId == user.PartnerId;
                case "sale.danh_de_see_all":
                    return x => true;
                default:
                    return null;
            }
        }

        public class AccountMoveLineItem
        {
            public string Type { get; set; }

            public string Name { get; set; }

            public decimal? PriceUnit { get; set; }

            public decimal? Quantity { get; set; }

            public decimal? Price { get; set; }

            public Account Account { get; set; }
            public long AccountId { get; set; }

            public long InvoiceId { get; set; }
        }

        public void DoKetQua(IEnumerable<long> ids)
        {
            var self = Search(x => ids.Contains(x.Id)).ToList();
            DoKetQua(self);
        }

        public void DoKetQua(IEnumerable<DanhDe> self)
        {
            var kqxsObj = DependencyResolver.Current.GetService<KetQuaXoSoService>();
            var journalObj = DependencyResolver.Current.GetService<AccountJournalService>();
            var accountMoveObj = DependencyResolver.Current.GetService<AccountMoveService>();
            var journal = journalObj.Search(x => x.Type == "sale").FirstOrDefault();
            foreach (var invoice in self)
            {
                if (invoice.TrungMove != null)
                    continue;

                //tìm kết quả xổ số
                var date = invoice.Date;
                var dai_id = invoice.DaiId;
                var kqxs = kqxsObj.Search(x => x.Ngay == date && x.DaiXSId == dai_id).FirstOrDefault();
                if (kqxs == null)
                    continue;

                //dựa vào đánh loại đề gì
                var res = new List<string>();
                var line_strung = new Dictionary<DanhDeLine, int>();
                foreach (var line in invoice.Lines)
                {
                    //nếu loại đề là bao lô 2 số thì dò từng giải và kiểm tra số đánh có trúng ko?
                    //loại đề có nhiều rule, nếu rule nào thỏa mãn thì là trúng
                    var number = HamDoKetQuaXS(line, line.LoaiDe, kqxs);
                    if (number > 0)
                    {
                        line_strung.Add(line, number);
                    }
                }

                //dò kết quả và ghi sổ trúng hoặc trật
                if (line_strung.Any())
                {
                    //tạo bút toán trúng số
                    //tạo account move: 1 dòng trong nhật ký
                    var move = new AccountMove()
                    {
                        JournalId = journal.Id,
                        Journal = journal,
                        CompanyId = invoice.CompanyId,
                        Company = invoice.Company,
                    };

                    var cur = invoice.Company.Currency;
                    var expense_account = invoice.Company.AccountExpense;
                    if (expense_account == null)
                        throw new Exception("Vui lòng cấu hình tài khoản chi phí cho công ty");
                    var order_account = invoice.Company.AccountReceivable;
                    if (order_account == null)
                        throw new Exception("Vui lòng cấu hình tài khoản phải thu của khách hàng cho công ty");
                    decimal? counter_part_total = 0;
                    var notes = new List<string>();
                    foreach (var item in line_strung)
                    {
                        var line = item.Key;
                        var number = item.Value;
                        var name = "Trúng " + line.LoaiDe.Name;
                        if (!string.IsNullOrEmpty(line.SoDanh))
                            name = name + " " + line.SoDanh;
                        if ((line.LoaiDe.Multi ?? true))
                            name += " " + number + " lần";
                        var amount = line.Quantity * (line.LoaiDe.ThangGap ?? 0) * 1000 * number;
                        var aml = new AccountMoveLine
                        {
                            Name = name,
                            Quantity = line.Quantity,
                            AccountId = expense_account.Id,
                            Account = expense_account,
                            Debit = amount,
                            Credit = 0,
                            PartnerId = line.DanhDe.PartnerId,
                            Move = move,
                        };

                        move.MoveLines.Add(aml);
                        counter_part_total += amount;
                        notes.Add(name);
                    }

                    var counter_part_line = new AccountMoveLine
                    {
                        Name = "Trúng - " + invoice.Number,
                        AccountId = order_account.Id,
                        Account = order_account,
                        Credit = counter_part_total,
                        Debit = 0,
                        PartnerId = invoice.PartnerId,
                        Move = move,
                    };
                    move.MoveLines.Add(counter_part_line);

                    accountMoveObj.Create(new List<AccountMove>() { move });

                    accountMoveObj.Post(new List<AccountMove>() { move }, danhde: invoice);

                    invoice.TrungMoveId = move.Id;
                    invoice.TrungMove = move;

                    invoice.KetQua = string.Join(@"\n", notes);
                }
                else
                {
                    invoice.KetQua = "Không trúng";
                }

                invoice.State = "done";
            }

            SaveChanges();
        }

        public int HamDoKetQuaXS(DanhDeLine ddline, LoaiDe loai_de, KetQuaXoSo kqxs)
        {
            if (ddline.LoaiDe.Type == "normal")
            {
                var so_do = ddline.SoDanh;
                var number = DoSo(so_do, loai_de.Rules, kqxs, timNLan: loai_de.Multi ?? true);
                return number;
            }
            else if (ddline.LoaiDe.Type == "xien")
            {
                var xien_numbers = ddline.XienNumbers;
                var soluongxien = ddline.LoaiDe.SoLuongXien ?? 1;
                foreach (var xien_number in xien_numbers)
                {
                    var so_xien = xien_number.SoXien;
                    var number = DoSo(so_xien, loai_de.Rules, kqxs, timNLan: loai_de.Multi ?? true);
                    if (number >= 1)
                        soluongxien--;
                    else
                        return 0;
                }

                if (soluongxien == 0)
                    return 1;
                return 0;
            }
            else if (ddline.LoaiDe.Type == "xientruot")
            {
                var xien_numbers = ddline.XienNumbers;
                var soluongxien = ddline.LoaiDe.SoLuongXien ?? 1;
                foreach (var xien_number in xien_numbers)
                {
                    var so_xien = xien_number.SoXien;
                    var number = DoSo(so_xien, loai_de.Rules, kqxs, timNLan: loai_de.Multi ?? true);
                    if (number == 0)
                        soluongxien--;
                    else
                        return 0;
                }

                if (soluongxien == 0)
                    return 1;
                return 0;
            }

            return 0;

        }

        public int DoSo(string so_do, IEnumerable<LoaiDeRule> rules, KetQuaXoSo kqxs, bool timNLan = true)
        {
            var number = 0;
            foreach (var rule in rules)
            {
                //nếu rule là xiên trượt
                foreach (var line in kqxs.Lines)
                {
                    if (rule.GiaiDanh != "all" && rule.GiaiDanh != line.Giai)
                        continue;
                    if (line.SoTrung.Length < rule.SoLuongDanh || line.SoTrung.Length < 2)
                        continue;
                    string so_tmp = "";
                    if (rule.ViTriDanh == "chu_so_cuoi")
                        so_tmp = line.SoTrung.Substring(line.SoTrung.Length - rule.SoLuongDanh, rule.SoLuongDanh);
                    else if (rule.ViTriDanh == "hang_chuc")
                        so_tmp = line.SoTrung.Substring(line.SoTrung.Length - 2, 1);

                    if (so_tmp == so_do)
                        number++;
                    if (!timNLan)
                        break;
                }

                if (!(rule.Cumulative ?? false))
                    break;
            }

            return number;
        }
    }
}
