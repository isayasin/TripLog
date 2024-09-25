using Microsoft.EntityFrameworkCore;
using TripLog.Context;
using TripLog.Models;

namespace TripLog.Repository;

public class TripRepository : Repository<Trip>
{
    private readonly ApplicationDbContext _context;

    public TripRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Trip>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Trips.Include(x => x.TripPhotos).Include(x => x.Tags).ToListAsync();
    }

}
