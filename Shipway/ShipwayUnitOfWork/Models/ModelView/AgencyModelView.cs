namespace ShipwayUnitOfWork.Models.ModelView
{
    public class AgencyModelView
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ActivityTime { get; set; }
        public bool IsCentralWarehouse { get; set; }
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string WardId { get; set; }
        public string WardName { get; set; }
    }
}