using CrudAppGrpc.Models;
using CrudAppGrpc.Repositories;
using CrudAppGrpc.Services;
using CrudAppGrpcOptions;
using Grpc.Core;
using Moq;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace CrudAppGrpc.Tests;

public class CrudOptionsServiceTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly CrudOptionsService _crudOptionsService;

    public CrudOptionsServiceTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _crudOptionsService = new CrudOptionsService(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateTaskShouldCallRepositoryCreateModel()
    {
        var request = new CreateTaskRequest
        {
            Title = "Test task",
            Text = "Example text",
            IsCompleted = false,
        };
        
        var response = await _crudOptionsService.CreateTask(request, new Mock<ServerCallContext>().Object);
        
        _projectRepositoryMock.Verify(r => r.CreateModel(It.Is<CustomTaskRequest>(c => 
            c.Title == request.Title &&
            c.Text == request.Text &&
            c.IsCompleted == request.IsCompleted)), Times.Once);

        Assert.True(response.Success);
    }

    [Fact]
    public async Task UpdateTaskShouldCallRepositoryUpdateModel()
    {
        var request = new UpdateTaskRequest
        {
            Id = 1,
            Title = "Updated test task",
            Text = "Updated example text",
            IsCompleted = true,
        };
        var response = await _crudOptionsService.UpdateTask(request, new Mock<ServerCallContext>().Object);
        _projectRepositoryMock.Verify(r => r.UpdateModel(It.Is<CustomTask>(c =>
            c.Id == request.Id &&
            c.Title == request.Title &&
            c.Text == request.Text &&
            c.IsCompleted == request.IsCompleted)), Times.Once);
        
        Assert.True(response.Success);
    }
    
    [Fact]
    public async Task ReadTasks_ShouldReturnTasksFromRepository()
    {
        var tasks = new List<CustomTask>
        {
            new (){ Id = 1, Title = "Task 1", Text = "Description 1", IsCompleted = false },
            new (){ Id = 2, Title = "Task 2", Text = "Description 2", IsCompleted = true }
        };
        _projectRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(tasks);
        
        var response = await _crudOptionsService.ReadTasks(new ListTaskRequest(), new Mock<ServerCallContext>().Object);
        
        Assert.Equal(2, response.Tasks.Count);
        Assert.Contains(response.Tasks, t => t.Title == "Task 1");
        Assert.Contains(response.Tasks, t => t.Title == "Task 2");
    }
    [Fact]
    public async Task DeleteTask_ShouldCallRepositoryDeleteById()
    {
        var request = new DeleteTaskRequest { Id = 1 };
        
        var response = await _crudOptionsService.DeleteTask(request, new Mock<ServerCallContext>().Object);
        
        _projectRepositoryMock.Verify(r => r.DeleteById(request.Id), Times.Once);
        Assert.True(response.Success);
    }
        
}