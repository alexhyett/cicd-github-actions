using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using GitHubActionsDemo.Persistance.Infrastructure;
using Microsoft.Extensions.Options;

namespace GitHubActionsDemo.Persistance;

public class DbContext : IDbContext
{
    private readonly DbSettings _dbSettings;

    public DbContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings?.Value ?? throw new ArgumentNullException(nameof(dbSettings));
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_dbSettings.ConnectionString);
    }

    public async Task Init()
    {
        await InitDatabase();
        await InitTables();
    }

    private async Task InitDatabase()
    {
        // create database if it doesn't exist
        using var connection = new MySqlConnection(_dbSettings.ConnectionString);
        var sql = $"CREATE DATABASE IF NOT EXISTS `{_dbSettings.Database}`;";
        await connection.ExecuteAsync(sql);
    }

    private async Task InitTables()
    {
        // create tables if they don't exist
        using var connection = CreateConnection();
        await _initAuthors();
        await _initBooks();

        async Task _initAuthors()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS authors (
                    author_id INT NOT NULL AUTO_INCREMENT,
                    first_name VARCHAR(255) NOT NULL,
                    last_name VARCHAR(255) NOT NULL,
                    date_created DATETIME NOT NULL,
                    date_modified DATETIME NOT NULL,
                    PRIMARY KEY (author_id)
                );
            """;
            await connection.ExecuteAsync(sql);
        }

        async Task _initBooks()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS books(
                    book_id INT NOT NULL AUTO_INCREMENT,
                    title VARCHAR(255),
                    author_id INT NOT NULL,
                    isbn VARCHAR(13) NOT NULL,
                    date_published DATETIME NOT NULL,
                    date_created DATETIME NOT NULL,
                    date_modified DATETIME NOT NULL,
                    PRIMARY KEY(book_id),
                    FOREIGN KEY(author_id) REFERENCES authors(author_id)
                );
            """;
            await connection.ExecuteAsync(sql);
        }


    }
}