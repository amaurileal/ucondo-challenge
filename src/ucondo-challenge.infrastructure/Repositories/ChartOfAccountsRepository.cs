using Microsoft.EntityFrameworkCore;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Repositories;
using ucondo_challenge.infrastructure.Persistence;
using ucondo_challenge.infrastructure.Utils;

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
            
            var orderedResult = result.OrderByCode(x => x.Code);

            return orderedResult;
        }

        public async Task<IEnumerable<ChartOfAccountsEntity>> GetAllParentsAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            var result = await dbContext.ChartOfAccounts
                .Where(coa => coa.TenantId == tenantId && coa.AllowEntries == false)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<ChartOfAccountsEntity?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken)
        {
            var result =  await dbContext.ChartOfAccounts                
                .FirstOrDefaultAsync(coa => coa.Id == id, cancellationToken);
            return result;
        }

        public async Task<IEnumerable<ChartOfAccountsEntity>> GetAllByParentId(Guid tenantId, Guid parentId, CancellationToken cancellationToken)
        {
            return await dbContext.ChartOfAccounts
                 .Where(coa => coa.TenantId == tenantId && coa.ParentId == parentId)
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task SaveChanges() => await dbContext.SaveChangesAsync();

        public Task<IEnumerable<ChartOfAccountsEntity>> GetAllByParentCode(Guid tenantId, Guid parentId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ChartOfAccountsEntity?> GetByCodeAsync(Guid tenantId, string code, CancellationToken cancellationToken)
        {
            var result = await dbContext.ChartOfAccounts
                .FirstOrDefaultAsync(coa => coa.TenantId == tenantId && coa.Code == code, cancellationToken);
            return result;
        }

        public async Task<ChartOfAccountsEntity?> GetByNameAsync(Guid tenantId, string name, CancellationToken cancellationToken)
        {
            var result = await dbContext.ChartOfAccounts
                .FirstOrDefaultAsync(coa => coa.TenantId == tenantId && coa.Name == name, cancellationToken);
            return result;
        }

        public async Task UpdateAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
