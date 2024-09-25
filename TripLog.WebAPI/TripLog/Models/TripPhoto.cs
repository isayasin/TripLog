namespace TripLog.Models;

public sealed class TripPhoto
{
    public TripPhoto()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }

    public string TripID { get; set; }
    public Trip Trip { get; set; }
}
