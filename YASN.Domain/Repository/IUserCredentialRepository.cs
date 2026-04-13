using YASN.Domain.Users;

namespace YASN.Domain.Repository;

public interface IUserCredentialRepository
{
    Task<UserCredential?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(UserCredential userCredential, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserCredential userCredential, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default);
}