using Logger.Entity;
using Microsoft.EntityFrameworkCore;

namespace Logger.Context
{
    public class LoggerContext : DbContext
    {
        public LoggerContext(DbContextOptions<LoggerContext> options): base(options) { }

        public DbSet<LogItem> LogItems { get; set; }
    }
}