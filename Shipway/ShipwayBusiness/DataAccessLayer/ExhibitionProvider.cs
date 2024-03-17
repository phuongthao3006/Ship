using ShipwayBusiness.CoreProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShipwayBusiness.Entities;
using ShipwayBusiness.Repository.UnitOfWork;
using System.Collections.ObjectModel;

namespace ShipwayBusiness.DataAccessLayer
{
    public class ExhibitionProvider : IExhibitionProvider
    {
        public bool DeleteExhibiton(Exhibition exhibiton)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                return uow.ExhibitionRepositoty.Delete(exhibiton);
            }
        }

        public List<Exhibition> GetAllExhibitionByAssignedShipper(int shipperId)
        {
            using( var uow= new ShipwayUnitOfWork())
            {
                return uow.ExhibitionRepositoty.FindAll(p => p.AssignShipperId == shipperId).ToList();
            }
        }

        public List<Exhibition> GetAllExhibitionOfCustomerById(int customerId)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                return uow.ExhibitionRepositoty.FindAll(p => p.CreatedUserId == customerId).ToList();
            }
        }

        public List<ExhibitionView> GetAllExhibitionView(int customerId)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                var user = uow.UserRepository.Context.User.FirstOrDefault(item => item.Id == customerId);
                var result = uow.ExhibitionRepositoty.Context.Exhibition.
                    Join(uow.ExhibitionRepositoty.Context.ExhibitionStatus, ex => ex.ExhibitionStatusId, es => es.ExhibitionStatusId, (ex, es) => new { Exhibition = ex, ExhibitionStatus = es })
                    .Where(p=> user.AgencyId == null || p.Exhibition.AssignToAgency == user.AgencyId)             
                    .Select(p => new ExhibitionView()
                    {
                        ExhibitionId= p.Exhibition.ExhibitionId,
                        ReceiverName=p.Exhibition.ReceiverName,
                        ReceiverAddress=p.Exhibition.ReceiverAddress,
                        ReceiverPhoneNumber=p.Exhibition.ReceiverPhoneNumber,
                        Cost=p.Exhibition.Cost,
                        CreatedDate=p.Exhibition.CreatedDate,
                        ExhibitionStatus=p.ExhibitionStatus.Name
                    }).ToList();
                return result;
            }
        }

        public List<Exhibition> GetAllExhibiton()
        {
            using( var uow= new ShipwayUnitOfWork())
            {
                return uow.ExhibitionRepositoty.GetAll().ToList();
            }
        }

        public DateTime GetCreateDate(string exhibitionId)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                return uow.ExhibitionRepositoty.Find(p => p.ExhibitionId == exhibitionId).CreatedDate;
            }
        }

        public Exhibition GetExhibitionById(string id)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                return uow.ExhibitionRepositoty.Find(p => p.ExhibitionId == id);
            }
        }

        public ObservableCollection<Exhibition> GetFailExhibition(int shipperId)
        {
            using( var uow= new ShipwayUnitOfWork())
            {
                //var exhibitionStatusId = uow.ExhibitionStatusRepository.Find(e => e.Name == "Giao thất bại").ExhibitionStatusId;
                var list= uow.ExhibitionRepositoty.FindAll(p => p.AssignShipperId == shipperId && p.ExhibitionStatusId == (int)EnumExhibitionStatus.giaothatbai).ToList();
                ObservableCollection<Exhibition> listExhibiton = new ObservableCollection<Exhibition>(list);
                return listExhibiton;
            }
        }

        public ObservableCollection<Exhibition> GetCompleteExhibition(int shipperId)
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                //var exhibitionStatusId = uow.ExhibitionStatusRepository.Find(e => e.Name == "Giao thành công").ExhibitionStatusId;
                var list = uow.ExhibitionRepositoty.FindAll(p => p.AssignShipperId == shipperId && p.ExhibitionStatusId == (int)EnumExhibitionStatus.giaothanhcong).ToList();
                ObservableCollection<Exhibition> listExhibiton = new ObservableCollection<Exhibition>(list);
                return listExhibiton;
            }
        }

        public ObservableCollection<Exhibition> GetNewExhibition(int shipperId)
        {
            using (var uow = new ShipwayUnitOfWork())
            {
               // var exhibitionStatusId = uow.ExhibitionStatusRepository.Find(e => e.Name == "Mới tạo").ExhibitionStatusId;
                var list = uow.ExhibitionRepositoty.FindAll(p => p.AssignShipperId == shipperId && p.ExhibitionStatusId == (int)EnumExhibitionStatus.khotinhgui).ToList();
                ObservableCollection<Exhibition> listExhibiton = new ObservableCollection<Exhibition>(list);
                return listExhibiton;
            }
        }

        public void InsertExhibiton(Exhibition exhibiton)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                uow.ExhibitionRepositoty.Add(exhibiton);
            }
        }

        public Exhibition UpdateExhibition(Exhibition exhibition)
        {
            using(var uow= new ShipwayUnitOfWork())
            {
                return uow.ExhibitionRepositoty.Update(exhibition);
            }
        }

        public ObservableCollection<Exhibition> GetWaitSendExhibition(int shipperId)
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                //var exhibitionStatusId = uow.ExhibitionStatusRepository.Find(e => e.Name == "Đã nhận").ExhibitionStatusId;
                var list = uow.ExhibitionRepositoty.FindAll(p => p.AssignShipperId == shipperId && p.ExhibitionStatusId == (int)EnumExhibitionStatus.danggiaohang).ToList();
                ObservableCollection<Exhibition> listExhibiton = new ObservableCollection<Exhibition>(list);
                return listExhibiton;
            }
        }

        public List<ExhibitionViewModel> GetAllExhibitionViewModel(int customerId, bool isImportExhibition = false, bool isExportExhibition = false, bool isCentralWarehouse = false)
        {           
            using (var uow = new ShipwayUnitOfWork())
            {
                var user = uow.UserRepository.Context.User.FirstOrDefault(item => item.Id == customerId);
                string agencyProvinceId = uow.UserRepository.Context.Agency.FirstOrDefault(item => item.AgencyId == user.AgencyId)?.ProvinceId;
                var result = uow.ExhibitionRepositoty.Context.Exhibition
                    .Join(uow.ExhibitionRepositoty.Context.ExhibitionStatus, ex => ex.ExhibitionStatusId, es => es.ExhibitionStatusId, (ex, es) => new { Exhibition = ex, ExhibitionStatus = es })
                    .Where(p => (user.AgencyId == null || p.Exhibition.AssignToAgency == user.AgencyId && (!isExportExhibition || (isExportExhibition && p.Exhibition.AssignShipperId == null && p.Exhibition.ExhibitionStatusId <= (int)EnumExhibitionStatus.khotrungchuyen)))
                    && (!isImportExhibition
                    || (isImportExhibition && isCentralWarehouse && p.Exhibition.ExhibitionStatusId == (int)EnumExhibitionStatus.dangvanchuyen)
                    || (isImportExhibition && !isCentralWarehouse && (agencyProvinceId != null && p.Exhibition.ProvinceReceiveId == agencyProvinceId) && p.Exhibition.ExhibitionStatusId == (int)EnumExhibitionStatus.dangvanchuyen))
                    )
                    .Select(p => new ExhibitionViewModel()
                    {
                        Exhibition= p.Exhibition,
                        ExhibitonStatus=p.ExhibitionStatus.Name
                    }).ToList();
                return result;               
            }
        }

        public List<ExhibitionViewModel> SearchExhibitionByStatus(int exhibitionStatusId)
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                var result = uow.ExhibitionRepositoty.Context.Exhibition.
                    Join(uow.ExhibitionRepositoty.Context.ExhibitionStatus, ex => ex.ExhibitionStatusId, es => es.ExhibitionStatusId, (ex, es) => new { Exhibition = ex, ExhibitionStatus = es })
                    .Where(p=> exhibitionStatusId == 0 ||  p.Exhibition.ExhibitionStatusId== exhibitionStatusId)
                    .Select(p => new ExhibitionViewModel()
                    {
                        Exhibition = p.Exhibition,
                        ExhibitonStatus = p.ExhibitionStatus.Name
                    }).ToList();
                return result;
            }
        }
        public List<ExhibitionViewModel> GetAllExhibitionToAssignShipper(string provinceId)
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                if (string.IsNullOrEmpty(provinceId)) return new List<ExhibitionViewModel>();

                var result = uow.ExhibitionRepositoty.Context.Exhibition.
                    Join(uow.ExhibitionRepositoty.Context.ExhibitionStatus, ex => ex.ExhibitionStatusId, es => es.ExhibitionStatusId, (ex, es) => new { Exhibition = ex, ExhibitionStatus = es })
                    .Where(p => p.Exhibition.ExhibitionStatusId == (int)EnumExhibitionStatus.khotinhnhan && p.Exhibition.ProvinceReceiveId == provinceId)
                    .Select(p => new ExhibitionViewModel()
                    {
                        Exhibition = p.Exhibition,
                        ExhibitonStatus = p.ExhibitionStatus.Name
                    }).ToList();
                return result;
            }                
        }

        public List<ExhibitionViewModel> GetAllExhibitionByShipperId(int? shipperId)
        {
            using (var uow = new ShipwayUnitOfWork())
            {
                if (shipperId == null) return new List<ExhibitionViewModel>();

                return uow.ExhibitionRepositoty.FindAll(p => p.AssignShipperId == shipperId && (p.ExhibitionStatusId == (int)EnumExhibitionStatus.danggiaohang || (p.ExhibitionStatusId == (int)EnumExhibitionStatus.giaothatbai && p.NumberOfFailedDeliveries < 3))).Select(p => new ExhibitionViewModel()
                {
                    Exhibition = p,
                    ExhibitonStatus = "Đơn hàng đang trên đường giao tới khách hàng"
                }).ToList();
            }
        }
    }
}