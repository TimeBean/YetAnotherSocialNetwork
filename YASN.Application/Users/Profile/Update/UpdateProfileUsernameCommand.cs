using MediatR;

namespace YASN.Application.Users.Profile.Update;

public record UpdateProfileUsernameCommand(Guid Id, string Username) : IRequest;