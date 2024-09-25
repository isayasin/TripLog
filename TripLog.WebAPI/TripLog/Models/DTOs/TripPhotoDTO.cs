namespace TripLog.WebAPI.Models.DTOs;

public record TripPhotoDTO(
    string title,
    string description,
    IFormFile photoUrl
    );
