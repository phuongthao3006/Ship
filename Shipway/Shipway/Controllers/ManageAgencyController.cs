using Shipway.Authorize;
using Shipway.Message;
using ShipwayBusiness.CoreProvider;
using ShipwayBusiness.DataAccessLayer;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Models.ModelView;
using ShipwayUnitOfWork.Repository;
using ShipwayUnitOfWork.UnitOfWork;
using System.Collections.Generic;
using System.Web.Mvc;
using District = ShipwayUnitOfWork.Models.District;
using Ward = ShipwayUnitOfWork.Models.Ward;

namespace Shipway.Controllers
{
    [ShipwayAuthorize]
    public class ManageAgencyController : Controller
    {
        private IAgencyProvider _agencyProvider;
        private IProvinceProvider _provinceProvider;
        private IDistrictProvider _districtProvider;
        private IWardProvider _wardProvider;

        private IProvinceRepository _provinceRepository;
        private IAgencyRepository _agencyRepository;
        private IDistrictRepository _districtRepository;
        private IWardRepository _wardRepository;
        private UnitOfWork<ShipwayDbEntities> unit = new UnitOfWork<ShipwayDbEntities>();
        public ManageAgencyController()
        {
            _agencyProvider = new AgencyProvider();
            _provinceProvider = new ProvinceProvider();
            _districtProvider = new DistrictProvider();
            _wardProvider = new WardProvider();

            _provinceRepository = new ProvinceRepository(unit);
            _agencyRepository = new AgencyRepository(unit);
            _districtRepository = new DistrictRepository(unit);
            _wardRepository = new WardRepository(unit);
        }

        // GET: ManageAgency
        public ActionResult Index()
        {
            ViewBag.ListProvince = _provinceRepository.GetAll();
            ViewBag.ListDistrict = new List<District>();
            ViewBag.ListWard = new List<Ward>();
            ViewBag.ListAgency = _agencyRepository.GetAll_V2();

            return View();
        }

        [HttpPost]
        public ActionResult SaveAgency(Agency agency)
        {
            List<string> errors = new List<string>();
            if (ModelState.IsValid)
            {
                bool result = _agencyRepository.Insert(agency);
                if (!result)
                {
                    errors.Add(UserMessage.INSERT_FAIL);
                }
            }
            TempData["errorMessages"] = errors;
            return RedirectToAction("Index", "ManageAgency");
        }

        [HttpGet]
        public ActionResult DeleteAgency(int id)
        {
            Agency agency = _agencyRepository.GetById(id);
            return PartialView("_DeleteAgency", agency);
        }

        [HttpPost]
        public ActionResult DeleteAgency(Agency agency)
        {
            List<string> errors = new List<string>();
            if (agency != null && agency.AgencyId != null)
            {
                bool result = _agencyRepository.Delete(agency.AgencyId);
                if (!result)
                    errors.Add(UserMessage.DELETE_FAIL);
            }

            TempData["errorMessages"] = errors;
            return RedirectToAction("Index", "ManageAgency");
        }

        [HttpGet]
        public ActionResult EditAgency(int id)
        {
            AgencyModelView agency = _agencyRepository.GetDataById_V2(id);
            ViewBag.ListProvince = _provinceRepository.GetAll();
            ViewBag.ListDistrict = _districtRepository.GetDataByProvinceId(agency.ProvinceId);
            ViewBag.ListWard = _wardRepository.GetDataByDistrictId(agency.DistrictId);

            return PartialView("_EditAgency", agency);
        }

        [HttpPost]
        public ActionResult EditAgency(Agency agency)
        {
            List<string> errors = new List<string>();
            if (ModelState.IsValid)
            {
                bool result = _agencyRepository.Update(agency);
                if (!result)
                {
                    errors.Add(UserMessage.DELETE_FAIL);
                }
            }

            TempData["errorMessages"] = errors;
            return RedirectToAction("Index", "ManageAgency");
        }
    }
}