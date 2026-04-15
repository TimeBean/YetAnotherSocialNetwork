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

    public async Task<Profile?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = "SELECT * FROM identity.profiles WHERE id = @id";
        return await connection.QueryFirstOrDefaultAsync<Profile>(new  CommandDefinition(sql, new { id }, cancellationToken: cancellationToken));
    }

    public async Task<Profile?> GetAsync(string username, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        const string sql = "SELECT * FROM identity.profiles WHERE username = @username";

        var profile = await connection.QueryFirstOrDefaultAsync<Profile>(new CommandDefinition(sql, new { username },
            cancellationToken: cancellationToken));

        return profile;
    }

    public async Task<bool> ExistsAsync(string username, CancellationToken cancellationToken = default)
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

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        SELECT EXISTS (
            SELECT 1 
            FROM identity.profiles 
            WHERE id = @id
        );";

        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, new { id },
            cancellationToken: cancellationToken));
    }

    public async Task<Guid> AddAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        INSERT INTO identity.profiles (id, username, description)
        VALUES (@Id, @Username, @Description)
        RETURNING id;";

        return await connection.ExecuteScalarAsync<Guid>(new CommandDefinition(sql, new { profile.Id, profile.Username, profile.Description },
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

    public async Task UpdateUsernameAsync(Guid id, string username, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        UPDATE identity.profiles
        SET username = @Username
        where id = @Id;";

        var rows = await connection.ExecuteAsync(new CommandDefinition(sql,
            new { username = username, id = id },
            cancellationToken: cancellationToken));

        if (rows == 0)
        {
            throw new Exception($"Profile '{id}' was not found.");
        }
    }

    public async Task UpdateDescriptionAsync(Guid id, string description, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        UPDATE identity.profiles
        SET description = @Description
        where id = @Id;";

        var rows = await connection.ExecuteAsync(new CommandDefinition(sql,
            new { Description = description, Id = id },
            cancellationToken: cancellationToken));

        if (rows == 0)
        {
            throw new Exception($"Profile '{id}' was not found.");
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

    public async Task DeleteAsync(string username, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        DELETE FROM identity.profiles 
        WHERE username = @username";

        var rows = await connection.ExecuteAsync(new CommandDefinition(sql, new { username },
            cancellationToken: cancellationToken));

        if (rows == 0)
        {
            throw new Exception($"Profile '{username}' was not found.");
        }
    }
}