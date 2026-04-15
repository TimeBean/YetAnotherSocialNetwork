using MediatR;
using YASN.Domain.Repository;

namespace YASN.Application.Users.Profile.Update;

public class UpdateProfileHandler : IRequestHandler<UpdateProfileUsernameCommand>,  IRequestHandler<UpdateProfileDescriptionCommand>
{
    private readonly IProfileRepository _profileRepository;

    public UpdateProfileHandler(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }
    
    public async Task Handle(UpdateProfileUsernameCommand updateUsernameCommand, CancellationToken cancellationToken)
    {
        var exists = await _profileRepository.ExistsAsync(updateUsernameCommand.Id, cancellationToken);
        
        if (!exists)
        {
            throw new Exception($"Profile with id:{updateUsernameCommand.Id} does not exists");
        }
        
        await _profileRepository.UpdateUsernameAsync(updateUsernameCommand.Id, updateUsernameCommand.Username, cancellationToken);
    }

    public async Task Handle(UpdateProfileDescriptionCommand updateDescriptionCommand, CancellationToken cancellationToken)
    {
        var exists = await _profileRepository.ExistsAsync(updateDescriptionCommand.Id, cancellationToken);
        
        if (!exists)
        {
            throw new Exception($"Profile with id:{updateDescriptionCommand.Id} does not exists");
        }
        
        await _profileRepository.UpdateUsernameAsync(updateDescriptionCommand.Id, updateDescriptionCommand.Description, cancellationToken);
    }
}