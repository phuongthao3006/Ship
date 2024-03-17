using Shipway.Authorize;
using Shipway.Message;
using ShipwayBusiness.CoreProvider;
using ShipwayBusiness.DataAccessLayer;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Repository;
using ShipwayUnitOfWork.UnitOfWork;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shipway.Controllers
{
    [ShipwayAuthorize]
    public class ManageServiceChargeController : Controller
    {
        private IServiceChargeProvider serviceChargeProvider;
        private IRouterProvider routerProvider;
        private IKindServiceProvider kindServiceProvider;
        private IKindTimeReceivedProvider kindTimeReceivedProvider;

        private IKindServiceRepository _kindServiceRepository;
        private IRouterRepository _routerRepository;
        private IKindTimeReceivedRepository _kindTimeReceivedRepository;
        private IServiceChargeRepository _serviceChargeRepository;
        private UnitOfWork<ShipwayDbEntities> unit = new UnitOfWork<ShipwayDbEntities>();
        public ManageServiceChargeController()
        {
            serviceChargeProvider = new ServiceChargeProvider();
            routerProvider = new RouterProvider();
            kindServiceProvider = new KindServiceProvider();
            kindTimeReceivedProvider = new KindTimeReceivedProvider();

            _kindServiceRepository = new KindServiceRepository(unit);
            _routerRepository = new RouterRepository(unit);
            _kindTimeReceivedRepository = new KindTimeReceivedRepository(unit);
            _serviceChargeRepository = new ServiceChargeRepository(unit);
        }
        // GET: ManageServiceCharge
        public ActionResult Index()
        {
            ViewBag.ListRouter = _routerRepository.GetAll();
            ViewBag.ListKindService = _kindServiceRepository.GetAll();
            ViewBag.ListKindTimeReceived = _kindTimeReceivedRepository.GetAll();
            List<ServiceCharge> listService = (List<ServiceCharge>)_serviceChargeRepository.GetAll();
            return View(listService);
        }

        [HttpPost]
        public ActionResult SaveServiceCharge(ServiceCharge model)
        {
            if (ModelState.IsValid)
            {
                bool result = _serviceChargeRepository.Insert(model);
                if (!result)
                {
                    TempData["errorMessage"] = ServiceChargeMessage.INSERT_FAIL;
                }
            }
            return RedirectToAction("Index", "ManageServiceCharge");
        }

        [HttpGet]
        public ActionResult EditServiceCharge(int id)
        {
            ServiceCharge serviceCharge = _serviceChargeRepository.GetById(id);
            ViewBag.ListRouter = _routerRepository.GetAll();
            ViewBag.ListKindService = _kindServiceRepository.GetAll();
            ViewBag.ListKindTimeReceived = _kindTimeReceivedRepository.GetAll();
            return PartialView("_EditServiceCharge", serviceCharge);
        }

        [HttpPost]
        public ActionResult EditServiceCharge(ServiceCharge model)
        {
            string error = "";
            if (ModelState.IsValid)
            {
                ServiceCharge service = _serviceChargeRepository.GetById(model.ServiceChargeId);
                if (service != null)
                {
                    service.RouterId = model.RouterId;
                    service.KindServiceId = model.KindServiceId;
                    service.KindTimeReceivedId = model.KindTimeReceivedId;
                    service.CostOderUrban = model.CostOderUrban;
                    service.CostOderSubUrban = model.CostOderSubUrban;
                    service.AddWeight = model.AddWeight;
                    service.AddMoney = model.AddMoney;
                    service.Weight = model.Weight;

                    bool result = _serviceChargeRepository.Update(service);
                    if (!result)
                    {
                        error = ServiceChargeMessage.UPDATE_FAIL;
                    }
                }
            }
            TempData["errorMessage"] = error;
            return RedirectToAction("Index", "ManageServiceCharge");
        }

        [HttpGet]
        public ActionResult DeleteServiceCharge(int id)
        {
            var serviceCharge = _serviceChargeRepository.GetById(id);
            return PartialView("_DeleteServiceCharge", serviceCharge);
        }

        [HttpPost]
        public ActionResult DeleteServiceCharge(ServiceCharge model)
        {
            bool result = _serviceChargeRepository.Delete(model.ServiceChargeId);
            if (!result)
            {
                TempData["errorMessage"] = ServiceChargeMessage.DELETE_FAIL;
            }
            return RedirectToAction("Index", "ManageServiceCharge");
        }
    }
}