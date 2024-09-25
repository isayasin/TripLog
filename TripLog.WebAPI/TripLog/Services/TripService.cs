using TripLog.Models;
using TripLog.Repository;
using TripLog.WebAPI.Models.DTOs;

namespace TripLog.WebAPI.Services;

public sealed class TripService(TripRepository tripRepository, TagRepository tagRepository)
{
    public async Task<List<Trip>> GetAllTripsAsync(CancellationToken cancellationToken)
    {
        return await tripRepository.GetAllAsync(cancellationToken);
    }

    public async Task CreateTripAsync(TripDTO tripDTO, CancellationToken cancellationToken)
    {

        string FileName = string.Join(".", DateTime.Now.ToFileTime().ToString(), tripDTO.image.FileName);

        var tagsArray = tripDTO.tags.Split(' ', StringSplitOptions.TrimEntries);

        var tags = await tagRepository.GetAllAsync(cancellationToken);
        var includedTags = tags.Where(tag => tagsArray.Contains(tag.Name)).ToList();
        var excludedTags = tagsArray.Where(tag => tags.All(t => t.Name != tag)).ToList();

        foreach (var item in excludedTags)
        {
            Tag tag = new Tag
            {
                Name = item,
            };
            await tagRepository.CreateAsync(tag, cancellationToken);
        }

        tags = await tagRepository.GetAllAsync(cancellationToken);
        var currentTags = tags.Where(tag => tagsArray.Contains(tag.Name)).ToList();


        List<TripPhoto> photos = new List<TripPhoto>();

        foreach (var item in tripDTO.TripPhotoDTOs)
        {
            string PhotoFileName = string.Join(".", DateTime.Now.ToFileTime().ToString(), item.photoUrl.FileName);

            TripPhoto tripPhoto = new()
            {
                Title = item.title,
                Description = item.description,
                PhotoUrl = PhotoFileName
            };
            using (var stream = System.IO.File.Create($"wwwroot/{PhotoFileName}"))
            {
                item.photoUrl.CopyTo(stream);
            }
            photos.Add(tripPhoto);
        }

        Trip trip = new Trip
        {
            Title = tripDTO.title,
            Description = tripDTO.description,
            Tags = currentTags,
            ImageUrl = FileName,
            TripPhotos = photos
        };

        await tripRepository.CreateAsync(trip, cancellationToken);
        using (var stream = System.IO.File.Create($"wwwroot/{FileName}"))
        {
            tripDTO.image.CopyTo(stream);
        }
    }
}
