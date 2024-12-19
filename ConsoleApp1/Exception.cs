namespace ConsoleApp1;

public class InvalidDataSourceParametersException : Exception
{
    public InvalidDataSourceParametersException(string message) : base(message) { }
}

public class NoSessionException : Exception
{
    public NoSessionException(string message) : base(message) { }
}