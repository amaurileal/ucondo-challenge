using Microsoft.EntityFrameworkCore;
using ucondo_challenge.business.Entities;

namespace ucondo_challenge.infrastructure.Persistence
{
    internal class UCondoChallengeDbContext(DbContextOptions<UCondoChallengeDbContext> options) : DbContext(options)
    {
        public DbSet<ChartOfAccountsEntity> ChartOfAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UCondoChallengeDbContext).Assembly);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            base.OnModelCreating(modelBuilder);
        }
    }


}
