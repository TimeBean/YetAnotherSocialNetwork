using MediatR;

namespace YASN.Application.Users.UserCredential.Create;

public record  CreateUserCredentialCommand(Guid Id, string Hash, string Salt) : IRequest<Guid>;