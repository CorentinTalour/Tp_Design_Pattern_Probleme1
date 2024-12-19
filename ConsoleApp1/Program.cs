using System;
using System.Collections.Generic;
using ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        //Correspond au problème 1
        try
        {
            var mssqlFactory = new MSSQLDataSourceFactory();
            var mssqlDataSource = ConnectionManager.GetConnection(mssqlFactory, "localhost", 1433, "mydb", "user", "password", new List<string> { "CREATE TABLE test;" });
            mssqlDataSource.Open();
            mssqlDataSource.ExecuteQuery("SELECT * FROM test;");
            mssqlDataSource.Close();
            
            var h2Factory = new H2DataSourceFactory();
            var h2DataSource = ConnectionManager.GetConnection(h2Factory, "localhost", 9092, "testdb", null, null, new List<string> { "CREATE TABLE test;" });
            h2DataSource.Open();
            h2DataSource.ExecuteQuery("SELECT * FROM test;");
            h2DataSource.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
