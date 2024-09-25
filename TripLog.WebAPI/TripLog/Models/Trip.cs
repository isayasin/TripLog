namespace TripLog.Models;

public sealed class Trip
{
    public Trip()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; } = default!;

    public ICollection<Tag> Tags { get; set; }
    public ICollection<TripPhoto> TripPhotos { get; set; }
}
