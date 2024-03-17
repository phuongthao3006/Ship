using Shipway.Authorize;using ShipwayBusiness.CoreProvider;
using ShipwayBusiness.DataAccessLayer;
using ShipwayBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shipway.Controllers
{
    [ShipwayAuthorize]
    public class ExhibitionCustomerController : Controller
    {
        private IExhibitionProvider _exhibitionProvider;
        private INoteRequiredProvider _noteRequiredProvider;
        private IKindServiceProvider _kindServiceProvider;
        private IExhibitionStatusProvider _exhibitionStatusProvider;
        private IProvinceProvider _provinceProvider;
        private IDistrictProvider _districtProvider;
        private IWardProvider _wardProvider;
        private int? _agencyIdOfCurrentUser;
        public ExhibitionCustomerController()
        {
            _exhibitionProvider = new ExhibitionProvider();
            _noteRequiredProvider = new NoteRequiredProvider();
            _kindServiceProvider = new KindServiceProvider();
            _exhibitionStatusProvider = new ExhibitionStatusProvider();
            _provinceProvider = new ProvinceProvider();
            _districtProvider = new DistrictProvider();
            _wardProvider = new WardProvider();   
        }
        // GET: ExhibitionCustomer
        public ActionResult Index()
        {
            ViewBag.ListProvince = _provinceProvider.GetAllProvince();
            ViewBag.ListDistrictSender = new List<District>();
            ViewBag.ListWardSender = new List<Ward>();
            ViewBag.ListDistrictReceive = new List<District>();
            ViewBag.ListWardReceive = new List<Ward>();
            var currentUser = (User)Session["CurrentUser"];
            _agencyIdOfCurrentUser = currentUser.AgencyId;
            ViewBag.ListExhibition = _exhibitionProvider.GetAllExhibitionView(currentUser.Id);
            ViewBag.ListNoteRequired = _noteRequiredProvider.GetAllNoteRequired();
            ViewBag.ListKindService = _kindServiceProvider.GetAllKindService();

            return View();
        }

        [HttpPost]
        public ActionResult SaveExhibiton(Exhibition exhibition)
        {
            if (ModelState.IsValid)
            {
                IServiceChargeProvider serviceChargeProvider = new ServiceChargeProvider();
                IKindTimeReceivedProvider kindTimeReceivedProvider = new KindTimeReceivedProvider();
                IRouterProvider routerProvider = new RouterProvider();
                int router;
                if (exhibition.ProvinceSenderId.Equals(exhibition.ProvinceReceiveId))
                {
                    router = routerProvider.GetIdRouterByName("Nội tỉnh");
                }
                else if (exhibition.ProvinceSenderId == exhibition.ProvinceReceiveId)
                {
                    router = routerProvider.GetIdRouterByName("Nội vùng");
                }
                else
                {
                    router = routerProvider.GetIdRouterByName("Liên vùng");
                }
                var serviceCharge = serviceChargeProvider.GetServiceChargeByKindService(router, exhibition.KindServiceId);
                int numberOfDayReceive = kindTimeReceivedProvider.GetKindTimeReceivedById(serviceCharge.KindTimeReceivedId).NumberOfDay;
                exhibition.DayReceive = DateTime.Now.AddDays(numberOfDayReceive);
                exhibition.CreatedDate = DateTime.Now;
                exhibition.CreatedUserId = ((User)Session["CurrentUser"]).Id;
                exhibition.ExhibitionId = GenCodeRandom();
                int exhibitionStatusId = (int)EnumExhibitionStatus.khotinhgui;
                exhibition.ExhibitionStatusId = exhibitionStatusId;
                exhibition.AssignToAgency = _agencyIdOfCurrentUser == null ? Convert.ToInt32(exhibition.ProvinceSenderId) : _agencyIdOfCurrentUser;
                _exhibitionProvider.InsertExhibiton(exhibition);

                IHistoryTripProvider historyTripProvider = new HistoryTripProvider();
                var historyTrip = new HistoryTrip();
                historyTrip.ExhibitionId = exhibition.ExhibitionId;
                historyTrip.DateTrip = exhibition.CreatedDate;
                historyTrip.ExhibitionStatusId = exhibitionStatusId;
                historyTrip.CurrentAddress = "";
                historyTripProvider.InsertHistoryTrip(historyTrip);
            }
            
            return RedirectToAction("Index", "ExhibitionCustomer");
        }

        public string GenCodeRandom()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public class HistoryView
        {
            public HistoryTrip HistoryTrip { get; set; }
            public string ExhibitionStatus { get; set; }
        }

        public ActionResult ViewDetail(string id)
        {
            List<HistoryView> list = new List<HistoryView>();
            IHistoryTripProvider historyTrip = new HistoryTripProvider();
            var exhibition = _exhibitionProvider.GetExhibitionById(id);
            var listhistory = historyTrip.GetAllHistoryTripByExhibitionId(id);
            ViewBag.KindService = _kindServiceProvider.GetKindServiceById(exhibition.KindServiceId).KindServiceName;
            HistoryView historyview = new HistoryView();
            foreach(var item in listhistory)
            {
                historyview.HistoryTrip = item;
                historyview.ExhibitionStatus = _exhibitionStatusProvider.GetExhibitionStatusById(item.ExhibitionStatusId.Value).Name;
                list.Add(historyview);
            }
            ViewBag.ListHistory = list;
            return PartialView("_ViewDetail", exhibition);
        }

        public ActionResult EditExhibition(string id)
        {
            var exhibition = _exhibitionProvider.GetExhibitionById(id);
            ViewBag.ListNoteRequired = _noteRequiredProvider.GetAllNoteRequired();
            ViewBag.ListKindService = _kindServiceProvider.GetAllKindService();
            ViewBag.ListProvince = this._provinceProvider.GetAllProvince();
            ViewBag.ListWardSender = this._wardProvider.GetWardtByIdDistrict(exhibition.DistrictSenderId);
            ViewBag.ListDistrictSender = this._districtProvider.GetDistrictByIdProvice(exhibition.ProvinceSenderId);
            ViewBag.ListWardReceive = this._wardProvider.GetWardtByIdDistrict(exhibition.DistrictReceiveId);
            ViewBag.ListDistrictReceive = this._districtProvider.GetDistrictByIdProvice(exhibition.ProvinceReceiveId);
            return View(exhibition);
        }

        [HttpPost]
        public ActionResult EditExhibition(Exhibition exhibition)
        {
            if (ModelState.IsValid)
            {
                exhibition.LastModifiedUserId= ((User)Session["CurrentUser"]).Id;
                exhibition.LastModifiedDate = DateTime.Now;
                exhibition.AssignToAgency = _agencyIdOfCurrentUser;
                var exhibitionNew = _exhibitionProvider.UpdateExhibition(exhibition);
            }
            
            return RedirectToAction("Index", "ExhibitionCustomer");
        }

        public class ResultExhibition
        {
            public decimal TotalMoney { get; set; }
            public string DayReceiver { get; set; }
        }

        [HttpPost]
        public JsonResult TotalMoney(int? weigh, string citysender, string cityReceiver, int? kindService)
       {
            int router;
            ServiceCharge serviceCharge = null;
            var resultExhibiton = new ResultExhibition();
            IServiceChargeProvider serviceChargeProvider = new ServiceChargeProvider();
            IKindTimeReceivedProvider kindTimeReceivedProvider = new KindTimeReceivedProvider();
            IProvinceProvider provinceProvider = new ProvinceProvider();
            IRouterProvider routerProvider = new RouterProvider();

            var regionSender = provinceProvider.GetRegionOfProvince(citysender);
            var regionReceiver = provinceProvider.GetRegionOfProvince(cityReceiver);
            if (regionReceiver == -1 || regionSender == -1)
            {
                return null;
            }
            if (citysender.Equals(cityReceiver))
            {
                router = routerProvider.GetIdRouterByName("Nội tỉnh");
            }
            else if (regionSender == regionReceiver)
            {
                router = routerProvider.GetIdRouterByName("Nội vùng");
            }
            else
            {
                router = routerProvider.GetIdRouterByName("Liên vùng");
            }
            serviceCharge = serviceChargeProvider.GetServiceChargeByKindService(router, kindService.Value);
            int numberOfDayReceive = kindTimeReceivedProvider.GetKindTimeReceivedById(serviceCharge.KindTimeReceivedId).NumberOfDay;
            if (weigh == null)
            {
                resultExhibiton.TotalMoney = serviceCharge.CostOderUrban;
            }
            else
            {
                var weighTemp = weigh;
                resultExhibiton.TotalMoney = weighTemp > weigh ? ((weighTemp > serviceCharge.Weigh) ? (serviceCharge.CostOderUrban + (int)(weighTemp - serviceCharge.Weigh) / serviceCharge.AddWeight * serviceCharge.AddMoney) : serviceCharge.CostOderUrban)
                    : ((weigh > serviceCharge.Weigh) ? (serviceCharge.CostOderUrban + (int)(weigh - serviceCharge.Weigh) / serviceCharge.AddWeight * serviceCharge.AddMoney) : serviceCharge.CostOderUrban);
            }

            resultExhibiton.DayReceiver = (DateTime.Now.Hour > 11) ? DateTime.Now.AddDays(numberOfDayReceive).ToString("dd/MM/yyyy") : DateTime.Now.AddDays(numberOfDayReceive + 1).ToString("dd/MM/yyyy");

            return Json(resultExhibiton, JsonRequestBehavior.AllowGet);
        }
    }
}