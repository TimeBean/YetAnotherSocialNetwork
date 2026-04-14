using MediatR;

namespace YASN.Application.Users.Profile.Create;

public record CreateProfileCommand(string Username, string? Description = null) : IRequest<Guid>;