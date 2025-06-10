using Microsoft.EntityFrameworkCore;
using ucondo_challenge.business.Entities;

namespace ucondo_challenge.infrastructure
{
    internal class UCondoChallengeDbContext(DbContextOptions<UCondoChallengeDbContext> options) : DbContext(options)
    {
        public DbSet<ChartOfAccountsEntity> ChartOfAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
        }
    }


}
