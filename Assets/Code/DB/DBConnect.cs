using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class DBConnect {

    IDbConnection conn;
    string dbLocation;

    public DBConnect(string dbLocation = "/Other/Scraped.db")
    {
        this.dbLocation = dbLocation;

        try
        {
            conn = new SqliteConnection();
            conn.ConnectionString = $"URI=file:{Application.dataPath}{this.dbLocation}";
        }
        catch(SqliteException ex)
        {
            Debug.Log(ex);
        }
    }

    public IDbConnection GetConnection()
    {
        return conn;
    }
}
