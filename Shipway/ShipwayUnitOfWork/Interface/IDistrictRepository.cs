using ShipwayUnitOfWork.Models;
using System.Collections.Generic;

namespace ShipwayUnitOfWork.Interface
{
    public interface IDistrictRepository : IGenericRepository<District>
    {
        List<District> GetDataByProvinceId(string provinceId);
    }
}
