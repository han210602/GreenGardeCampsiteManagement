namespace BusinessObject.DTOs
{
    public class EventDTO
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
        public DateTime? CreatedAt { get; set; }
        public int? CreateBy { get; set; }
        public string? NameCreateByUser { get; set; }
    }
    public class CreateEventDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime EventDate { get; set; }
        public string StartTime { get; set; } = "00:00:00";  // Sử dụng string với định dạng HH:mm:ss
        public string EndTime { get; set; } = "00:00:00";
        public string? Location { get; set; }
        public string? PictureUrl { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreateBy { get; set; }
    }
    public class UpdateEventDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime EventDate { get; set; }
        public string StartTime { get; set; } = "00:00:00";  // Sử dụng string với định dạng HH:mm:ss
        public string EndTime { get; set; } = "00:00:00";
        public string? Location { get; set; }
        public string? PictureUrl { get; set; }
        public bool? IsActive { get; set; }
    }
}
