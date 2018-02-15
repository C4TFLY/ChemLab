using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
using System.Linq;

public class Temp : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

        Dictionary<string, int> neutronDict = new Dictionary<string, int>();
        string conn = "URI=file:" + Application.dataPath + "/Other/scraped.db";
        IDbConnection dbconn;
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "SELECT isotope, protons FROM Scraped";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int protons = reader.GetInt32(1);
            string isotope = reader.GetString(0);

            string massNumberString = new String(isotope.Where(c => Char.IsDigit(c)).ToArray());
            int massNumber = Int32.Parse(massNumberString);
            int neutrons = massNumber - protons;

            neutronDict.Add(isotope, neutrons);
            //TODO: Add mass number with isotope to dictionary, then make a new query outside of reader.Read()
        }
        reader.Close();
        reader = null;

        foreach(KeyValuePair<string, int> entry in neutronDict)
        {
            sqlQuery = "UPDATE Scraped SET neutrons = " + entry.Value + " WHERE isotope = '" + entry.Key + "'";
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteNonQuery();
        }


        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
