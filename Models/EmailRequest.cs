﻿namespace ZhooNotification.API.Models
{
    public class EmailRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Message { get; set; }
    }
}
