using ShipwayUnitOfWork.Models;
using System.Collections.Generic;

namespace ShipwayUnitOfWork.Interface
{
    public interface IWardRepository : IGenericRepository<Ward>
    {
        List<Ward> GetDataByDistrictId(string districtId);
    }
}
