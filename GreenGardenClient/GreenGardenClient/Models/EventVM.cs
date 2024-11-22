using System.ComponentModel.DataAnnotations;

namespace GreenGardenClient.Models
{
    public class EventVM
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Tên sự kiện không được để trống.")]
        public string EventName { get; set; } = null!;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Ngày sự kiện không được để trống.")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Thời gian bắt đầu không được để trống.")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc không được để trống.")]
        public TimeSpan? EndTime { get; set; }

        [Required(ErrorMessage = "Địa điểm không được để trống.")]
        public string? Location { get; set; }

        public string? PictureUrl { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreateBy { get; set; }
        public string CreatedByUserName { get; set; }

    }

    public class UpdateEvent
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Location { get; set; }
        public string? PictureUrl { get; set; }
        public bool? IsActive { get; set; }

    }
}
