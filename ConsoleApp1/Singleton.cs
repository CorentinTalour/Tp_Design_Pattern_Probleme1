namespace ConsoleApp1;

public class ConnectionManager
{
    private static readonly Dictionary<string, IDataSource> _dataSources = new Dictionary<string, IDataSource>();
    private static readonly object Lock = new object();

    private ConnectionManager() { }
    
    public static IDataSource GetConnection(DataSourceFactory factory, string host, int port, string databaseName, string username = null, string password = null, List<string> scripts = null)
    {
        string key = $"{host}-{databaseName}-{port}";
        
        if (_dataSources.ContainsKey(key))
        {
            return _dataSources[key];
        }

        lock (Lock)  //Assurer un accès thread-safe
        {
            if (!_dataSources.ContainsKey(key))
            {
                IDataSource dataSource = factory.CreateDataSource(host, port, databaseName, username, password, scripts);
                _dataSources[key] = dataSource;
                Console.WriteLine("DataSource instance created.");
            }
        }

        return _dataSources[key];
    }
}