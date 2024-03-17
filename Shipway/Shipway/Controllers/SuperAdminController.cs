using Shipway.Authorize;
using Shipway.Constant;
using Shipway.Message;
using ShipwayUnitOfWork.Helper;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Repository;
using ShipwayUnitOfWork.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using District = ShipwayUnitOfWork.Models.District;
using Ward = ShipwayUnitOfWork.Models.Ward;

namespace Shipway.Controllers
{
    [ShipwayAuthorize]
    public class SuperAdminController : Controller
    {
        // GET: SuperAdmin: hoàn

        private IUserRepository _userRepository;
        private IAgencyRepository _agencyRepository;
        private IProvinceRepository _provinceRepository;
        private IDistrictRepository _districtRepository;
        private IWardRepository _wardRepository;

        private UnitOfWork<ShipwayDbEntities> unitOfWork = new UnitOfWork<ShipwayDbEntities>();

        public SuperAdminController()
        {
            _userRepository = new UserRepository(unitOfWork);
            _agencyRepository = new AgencyRepository(unitOfWork);
            _provinceRepository = new ProvinceRepository(unitOfWork);
            _districtRepository = new DistrictRepository(unitOfWork);
            _wardRepository = new WardRepository(unitOfWork);
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ListAgency = _agencyRepository.GetAll();
            ViewBag.Permission = _userRepository.GetTypeUsers();
            ViewBag.ListAccount = _userRepository.GetAll_OrderByTypeId();

            ViewBag.ListProvince = new SelectList(_provinceRepository.GetAll(), "ProvinceId", "Name");
            ViewBag.ListDistrict = new List<District>();
            ViewBag.ListWard = new List<Ward>();
            return View(new Users());
        }

        [HttpPost]
        public ActionResult SaveAccount(Users model, string RePassword)
        {
            List<string> errorMessages = new List<string>();
            if (ModelState.IsValid)
            {
                if (model.Password.Equals(RePassword))
                {
                    if (_userRepository.ExistUserName(model.UserName))
                    {
                        errorMessages.Add(UserMessage.EXIST_USERNAME);
                    }
                    else
                    {
                        model.Password = Utils.MD5(model.Password);

                        if (_userRepository.Insert(model))
                        {
                            return RedirectToAction("Index", "SuperAdmin");
                        }
                        else
                        {
                            errorMessages.Add(UserMessage.INSERT_FAIL);
                        }
                    }
                }
                else
                {
                    errorMessages.Add(UserMessage.REPASSWORD_NOTEQUAL_PASSWORD);
                }
            }

            TempData["errorMessages"] = errorMessages;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditAccount(int id)
        {
            Users account = _userRepository.GetById(id);

            ViewBag.Permission = _userRepository.GetTypeUsers();
            ViewBag.ListAgency = _agencyRepository.GetAll();

            return PartialView("_editAccount", account);
        }

        [HttpPost]
        public ActionResult EditAccount(Users model)
        {
            List<string> errorMessages = new List<string>();
            Users user = _userRepository.GetById(model.Id);
            if (user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.TypeId = model.TypeId;
                user.AgencyId = model.TypeId == UserConstant.TYPEUSER_SUPPERADMIN ? null : model.AgencyId;
                bool result = _userRepository.Update(user);
                if (!result)
                {
                    errorMessages.Add(UserMessage.UPDATE_FAIL);
                }
            }
            TempData["errorMessages"] = errorMessages;
            return RedirectToAction("Index", "SuperAdmin");
        }

        [HttpGet]
        public ActionResult DeleteAccount(int id)
        {
            var account = _userRepository.GetById(id);
            return PartialView("_DeleteAccount", account);
        }

        [HttpPost]
        public ActionResult DeleteAccount(Users user)
        {
            List<string> errorMessages = new List<string>();
            bool success = _userRepository.Delete(user.Id);
            if (!success)
            {
                errorMessages.Add(UserMessage.DELETE_FAIL);
            }
            TempData["errorMessages"] = errorMessages;
            return RedirectToAction("Index", "SuperAdmin");
        }

        [HttpPost]
        public JsonResult ResetPassword(int userId)
        {
            bool success = true;
            try
            {
                _userRepository.ResetPassword(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Reset pass: " + ex.ToString());
                success = false;
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        //Customer
        //[HttpGet]
        //public ActionResult ManageCustomer()
        //{
        //    List<Users> listCustomer = _userRepository.GetDataByTypeUser(UserConstant.TYPEUSER_CUSTOMER);
        //    ViewBag.ListCustomer = listCustomer;
        //    return View(new Users());
        //}

        //[HttpPost]
        //public ActionResult SaveCustomer(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.IsCustomer = true;
        //        _userProvider.InsertUser(user);
        //        return RedirectToAction("ManageCustomer", "SuperAdmin");
        //    }
        //    return RedirectToAction("ManageCustomer", "SuperAdmin");
        //}

        //public ActionResult EditCustomer(int id)
        //{
        //    var customer = _userProvider.GetUserById(id);
        //    return PartialView("_EditCustomer", customer);
        //}

        //[HttpPost]
        //public ActionResult EditCustomer(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.IsCustomer = true;
        //        user = _userProvider.UpdateUser(user);
        //        return RedirectToAction("ManageCustomer", "SuperAdmin");
        //    }
        //    return RedirectToAction("ManageCustomer", "SuperAdmin");
        //}

        //public ActionResult DeleteCustomer(int id)
        //{
        //    var customer = _userProvider.GetUserById(id);
        //    return PartialView("_DeleteCustomer", customer);
        //}

        //[HttpPost]
        //public ActionResult DeleteCustomer(User user)
        //{
        //    bool success = _userProvider.Delete(user);
        //    if (success)
        //    {
        //        TempData["Success"] = "Xóa khách hàng thành công";
        //        return RedirectToAction("ManageCustomer", "SuperAdmin");
        //    }
        //    else
        //    {
        //        TempData["Success"] = "Xóa khách hàng thất bại";
        //        return RedirectToAction("ManageCustomer", "SuperAdmin");
        //    }
        //}
    }
}