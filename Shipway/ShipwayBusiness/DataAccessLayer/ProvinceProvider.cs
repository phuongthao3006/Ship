using ShipwayBusiness.CoreProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShipwayBusiness.Entities;
using ShipwayBusiness.Repository.UnitOfWork;

namespace ShipwayBusiness.DataAccessLayer
{
    public class ProvinceProvider : IProvinceProvider
    {
        public List<Province> GetAllProvince()
        {
            using( var uow= new ShipwayUnitOfWork())
            {
                return uow.ProvinceRepository.GetAll().ToList();
            }
        }

        public List<Province> GetListProvinceByRegion(int region)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                return uow.ProvinceRepository.FindAll(p => p.Region == region).ToList();
            }
        }

        public Province GetProvinceById(string id)
        {
            using( var uow= new ShipwayUnitOfWork())
            {
                return uow.ProvinceRepository.Find(p => p.ProvinceId == id);
            }
        }

        public int? GetRegionOfProvince(string provinceId)
        {
            using( var uow= new ShipwayUnitOfWork())
            {
                return uow.ProvinceRepository.Find(p => p.ProvinceId == provinceId)!= null? uow.ProvinceRepository.Find(p => p.ProvinceId == provinceId).Region: -1;
            }
        }
    }
}