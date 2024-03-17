using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipwayBusiness.Entities
{
    public class Agency
    {
        public int AgencyId { get; set; }
        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AgencyName { get; set; }
        public string ActivityTime { get; set; }
        public bool IsCentralWarehouse { get; set; }
    }

    public class AgencyViewModel
    {
        public Agency Agency { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }
        public Ward Ward { get; set; }
    }
}