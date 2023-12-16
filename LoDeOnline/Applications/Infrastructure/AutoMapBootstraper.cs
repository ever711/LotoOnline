using AutoMapper;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoDeOnline.Applications.Infrastructure
{
    public static class AutoMapBootstraper
    {
        public static void Initialise()
        {
            Mapper.Initialize(cfg =>
            {
                #region DaiXoSo
                cfg.CreateMap<DaiXoSo, DaiXoSoDTO>()
                    .ForMember(dest => dest.Mien, mo => mo.Ignore())
                    .ForMember(dest => dest.Rules, mo => mo.Ignore());
                cfg.CreateMap<DaiXoSoDTO, DaiXoSo>()
                    .ForMember(dest => dest.Mien, mo => mo.Ignore())
                    .ForMember(dest => dest.Rules, mo => mo.Ignore());
                #endregion
                #region KetQuaXoSo
                cfg.CreateMap<KetQuaXoSo, KetQuaXoSoDTO>()
                    .ForMember(dest => dest.DaiXS, mo => mo.Ignore())
                    .ForMember(dest => dest.Lines, mo => mo.Ignore());
                cfg.CreateMap<KetQuaXoSoDTO, KetQuaXoSo>()
                    .ForMember(dest => dest.DaiXS, mo => mo.Ignore())
                    .ForMember(dest => dest.Lines, mo => mo.Ignore());
                #endregion
                #region KetQuaXoSoCT
                cfg.CreateMap<KetQuaXoSoCT, KetQuaXoSoCTDTO>();
                cfg.CreateMap<KetQuaXoSoCTDTO, KetQuaXoSoCT>();
                #endregion
                #region LoaiDe
                cfg.CreateMap<LoaiDe, LoaiDeDTO>()
                    .ForMember(dest => dest.Rules, mo => mo.Ignore())
                    .ForMember(dest => dest.LoaiDeCateg, mo => mo.Ignore())
                    .ForMember(dest => dest.LoDeCateg, mo => mo.Ignore());
                cfg.CreateMap<LoaiDeDTO, LoaiDe>()
                    .ForMember(dest => dest.Rules, mo => mo.Ignore())
                    .ForMember(dest => dest.LoaiDeCateg, mo => mo.Ignore())
                    .ForMember(dest => dest.LoDeCateg, mo => mo.Ignore());
                #endregion
                #region LoaiDeRule
                cfg.CreateMap<LoaiDeRule, LoaiDeRuleDTO>().ReverseMap();
                #endregion
                #region LoDeCategory
                cfg.CreateMap<LoDeCategory, LoDeCategoryDTO>().ReverseMap();
                #endregion
                #region LoaiDeCategory
                cfg.CreateMap<LoaiDeCategory, LoaiDeCategoryDTO>()
                  .ForMember(dest => dest.LoDeCategories, mo => mo.Ignore());
                cfg.CreateMap<LoaiDeCategoryDTO, LoaiDeCategory>()
                  .ForMember(dest => dest.LoDeCategories, mo => mo.Ignore());
                #endregion
                #region DaiXoSoRule
                cfg.CreateMap<DaiXoSoRule, DaiXoSoRuleDTO>().ReverseMap();
                #endregion
                #region AccountJournal
                cfg.CreateMap<AccountJournal, AccountJournalDTO>()
                    .ForMember(dest => dest.BankAccNumber, mo => mo.MapFrom(x => x.BankAccount.AccNumber))
                    .ForMember(dest => dest.BankName, mo => mo.MapFrom(x => x.BankAccount.Bank.Name));
                cfg.CreateMap<AccountJournalDTO, AccountJournal>();
                #endregion
                #region ResBank
                cfg.CreateMap<ResBank, ResBankDTO>();
                cfg.CreateMap<ResBankDTO, ResBank>();
                #endregion
                #region AccountPayment
                cfg.CreateMap<AccountPayment, AccountPaymentDTO>()
                    .ForMember(dest => dest.BankAccNumber, mo => mo.MapFrom(x => x.Journal.BankAccount.AccNumber));
                cfg.CreateMap<AccountPaymentDTO, AccountPayment>();
                #endregion
                #region Partner
                cfg.CreateMap<Partner, PartnerDTO>();
                cfg.CreateMap<PartnerDTO, Partner>();
                #endregion
                #region DanhDe
                cfg.CreateMap<DanhDe, DanhDeDTO>()
                    .ForMember(dest => dest.Partner, mo => mo.Ignore())
                    .ForMember(dest => dest.Lines, mo => mo.Ignore())
                    .ForMember(dest => dest.Dai, mo => mo.Ignore());
                cfg.CreateMap<DanhDeDTO, DanhDe>()
                    .ForMember(dest => dest.Partner, mo => mo.Ignore())
                    .ForMember(dest => dest.Lines, mo => mo.Ignore())
                    .ForMember(dest => dest.Dai, mo => mo.Ignore());
                #endregion
                #region DanhDeLine
                cfg.CreateMap<DanhDeLine, DanhDeLineDTO>()
                    .ForMember(dest => dest.LoaiDe, mo => mo.Ignore())
                    .ForMember(dest => dest.XienNumbers, mo => mo.Ignore());
                cfg.CreateMap<DanhDeLineDTO, DanhDeLine>()
                    .ForMember(dest => dest.LoaiDe, mo => mo.Ignore())
                    .ForMember(dest => dest.XienNumbers, mo => mo.Ignore());
                #endregion
                #region DanhDeLineXien
                cfg.CreateMap<DanhDeLineXien, DanhDeLineXienDTO>();
                cfg.CreateMap<DanhDeLineXienDTO, DanhDeLineXien>();
                #endregion
                #region ResGroup
                cfg.CreateMap<ResGroup, ResGroupDTO>()
                    .ForMember(dest => dest.ModelAccesses, mo => mo.Ignore())
                    .ForMember(dest => dest.Rules, mo => mo.Ignore())
                    .ForMember(dest => dest.Users, mo => mo.Ignore())
                    .ForMember(dest => dest.Implieds, mo => mo.Ignore());
                cfg.CreateMap<ResGroupDTO, ResGroup>()
                    .ForMember(dest => dest.ModelAccesses, mo => mo.Ignore())
                    .ForMember(dest => dest.Rules, mo => mo.Ignore())
                    .ForMember(dest => dest.Users, mo => mo.Ignore())
                    .ForMember(dest => dest.Implieds, mo => mo.Ignore())
                    .ForMember(dest => dest.Category, mo => mo.Ignore());
                #endregion
                #region IRModel
                cfg.CreateMap<IRModel, IRModelDTO>();
                cfg.CreateMap<IRModelDTO, IRModel>();
                #endregion
                #region IRModelAccess
                cfg.CreateMap<IRModelAccess, IRModelAccessDTO>()
                    .ForMember(dest => dest.Model, mo => mo.Ignore());
                cfg.CreateMap<IRModelAccessDTO, IRModelAccess>()
                    .ForMember(dest => dest.Group, mo => mo.Ignore())
                    .ForMember(dest => dest.Model, mo => mo.Ignore());
                #endregion
                #region IRRule
                cfg.CreateMap<IRRule, IRRuleDTO>()
                    .ForMember(dest => dest.Groups, mo => mo.Ignore())
                    .ForMember(dest => dest.Model, mo => mo.Ignore());
                cfg.CreateMap<IRRuleDTO, IRRule>()
                    .ForMember(dest => dest.Groups, mo => mo.Ignore())
                    .ForMember(dest => dest.Model, mo => mo.Ignore());
                #endregion
                #region ApplicationUser
                cfg.CreateMap<ApplicationUser, ApplicationUserDTO>()
                    .ForMember(dest => dest.Groups, mo => mo.Ignore());
                cfg.CreateMap<ApplicationUserDTO, ApplicationUser>()
                    .ForMember(dest => dest.Groups, mo => mo.Ignore());
                #endregion
            });
        }
    }
}