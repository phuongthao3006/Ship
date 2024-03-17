using Shipway.Authorize;
using Shipway.Constant;
using Shipway.Message;
using ShipwayBusiness.CoreProvider;
using ShipwayBusiness.DataAccessLayer;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Models.ModelView;
using ShipwayUnitOfWork.UnitOfWork;
using System.Collections.Generic;
using System.Web.Mvc;
using AgencyRepository = ShipwayUnitOfWork.Repository.AgencyRepository;
using District = ShipwayUnitOfWork.Models.District;
using DistrictRepository = ShipwayUnitOfWork.Repository.DistrictRepository;
using ProvinceRepository = ShipwayUnitOfWork.Repository.ProvinceRepository;
using UserRepository = ShipwayUnitOfWork.Repository.UserRepository;
using Ward = ShipwayUnitOfWork.Models.Ward;
using WardRepository = ShipwayUnitOfWork.Repository.WardRepository;

namespace Shipway.Controllers
{

    [ShipwayAuthorize]
    public class ManageShipperController : Controller
    {
        private IShipperProvider _shipperProvider;
        private IAgencyProvider _agencyProvider;

        private IUserRepository _userRepository;
        private IAgencyRepository _agencyRepository;
        private IProvinceRepository _provinceRepository;
        private IDistrictRepository _districtRepository;
        private IWardRepository _wardRepository;
        private UnitOfWork<ShipwayDbEntities> unit = new UnitOfWork<ShipwayDbEntities>();
        public ManageShipperController()
        {
            _shipperProvider = new ShipperProvider();
            _agencyProvider = new AgencyProvider();

            _userRepository = new UserRepository(unit);
            _agencyRepository = new AgencyRepository(unit);
            _provinceRepository = new ProvinceRepository(unit);
            _districtRepository = new DistrictRepository(unit);
            _wardRepository = new WardRepository(unit);
        }

        // GET: ManageShipper
        public ActionResult Index()
        {
            ViewBag.Agency = new SelectList(_agencyRepository.GetAll(), "AgencyId", "AgencyName");
            ViewBag.ListShipper = _userRepository.GetDataByTypeUser_V2(UserConstant.TYPEUSER_SHIPPER);

            ViewBag.ListProvince = _provinceRepository.GetAll();
            ViewBag.ListDistrict = new List<District>();
            ViewBag.ListWard = new List<Ward>();
            return View();
        }

        //[HttpPost]
        //public ActionResult SaveShipper(Shipper shipper)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        shipper.Password = ShipwayBusiness.Helper.Utils.MD5(shipper.Password);
        //        _shipperProvider.InsertShipper(shipper);
        //        return RedirectToAction("Index", "ManageShipper");
        //    }
        //    return RedirectToAction("Index", "ManageShipper");
        //}

        [HttpGet]
        public ActionResult EditShipper(int id)
        {
            UsersModelView shipper = _userRepository.GetDataById_V2(id);
            ViewBag.Agency = new SelectList(_agencyRepository.GetAll(), "AgencyId", "AgencyName");
            ViewBag.ListProvince = _provinceRepository.GetAll();
            ViewBag.ListDistrict = _districtRepository.GetDataByProvinceId(shipper.ProvinceId);
            ViewBag.ListWard = _wardRepository.GetDataByDistrictId(shipper.DistrictId);
            return PartialView("_EditShipper", shipper);
        }

        [HttpPost]
        public ActionResult EditShipper(Users model)
        {
            List<string> errors = new List<string>();
            if (ModelState.IsValid)
            {
                Users user = _userRepository.GetById(model.Id);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.AgencyId = model.AgencyId;
                    user.Address = model.Address;
                    user.WardId = model.WardId;
                    bool result = _userRepository.Update(user);
                    if (!result)
                    {
                        errors.Add(UserMessage.UPDATE_FAIL);
                    }
                }

            }
            TempData["errorMessages"] = errors;
            return RedirectToAction("Index", "ManageShipper");
        }

        [HttpGet]
        public ActionResult DeleteShipper(int id)
        {
            Users shipper = _userRepository.GetById(id);
            return PartialView("_DeleteShipper", shipper);
        }

        //[HttpPost]
        //public ActionResult DeleteShipper(Shipper shipper)
        //{
        //    bool success = _shipperProvider.DeleteShipper(shipper);

        //    if (success)
        //    {
        //        TempData["Success"] = "Xóa người giao hàng thành công";
        //        return RedirectToAction("Index", "ManageShipper");
        //    }
        //    else
        //    {
        //        TempData["Success"] = "Xóa người giao hàng thất bại";
        //        return RedirectToAction("Index", "ManageShipper");
        //    }
        //}
    }
}