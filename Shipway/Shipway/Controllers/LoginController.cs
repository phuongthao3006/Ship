using Shipway.Constant;
using Shipway.Cookies;
using Shipway.Message;
using ShipwayBusiness.Helper;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Repository;
using ShipwayUnitOfWork.UnitOfWork;
using System.Web.Mvc;

namespace Shipway.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository _userRepository;
        private UnitOfWork<ShipwayDbEntities> unit = new UnitOfWork<ShipwayDbEntities>();
        public LoginController()
        {
            _userRepository = new UserRepository(unit);
        }
        // GET: Login
        public ActionResult Index()
        {
            Users userCookie = UserCookie.GetDataCookie();
            if (userCookie != null)
            {
                switch (userCookie.TypeId)
                {
                    case UserConstant.TYPEUSER_SUPPERADMIN:
                        return RedirectToAction("Index", "SuperAdmin");
                    case UserConstant.TYPEUSER_ADMIN:
                        return RedirectToAction("ManageCustomer", "SuperAdmin");
                    case UserConstant.TYPEUSER_CUSTOMER:
                        return RedirectToAction("Index", "ExhibitionCustomer");
                    case UserConstant.TYPEUSER_SHIPPER:
                        break;
                }
            }
            Users user = new Users();
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            user.Password = Utils.MD5(user.Password);
            Users model = _userRepository.Login(user);
            if (model != null)
            {
                UserCookie.SetDataCookie(model);

                switch (model.TypeId)
                {
                    case UserConstant.TYPEUSER_SUPPERADMIN:
                        return RedirectToAction("Index", "SuperAdmin");
                    case UserConstant.TYPEUSER_ADMIN:
                        return RedirectToAction("ManageCustomer", "SuperAdmin");
                    case UserConstant.TYPEUSER_CUSTOMER:
                        return RedirectToAction("Index", "ExhibitionCustomer");
                    case UserConstant.TYPEUSER_SHIPPER:
                        break;
                }
            }
            else
            {
                ViewBag.Invalid = true;
                TempData["ErrorMessage"] = LoginMessage.LOGIN_FAIL;
                return View("Index");
            }
            return RedirectToAction("Index", "Login");
            /*
            IUserProvider userProvider = new UserProvider();
            IShipperProvider shipperProvider = new ShipperProvider();
            if (ModelState.IsValid)
            {
                var passwordHash = ShipwayBusiness.Helper.Utils.MD5(user.Password);

                User usernameExist = null;
                var existUser = userProvider.Login(user.UserName, passwordHash, out usernameExist);
                if (existUser == null)
                {
                    var shipper = shipperProvider.GetShipperByUsernameAndPassword(user.UserName, passwordHash);
                    if (shipper != null)
                    {
                        IAgencyProvider agencyProvider = new AgencyProvider();
                        var agencyOfUser = agencyProvider.GetAgencyById(shipper.AgencyId);
                        Session["CurrentProvinceId"] = agencyOfUser.ProvinceId;
                        Session["ShipperName"] = shipper.Name;

                        Session["ShipperId"] = shipper.ShipperId;
                        return RedirectToAction("Index", "SuperAdmin");
                    }
                }

                if (existUser != null)
                {
                    if (existUser.AgencyId != null)
                    {
                        IAgencyProvider agencyProvider = new AgencyProvider();
                        var agencyOfUser = agencyProvider.GetAgencyById((int)usernameExist.AgencyId);
                        Session["CurrentProvinceId"] = agencyOfUser.ProvinceId;
                    }

                    Session["CurrentUser"] = existUser;
                    if (existUser.IsSuperAdmin)
                    {
                        return RedirectToAction("Index", "SuperAdmin");
                    }
                    if (existUser.IsAdmin == true)
                    {
                        return RedirectToAction("ManageCustomer", "SuperAdmin");
                    }
                    if (existUser.IsCustomer == true)
                    {
                        return RedirectToAction("Index", "ExhibitionCustomer");
                    }
                }
                else if (usernameExist != null && usernameExist.Id > 0)
                {
                    ViewBag.Invalid = true;
                    TempData["ErrorMessage"] = "Mật khẩu không đúng";
                    return View("Index");
                }
                else
                {
                    ViewBag.Invalid = true;
                    TempData["ErrorMessage"] = "Tên đăng nhập không tồn tại";
                    return View("Index");
                }
            }
            */

        }

        public ActionResult Logout()
        {
            UserCookie.RemoveDataCookie();
            return RedirectToAction("Index");
        }
    }
}