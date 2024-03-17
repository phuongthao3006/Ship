using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Models.ModelView;
using System.Collections.Generic;

namespace ShipwayUnitOfWork.Interface
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        void ResetPassword(int id);
        Users Login(Users user);
        IEnumerable<TypeUsers> GetTypeUsers();
        bool ExistUserName(string userName);
        List<Users> GetDataByTypeUser(int typeUserId);
        List<UsersModelView> GetDataByTypeUser_V2(int typeUserId);
        UsersModelView GetDataById_V2(int id);
        List<Users> GetAll_OrderByTypeId();
    }
}
