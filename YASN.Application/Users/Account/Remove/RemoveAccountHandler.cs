using MediatR;
using YASN.Application.Users.Profile.Remove;
using YASN.Application.Users.UserCredential.Remove;
using YASN.Domain.Repository;
using YASN.Domain.Service;

namespace YASN.Application.Users.Account.Remove;

public class RemoveAccountHandler : IRequestHandler<RemoveAccountCommand>
{
    private readonly IMediator _mediator;
    
    public RemoveAccountHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Handle(RemoveAccountCommand removeAccountCommand, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemoveUserCredentialCommand(removeAccountCommand.Id), cancellationToken);
        await _mediator.Send(new RemoveProfileByIdCommand(removeAccountCommand.Id), cancellationToken);
    }
}