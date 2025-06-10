using Microsoft.EntityFrameworkCore;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Repositories;
using ucondo_challenge.infrastructure.Persistence;

namespace ucondo_challenge.infrastructure.Repositories
{
    internal class ChartOfAccountsRepository(
        UCondoChallengeDbContext dbContext
        ) : IChartOfAccountsRepository
    {
        public async Task<Guid> CreateAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken)
        {
            dbContext.ChartOfAccounts.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken)
        {
            dbContext.ChartOfAccounts.Remove(entity);
            var result = await dbContext.SaveChangesAsync(cancellationToken);            
            return result;
        }

        public async Task<IEnumerable<ChartOfAccountsEntity>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            var result = await dbContext.ChartOfAccounts
                .Where(coa => coa.TenantId == tenantId)
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<ChartOfAccountsEntity>> GetAllParentsAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            var result = await dbContext.ChartOfAccounts
                .Where(coa => coa.TenantId == tenantId && coa.AllowEntries == false)
                .AsNoTracking()                
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<ChartOfAccountsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.ChartOfAccounts
                .AsNoTracking()
                .FirstOrDefaultAsync(coa => coa.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<ChartOfAccountsEntity>> GetByParentId(Guid tenantId, Guid parentId, CancellationToken cancellationToken)
        {
           return await dbContext.ChartOfAccounts
                .Where(coa => coa.TenantId == tenantId && coa.ParentId == parentId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}
