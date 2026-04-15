using MediatR;
using YASN.Domain.Repository;

namespace YASN.Application.Users.Profile.Remove;

public class RemoveProfileHandler : IRequestHandler<RemoveProfileByIdCommand>, IRequestHandler<RemoveProfileByUsernameCommand>
{
    private readonly IProfileRepository _profileRepository;

    public RemoveProfileHandler(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }
    
    public async Task Handle(RemoveProfileByIdCommand byIdCommand, CancellationToken cancellationToken)
    {
        var exists = await _profileRepository.ExistsAsync(byIdCommand.Id, cancellationToken);
        
        if (!exists)
        {
            throw new Exception($"Profile with id:{byIdCommand.Id} does not exists");
        }
        
        await _profileRepository.DeleteAsync(byIdCommand.Id, cancellationToken);
    }

    public async Task Handle(RemoveProfileByUsernameCommand byUsernameCommand, CancellationToken cancellationToken)
    {
        var exists = await _profileRepository.ExistsAsync(byUsernameCommand.Username, cancellationToken);
        
        if (!exists)
        {
            throw new Exception($"Profile with username:{byUsernameCommand.Username} does not exists");
        }
        
        await _profileRepository.DeleteAsync(byUsernameCommand.Username, cancellationToken);
    }
}