using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;

namespace ShipwayUnitOfWork.Repository
{
    public class RouterRepository : GenericRepository<Router>, IRouterRepository
    {
        private GenericRepository<Router> _repository;
        public RouterRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
        }

        public RouterRepository(ShipwayDbEntities context) : base(context)
        {
            _repository = new GenericRepository<Router>(context);
        }
    }
}