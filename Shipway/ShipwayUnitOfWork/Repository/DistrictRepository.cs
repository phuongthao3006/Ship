using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShipwayUnitOfWork.Repository
{
    public class DistrictRepository : GenericRepository<District>, IDistrictRepository
    {
        private GenericRepository<District> _repository;
        public DistrictRepository(ShipwayDbEntities context) : base(context) { }

        public DistrictRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
            _repository = new GenericRepository<District>(unitOfWork);
        }

        public List<District> GetDataByProvinceId(string provinceId)
        {
            return _repository.FindAll(d => d.ProvinceId.Equals(provinceId)).ToList();
        }
    }
}