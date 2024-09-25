using Microsoft.EntityFrameworkCore;
using TripLog.Context;

namespace TripLog.Repository;

public class Repository<T>(ApplicationDbContext context) where T : class
{
    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await context.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await context.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        context.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        context.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
