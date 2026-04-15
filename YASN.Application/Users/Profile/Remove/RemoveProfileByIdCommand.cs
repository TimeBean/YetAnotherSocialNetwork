using MediatR;

namespace YASN.Application.Users.Profile.Remove;

public record RemoveProfileByIdCommand(Guid Id) : IRequest;