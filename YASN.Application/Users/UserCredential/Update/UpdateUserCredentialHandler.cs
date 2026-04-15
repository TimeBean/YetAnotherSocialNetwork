using MediatR;
using YASN.Domain.Repository;

namespace YASN.Application.Users.UserCredential.Update;

public class UpdateUserCredentialHandler : IRequestHandler<UpdateUserCredentialCommand>
{
    private readonly IUserCredentialRepository _userCredentialRepository;

    public UpdateUserCredentialHandler(IUserCredentialRepository userCredentialRepository)
    {
        _userCredentialRepository = userCredentialRepository;
    }

    public async Task Handle(UpdateUserCredentialCommand updateUserCredentialCommand,
        CancellationToken cancellationToken)
    {
        var exists =
            await _userCredentialRepository.ExistsAsync(updateUserCredentialCommand.UserCredential.UserId,
                cancellationToken);

        if (!exists)
        {
            throw new Exception($"Profile with id:{updateUserCredentialCommand.UserCredential.UserId} does not exists");
        }

        await _userCredentialRepository.UpdateAsync(updateUserCredentialCommand.UserCredential, cancellationToken);
    }
}