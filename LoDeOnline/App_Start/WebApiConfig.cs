using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace LoDeOnline
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.Namespace = "ODataService";

            #region KetQuaXoSo
            builder.EntitySet<KetQuaXoSoDTO>("KetQuaXoSo");
            builder.EntityType<KetQuaXoSoDTO>().Collection
                .Action("LayKetQua")
                .Parameter<DateTime?>("date");
            #endregion

            #region LoaiDe
            builder.EntitySet<LoaiDeDTO>("LoaiDe");
            builder.EntityType<LoaiDeDTO>().Collection
                .Function("DefaultGet")
                .ReturnsFromEntitySet<LoaiDeDTO>("LoaiDe");
            #endregion

            #region LoaiDeRule
            builder.EntitySet<LoaiDeRuleDTO>("LoaiDeRule");
            builder.EntityType<LoaiDeRuleDTO>().Collection
                .Function("DefaultGet")
                .ReturnsFromEntitySet<LoaiDeRuleDTO>("LoaiDeRule");
            #endregion

            #region AccountJournal
            builder.EntitySet<AccountJournalDTO>("AccountJournal");
            builder.EntityType<AccountJournalDTO>().Collection
                .Action("DefaultGet")
                .ReturnsFromEntitySet<AccountJournalDTO>("AccountJournal")
                .EntityParameter<AccountJournalDTO>("model");
            #endregion

            #region ResBank
            builder.EntitySet<ResBankDTO>("ResBank");
            builder.EntityType<ResBankDTO>().Collection
                .Function("DefaultGet")
                .ReturnsFromEntitySet<ResBankDTO>("ResBank");
            #endregion

            #region AccountPayment
            builder.EntitySet<AccountPaymentDTO>("AccountPayment");
            builder.EntityType<AccountPaymentDTO>().Collection
                .Function("DefaultGet")
                .ReturnsFromEntitySet<AccountPaymentDTO>("AccountPayment");

            builder.EntityType<AccountPaymentDTO>().Collection
               .Action("ActionPost")
               .CollectionParameter<long>("ids");

            builder.EntityType<AccountPaymentDTO>().Collection
              .Action("Unlink")
              .CollectionParameter<long>("ids");
            #endregion

            #region DanhDe
            builder.EntitySet<DanhDeDTO>("DanhDe");
            builder.EntityType<DanhDeDTO>().Collection
                .Function("DefaultGet")
                .ReturnsFromEntitySet<DanhDeDTO>("DanhDe");

            builder.EntityType<DanhDeDTO>().Collection
              .Action("ActionInvoiceOpen")
              .CollectionParameter<long>("ids");

            builder.EntityType<DanhDeDTO>().Collection
            .Action("DoKetQua")
            .CollectionParameter<long>("ids");

            builder.EntityType<DanhDeDTO>().Collection
                .Action("DoKetQuaAll");
            #endregion

            #region DanhDeLine
            builder.EntitySet<DanhDeLineDTO>("DanhDeLine");
            builder.EntityType<DanhDeLineDTO>().Collection
                .Function("DefaultGet")
                .ReturnsFromEntitySet<DanhDeLineDTO>("DanhDeLine");
            builder.EntityType<DanhDeLineDTO>().Collection
                .Action("OnChangeLoaiDe")
                .ReturnsFromEntitySet<DanhDeLineDTO>("DanhDeLine")
                .EntityParameter<DanhDeLineDTO>("model");
            #endregion

            #region DaiXoSo
            builder.EntitySet<DaiXoSoDTO>("DaiXoSo");
            #endregion

            #region ResGroup
            builder.EntitySet<ResGroupDTO>("ResGroup");
            builder.EntityType<ResGroupDTO>().Collection
                .Function("DefaultGet")
                .ReturnsFromEntitySet<ResGroupDTO>("ResGroup");
            #endregion

            #region IRModel
            builder.EntitySet<IRModelDTO>("IRModel");
            builder.EntityType<IRModelDTO>().Collection
             .Function("DefaultGet")
             .ReturnsFromEntitySet<IRModelDTO>("IRModel");
            #endregion

            #region IRRule
            builder.EntitySet<IRRuleDTO>("IRRule");
            builder.EntityType<IRRuleDTO>().Collection
                .Function("DefaultGet")
                .ReturnsFromEntitySet<IRRuleDTO>("IRRule");
            #endregion

            #region IRModelAccess
            builder.EntitySet<IRModelAccessDTO>("IRModelAccess");
            builder.EntityType<IRModelAccessDTO>().Collection
               .Function("DefaultGet")
               .ReturnsFromEntitySet<IRModelAccessDTO>("IRModelAccess");
            #endregion

            builder.EntitySet<ApplicationUserDTO>("ApplicationUser");
            builder.EntitySet<PartnerDTO>("Partner");
            builder.EntitySet<KetQuaXoSoCTDTO>("KetQuaXoSoCT");
            builder.EntitySet<LoDeCategoryDTO>("LoDeCategory");
            builder.EntitySet<LoaiDeCategoryDTO>("LoaiDeCategory");

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
