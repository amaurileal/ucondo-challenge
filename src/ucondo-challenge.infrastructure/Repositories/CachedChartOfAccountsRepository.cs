using Microsoft.EntityFrameworkCore;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Repositories;
using ucondo_challenge.infrastructure.Cache;
using ucondo_challenge.infrastructure.Persistence;
using ucondo_challenge.infrastructure.teste;

namespace ucondo_challenge.infrastructure.Repositories;

internal class CachedChartOfAccountsRepository(
    UCondoChallengeDbContext dbContext, 
    IRedisCache cache
    ) : IChartOfAccountsRepository
{
    private const string CacheKey = CacheKeys.ChartOfAccountsKey;

    private async Task<IList<ChartOfAccountsEntity>> GetCachedRegistersAsync(Guid tenantId) => (await cache.GetAsync<IEnumerable<ChartOfAccountsEntity>>($"{CacheKey}:{tenantId}") ?? new List<ChartOfAccountsEntity>()).ToList();

    public async Task<Guid> CreateAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken)
    {
        try
        {

        
        dbContext.ChartOfAccounts.Add(entity);
        await dbContext.SaveChangesAsync();

        var cachedRegisters = await GetCachedRegistersAsync(entity.TenantId);
        cachedRegisters.Add(entity);
        await cache.SetAsync($"{CacheKey}:{entity.TenantId}", cachedRegisters, DefaultCacheTime.ExpiresInYear);
       
        return entity.Id;

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public async Task<int> DeleteAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken)
    {
        dbContext.ChartOfAccounts.Remove(entity);
        var result = await dbContext.SaveChangesAsync(cancellationToken);

        var cachedRegisters = await GetCachedRegistersAsync(entity.TenantId);
        var newRegister = cachedRegisters.Where(coa => coa.Id != entity.Id);
        
        await cache.SetAsync($"{CacheKey}:{entity.TenantId}", newRegister, DefaultCacheTime.ExpiresInYear);

        return result;
    }

    public async Task<IEnumerable<ChartOfAccountsEntity>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        var registers = await GetCachedRegistersAsync(tenantId);

        if (registers.Any())
            return registers;

        var result = await dbContext.ChartOfAccounts
                .Where(coa => coa.TenantId == tenantId)
                .ToListAsync(cancellationToken);

        await cache.SetAsync($"{CacheKey}:{tenantId}", result, DefaultCacheTime.ExpiresInYear);

        return result;
    }

    public async Task<IEnumerable<ChartOfAccountsEntity>> GetAllParentsAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        var cachedRegisters = await GetCachedRegistersAsync(tenantId);

        if (cachedRegisters.Any())
        {
            cachedRegisters =  cachedRegisters
             .Where(coa => coa.AllowEntries == false)
             .ToList();
        }

        return cachedRegisters;

    }

    public async Task<ChartOfAccountsEntity?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken)
    {
        var cachedRegisters = await GetCachedRegistersAsync(tenantId);

        if (cachedRegisters.Any())
        {
            return cachedRegisters
             .Where(coa => coa.Id == id)
             .FirstOrDefault();
        }

        var dbRegister = dbContext.ChartOfAccounts.Where(x => x.Id == id).FirstOrDefault();

        if (dbRegister != null)
            await FillCacheByTenantAsync(tenantId, cancellationToken);

        return dbRegister;
    }

    public async Task<IEnumerable<ChartOfAccountsEntity>> GetAllByParentId(Guid tenantId, Guid parentId, CancellationToken cancellationToken)
    {
        var cachedRegisters = await GetCachedRegistersAsync(tenantId);

        if (cachedRegisters.Any())
        {
            return cachedRegisters
             .Where(coa => coa.ParentId == parentId)
             .ToList();
        }

        var dbRegisters = await dbContext.ChartOfAccounts.Where(x => x.ParentId == parentId).ToListAsync(cancellationToken);

        if (dbRegisters != null)
            await FillCacheByTenantAsync(tenantId, cancellationToken);

        return dbRegisters;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
    
    

    public async Task<ChartOfAccountsEntity?> GetByCodeAsync(Guid tenantId, string code, CancellationToken cancellationToken)
    {
        var cachedRegisters = await GetCachedRegistersAsync(tenantId);

        if (cachedRegisters != null || cachedRegisters.Any())
        {
            return cachedRegisters
             .Where(coa => coa.Code == code)
             .FirstOrDefault();
        }

        var dbRegister = dbContext.ChartOfAccounts.Where(x => x.Code == code).FirstOrDefault();

        if (dbRegister != null)
            await FillCacheByTenantAsync(tenantId, cancellationToken);

        return dbRegister;
    }

    public async Task<ChartOfAccountsEntity?> GetByNameAsync(Guid tenantId, string name, CancellationToken cancellationToken)
    {
        var cachedRegisters = await GetCachedRegistersAsync(tenantId);

        if (cachedRegisters != null || cachedRegisters.Any())
        {
            return cachedRegisters
             .Where(coa => coa.Name == name)
             .FirstOrDefault();
        }

        var dbRegister = dbContext.ChartOfAccounts.Where(x => x.Name == name).FirstOrDefault();

        if (dbRegister != null)
            await FillCacheByTenantAsync(tenantId, cancellationToken);

        return dbRegister;
    }

    public async Task UpdateAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken)
    {
        var cachedRegisters = await GetCachedRegistersAsync(entity.TenantId);
        if (cachedRegisters != null || cachedRegisters.Any())
        {
            var existingEntity = cachedRegisters.FirstOrDefault(coa => coa.Id == entity.Id);
            if (existingEntity != null)
            {
                existingEntity.Name = entity.Name;
                existingEntity.Code = entity.Code;
                existingEntity.AllowEntries = entity.AllowEntries;
                existingEntity.ParentId = entity.ParentId;
                
                dbContext.ChartOfAccounts.Update(existingEntity);
                await dbContext.SaveChangesAsync(cancellationToken);
                
                await cache.SetAsync($"{CacheKey}:{entity.TenantId}", cachedRegisters, DefaultCacheTime.ExpiresInYear);
            }
        }
    }

    private async Task FillCacheByTenantAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        var allDbRegistersByTenant = await dbContext.ChartOfAccounts.Where(coa => coa.TenantId == tenantId).ToListAsync(cancellationToken);
        await cache.RemoveAsync($"{CacheKey}:{tenantId}");
        await cache.SetAsync($"{CacheKey}:{tenantId}", allDbRegistersByTenant, DefaultCacheTime.ExpiresInYear);
    }
}