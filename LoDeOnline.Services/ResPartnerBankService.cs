using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class ResPartnerBankService : Service<ResPartnerBank>
    {
        public ResPartnerBankService(IRepository<ResPartnerBank> repository)
            : base(repository)
        {
        }

        public ResPartnerBank Create(ResPartnerBank self)
        {
            self.SanitizedAccNumber = SanitizeAccountNumber(self);
            return Insert(self);
        }

        private string SanitizeAccountNumber(ResPartnerBank self)
        {
            if (!string.IsNullOrEmpty(self.AccNumber))
                return Regex.Replace(self.AccNumber, @"\W+", "").ToUpper();
            return "";
        }

        public void Write(ResPartnerBank self)
        {
            self.SanitizedAccNumber = SanitizeAccountNumber(self);
            Update(self);
        }
    }
}
