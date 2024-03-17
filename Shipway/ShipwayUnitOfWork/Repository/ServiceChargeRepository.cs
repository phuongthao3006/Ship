using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;

namespace ShipwayUnitOfWork.Repository
{
    public class ServiceChargeRepository : GenericRepository<ServiceCharge>, IServiceChargeRepository
    {
        private GenericRepository<ServiceCharge> _repository;
        public ServiceChargeRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
        }

        public ServiceChargeRepository(ShipwayDbEntities context) : base(context)
        {
            _repository = new GenericRepository<ServiceCharge>(context);
        }
    }
}