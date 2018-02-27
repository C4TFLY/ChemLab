using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class DBConnect {

    SqliteConnection conn;
    string dbLocation;

    public DBConnect(string dbLocation = "/Other/Scraped.db")
    {
        this.dbLocation = dbLocation;

        try
        {
            conn = new SqliteConnection
            {
                ConnectionString = $"URI=file:{Application.dataPath}{this.dbLocation}"
            };
        }
        catch(SqliteException ex)
        {
            Debug.Log(ex);
        }
    }

    public SqliteConnection GetConnection()
    {
        return conn;
    }
}
