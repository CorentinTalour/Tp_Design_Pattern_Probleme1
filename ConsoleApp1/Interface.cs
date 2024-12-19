namespace ConsoleApp1;

public interface IDataSource
{
    void Open();
    void Close();
    void ExecuteQuery(string query);
}