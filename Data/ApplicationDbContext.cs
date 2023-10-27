using ELabel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ELabel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
        }

        public DbSet<Product> Product { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditing();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditing()
        {
            var modifiedEntries = ChangeTracker.Entries()
                                               .Where(x => x.Entity is IAuditableEntity &&
                                                     (x.State == EntityState.Added || x.State == EntityState.Modified));

            DateTime now = DateTime.UtcNow;
            string? userName = _contextAccessor.HttpContext?.User.Identity?.Name;

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity? entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = userName ?? "system";
                        entity.CreatedOn = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedOn).IsModified = false;
                    }

                    entity.UpdatedBy = userName ?? "system";
                    entity.UpdatedOn = now;
                }
            }
        }
    }
}