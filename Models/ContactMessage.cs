namespace ZhooNotification.API.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string? QueryType { get; set; } // <- Optional for now
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }


}
