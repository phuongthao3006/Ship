using System;

namespace ShipwayUnitOfWork.Models.ModelView
{
    public class UsersModelView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string WardId { get; set; }
        public string WardName { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<int> AgencyId { get; set; }
        public string AgencyName { get; set; }
    }
}