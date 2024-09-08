using System.Data;
using CrudAppGrpc.Models;
using Dapper;

namespace CrudAppGrpc.Repositories.ProjectRepository;

public class Repository(IDbConnection db): IProjectRepository
{
    public async Task CreateModel(CustomTaskRequest model)
    {
        var query = "INSERT INTO \"CustomTasks\" (\"Title\", \"Text\", \"IsCompleted\") values (@Title, @Text, @IsCompleted)";
        await db.ExecuteAsync(query, model);
    }

    public async Task<CustomTask?> GetById(int id)
    {
        var query = "SELECT * FROM \"CustomTasks\" WHERE \"Id\"=@Id";
        return await db.QuerySingleOrDefaultAsync<CustomTask>(query, new { Id = id });
    }

    public async Task<IEnumerable<CustomTask>> GetAll()
    {
        var query = $"SELECT * FROM \"CustomTasks\"";
        return await db.QueryAsync<CustomTask>(query);
    }

    public async Task UpdateModel(CustomTask model)
    {
        var query = "UPDATE \"CustomTasks\" SET \"Title\" = @Title, \"Text\" = @Text, \"IsCompleted\"=@IsCompleted WHERE \"Id\" = @Id";
        await db.ExecuteAsync(query, model);
    }

    public async Task DeleteById(int id)
    {
        var query = $"DELETE FROM \"CustomTasks\" WHERE \"Id\" = @Id";
        await db.ExecuteAsync(query, new { Id = id });
    }
}