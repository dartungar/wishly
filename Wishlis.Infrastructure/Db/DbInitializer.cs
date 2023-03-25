using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Serilog;
using Npgsql;

namespace Wishlis.Infrastructure.Db;

public class DbInitializer
{
    private const string DB_NAME = "wishlis";
    private readonly IDbConnection _connection;

    public DbInitializer(IOptions<DbOptions> options)
    {
        _connection = new NpgsqlConnection(options.Value.ConnectionString);
    }

    public async void InitDb()
    {
        try
        {
            await _connection.ExecuteAsync(DbCreationSql());
            Log.Debug("Successfully created database.");
        }
        // TODO: catch specifically errors that arise when DB/Table already exist
        catch (PostgresException e)
        {
            Log.Information("Database is probably created:", e);
        }

        foreach (var tableSql in TablesCreationSql())
        {
            try
            {
                await _connection.ExecuteAsync(tableSql);
                Log.Debug("Successfully created table");
            }
            catch (Exception e)
            {
                Log.Information("Table probably exists:", e);
                continue;
            }
        }
    }

    private string DbCreationSql()
        => $@"CREATE DATABASE {DB_NAME}";

    private string[] TablesCreationSql()
        => new[]
        {
            @"CREATE TABLE IF NOT EXISTS users (
                id SERIAL NOT NULL PRIMARY KEY,
                name VARCHAR(50) NOT NULL,
                public_id VARCHAR(10) NOT NULL UNIQUE,
                date_of_birth DATE
            );",
            
            @"CREATE TABLE IF NOT EXISTS external_ids (
                external_id VARCHAR(100) NOT NULL,
                user_id INT NOT NULL,
                
                CONSTRAINT FK_ExternalId_User_Id FOREIGN KEY (user_id) REFERENCES Users(id),
                UNIQUE (external_id, user_id)
            );",
            
            @"CREATE TABLE IF NOT EXISTS wishlist_items (
                id SERIAL NOT NULL PRIMARY KEY,
                user_id INT NOT NULL,
                name VARCHAR(250) NOT NULL,
                external_url VARCHAR(300) NOT NULL,
                cost MONEY NULL,
                currency INT NULL,
                is_joint_purchase BOOLEAN NOT NULL DEFAULT FALSE,
                
                CONSTRAINT FK_User_Id FOREIGN KEY (user_id) REFERENCES Users(Id)
            );"
        };

}