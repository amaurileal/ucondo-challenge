using ucondo_challenge.business.Entities;

namespace ucondo_challenge.business.Repositories
{
    public interface IChartOfAccountsRepository
    {
        Task<Guid> CreateAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken);        
        Task<int> DeleteAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken);
        Task<ChartOfAccountsEntity?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ChartOfAccountsEntity>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken);
        Task<IEnumerable<ChartOfAccountsEntity>> GetAllByParentId(Guid tenantId, Guid parentId, CancellationToken cancellationToken);
        Task<IEnumerable<ChartOfAccountsEntity>> GetAllParentsAsync(Guid tenantId, CancellationToken cancellationToken);
        Task<ChartOfAccountsEntity?> GetByCodeAsync(Guid tenantId, string code, CancellationToken cancellationToken);
        Task<ChartOfAccountsEntity?> GetByNameAsync(Guid tenantId, string name, CancellationToken cancellationToken);
        Task UpdateAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken);
        Task SaveChanges();
    }
}
