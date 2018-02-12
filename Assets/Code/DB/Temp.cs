using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
using System.Linq;

public class Temp : MonoBehaviour {

    Dictionary<string, int> neutrons;

	// Use this for initialization
	void Start () {
        neutrons = new Dictionary<string, int>();
        string conn = "URI=file:" + Application.dataPath + "/Other/scraped.db";
        IDbConnection dbconn;
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "SELECT name, isotope, protons FROM Scraped";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            int protons = reader.GetInt32(2);
            string name = reader.GetString(0);
            string isotope = reader.GetString(1);

            string massNumberString = new String(isotope.Where(c => Char.IsDigit(c)).ToArray());
            int massNumber = Int32.Parse(massNumberString);
            int neutrons = massNumber - protons;


            asdasd

            //TODO: Add mass number with isotope to dictionary, then make a new query outside of reader.Read()
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
