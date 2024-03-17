using Shipway.Authorize;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Models.ModelView;
using ShipwayUnitOfWork.Repository;
using ShipwayUnitOfWork.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Shipway.Controllers
{
    [ShipwayAuthorize]
    public class ManageExhibitionController : Controller
    {

        private IAgencyRepository _agencyRepository;
        private IExhibitionRepository _exhibitionRepository;
        private UnitOfWork<ShipwayDbEntities> unit = new UnitOfWork<ShipwayDbEntities>();
        public ManageExhibitionController()
        {

            _agencyRepository = new AgencyRepository(unit);
            _exhibitionRepository = new ExhibitionRepository(unit);
        }
        // GET: ManageExhibition
        public ActionResult Index(string ExhibitionId = null, int AgencyId = 0, int ExhibitionStatusId = 0)
        {
            ViewBag.ListAgency = _agencyRepository.GetAll();
            ViewBag.ExhibitionStatus = _exhibitionRepository.GetExhibitionStatuses();
            List<ExhibitionModelView> exhibitions = _exhibitionRepository.GetAll_V2_ViewSupperAdmin();
            if (!string.IsNullOrEmpty(ExhibitionId))
            {
                exhibitions = exhibitions.Where(m => m.ExhibitionId.ToLower().Contains(ExhibitionId.ToLower())).ToList();
                ViewBag.ExhibitionId = ExhibitionId;
            }
            if (AgencyId != 0)
            {
                exhibitions = exhibitions.Where(m => m.Agency.AgencyId == AgencyId).ToList();
            }
            if (ExhibitionStatusId != 0)
            {
                exhibitions = exhibitions.Where(m => m.ExhibitionStatus.ExhibitionStatusId == ExhibitionStatusId).ToList();
            }
            return View(exhibitions);
        }
    }
}