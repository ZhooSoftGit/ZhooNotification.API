namespace ZhooNotification.API.Models
{
    public class CareerApplication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string PhoneNo { get; set; }
        public string? DesiredPosition { get; set; }
        public string? CoverLetter { get; set; }
        public string ResumeUrl { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }

}
