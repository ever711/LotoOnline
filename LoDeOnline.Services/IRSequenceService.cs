using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Data.SqlClient;
using LoDeOnline.Domain;
using LoDeOnline.Data;

namespace LoDeOnline.Services
{
    public class IRSequenceService: Service<IRSequence>
    {
        public IRSequenceService(IRepository<IRSequence> iRSequenceRepository)
            :base(iRSequenceRepository)
        {
        }

        public string GetId(string code)
        {
            return NextByCode(code);
        }

        public IRSequence GetByCode(string code)
        {
            return Search(domain: x => x.Code == code).FirstOrDefault();
        }

        public string GetId(long Id)
        {
            return NextById(Id);
        }

        public string NextByCode(string code)
        {
            var sequence = GetByCode(code);
            return Next(sequence);
        }

        public string NextById(long id)
        {
            var sequence = GetById(id);
            return Next(sequence);
        }

        public string Next(IRSequence sequence)
        {
            if (sequence == null)
                return null;

            var numberNext = sequence.NumberNext;
            sequence.NumberNext += sequence.NumberIncrement;

            var result = ExecuteSqlCommand("UPDATE IRSequences SET NumberNext=@number_next WHERE id=@id",
                parameters: new SqlParameter[]
                {
                    new SqlParameter("number_next", numberNext + sequence.NumberIncrement),
                    new SqlParameter("id", sequence.Id),
                });
            if (result == 0)
                throw new Exception("Update Sequence Fail");
            
            try
            {
                var now = DateTime.Now;
                var interpolatedPrefix = "";
                var interpolatedSuffix = "";
                var d = _InterpolationDict();
                if (!string.IsNullOrEmpty(sequence.Prefix))
                {
                    interpolatedPrefix = _Interpolate(sequence.Prefix, d);
                }
            
                if (!string.IsNullOrEmpty(sequence.Suffix))
                {
                     interpolatedSuffix = _Interpolate(sequence.Suffix, d);
                }

                return interpolatedPrefix + numberNext.ToString("D" + sequence.Padding) + interpolatedSuffix;
            }
            catch
            {
                throw new Exception("Invalid prefix or suffix for sequence");
            }
        }

        public IDictionary<string,string> _InterpolationDict()
        {
            var t = DateTime.Now;
            var dict = new Dictionary<string, string>();
            dict.Add("{yy}", t.ToString("yy"));
            dict.Add("{yyyy}", t.ToString("yyyy"));
            dict.Add("{MM}", t.ToString("MM"));
            dict.Add("{dd}", t.ToString("dd"));
            return dict;
        }

        public string _Interpolate(string s, IDictionary<string, string> d)
        {
            var regex = new Regex(@"\{\w+\}");
            var matches = regex.Matches(s);
            foreach(Match match in matches)
            {
                if (d.ContainsKey(match.Value))
                    s = s.Replace(match.Value, d[match.Value]);
            }
            return s;
        }
    }
}
