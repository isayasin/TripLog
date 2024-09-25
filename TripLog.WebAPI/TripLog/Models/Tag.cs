namespace TripLog.Models;

public sealed class Tag
{
    public Tag()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public string Name { get; set; }

    public ICollection<Trip> Trips { get; set; }
}
