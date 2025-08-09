namespace ZhooNotification.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using ZhooNotification.API.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CareerApplication> CareerApplications { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }

}
