using CrudAppGrpc.Models;
using CrudAppGrpc.Repositories;
using CrudAppGrpcOptions;
using Grpc.Core;
using Status = CrudAppGrpcOptions.Status;
using Task = CrudAppGrpcOptions.Task;

namespace CrudAppGrpc.Services;

public class CrudOptionsService(IProjectRepository repository) : CrudOptions.CrudOptionsBase
{
    public override async Task<Status> CreateTask(CreateTaskRequest request, ServerCallContext context)
    {
        var customTaskRequest = new CustomTaskRequest
        {
            Title = request.Title,
            Text = request.Text,
            IsCompleted = request.IsCompleted,
        };
        await repository.CreateModel(customTaskRequest);
        Console.WriteLine("Created Custom Task " + customTaskRequest.Title);
        return new Status {Success = true };
    }

    public override async Task<ListTaskResponse> ReadTasks(ListTaskRequest request, ServerCallContext context)
    {
        var listCustomTask = await repository.GetAll();
        var response = new ListTaskResponse();
        foreach (var customTask in listCustomTask)
        {
            response.Tasks.Add(new Task()
            {
                Id = customTask.Id,
                Title = customTask.Title,
                Text = customTask.Text,
                IsCompleted = customTask.IsCompleted,
            });
        }
        return response;
    }
    public override async Task<Status> UpdateTask(UpdateTaskRequest request, ServerCallContext context)
    {
        var customTask = new CustomTask
        {
            Id = request.Id,
            Title = request.Title,
            Text = request.Text,
            IsCompleted = request.IsCompleted,
        };
        await repository.UpdateModel(customTask);
        return new Status {Success = true};
    }

    public override async Task<DeleteTaskResponse> DeleteTask(DeleteTaskRequest request, ServerCallContext context)
    {
        await repository.DeleteById(request.Id);
        return new DeleteTaskResponse() {Success = true};
    }
    
}