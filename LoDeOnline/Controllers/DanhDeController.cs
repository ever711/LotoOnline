using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Models;
using LoDeOnline.Services;
using Microsoft.AspNet.Identity;
using MyERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Controllers
{
    public class DanhDeController : Controller
    {
        private readonly DanhDeService _danhDeService;
        private readonly DaiXoSoService _daixsService;
        private readonly LoaiDeService _loaiDeService;
        private readonly ApplicationUserManager _userManager;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly PartnerService _partnerService;
        public DanhDeController(IUnitOfWorkAsync unitOfWork,
            DanhDeService danhDeService,
            ApplicationUserManager userManager,
            DaiXoSoService daixsService,
            LoaiDeService loaiDeService,
            PartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _danhDeService = danhDeService;
            _userManager = userManager;
            _daixsService = daixsService;
            _loaiDeService = loaiDeService;
            _partnerService = partnerService;
        }

        [HttpPost]
        public ActionResult Ghi(CreateDanhDeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Dữ liệu không hợp lệ",
                }, JsonRequestBehavior.AllowGet);
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    success = false,
                    message = "Bạn cần đăng nhập để thực hiện chức năng này.",
                }, JsonRequestBehavior.AllowGet);
            }

            if (model.Quantity <= 0)
            {
                return Json(new
                {
                    success = false,
                    message = "Vui lòng nhập số tiền trên 1 con",
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                _unitOfWork.BeginTransaction();

                var user = _userManager.FindById(User.Identity.GetUserId());
                var daixs = _daixsService.GetById(model.DaiId);
                var loaiDe = _loaiDeService.GetById(model.LoaiDeId);

                var danh_de = new DanhDe
                {
                    DaiId = model.DaiId,
                    Dai = daixs,
                    Date = model.Date,
                    Company = user.Company,
                    Partner = user.Partner,
                };

                if (loaiDe.Type == "xien" || loaiDe.Type == "xientruot")
                {
                    var numbers = model.SoDanh.Split(new string[] { " - " }, StringSplitOptions.None);
                    danh_de.Lines.Add(new DanhDeLine
                    {
                        DanhDe = danh_de,
                        LoaiDe = loaiDe,
                        Quantity = model.Quantity,
                        PriceUnit = (loaiDe.ThanhToan1K ?? 0) * 1000,
                        XienNumbers = numbers.Select(x => new DanhDeLineXien
                        {
                            SoXien = x
                        }).ToList()
                    });
                }
                else
                {
                    var numbers = model.SoDanh.Split(new string[] { " - " }, StringSplitOptions.None);
                    foreach (var number in numbers)
                    {
                        danh_de.Lines.Add(new DanhDeLine
                        {
                            DanhDe = danh_de,
                            LoaiDe = loaiDe,
                            Quantity = model.Quantity,
                            PriceUnit = (loaiDe.ThanhToan1K ?? 0) * 1000,
                            SoDanh = number,
                        });
                    }
                }

                _danhDeService.Compute(danh_de);

                //check xem có đủ tiền để đánh không
                var credit = -_partnerService.CreditDebitGet(new List<long>() { user.PartnerId })[user.PartnerId].Credit;
                if (credit < danh_de.AmountTotal)
                {
                    return Json(new
                    {
                        success = false,
                        message = string.Format("Số dư không đủ để đánh đề. Số dư {0} K, tổng tiền đánh {1} K", credit / 1000, danh_de.AmountTotal / 1000),
                    }, JsonRequestBehavior.AllowGet);
                }

                //check xem còn có thể ghi đề được hay không?
                var time = DateTime.Now;
                var thu = ((int)danh_de.Date.DayOfWeek).ToString();
                var dai_rule = danh_de.Dai.Rules.FirstOrDefault(x => x.Thu == thu);
                if (dai_rule == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = string.Format("Đài {0} không có mở số ngày {1}", danh_de.Dai.Name, danh_de.Date.ToString("d")),
                    }, JsonRequestBehavior.AllowGet);
                }

                if (!dai_rule.TimeOpen.HasValue)
                {
                    return Json(new
                    {
                        success = false,
                        message = string.Format("Đài {0} chưa quy định giờ mở số", danh_de.Dai.Name),
                    }, JsonRequestBehavior.AllowGet);
                }

                var soPhut = danh_de.Company.SoPhutTruocKhiMoSo ?? 0;
                var timeDaiOpen = dai_rule.TimeOpen.Value.TimeOfDay;
                var timeChoPhep = danh_de.Date.Add(timeDaiOpen).AddMinutes(-soPhut);
                if (time >= timeChoPhep)
                {
                    return Json(new
                    {
                        success = false,
                        message = string.Format("Hết giờ đánh đề. Vui lòng đánh trước {0}", timeChoPhep.ToString("HH:mm")),
                    }, JsonRequestBehavior.AllowGet);
                }

                _danhDeService.Insert(danh_de);

                _danhDeService.ActionInvoiceOpen(new List<DanhDe>() { danh_de });

                _unitOfWork.Commit();

                return Json(new
                {
                    success = true,
                    message = "Ghi đề thành công."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                return Json(new
                {
                    success = false,
                    message = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}