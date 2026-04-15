using MediatR;
using YASN.Domain.Repository;

namespace YASN.Application.Users.UserCredential.Remove;

public class RemoveUserCredentialHandler : IRequestHandler<RemoveUserCredentialCommand>
{
    private readonly IUserCredentialRepository _userCredentialRepository;
    
    public RemoveUserCredentialHandler(IUserCredentialRepository userCredentialRepository)
    {
        _userCredentialRepository = userCredentialRepository;
    }
    
    public async Task Handle(RemoveUserCredentialCommand removeUserCredentialCommand, CancellationToken cancellationToken)
    {
        var exists = await _userCredentialRepository.ExistsAsync(removeUserCredentialCommand.Id, cancellationToken);

        if (exists)
        {
            throw new Exception($"Profile {removeUserCredentialCommand.Id} does not  exists");
        }
        
        await _userCredentialRepository.RemoveAsync(removeUserCredentialCommand.Id, cancellationToken);
    }
}