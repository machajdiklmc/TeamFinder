using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using TeamFinder.Server.Models;

namespace TeamFinder.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<SportEvent> Events { get; set; }
        public DbSet<UserEvents> UserEvents { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            /*
            builder.Entity<JoinedEvents>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.JoinedEvents)
                .HasForeignKey(bc => new {bc.UserId, bc.SportEventId})
                .OnDelete(DeleteBehavior.NoAction);*/
            builder.Entity<UserEvents>().HasKey(sc => new { sc.UserId, sc.SportEventId });
           /* builder.Entity<ApplicationUser>()
                .HasMany(a => a.Events)
                .WithMany(a => a.Users);*/
        } 
    }
}