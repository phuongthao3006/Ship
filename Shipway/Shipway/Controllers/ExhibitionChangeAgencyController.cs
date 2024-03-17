using Shipway.Authorize;
using ShipwayBusiness.CoreProvider;
using ShipwayBusiness.DataAccessLayer;
using ShipwayBusiness.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Shipway.Controllers
{
    [ShipwayAuthorize]
    public class ExhibitionChangeAgencyController : Controller
    {
        private IExhibitionProvider _exhibitionProvider;
        private INoteRequiredProvider _noteRequiredProvider;
        private IKindServiceProvider _kindServiceProvider;
        private IExhibitionStatusProvider _exhibitionStatusProvider;
        private IProvinceProvider _provinceProvider;
        private IDistrictProvider _districtProvider;
        private IWardProvider _wardProvider;
        private int? _agencyIdOfCurrentUser;

        public ExhibitionChangeAgencyController()
        {
            _exhibitionProvider = new ExhibitionProvider();
            _noteRequiredProvider = new NoteRequiredProvider();
            _kindServiceProvider = new KindServiceProvider();
            _exhibitionStatusProvider = new ExhibitionStatusProvider();
            _provinceProvider = new ProvinceProvider();
            _districtProvider = new DistrictProvider();
            _wardProvider = new WardProvider();
        }

        public ActionResult Import()
        {
            bool isCentralWarehouse = false;
            var currentUser = (User)Session["CurrentUser"];
            var agencyCentralWarehouse = new AgencyProvider().GetAllAgency().First(item => item.IsCentralWarehouse == true);
            if (currentUser.AgencyId == agencyCentralWarehouse.AgencyId) isCentralWarehouse = true;
            _agencyIdOfCurrentUser = currentUser.AgencyId;
            ViewBag.ListExhibition = _exhibitionProvider.GetAllExhibitionViewModel(currentUser.Id, isImportExhibition: true, isCentralWarehouse: isCentralWarehouse);
            //ViewBag.ListNoteRequired = _noteRequiredProvider.GetAllNoteRequired();
            //ViewBag.ListKindService = _kindServiceProvider.GetAllKindService();
            return View();
        }

        [Route("ExhibitionChangeAgency/ImportExhibition/{idExhibition}")]
        public ActionResult ImportExhibition(string idExhibition)
        {
            bool isCentralWarehouse = false;
            var currentUser = (User)Session["CurrentUser"];
            var agencyCentralWarehouse = new AgencyProvider().GetAllAgency().First(item => item.IsCentralWarehouse == true);
            if (currentUser.AgencyId == agencyCentralWarehouse.AgencyId) isCentralWarehouse = true;
            _agencyIdOfCurrentUser = currentUser.AgencyId;
            var exhibition = _exhibitionProvider.GetExhibitionById(idExhibition);
            exhibition.AssignToAgency = _agencyIdOfCurrentUser;
            exhibition.ExhibitionStatusId = isCentralWarehouse ? (int)EnumExhibitionStatus.khotrungchuyen : (int)EnumExhibitionStatus.khotinhnhan;

            _exhibitionProvider.UpdateExhibition(exhibition);
            return RedirectToAction("Import", "ExhibitionChangeAgency");
        }

        public ActionResult Export()
        {
            var currentUser = (User)Session["CurrentUser"];
            _agencyIdOfCurrentUser = currentUser.AgencyId;
            ViewBag.ListExhibition = _exhibitionProvider.GetAllExhibitionViewModel(currentUser.Id, isExportExhibition: true);

            return View();
        }

        [Route("ExhibitionChangeAgency/ExportExhibition/{idExhibition}")]
        public ActionResult ExportExhibition(string idExhibition)
        {
            var agencyProvider = new AgencyProvider();
            var currentUser = (User)Session["CurrentUser"];
            var agencyCentralWarehouse = agencyProvider.GetAllAgency().First(item => item.IsCentralWarehouse == true);
            int agencyId = 0;
            _agencyIdOfCurrentUser = currentUser.AgencyId;
            var exhibition = _exhibitionProvider.GetExhibitionById(idExhibition);
            var agencyOfProvinceReceive = agencyProvider.GetAgencyByProvinceId(exhibition.ProvinceReceiveId);

            if(agencyOfProvinceReceive == null)
            {
                var agencyInsert = new Agency();
                agencyInsert.PhoneNumber = "0987654321";
                agencyInsert.Address = $"address {exhibition.ProvinceReceiveId}";
                agencyInsert.ActivityTime = "7";
                agencyInsert.IsCentralWarehouse = false;
                agencyInsert.ProvinceId = exhibition.ProvinceReceiveId;
                agencyInsert.DistrictId = exhibition.DistrictReceiveId;
                agencyInsert.WardId = exhibition.WardReceiveId;
                agencyId = agencyProvider.InsertAgency(agencyInsert).AgencyId;
            }
            else
            {
                agencyId = agencyOfProvinceReceive.AgencyId;
            }

            exhibition.AssignToAgency = exhibition.AssignToAgency == agencyCentralWarehouse.AgencyId ? agencyId : agencyCentralWarehouse.AgencyId;
            exhibition.ExhibitionStatusId =  (int)EnumExhibitionStatus.dangvanchuyen;

            _exhibitionProvider.UpdateExhibition(exhibition);
            return RedirectToAction("Export", "ExhibitionChangeAgency");
        }

        public ActionResult GetExhibitionAssginToShipper()
        {
            var provinceId = (string)Session["CurrentProvinceId"];
            ViewBag.ListExhibition = _exhibitionProvider.GetAllExhibitionToAssignShipper(provinceId);

            return View();
        }

        [Route("ExhibitionChangeAgency/AssginToShipper/{idExhibition}")]
        public ActionResult AssginToShipper(string idExhibition)
        {
            var shipperId = (int)Session["ShipperId"];
            var shipper = new ShipperProvider().GetShipperById(shipperId);
            _agencyIdOfCurrentUser = shipper.AgencyId;
            var exhibition = _exhibitionProvider.GetExhibitionById(idExhibition);
            exhibition.AssignShipperId = shipper.ShipperId;
            exhibition.DayReceive = DateTime.Now;
            exhibition.ExhibitionStatusId = (int)EnumExhibitionStatus.danggiaohang;

            _exhibitionProvider.UpdateExhibition(exhibition);
            return RedirectToAction("AssginToShipper", "ExhibitionChangeAgency");
        }

        public ActionResult GetExhibitionAssginToCustomer()
        {
            var shipperIs = (int)Session["ShipperId"];
            ViewBag.ListExhibition = _exhibitionProvider.GetAllExhibitionByShipperId(shipperIs);
            return View();
        }

        [Route("ExhibitionChangeAgency/AssginToCustomer/{idExhibition}/{success?}")]
        public ActionResult AssginToCustomer(string idExhibition, bool success = false)
        {
            var currentUser = (User)Session["CurrentUser"];
            _agencyIdOfCurrentUser = currentUser.AgencyId;
            var exhibition = _exhibitionProvider.GetExhibitionById(idExhibition);
            if(success)
            {
                exhibition.ExhibitionStatusId = (int)EnumExhibitionStatus.giaothanhcong;
            }
            else
            {
                exhibition.NumberOfFailedDeliveries = exhibition.NumberOfFailedDeliveries == null ? 1 : exhibition.NumberOfFailedDeliveries++;
                exhibition.ExhibitionStatusId = exhibition.NumberOfFailedDeliveries == 3 ? (int)EnumExhibitionStatus.hanghoan : (int)EnumExhibitionStatus.giaothatbai;
            }         

            _exhibitionProvider.UpdateExhibition(exhibition);
            return RedirectToAction("AssginToShipper", "ExhibitionChangeAgency");
        }
    }
}