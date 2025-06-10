using ucondo_challenge.business.Entities;

namespace ucondo_challenge.business.Repositories
{
    public interface IChartOfAccountsRepository
    {
        Task<Guid> CreateAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken);        
        Task<int> DeleteAsync(ChartOfAccountsEntity entity, CancellationToken cancellationToken);
        Task<ChartOfAccountsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ChartOfAccountsEntity>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken);
        Task<IEnumerable<ChartOfAccountsEntity>> GetByParentId(Guid tenantId, int parentId, CancellationToken cancellationToken);
        Task<IEnumerable<ChartOfAccountsEntity>> GetAllParentsAsync(Guid tenantId, CancellationToken cancellationToken);
        Task SaveChanges();
    }
}
