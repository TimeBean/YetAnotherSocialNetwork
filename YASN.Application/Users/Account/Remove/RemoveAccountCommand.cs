using MediatR;

namespace YASN.Application.Users.Account.Remove;

public record RemoveAccountCommand(Guid Id) : IRequest;