using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShipwayUnitOfWork.Repository
{
    public class WardRepository : GenericRepository<Ward>, IWardRepository
    {
        private GenericRepository<Ward> _repository;
        public WardRepository(ShipwayDbEntities context) : base(context) { }

        public WardRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
            _repository = new GenericRepository<Ward>(unitOfWork);
        }

        public List<Ward> GetDataByDistrictId(string districtId)
        {
            return _repository.FindAll(w => w.DistrictId.Equals(districtId)).ToList();
        }
    }
}