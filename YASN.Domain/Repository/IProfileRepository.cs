using YASN.Domain.Users;

namespace YASN.Domain.Repository;

public interface IProfileRepository
{
    Task<Profile?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Profile?> GetAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> AddAsync(Profile profile, CancellationToken cancellationToken = default);
    Task UpdateAsync(Profile profile, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(string username, CancellationToken cancellationToken = default);
}