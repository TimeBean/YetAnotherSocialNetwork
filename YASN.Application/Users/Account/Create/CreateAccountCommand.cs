using MediatR;

namespace YASN.Application.Users.Account.Create;

public record CreateAccountCommand(string Username, string Password, string Salt) : IRequest<Guid>;