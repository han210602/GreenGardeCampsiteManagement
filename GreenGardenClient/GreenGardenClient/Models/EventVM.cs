namespace GreenGardenClient.Models
{
    public class EventVM
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
