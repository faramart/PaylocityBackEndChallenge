namespace Api.Models;

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}

public class ApiResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}