using TripLog.Context;
using TripLog.Models;

namespace TripLog.Repository;

public class TagRepository : Repository<Tag>
{
    public TagRepository(ApplicationDbContext context) : base(context)
    {
    }
}
