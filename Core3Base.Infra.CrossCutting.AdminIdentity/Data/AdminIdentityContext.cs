using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core3Base.Infra.CrossCutting.AdminIdentity.Entity;
using Core3Base.Infra.Data.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core3Base.Infra.CrossCutting.AdminIdentity.Data
{
    public class AdminIdentityContext : DbContext
    {

        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleGroup> RoleGroups { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        private readonly IHostingEnvironment _env;

        public AdminIdentityContext(
            DbContextOptions<AdminIdentityContext> options,
            IHostingEnvironment env) : base(options)
        {
            _env = env;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true)
                .Build();
            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseLazyLoadingProxies();
        }
        public override int SaveChanges()
        {
            CheckDeletable();
            AddTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            CheckDeletable();
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckDeletable();
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckDeletable();
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void CheckDeletable()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntityWithDate && (x.State == EntityState.Deleted && ((BaseEntityWithDate)x.Entity).IsDeletable != true));

            if (entities != null && entities.Count() > 0)
            {
                throw new Exception("Seçilen veri silinemez");
            }
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntityWithDate && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntityWithDate)entity.Entity).DateCreated = DateTime.Now;
                }

                ((BaseEntityWithDate)entity.Entity).DateModified = DateTime.Now;
            }
        }
    }
}
