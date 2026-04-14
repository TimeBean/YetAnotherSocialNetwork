using MediatR;

namespace YASN.Application.Users.Profile.Remove.Username;

public record RemoveProfileByIdCommand(Guid Id) : IRequest;