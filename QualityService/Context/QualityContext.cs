using Microsoft.EntityFrameworkCore;
using QualityService.Models;

namespace QualityService.Context
{
    public class QualityContext : DbContext
    {

        public QualityContext(DbContextOptions<QualityContext> options) : base(options)
        {
        }
        public DbSet<Feedback> Feedback { get; set; }
    }
}