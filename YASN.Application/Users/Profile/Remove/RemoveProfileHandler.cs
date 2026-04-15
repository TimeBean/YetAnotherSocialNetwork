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
    
    public async Task Handle(RemoveProfileByIdCommand removeProfileByIdCommand, CancellationToken cancellationToken)
    {
        var exists = await _profileRepository.ExistsAsync(removeProfileByIdCommand.Id, cancellationToken);
        
        if (!exists)
        {
            throw new Exception($"Profile with id:{removeProfileByIdCommand.Id} does not exists");
        }
        
        await _profileRepository.DeleteAsync(removeProfileByIdCommand.Id, cancellationToken);
    }

    public async Task Handle(RemoveProfileByUsernameCommand removeProfileByUsernameCommand, CancellationToken cancellationToken)
    {
        var exists = await _profileRepository.ExistsAsync(removeProfileByUsernameCommand.Username, cancellationToken);
        
        if (!exists)
        {
            throw new Exception($"Profile with username:{removeProfileByUsernameCommand.Username} does not exists");
        }
        
        await _profileRepository.DeleteAsync(removeProfileByUsernameCommand.Username, cancellationToken);
    }
}