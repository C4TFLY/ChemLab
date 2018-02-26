using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;

public class DBTest : MonoBehaviour {

	void Start () {
        //QueryTest();
	}

    void QueryTest()
    {
        string conn = "URI=file:" + Application.dataPath + "/Other/scraped.db";
        print(conn);
        IDbConnection dbconn;
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "SELECT * FROM Scraped LIMIT 100";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            string name = reader.GetString(0);
            string isotope = reader.GetString(1);
            string chemicalName = reader.GetString(2);
            int protons = reader.GetInt32(3);
            int neutrons = reader.GetInt32(4);
            int massNumber = reader.GetInt32(5);

            Debug.Log("<b>Name:</b> " + name);
            Debug.Log("<b>Isotope:</b> " + isotope);
            Debug.Log("<b>Chemical Name:</b> " + chemicalName);
            Debug.Log("<b>Protons:</b> " + protons);
            Debug.Log("<b>Neutrons:</b> " + neutrons);
            Debug.Log("<b>Massnumber:</b> " + massNumber);
            Debug.Log("--------------------------------------------------");
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
