using System.ComponentModel.DataAnnotations;

namespace CrudAppGrpc.Models;

public class CustomTaskRequest
{
    [Required]
    public string? Title {get; set;}
    [Required]
    public string? Text {get; set;}
    public bool IsCompleted {get; set;}
}