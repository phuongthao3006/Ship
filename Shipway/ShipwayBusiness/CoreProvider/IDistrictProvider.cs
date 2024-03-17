using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipwayBusiness.Entities;
using System.Collections.ObjectModel;

namespace ShipwayBusiness.CoreProvider
{
    public interface IDistrictProvider
    {
        List<District> GetAllDistrict();
        List<District> GetDistrictByIdProvice(string idProvice);
    }
}
