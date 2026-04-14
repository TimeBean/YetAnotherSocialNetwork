using MediatR;
using YASN.Domain.Repository;

namespace YASN.Application.Users.Profile.Create;

public class CreateProfileHandler : IRequestHandler<CreateProfileCommand, Guid>
{
    private readonly IProfileRepository _profileRepository;

    public CreateProfileHandler(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<Guid> Handle(CreateProfileCommand createProfileCommand, CancellationToken cancellationToken)
    {
        var exists = await _profileRepository.ExistsByUsernameAsync(createProfileCommand.Username, cancellationToken);
        
        if (exists)
        {
            throw new Exception($"Profile {createProfileCommand.Username} already exists");
        }
        
        var profile = new Domain.Users.Profile(Guid.NewGuid(), createProfileCommand.Username,
            createProfileCommand.Description);

        await _profileRepository.AddAsync(profile, cancellationToken);
        return profile.Id;
    }
}