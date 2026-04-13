using YASN.Domain.Users;

namespace YASN.Domain.Repository;

public interface IProfileRepository
{
    Task<Profile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Profile?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<IEnumerable<Profile>> SearchAsync(string query, int limit, CancellationToken cancellationToken = default);
    Task<IEnumerable<Profile>> GetFollowersAsync(Guid profileId, int limit, CancellationToken cancellationToken = default);
    Task<IEnumerable<Profile>> GetFollowingAsync(Guid profileId, int limit, CancellationToken cancellationToken = default);
    Task<int> GetFollowersCountAsync(Guid profileId, CancellationToken cancellationToken = default);
    Task<int> GetFollowingCountAsync(Guid profileId, CancellationToken cancellationToken = default);
    Task AddAsync(Profile profile, CancellationToken cancellationToken = default);
    Task UpdateAsync(Profile profile, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}