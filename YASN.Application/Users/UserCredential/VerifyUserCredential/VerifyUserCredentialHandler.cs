using MediatR;
using YASN.Domain.Repository;
using YASN.Domain.Service;

namespace YASN.Application.Users.UserCredential.VerifyUserCredential;

public class VerifyUserCredentialHandler : IRequestHandler<VerifyUserCredentialCommand, bool>
{
    private readonly IUserCredentialRepository _userCredentialRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IPasswordService _passwordService;

    public VerifyUserCredentialHandler(IUserCredentialRepository userCredentialRepository,
        IProfileRepository profileRepository, IPasswordService passwordService)
    {
        _userCredentialRepository = userCredentialRepository;
        _profileRepository = profileRepository;
        _passwordService = passwordService;
    }

    public async Task<bool> Handle(VerifyUserCredentialCommand verifyUserCredential,
        CancellationToken cancellationToken)
    {
        var exists = await _profileRepository.ExistsAsync(verifyUserCredential.Username, cancellationToken);
        if (!exists)
        {
            throw new Exception($"User {verifyUserCredential.Username} not found");
        }

        var profile = await _profileRepository.GetAsync(verifyUserCredential.Username, cancellationToken);
        if (profile is null)
        {
            return false;
        }

        var userCredential = await _userCredentialRepository.GetAsync(profile.Id, cancellationToken);
        if (userCredential is null)
        {
            return false;
        }

        return _passwordService.VerifyHash(userCredential.Hash, verifyUserCredential.Password,
            userCredential.Salt);
    }
}