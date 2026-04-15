using Dapper;
using Npgsql;
using YASN.Domain.Repository;
using YASN.Domain.Users;

namespace YASN.Infrastructure.Repository;

public class DatabaseUserCredentialRepository : IUserCredentialRepository
{
    private readonly string _connectionString;

    public DatabaseUserCredentialRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<UserCredential?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                id       AS UserId, 
                hash     AS Hash, 
                salt     AS Salt
            FROM identity.user_credentials 
            WHERE id = @id";

        return await connection.QueryFirstOrDefaultAsync<UserCredential>(
            new CommandDefinition(sql, new { id = userId }, cancellationToken: cancellationToken));
    }

    public async Task AddAsync(UserCredential userCredential, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        INSERT INTO identity.user_credentials (id, hash, salt)
        VALUES (@Id, @Hash, @Salt)
        RETURNING id;";

        await connection.ExecuteScalarAsync<Guid>(new CommandDefinition(sql,
            new { Id = userCredential.UserId, userCredential.Hash, userCredential.Salt },
            cancellationToken: cancellationToken));
    }

    public async Task UpdateAsync(UserCredential userCredential, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        UPDATE identity.user_credentials
        SET id = @Id, hash = @Hash, salt = @Salt
        where id = @Id;";

        var rows = await connection.ExecuteAsync(new CommandDefinition(sql,
            new { userCredential.UserId, userCredential.Hash, userCredential.Salt },
            cancellationToken: cancellationToken));

        if (rows == 0)
        {
            throw new Exception($"Profile '{userCredential.UserId}' was not found.");
        }
    }

    public async Task RemoveAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        DELETE FROM identity.user_credentials 
        WHERE id = @Id";

        var rows = await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = userId },
            cancellationToken: cancellationToken));

        if (rows == 0)
        {
            throw new Exception($"Profile '{userId}' was not found.");
        }
    }

    public async Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        SELECT EXISTS (
            SELECT 1 
            FROM identity.user_credentials 
            WHERE id = @Id
        );";

        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, new { Id = userId },
            cancellationToken: cancellationToken));
    }
}