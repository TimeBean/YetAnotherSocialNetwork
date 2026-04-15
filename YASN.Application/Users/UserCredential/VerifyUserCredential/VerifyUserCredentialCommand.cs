using MediatR;

namespace YASN.Application.Users.UserCredential.VerifyUserCredential;

public record VerifyUserCredentialCommand(string Username, string Password) : IRequest<bool>;