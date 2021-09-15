using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class Country
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string TwoCharCountryCode { get; set; }
        public string ThreeCharCountryCode { get; set; }
    }
}
