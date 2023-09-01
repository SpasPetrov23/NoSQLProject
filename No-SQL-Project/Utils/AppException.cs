namespace No_SQL_Project.Utils;

public class AppException : Exception
{
    public AppException(string message) : base(message) { }
    
    public AppException(ErrorMessages message) : base(message.ToString()) { }
    
    public AppException(string message, List<string> args) : base(message)
    {
        Args = args;
    }
    
    public AppException(ErrorMessages message, List<string> args) : base(message.ToString())
    {
        Args = args;
    }

    public List<string> Args { get; set; } = new();
}