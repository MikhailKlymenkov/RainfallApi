namespace RainfallApi.Model.Errors;

public class Error
{
    public Error(string message)
    {
        Message = message;
    }

    public string Message { get; set; }

    public List<ErrorDetail>? Detail { get; set; }
}
