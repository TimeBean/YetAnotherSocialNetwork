using MediatR;
using YASN.Domain.Repository;
using YASN.Infrastructure;

namespace YASN.Application.Users.UserCredential.Create;

public class CreateUserCredentialHandler : IRequestHandler<CreateUserCredentialCommand, Guid>
{
    private readonly IUserCredentialRepository _userCredentialRepository;

    public CreateUserCredentialHandler(IUserCredentialRepository userCredentialRepository)
    {
        _userCredentialRepository = userCredentialRepository;
    }

    public async Task<Guid> Handle(CreateUserCredentialCommand userCredentialCommand,
        CancellationToken cancellationToken)
    {
        var exists = await _userCredentialRepository.ExistsAsync(userCredentialCommand.Id, cancellationToken);

        if (exists)
        {
            throw new Exception($"Profile {userCredentialCommand.Id} already exists");
        }

        var userCredential = new Domain.Users.UserCredential(userCredentialCommand.Id, userCredentialCommand.Hash,
            userCredentialCommand.Salt);

        await _userCredentialRepository.AddAsync(userCredential, cancellationToken);
        return userCredential.UserId;
    }
}