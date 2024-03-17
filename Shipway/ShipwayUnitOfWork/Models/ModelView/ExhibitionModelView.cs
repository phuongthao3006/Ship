using System;

namespace ShipwayUnitOfWork.Models.ModelView
{
    public class ExhibitionModelView
    {
        public string ExhibitionId { get; set; }
        public Nullable<int> SenderId { get; set; }
        public Users SenderUser { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string ReceiverAddress { get; set; }
        public Province ProvinceReceive { get; set; }
        public District DistrictReceive { get; set; }
        public Ward WardReceive { get; set; }
        public Nullable<int> PackageWeigh { get; set; }
        public Nullable<int> PackageLong { get; set; }
        public Nullable<int> PackageWide { get; set; }
        public Nullable<int> PackageHigh { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Users CreatedUser { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Users LastModifiedUser { get; set; }
        public KindService KindService { get; set; }
        public Nullable<int> Cost { get; set; }
        public ExhibitionStatus ExhibitionStatus { get; set; }
        public Nullable<System.DateTime> DayReceive { get; set; }
        public string LocationSender { get; set; }
        public string LocationReceive { get; set; }
        public Nullable<bool> IsSendToAgency { get; set; }
        public Agency AssignToAgency { get; set; }
        public Nullable<int> GroupLump { get; set; }
        public Nullable<int> NumberOfFailedDeliveries { get; set; }
        public Agency Agency { get; set; }
    }
}