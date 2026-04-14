using Dapper;
using Npgsql;
using YASN.Domain.Repository;
using YASN.Domain.Users;

namespace YASN.Infrastructure;

public class DatabaseProfileRepository : IProfileRepository
{
    private readonly string _connectionString;

    public DatabaseProfileRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task<Profile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Profile?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var profile = await connection.QueryFirstOrDefaultAsync<Profile>(
            "SELECT * FROM identity.profiles WHERE username = @username",
            new { username });

        return profile;
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        SELECT EXISTS (
            SELECT 1 
            FROM identity.profiles 
            WHERE username = @username
        );";

        return await connection.ExecuteScalarAsync<bool>(sql, new { username });
    }

    public async Task<Guid> AddAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        INSERT INTO identity.profiles (username, description)
        VALUES (@Username, @Description)
        RETURNING id;";

        return await connection.ExecuteScalarAsync<Guid>(sql, profile);
    }

    public Task UpdateAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}