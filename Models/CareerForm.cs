namespace ZhooNotification.API.Models
{
    public class CareerForm
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string PhoneNo { get; set; }
        public string? DesiredPosition { get; set; }
        public string? CoverLetter { get; set; }
        public IFormFile Resume { get; set; }
    }
}
