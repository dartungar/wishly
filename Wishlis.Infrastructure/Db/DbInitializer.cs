using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using NLog;
using Npgsql;
using Wishlis.Infrastructure.Logging;

namespace Wishlis.Infrastructure;

public class DbInitializer
{
    private const string DB_NAME = "Wishlis";
    private ILogger _logger;
    private readonly IDbConnection _connection;

    public DbInitializer(IOptions<DbOptions> options)
    {
        _connection = new NpgsqlConnection(options.Value.ConnectionString);
        _logger = WishlisLogger.GetLogger();
    }

    public async void InitDb()
    {
        try
        {
            await _connection.ExecuteAsync(DbCreationSql());
            _logger.Debug("Successfully created database.");
        }
        // TODO: catch specifically errors that arise when DB/Table already exist
        catch (PostgresException e)
        {
            _logger.Debug("Database is already created.");
        }

        try
        {
            await _connection.ExecuteAsync(TablesCreationSql());
            _logger.Debug("Successfully created tables");
        }
        catch (PostgresException e)
        {
            _logger.Debug("Tables are already created");
        }
    }

    private string DbCreationSql()
        => $@"CREATE DATABASE {DB_NAME}";

    private string TablesCreationSql()
        => @"CREATE TABLE IF NOT EXISTS Users (
            Id SERIAL NOT NULL PRIMARY KEY,
            Name VARCHAR(50) NOT NULL,
            PublicId VARCHAR(20) NOT NULL,
            DateOfBirth DATE
            );
            
            ";

}