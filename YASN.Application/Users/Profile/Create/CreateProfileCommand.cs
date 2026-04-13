namespace YASN.Application.Users.Profile.Create;

public record CreateProfileCommand(Guid Id, string Username, string? Description = null);