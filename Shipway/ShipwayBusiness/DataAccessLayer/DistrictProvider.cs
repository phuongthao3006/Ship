using ShipwayBusiness.CoreProvider;
using ShipwayBusiness.Entities;
using ShipwayBusiness.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ShipwayBusiness.DataAccessLayer
{
    public class DistrictProvider : IDistrictProvider
    {
        public List<District> GetAllDistrict()
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                return uow.DistrictRepository.GetAll().ToList();
            }
        }

        public List<District> GetDistrictByIdProvice(string idProvice)
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                return uow.DistrictRepository.FindAll(p => p.ProvinceId == idProvice).ToList();
            }
        }
    }
}