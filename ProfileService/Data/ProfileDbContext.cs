using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Data
{
    public class ProfileDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ProfileDb"));
        }

        //public DbSet<AuthInfo> AuthInfo { get; set; }
        public DbSet<UserDetails> UserDetails {get; set;}
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<CorporateUserDetails> CorporateUserDetails { get; set; }
    }

}
