using MediatR;
using YASN.Application.Users.Profile.Create;
using YASN.Application.Users.UserCredential.Create;
using YASN.Domain.Repository;
using YASN.Domain.Service;

namespace YASN.Application.Users.Account.Create;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IPasswordService _passwordService;
    private readonly IMediator _mediator;
    
    public CreateAccountHandler(IPasswordService passwordService, IMediator mediator)
    {
        _passwordService = passwordService;
        _mediator = mediator;
    }
    
    public async Task<Guid> Handle(CreateAccountCommand createAccountCommand, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(new CreateProfileCommand(createAccountCommand.Username), cancellationToken);
        var hash = _passwordService.ComputeHash(createAccountCommand.Password, createAccountCommand.Salt);

        await _mediator.Send(new CreateUserCredentialCommand(id, hash, createAccountCommand.Salt), cancellationToken);
        
        return id;
    }
}