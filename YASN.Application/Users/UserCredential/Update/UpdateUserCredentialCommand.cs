using MediatR;

namespace YASN.Application.Users.UserCredential.Update;

public record UpdateUserCredentialCommand(Domain.Users.UserCredential UserCredential) : IRequest;