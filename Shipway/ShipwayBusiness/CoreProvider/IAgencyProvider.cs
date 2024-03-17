using ShipwayBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipwayBusiness.CoreProvider
{
    public interface IAgencyProvider
    {
        Agency GetAgencyById(int id);
        List<Agency> GetAllAgency();
        List<AgencyViewModel> GetAllAgency_V2();
        Agency GetAgencyByProvinceId(string provinceId);
        Agency InsertAgency(Agency agency);
        bool DeleteAgency(Agency agency);
        Agency UpdateAgency(Agency agency);
    }
}
