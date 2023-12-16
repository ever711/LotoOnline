using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoDeOnline.Applications.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        #region DaiXoSo

        public static DaiXoSoDTO ToModel(this DaiXoSo entity)
        {
            return entity.MapTo<DaiXoSo, DaiXoSoDTO>();
        }

        public static DaiXoSo ToEntity(this DaiXoSoDTO model)
        {
            return model.MapTo<DaiXoSoDTO, DaiXoSo>();
        }

        public static DaiXoSo ToEntity(this DaiXoSoDTO model, DaiXoSo destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region KetQuaXoSo

        public static KetQuaXoSoDTO ToModel(this KetQuaXoSo entity)
        {
            return entity.MapTo<KetQuaXoSo, KetQuaXoSoDTO>();
        }

        public static KetQuaXoSo ToEntity(this KetQuaXoSoDTO model)
        {
            return model.MapTo<KetQuaXoSoDTO, KetQuaXoSo>();
        }

        public static KetQuaXoSo ToEntity(this KetQuaXoSoDTO model, KetQuaXoSo destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region KetQuaXoSoCT

        public static KetQuaXoSoCTDTO ToModel(this KetQuaXoSoCT entity)
        {
            return entity.MapTo<KetQuaXoSoCT, KetQuaXoSoCTDTO>();
        }

        public static KetQuaXoSoCT ToEntity(this KetQuaXoSoCTDTO model)
        {
            return model.MapTo<KetQuaXoSoCTDTO, KetQuaXoSoCT>();
        }

        public static KetQuaXoSoCT ToEntity(this KetQuaXoSoCTDTO model, KetQuaXoSoCT destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region LoaiDe

        public static LoaiDeDTO ToModel(this LoaiDe entity)
        {
            return entity.MapTo<LoaiDe, LoaiDeDTO>();
        }

        public static LoaiDe ToEntity(this LoaiDeDTO model)
        {
            return model.MapTo<LoaiDeDTO, LoaiDe>();
        }

        public static LoaiDe ToEntity(this LoaiDeDTO model, LoaiDe destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region LoaiDeRule

        public static LoaiDeRuleDTO ToModel(this LoaiDeRule entity)
        {
            return entity.MapTo<LoaiDeRule, LoaiDeRuleDTO>();
        }

        public static LoaiDeRule ToEntity(this LoaiDeRuleDTO model)
        {
            return model.MapTo<LoaiDeRuleDTO, LoaiDeRule>();
        }

        public static LoaiDeRule ToEntity(this LoaiDeRuleDTO model, LoaiDeRule destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region LoDeCategory

        public static LoDeCategoryDTO ToModel(this LoDeCategory entity)
        {
            return entity.MapTo<LoDeCategory, LoDeCategoryDTO>();
        }

        public static LoDeCategory ToEntity(this LoDeCategoryDTO model)
        {
            return model.MapTo<LoDeCategoryDTO, LoDeCategory>();
        }

        public static LoDeCategory ToEntity(this LoDeCategoryDTO model, LoDeCategory destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region LoaiDeCategory

        public static LoaiDeCategoryDTO ToModel(this LoaiDeCategory entity)
        {
            return entity.MapTo<LoaiDeCategory, LoaiDeCategoryDTO>();
        }

        public static LoaiDeCategory ToEntity(this LoaiDeCategoryDTO model)
        {
            return model.MapTo<LoaiDeCategoryDTO, LoaiDeCategory>();
        }

        public static LoaiDeCategory ToEntity(this LoaiDeCategoryDTO model, LoaiDeCategory destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region DaiXoSoRule

        public static DaiXoSoRuleDTO ToModel(this DaiXoSoRule entity)
        {
            return entity.MapTo<DaiXoSoRule, DaiXoSoRuleDTO>();
        }

        public static DaiXoSoRule ToEntity(this DaiXoSoRuleDTO model)
        {
            return model.MapTo<DaiXoSoRuleDTO, DaiXoSoRule>();
        }

        public static DaiXoSoRule ToEntity(this DaiXoSoRuleDTO model, DaiXoSoRule destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region AccountJournal

        public static AccountJournalDTO ToModel(this AccountJournal entity)
        {
            return entity.MapTo<AccountJournal, AccountJournalDTO>();
        }

        public static AccountJournal ToEntity(this AccountJournalDTO model)
        {
            return model.MapTo<AccountJournalDTO, AccountJournal>();
        }

        public static AccountJournal ToEntity(this AccountJournalDTO model, AccountJournal destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region ResBank

        public static ResBankDTO ToModel(this ResBank entity)
        {
            return entity.MapTo<ResBank, ResBankDTO>();
        }

        public static ResBank ToEntity(this ResBankDTO model)
        {
            return model.MapTo<ResBankDTO, ResBank>();
        }

        public static ResBank ToEntity(this ResBankDTO model, ResBank destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region AccountPayment

        public static AccountPaymentDTO ToModel(this AccountPayment entity)
        {
            return entity.MapTo<AccountPayment, AccountPaymentDTO>();
        }

        public static AccountPayment ToEntity(this AccountPaymentDTO model)
        {
            return model.MapTo<AccountPaymentDTO, AccountPayment>();
        }

        public static AccountPayment ToEntity(this AccountPaymentDTO model, AccountPayment destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Partner

        public static PartnerDTO ToModel(this Partner entity)
        {
            return entity.MapTo<Partner, PartnerDTO>();
        }

        public static Partner ToEntity(this PartnerDTO model)
        {
            return model.MapTo<PartnerDTO, Partner>();
        }

        public static Partner ToEntity(this PartnerDTO model, Partner destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region DanhDe

        public static DanhDeDTO ToModel(this DanhDe entity)
        {
            return entity.MapTo<DanhDe, DanhDeDTO>();
        }

        public static DanhDe ToEntity(this DanhDeDTO model)
        {
            return model.MapTo<DanhDeDTO, DanhDe>();
        }

        public static DanhDe ToEntity(this DanhDeDTO model, DanhDe destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region DanhDeLine

        public static DanhDeLineDTO ToModel(this DanhDeLine entity)
        {
            return entity.MapTo<DanhDeLine, DanhDeLineDTO>();
        }

        public static DanhDeLine ToEntity(this DanhDeLineDTO model)
        {
            return model.MapTo<DanhDeLineDTO, DanhDeLine>();
        }

        public static DanhDeLine ToEntity(this DanhDeLineDTO model, DanhDeLine destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region DanhDeLineXien

        public static DanhDeLineXienDTO ToModel(this DanhDeLineXien entity)
        {
            return entity.MapTo<DanhDeLineXien, DanhDeLineXienDTO>();
        }

        public static DanhDeLineXien ToEntity(this DanhDeLineXienDTO model)
        {
            return model.MapTo<DanhDeLineXienDTO, DanhDeLineXien>();
        }

        public static DanhDeLineXien ToEntity(this DanhDeLineXienDTO model, DanhDeLineXien destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region ResGroup

        public static ResGroupDTO ToModel(this ResGroup entity)
        {
            return entity.MapTo<ResGroup, ResGroupDTO>();
        }

        public static ResGroup ToEntity(this ResGroupDTO model)
        {
            return model.MapTo<ResGroupDTO, ResGroup>();
        }

        public static ResGroup ToEntity(this ResGroupDTO model, ResGroup destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region IRModelAccess

        public static IRModelAccessDTO ToModel(this IRModelAccess entity)
        {
            return entity.MapTo<IRModelAccess, IRModelAccessDTO>();
        }

        public static IRModelAccess ToEntity(this IRModelAccessDTO model)
        {
            return model.MapTo<IRModelAccessDTO, IRModelAccess>();
        }

        public static IRModelAccess ToEntity(this IRModelAccessDTO model, IRModelAccess destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region IRRule

        public static IRRuleDTO ToModel(this IRRule entity)
        {
            return entity.MapTo<IRRule, IRRuleDTO>();
        }

        public static IRRule ToEntity(this IRRuleDTO model)
        {
            return model.MapTo<IRRuleDTO, IRRule>();
        }

        public static IRRule ToEntity(this IRRuleDTO model, IRRule destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region IRModel

        public static IRModelDTO ToModel(this IRModel entity)
        {
            return entity.MapTo<IRModel, IRModelDTO>();
        }

        public static IRModel ToEntity(this IRModelDTO model)
        {
            return model.MapTo<IRModelDTO, IRModel>();
        }

        public static IRModel ToEntity(this IRModelDTO model, IRModel destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region ApplicationUser

        public static ApplicationUserDTO ToModel(this ApplicationUser entity)
        {
            return entity.MapTo<ApplicationUser, ApplicationUserDTO>();
        }

        public static ApplicationUser ToEntity(this ApplicationUserDTO model)
        {
            return model.MapTo<ApplicationUserDTO, ApplicationUser>();
        }

        public static ApplicationUser ToEntity(this ApplicationUserDTO model, ApplicationUser destination)
        {
            return model.MapTo(destination);
        }

        #endregion
    }
}