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

    public async Task<Profile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = "SELECT * FROM identity.profiles WHERE id = @id";
        return await connection.QueryFirstOrDefaultAsync<Profile>(new  CommandDefinition(sql, new { id }, cancellationToken: cancellationToken));
    }

    public async Task<Profile?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        const string sql = "SELECT * FROM identity.profiles WHERE username = @username";

        var profile = await connection.QueryFirstOrDefaultAsync<Profile>(new CommandDefinition(sql, new { username },
            cancellationToken: cancellationToken));

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

        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, new { username },
            cancellationToken: cancellationToken));
    }

    public async Task<Guid> AddAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        INSERT INTO identity.profiles (username, description)
        VALUES (@Username, @Description)
        RETURNING id;";

        return await connection.ExecuteScalarAsync<Guid>(new CommandDefinition(sql, profile,
            cancellationToken: cancellationToken));
    }

    public async Task UpdateAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        UPDATE identity.profiles
        SET username = @Username, description = @Description
        where id = @Id;";

        var rows = await connection.ExecuteAsync(new CommandDefinition(sql,
            new { username = profile.Username, description = profile.Description, id = profile.Id },
            cancellationToken: cancellationToken));

        if (rows == 0)
        {
            throw new Exception($"Profile '{profile.Id}' was not found.");
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        DELETE FROM identity.profiles 
        WHERE id = @id";

        var rows = await connection.ExecuteAsync(new CommandDefinition(sql, new { id },
            cancellationToken: cancellationToken));

        if (rows == 0)
        {
            throw new Exception($"Profile '{id}' was not found.");
        }
    }
}