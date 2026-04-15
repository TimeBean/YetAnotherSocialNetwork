using MediatR;

namespace YASN.Application.Users.UserCredential.Remove;

public record RemoveUserCredentialCommand(Guid Id) : IRequest;