using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Models.ModelView;
using System.Collections.Generic;

namespace ShipwayUnitOfWork.Interface
{
    public interface IAgencyRepository : IGenericRepository<Agency>
    {
        List<AgencyModelView> GetAll_V2();
        AgencyModelView GetDataById_V2(int id);
    }
}
