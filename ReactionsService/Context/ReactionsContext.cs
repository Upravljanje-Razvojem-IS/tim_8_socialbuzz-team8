using Microsoft.EntityFrameworkCore;
using ReactionsService.Entity;

namespace ReactionsService.Context
{
    public class ReactionsContext : DbContext
    {

        public ReactionsContext(DbContextOptions<ReactionsContext> options) : base(options)
        {
        }
        public DbSet<ReactionType> ReactionTypes { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
    }
}
