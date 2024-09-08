using CrudAppGrpc.Models;
using CrudAppGrpcOptions;

namespace CrudAppGrpc.Mappers;

public static class CreateTaskMapper
{
    public static CustomTask ToTask(this CreateTaskRequest createTaskRequest)
    {
        return new CustomTask
        {
            Title = createTaskRequest.Title,
            Text = createTaskRequest.Text,
            IsCompleted = createTaskRequest.IsCompleted,
        };
    }
}