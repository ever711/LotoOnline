using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class AccountMoveService : Service<AccountMove>
    {
        public AccountMoveService(IRepository<AccountMove> repository)
            : base(repository)
        {
        }

        public void Create(IEnumerable<AccountMove> self)
        {
            var amlObj = DependencyResolver.Current.GetService<AccountMoveLineService>();
            foreach (var move in self)
            {
                amlObj._Compute(move.MoveLines);
            }
            _Compute(self);
            Insert(self);
        }

        private void _Compute(IEnumerable<AccountMove> self)
        {
            foreach (var move in self)
            {
                move.CompanyId = move.Journal.CompanyId;
                move.Company = move.Journal.Company;
            }

            _ComputePartner(self);
            _ComputeAmount(self);
        }

        private void _ComputeAmount(IEnumerable<AccountMove> self)
        {
            foreach (var move in self)
            {
                decimal? total = 0;
                foreach (var line in move.MoveLines)
                {
                    total += line.Debit;
                }
                move.Amount = total;
            }
        }

        private void _ComputePartner(IEnumerable<AccountMove> self)
        {
            foreach (var move in self)
            {
                var partners = move.MoveLines.Select(x => x.Partner).Distinct().ToList();
                if (partners.Count == 1 && partners[0] != null)
                    move.PartnerId = partners[0].Id;
            }
        }

        public void Post(IEnumerable<AccountMove> self, DanhDe danhde = null)
        {
            _PostValidate(self);
            foreach (var move in self)
            {
                if (move.Name == "/")
                {
                    var newName = "";
                    var journal = move.Journal;
                    if (danhde != null && !string.IsNullOrEmpty(danhde.MoveName) && danhde.MoveName != "/")
                        newName = danhde.MoveName;
                    else
                    {
                        if (journal.Sequence != null)
                        {
                            var sequence = journal.Sequence;
                            newName = DependencyResolver.Current.GetService<IRSequenceService>().GetId(sequence.Id);
                        }
                        else
                            throw new Exception("Vui lòng định nghĩa trình tự cho nhật ký này");

                        if (!string.IsNullOrEmpty(newName))
                            move.Name = newName;
                    }
                }

                move.State = "posted";
            }
        }

        private void _PostValidate(IEnumerable<AccountMove> moves)
        {
            foreach (var move in moves)
            {
                if (move.MoveLines.Any())
                {
                    if (!move.MoveLines.All(x => x.CompanyId == move.CompanyId))
                        throw new Exception("Không thể tạo bút toán khác công ty.");
                }
            }

            AssertBalanced(moves);
        }

        public void AssertBalanced(IEnumerable<AccountMove> moves)
        {
            var moveLineObj = DependencyResolver.Current.GetService<AccountMoveLineService>();
            var moveIds = moves.Select(x => x.Id).ToList();
            AssertBalanced(moveIds);
        }

        public void AssertBalanced(IList<long> ids)
        {
            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("lm", 0.00001));
            var res = SqlQuery<long?>("SELECT MoveId " +
                "FROM AccountMoveLines " +
                "WHERE MoveId in (" + string.Join(", ", ids) + ") " +
                "GROUP BY MoveId " +
                "HAVING abs(sum(Debit) - sum(Credit)) > @lm", parameters.ToArray()).ToList();

            if (res.Count != 0)
                throw new Exception("Không thể tạo bút toán không cân đối.");
        }
    }
}
