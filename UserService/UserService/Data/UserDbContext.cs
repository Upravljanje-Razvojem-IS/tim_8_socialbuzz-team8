using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Guid _personalUseId = Guid.Parse("b3851f6d-8984-43b2-aecd-02b115687204");
            Guid _corporateUserId = Guid.Parse("b3851f6d-8984-43b2-aecd-02b125687004");
            #endregion

            #region Role Ids
            Guid _adminRole = Guid.Parse("067ea5db-9991-4ba3-80d1-821cc217fe3c");
            Guid _regularUserRole = Guid.Parse("8157308d-de73-435a-bda3-a91ad6d23c84");
            #endregion

            #region Seed data
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = _personalUseId,
                    UserName = "fefolino",
                    Email = "fefa@gmail.com",
                    PhoneNumber = "+38105050505",
                    AccountIsActive = true,
                    PasswordHash = "l4g506m3",
                    TwoFactorEnabled = true
                },
                new ApplicationUser
                {
                    Id = _adminUserId,
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "+38105056665",
                    AccountIsActive = true,
                    PasswordHash = "x3x8tte0",
                    TwoFactorEnabled = true
                },
                new ApplicationUser
                {
                    Id = _corporateUserId,
                    UserName = "Dexico",
                    Email = "dexico@gmail.com",
                    PhoneNumber = "+01205050505",
                    AccountIsActive = true,
                    PasswordHash = "l4dxvuqru0y12euh",
                    TwoFactorEnabled = true
                }
         );

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole(_adminRole, "Admin", "Role that enables root level privileges"),
                new ApplicationRole(_regularUserRole, "Regular User", "Role that enables root level privileges")
         );
        }
    }
    #endregion
}
