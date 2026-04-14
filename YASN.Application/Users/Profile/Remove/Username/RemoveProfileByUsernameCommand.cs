using MediatR;

namespace YASN.Application.Users.Profile.Remove.Username;

public record RemoveProfileByUsernameCommand(string Username) : IRequest;