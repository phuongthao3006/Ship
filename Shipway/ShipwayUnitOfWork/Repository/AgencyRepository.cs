using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Models.ModelView;
using System.Collections.Generic;
using System.Linq;

namespace ShipwayUnitOfWork.Repository
{
    public class AgencyRepository : GenericRepository<Agency>, IAgencyRepository, IGenericRepository<Agency>
    {
        private GenericRepository<Agency> _repository;
        public AgencyRepository(ShipwayDbEntities context) : base(context) { }

        public AgencyRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
            _repository = new GenericRepository<Agency>(unitOfWork);
        }

        public List<AgencyModelView> GetAll_V2()
        {
            var agencys = from a in Context.Agency
                          join w in Context.Ward on a.WardId equals w.WardId
                          join d in Context.District on w.DistrictId equals d.DistrictId
                          join p in Context.Province on d.ProvinceId equals p.ProvinceId
                          select new AgencyModelView()
                          {
                              AgencyId = a.AgencyId,
                              AgencyName = a.AgencyName,
                              PhoneNumber = a.PhoneNumber,
                              Address = a.Address,
                              ActivityTime = a.ActivityTime,
                              IsCentralWarehouse = a.IsCentralWarehouse ?? false,
                              ProvinceId = p.ProvinceId,
                              ProvinceName = p.Name,
                              DistrictId = d.DistrictId,
                              DistrictName = d.Name,
                              WardId = w.WardId,
                              WardName = w.Name
                          };
            return agencys.ToList();
        }

        public AgencyModelView GetDataById_V2(int id)
        {
            var agency = from a in Context.Agency
                         join w in Context.Ward on a.WardId equals w.WardId
                         join d in Context.District on w.DistrictId equals d.DistrictId
                         join p in Context.Province on d.ProvinceId equals p.ProvinceId
                         where a.AgencyId == id
                         select new AgencyModelView()
                         {
                             AgencyId = a.AgencyId,
                             AgencyName = a.AgencyName,
                             PhoneNumber = a.PhoneNumber,
                             Address = a.Address,
                             ActivityTime = a.ActivityTime,
                             IsCentralWarehouse = a.IsCentralWarehouse ?? false,
                             ProvinceId = p.ProvinceId,
                             ProvinceName = p.Name,
                             DistrictId = d.DistrictId,
                             DistrictName = d.Name,
                             WardId = w.WardId,
                             WardName = w.Name
                         };
            return agency.FirstOrDefault();
        }
    }
}