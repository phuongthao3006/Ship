using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipwayBusiness.Entities
{
    public class ExhibitionStatus
    {
        public int ExhibitionStatusId { get; set; }
        public string Name { get; set; }
    }

    public enum EnumExhibitionStatus
    {
        // đã tới kho trung chuyển của tỉnh
        khotinhgui = 1,
        // đã chuyển tới khu trung chuyển trung tâm
        khotrungchuyen = 2,
        // đã chuyển tới kho trung chuyển của tỉnh
        khotinhnhan = 3,
        // đang được giao tới bạn 
        danggiaohang = 4,
        // đơn hàng được giao thành công 
        giaothanhcong = 5,
        // đơn hàng giao không thành công
        giaothatbai = 6,
        // đang được chuyển tới kho trung chuyển
        dangvanchuyen = 7,
        // hoàn trả người gửi 
        hanghoan = 8,
    }
}