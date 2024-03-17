using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;

namespace ShipwayUnitOfWork.Repository
{
    public class ProvinceRepository : GenericRepository<Province>, IProvinceRepository
    {
        private GenericRepository<Province> _repository;
        public ProvinceRepository(ShipwayDbEntities context) : base(context) { }

        public ProvinceRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
            _repository = new GenericRepository<Province>(unitOfWork);
        }
    }
}