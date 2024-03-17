using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;

namespace ShipwayUnitOfWork.Repository
{
    public class KindServiceRepository : GenericRepository<KindService>, IKindServiceRepository
    {
        private GenericRepository<KindService> _repository;
        public KindServiceRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
        }

        public KindServiceRepository(ShipwayDbEntities context) : base(context)
        {
            _repository = new GenericRepository<KindService>(context);
        }
    }
}