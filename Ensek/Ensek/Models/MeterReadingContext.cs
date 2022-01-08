using Microsoft.EntityFrameworkCore;

namespace Ensek.Models
{
    public class MeterReadingContext : DbContext
    {
        public MeterReadingContext(DbContextOptions<MeterReadingContext> options)
            : base(options)
        {
        }

        public DbSet<MeterReadingItem> MeterReadingItems { get; set; }
    }
}