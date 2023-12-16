using HtmlAgilityPack;
using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using MyERP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class DaiXoSoService : Service<DaiXoSo>
    {
        public DaiXoSoService(IRepository<DaiXoSo> repository)
            : base(repository)
        {
        }

        public void LayKetQuaXS(DaiXoSoDTO self, DateTime? date = null)
        {
            var kqxsObj = DependencyResolver.Current.GetService<KetQuaXoSoService>();
            var d = date ?? DateTime.Today;
            var kq = kqxsObj.Search(x => x.DaiXSId == self.Id && x.Ngay == d).FirstOrDefault();
            if (kq != null)
                return;

            

            HtmlWeb web = new HtmlWeb();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var url_format = @"https://xskt.com.vn/ket-qua-xo-so-theo-ngay/{0}/{1}-{2}-{3}.html";
            var url = string.Format(url_format, StringUtils.CreateUrl(self.Name + " " + self.Code, "-"), d.Day, d.Month, d.Year);
            HtmlDocument document = web.Load(url);
            var selector = self.Code.Substring(2).ToUpper() + "0";
            var resultNode = document.DocumentNode.SelectSingleNode("//table[@id=\"" + selector + "\"]");
            if (resultNode == null)
                return;

            kq = new KetQuaXoSo()
            {
                Name = string.Format("{0} ngày {1}", self.Name, d.ToString("d")),
                DaiXSId = self.Id,
                Ngay = d,
            };

            var dict = new Dictionary<string, string>() {
                {"G8", "8" },
                {"G7", "7" },
                {"G6", "6" },
                {"G5", "5" },
                {"G4", "4" },
                {"G3", "3" },
                {"G2", "2" },
                {"G1", "1" },
                {"ĐB", "0" },
            };

            var trs = resultNode.SelectNodes("tr").Skip(1);
            foreach (var tr in trs)
            {
                var tdF = tr.SelectSingleNode("td[1]");
                var tdS = tr.SelectSingleNode("td[2]");
                if (tdF == null || tdS == null)
                    break;

                var tenGiai = tdF.InnerText.Trim();
                if (!dict.ContainsKey(tenGiai))
                    continue;

                var soTrungNode = tdS.SelectSingleNode("em");
                if (soTrungNode == null)
                    soTrungNode = tdS.SelectSingleNode("p");
                if (soTrungNode == null)
                    continue;
                var soTrungsText = StringUtils.StripHtmlFromString(Regex.Replace(soTrungNode.InnerHtml, @"<br>", " "));
                if (string.IsNullOrEmpty(soTrungsText))
                    return;
                var soTrungs = soTrungsText.Split(' ');
                foreach (var so in soTrungs)
                {
                    kq.Lines.Add(new KetQuaXoSoCT
                    {
                        Giai = dict[tenGiai],
                        SoTrung = so
                    });
                }
            }

            if (kq.Lines.Any())
                kqxsObj.Create(kq);
        }

        public IQueryable<DaiXoSo> GetDaiTheoNgay(DateTime date)
        {
            var thu = ((int)date.DayOfWeek).ToString();
            return Search(x => x.Rules.Any(s => s.Thu == thu));
        }
    }
}
