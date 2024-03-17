using ShipwayUnitOfWork.GenericRepository;
using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using ShipwayUnitOfWork.Models.ModelView;
using System.Collections.Generic;
using System.Linq;

namespace ShipwayUnitOfWork.Repository
{
    public class ExhibitionRepository : GenericRepository<Exhibition>, IExhibitionRepository
    {
        private GenericRepository<Exhibition> _repository;
        public ExhibitionRepository(ShipwayDbEntities context) : base(context) { }
        public ExhibitionRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : base(unitOfWork)
        {
            _repository = new GenericRepository<Exhibition>(unitOfWork);
        }

        public List<ExhibitionModelView> GetAll_V2()
        {
            var exhibitions = from ex in Context.Exhibition
                              join sender in Context.Users on ex.SenderId equals sender.Id
                              join ward in Context.Ward on ex.WardReceiveId equals ward.WardId
                              join district in Context.District on ward.DistrictId equals district.DistrictId
                              join province in Context.Province on district.ProvinceId equals province.ProvinceId
                              join createdUser in Context.Users on ex.CreatedUserId equals createdUser.Id
                              join modifiedUser in Context.Users on ex.LastModifiedUserId equals modifiedUser.Id
                              join kind in Context.KindService on ex.KindServiceId equals kind.KindServiceId
                              join status in Context.ExhibitionStatus on ex.ExhibitionStatusId equals status.ExhibitionStatusId
                              join assign in Context.Agency on ex.AssignToAgencyId equals assign.AgencyId
                              join agen in Context.Agency on ex.AgencyId equals agen.AgencyId
                              select new ExhibitionModelView()
                              {
                                  ExhibitionId = ex.ExhibitionId,
                                  SenderId = ex.SenderId,
                                  SenderUser = sender,
                                  ReceiverName = ex.ReceiverName,
                                  ReceiverPhoneNumber = ex.ReceiverPhoneNumber,
                                  ReceiverAddress = ex.ReceiverAddress,
                                  ProvinceReceive = province,
                                  DistrictReceive = district,
                                  WardReceive = ward,
                                  PackageWeigh = ex.PackageWeigh,
                                  PackageLong = ex.PackageLong,
                                  PackageWide = ex.PackageWide,
                                  PackageHigh = ex.PackageHigh,
                                  Note = ex.Note,
                                  CreatedDate = ex.CreatedDate,
                                  CreatedUser = createdUser,
                                  LastModifiedDate = ex.LastModifiedDate,
                                  LastModifiedUser = modifiedUser,
                                  KindService = kind,
                                  Cost = ex.Cost,
                                  ExhibitionStatus = status,
                                  DayReceive = ex.DayReceive,
                                  LocationSender = ex.LocationSender,
                                  LocationReceive = ex.LocationReceive,
                                  IsSendToAgency = ex.IsSendToAgency,
                                  AssignToAgency = assign,
                                  GroupLump = ex.GroupLump,
                                  NumberOfFailedDeliveries = ex.NumberOfFailedDeliveries,
                                  Agency = agen
                              };
            return exhibitions.ToList();
        }

        public List<ExhibitionModelView> GetAll_V2_ViewSupperAdmin()
        {
            var exhibitions = from ex in Context.Exhibition
                              join sender in Context.Users on ex.SenderId equals sender.Id
                              join ward in Context.Ward on ex.WardReceiveId equals ward.WardId
                              join district in Context.District on ward.DistrictId equals district.DistrictId
                              join province in Context.Province on district.ProvinceId equals province.ProvinceId
                              join kind in Context.KindService on ex.KindServiceId equals kind.KindServiceId
                              join status in Context.ExhibitionStatus on ex.ExhibitionStatusId equals status.ExhibitionStatusId
                              join agen in Context.Agency on ex.AgencyId equals agen.AgencyId
                              select new ExhibitionModelView()
                              {
                                  ExhibitionId = ex.ExhibitionId,
                                  SenderId = ex.SenderId,
                                  SenderUser = sender,
                                  ReceiverName = ex.ReceiverName,
                                  ReceiverPhoneNumber = ex.ReceiverPhoneNumber,
                                  ReceiverAddress = ex.ReceiverAddress + ", " + ward.Name + ", " + district.Name + ", " + province.Name,
                                  ProvinceReceive = province,
                                  DistrictReceive = district,
                                  WardReceive = ward,
                                  PackageWeigh = ex.PackageWeigh,
                                  PackageLong = ex.PackageLong,
                                  PackageWide = ex.PackageWide,
                                  PackageHigh = ex.PackageHigh,
                                  Note = ex.Note,
                                  CreatedDate = ex.CreatedDate,
                                  LastModifiedDate = ex.LastModifiedDate,
                                  KindService = kind,
                                  Cost = ex.Cost,
                                  ExhibitionStatus = status,
                                  DayReceive = ex.DayReceive,
                                  LocationSender = ex.LocationSender,
                                  LocationReceive = ex.LocationReceive,
                                  IsSendToAgency = ex.IsSendToAgency,
                                  GroupLump = ex.GroupLump,
                                  NumberOfFailedDeliveries = ex.NumberOfFailedDeliveries,
                                  Agency = agen
                              };
            return exhibitions.ToList();
        }

        public List<ExhibitionModelView> GetByAgencyIdAndStatus_V2_ViewSupperAdmin(int agencyId, int statusId)
        {
            var exhibitions = from ex in Context.Exhibition
                              join sender in Context.Users on ex.SenderId equals sender.Id
                              join ward in Context.Ward on ex.WardReceiveId equals ward.WardId
                              join district in Context.District on ward.DistrictId equals district.DistrictId
                              join province in Context.Province on district.ProvinceId equals province.ProvinceId
                              join kind in Context.KindService on ex.KindServiceId equals kind.KindServiceId
                              join status in Context.ExhibitionStatus on ex.ExhibitionStatusId equals status.ExhibitionStatusId
                              join agen in Context.Agency on ex.AgencyId equals agen.AgencyId
                              where ex.AgencyId == agencyId && ex.ExhibitionStatusId == statusId
                              select new ExhibitionModelView()
                              {
                                  ExhibitionId = ex.ExhibitionId,
                                  SenderId = ex.SenderId,
                                  SenderUser = sender,
                                  ReceiverName = ex.ReceiverName,
                                  ReceiverPhoneNumber = ex.ReceiverPhoneNumber,
                                  ReceiverAddress = ex.ReceiverAddress + ", " + ward.Name + ", " + district.Name + ", " + province.Name,
                                  ProvinceReceive = province,
                                  DistrictReceive = district,
                                  WardReceive = ward,
                                  PackageWeigh = ex.PackageWeigh,
                                  PackageLong = ex.PackageLong,
                                  PackageWide = ex.PackageWide,
                                  PackageHigh = ex.PackageHigh,
                                  Note = ex.Note,
                                  CreatedDate = ex.CreatedDate,
                                  LastModifiedDate = ex.LastModifiedDate,
                                  KindService = kind,
                                  Cost = ex.Cost,
                                  ExhibitionStatus = status,
                                  DayReceive = ex.DayReceive,
                                  LocationSender = ex.LocationSender,
                                  LocationReceive = ex.LocationReceive,
                                  IsSendToAgency = ex.IsSendToAgency,
                                  GroupLump = ex.GroupLump,
                                  NumberOfFailedDeliveries = ex.NumberOfFailedDeliveries,
                                  Agency = agen
                              };
            return exhibitions.ToList();
        }

        public List<Exhibition> GetDataByAgencyId(int agencyId)
        {
            return _repository.FindAll(e => e.AgencyId == agencyId).ToList();
        }

        public List<ExhibitionStatus> GetExhibitionStatuses()
        {
            return Context.ExhibitionStatus.ToList();
        }
    }
}