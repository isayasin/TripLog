using TripLog.Models;
using TripLog.Repository;
using TripLog.WebAPI.Models.DTOs;

namespace TripLog.WebAPI.Services;

public sealed class TagService(TagRepository tagRepository)
{
    public async Task CreateTagAsync(TagDTO tagDTO, CancellationToken cancellationToken)
    {
        Tag tag = new Tag
        {
            Name = tagDTO.Name,
        };

        await tagRepository.CreateAsync(tag, cancellationToken);
    }

    public async Task<List<Tag>> GetAllTagsAsync(CancellationToken cancellationToken)
    {
        return await tagRepository.GetAllAsync(cancellationToken);
    }
}
