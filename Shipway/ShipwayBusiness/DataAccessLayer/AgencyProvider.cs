using ShipwayBusiness.CoreProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShipwayBusiness.Entities;
using ShipwayBusiness.Repository.UnitOfWork;

namespace ShipwayBusiness.DataAccessLayer
{
    public class AgencyProvider : IAgencyProvider
    {
        public bool DeleteAgency(Agency agency)
        {
            using( var uow= new ShipwayUnitOfWork())
            {
                return uow.AgencyRepository.Delete(agency);
            }
        }

        public Agency GetAgencyById(int id)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                return uow.AgencyRepository.Find(p => p.AgencyId == id);
            }
        }

        public List<Agency> GetAllAgency()
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                return uow.AgencyRepository.GetAll().ToList();
            }
        }

        public List<AgencyViewModel> GetAllAgency_V2()
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                var data = uow.AgencyRepository.Context.Agency
                           .Join(uow.AgencyRepository.Context.Province, ag => ag.ProvinceId, pr => pr.ProvinceId, (ag, pr) => new { Agency = ag, Province = pr })
                           .Join(uow.AgencyRepository.Context.Districts, temp1 => temp1.Agency.DistrictId, ds => ds.DistrictId, (temp1, ds) => new { temp1.Agency, temp1.Province, District = ds})
                           .Join(uow.AgencyRepository.Context.Ward, ag => ag.Agency.WardId, pr => pr.WardId, (temp2, wd) => new { temp2.Agency, temp2.Province, temp2.District, Ward = wd })
                           .Select(item => new AgencyViewModel()
                           {
                               Agency = item.Agency,
                               Province = item.Province,
                               District = item.District,
                               Ward = item.Ward,
                           });
                return data.ToList();
            }
        }

        public Agency GetAgencyByProvinceId(string provinceId)
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                return uow.AgencyRepository.Find(p => p.ProvinceId == provinceId);
            }
        }
        public Agency InsertAgency(Agency agency)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                return uow.AgencyRepository.Add(agency);
            }
        }

        public Agency UpdateAgency(Agency agency)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                return uow.AgencyRepository.Update(agency);
            }
        }
    }
}