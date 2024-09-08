namespace CrudAppGrpc.Repositories;

public interface IRepository<TModel, in TRequest>
{
    Task CreateModel(TRequest model);
    Task<TModel?> GetById(int id);
    Task<IEnumerable<TModel>> GetAll();
    Task UpdateModel(TModel model);
    Task DeleteById(int id);
}