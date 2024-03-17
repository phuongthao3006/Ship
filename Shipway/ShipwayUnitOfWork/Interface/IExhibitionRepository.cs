using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Models.ModelView;
using System.Collections.Generic;

namespace ShipwayUnitOfWork.Interface
{
    public interface IExhibitionRepository : IGenericRepository<Exhibition>
    {
        List<ExhibitionModelView> GetAll_V2();
        List<ExhibitionModelView> GetAll_V2_ViewSupperAdmin();
        List<ExhibitionModelView> GetByAgencyIdAndStatus_V2_ViewSupperAdmin(int agencyId, int statusId);
        List<Exhibition> GetDataByAgencyId(int agencyId);
        List<ExhibitionStatus> GetExhibitionStatuses();
    }
}
