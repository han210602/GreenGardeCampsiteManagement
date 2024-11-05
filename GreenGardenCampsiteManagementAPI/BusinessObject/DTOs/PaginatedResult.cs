using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }  // Danh sách các mục trong trang hiện tại
        public int TotalItems { get; set; }  // Tổng số mục có trong cơ sở dữ liệu
        public int TotalPages { get; set; }   // Tổng số trang
        public int PageNumber { get; set; }   // Số trang hiện tại
        public int PageSize { get; set; }     // Số mục mỗi trang
    }
}
