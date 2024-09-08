namespace CrudAppGrpc.Models;

public class CustomTask
{
    public int Id {get; set;}
    public string? Title {get; set;}
    public string? Text {get; set;}
    public bool IsCompleted {get; set;}
};
