using ShipwayBusiness.DatabaseContext;
using ShipwayBusiness.Entities;

namespace ShipwayBusiness.Repository.Repositories
{
    public class UserRepository : ShipwayRepository<User>
    {
        public UserRepository(ShipwayContext context) : base(context)
        {
        }
    }
}