using CrudAppGrpc.Models;

namespace CrudAppGrpc.Repositories;

public interface IProjectRepository: IRepository<CustomTask, CustomTaskRequest>
{
    
}