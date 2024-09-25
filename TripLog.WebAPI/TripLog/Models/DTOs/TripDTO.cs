namespace TripLog.WebAPI.Models.DTOs;

public record TripDTO(
    string title,
    string description,
    IFormFile image,
    string tags,
    List<TripPhotoDTO> TripPhotoDTOs
    );
