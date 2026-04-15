using YASN.Domain.Users;

namespace YASN.Domain.Repository;

public interface IUserCredentialRepository
{
    Task<UserCredential?> GetAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(UserCredential userCredential, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserCredential userCredential, CancellationToken cancellationToken = default);
    Task RemoveAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default);
}