using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Entities
{
    public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly IConfiguration configuration;
        public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region User Ids
            Guid _adminUserId = Guid.Parse("b3851f6d-8984-43b2-aecd-02b125687204");
            Guid _personalUserId = Guid.Parse("b3851f6d-8984-43b2-aecd-02b115687204");
            Guid _corporateUserId = Guid.Parse("b3851f6d-8984-43b2-aecd-02b125687004");
            #endregion

            #region Role Ids
            Guid _adminRole = Guid.Parse("067ea5db-9991-4ba3-80d1-821cc217fe3c");
            Guid _regularUserRole = Guid.Parse("8157308d-de73-435a-bda3-a91ad6d23c84");
            #endregion

            #region Seed data
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser (_personalUserId, "fefolino", "fefa@gmail.com", "+38105050505", UserAccountTypesExtensions.Personal),
                
                new ApplicationUser (_adminUserId, "admin", "admin@gmail.com", "+38105056665", UserAccountTypesExtensions.Admin ),
                
                new ApplicationUser (_corporateUserId, "Dexico", "dexico@gmail.com", "+01205050505", UserAccountTypesExtensions.Corporate)
         );

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole(_adminRole, "Admin", "Role that enables root level privileges"),
                new ApplicationRole(_regularUserRole, "Regular User", "Role that basic level privileges")
         );
        }
    }
    #endregion
}
