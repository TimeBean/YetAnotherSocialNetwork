using MediatR;

namespace YASN.Application.Users.Profile.Remove;

public record RemoveProfileByUsernameCommand(string Username) : IRequest;