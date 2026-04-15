using MediatR;

namespace YASN.Application.Users.Profile.Update;

public record UpdateProfileDescriptionCommand(Guid Id, string Description) : IRequest;