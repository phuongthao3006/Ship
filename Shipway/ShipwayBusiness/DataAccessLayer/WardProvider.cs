using ShipwayBusiness.CoreProvider;
using ShipwayBusiness.Entities;
using ShipwayBusiness.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ShipwayBusiness.DataAccessLayer
{
    public class WardProvider : IWardProvider
    {
        public List<Ward> GetWardtByIdDistrict(string idDistrict)
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                return uow.WardRepository.FindAll(p => p.DistrictId == idDistrict).ToList();
            }
        }
    }
}