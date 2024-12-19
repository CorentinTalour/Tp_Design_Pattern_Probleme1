namespace ConsoleApp1;

public abstract class DataSourceFactory
{
    public abstract IDataSource CreateDataSource(string host, int port, string databaseName, string username = null, string password = null, List<string> scripts = null);
}

public class MSSQLDataSource : IDataSource
{
    private bool isOpen = false;
    private string connectionString;
    private List<string> scriptsToExecute;

    public MSSQLDataSource(string host, int port, string databaseName, string username, string password, List<string> scripts)
    {
        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(databaseName))
            throw new InvalidDataSourceParametersException("Host and DatabaseName must be provided for MSSQL.");
        
        connectionString = $"Server={host},{port};Database={databaseName};User Id={username};Password={password};";
        scriptsToExecute = scripts ?? new List<string>();
    }

    public void Open()
    {
        if (!isOpen)
        {
            foreach (var script in scriptsToExecute)
            {
                Console.WriteLine($"Executing script: {script}");
            }
            isOpen = true;
            Console.WriteLine("Connection to MSSQL opened.");
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            isOpen = false;
            Console.WriteLine("Connection to MSSQL closed.");
        }
    }

    public void ExecuteQuery(string query)
    {
        if (!isOpen)
            throw new NoSessionException("The DataSource is closed. Please open the connection first.");
        
        Console.WriteLine($"Executing query: {query}");
    }
}

public class H2DataSource : IDataSource
{
    private bool isOpen = false;
    private string connectionString;
    private List<string> scriptsToExecute;

    public H2DataSource(string host, int port, string databaseName, List<string> scripts)
    {
        // H2 DataSource connection string building
        connectionString = $"jdbc:h2:{host}:{port}/{databaseName}";
        scriptsToExecute = scripts ?? new List<string>();
    }

    public void Open()
    {
        if (!isOpen)
        {
            foreach (var script in scriptsToExecute)
            {
                Console.WriteLine($"Executing script: {script}");
            }
            isOpen = true;
            Console.WriteLine("Connection to H2 opened.");
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            isOpen = false;
            Console.WriteLine("Connection to H2 closed.");
        }
    }

    public void ExecuteQuery(string query)
    {
        if (!isOpen)
            throw new NoSessionException("The DataSource is closed. Please open the connection first.");

        Console.WriteLine($"Executing query: {query}");
    }
}

public class MSSQLDataSourceFactory : DataSourceFactory
{
    public override IDataSource CreateDataSource(string host, int port, string databaseName, string username = null, string password = null, List<string> scripts = null)
    {
        return new MSSQLDataSource(host, port, databaseName, username, password, scripts);
    }
}

public class H2DataSourceFactory : DataSourceFactory
{
    public override IDataSource CreateDataSource(string host, int port, string databaseName, string username = null, string password = null, List<string> scripts = null)
    {
        return new H2DataSource(host, port, databaseName, scripts);
    }
}