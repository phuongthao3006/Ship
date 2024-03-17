using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Helper;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Models.ModelView;
using System.Collections.Generic;
using System.Linq;

namespace ShipwayUnitOfWork.Repository
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        private GenericRepository<Users> _repository;

        public UserRepository(ShipwayDbEntities context) : base(context) { }

        public UserRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
            _repository = new GenericRepository<Users>(unitOfWork);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Users GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void ResetPassword(int id)
        {
            Users user = GetById(id);
            if (user != null)
            {
                user.Password = Utils.MD5("123456");
                _repository.Update(user);
            }
        }

        public Users Login(Users user)
        {
            Users model = _repository.Find(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password));
            return model;
        }

        public IEnumerable<TypeUsers> GetTypeUsers()
        {
            return Context.TypeUsers.ToList();
        }

        public bool ExistUserName(string userName)
        {
            return _repository.Find(u => u.UserName.Equals(userName)) != null;
        }

        public List<Users> GetDataByTypeUser(int typeUserId)
        {
            return _repository.FindAll(m => m.TypeId == typeUserId).ToList();
        }

        public List<UsersModelView> GetDataByTypeUser_V2(int typeUserId)
        {
            var users = from u in Context.Users
                        join w in Context.Ward on u.WardId equals w.WardId
                        join d in Context.District on w.DistrictId equals d.DistrictId
                        join p in Context.Province on d.ProvinceId equals p.ProvinceId
                        join a in Context.Agency on u.AgencyId equals a.AgencyId
                        where u.TypeId == typeUserId
                        orderby u.AgencyId
                        select new UsersModelView()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            Name = u.Name,
                            Address = u.Address,
                            TypeId = typeUserId,
                            AgencyId = u.AgencyId,
                            AgencyName = a.AgencyName,
                            ProvinceId = p.ProvinceId,
                            ProvinceName = p.Name,
                            DistrictId = d.DistrictId,
                            DistrictName = d.Name,
                            WardId = w.WardId,
                            WardName = w.Name
                        };
            return users.ToList();
        }

        public UsersModelView GetDataById_V2(int id)
        {
            var users = from u in Context.Users
                        join w in Context.Ward on u.WardId equals w.WardId
                        join d in Context.District on w.DistrictId equals d.DistrictId
                        join p in Context.Province on d.ProvinceId equals p.ProvinceId
                        join a in Context.Agency on u.AgencyId equals a.AgencyId
                        where u.Id == id
                        select new UsersModelView()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            Name = u.Name,
                            Address = u.Address,
                            TypeId = u.TypeId,
                            AgencyId = u.AgencyId,
                            AgencyName = a.AgencyName,
                            ProvinceId = p.ProvinceId,
                            ProvinceName = p.Name,
                            DistrictId = d.DistrictId,
                            DistrictName = d.Name,
                            WardId = w.WardId,
                            WardName = w.Name
                        };
            return users.FirstOrDefault();
        }

        public List<Users> GetAll_OrderByTypeId()
        {
            return _repository.GetAll().OrderBy(x => x.TypeId).ToList();
        }
    }
}