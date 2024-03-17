using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Repository;
using ShipwayUnitOfWork.UnitOfWork;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shipway.Controllers
{
    public class AddressController : Controller
    {
        private IDistrictRepository _districtRepository;
        private IWardRepository _wardRepository;
        private UnitOfWork<ShipwayDbEntities> unitOfWork = new UnitOfWork<ShipwayDbEntities>();
        public AddressController()
        {
            _districtRepository = new DistrictRepository(unitOfWork);
            _wardRepository = new WardRepository(unitOfWork);
        }

        [HttpPost]
        public JsonResult GetDistrictByIdProvice(string idProvince)
        {
            List<District> result = new List<District>();
            if (!string.IsNullOrEmpty(idProvince))
            {
                result = _districtRepository.GetDataByProvinceId(idProvince);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetWardByIdDistrict(string idDistrict)
        {
            List<Ward> result = new List<Ward>();
            if (!string.IsNullOrEmpty(idDistrict))
            {
                result = _wardRepository.GetDataByDistrictId(idDistrict);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}