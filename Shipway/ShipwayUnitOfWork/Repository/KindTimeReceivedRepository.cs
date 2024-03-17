using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;

namespace ShipwayUnitOfWork.Repository
{
    public class KindTimeReceivedRepository : GenericRepository<KindTimeReceived>, IKindTimeReceivedRepository
    {
        private GenericRepository<KindTimeReceived> _repository;
        public KindTimeReceivedRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
        }

        public KindTimeReceivedRepository(ShipwayDbEntities context) : base(context)
        {
            _repository = new GenericRepository<KindTimeReceived>(context);
        }
    }
}