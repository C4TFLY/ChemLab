using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DBTest : MonoBehaviour {

	void Start () {
        string conn = "URI=file:" + Application.dataPath + "/Assets/Other/Combinations.db";
        IDbConnection dbconn;
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "SELECT atomName, protonAmount, neutronAmount FROM atoms";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            string name = reader.GetString(0);
            int protons = reader.GetInt32(1);
            int neutrons = reader.GetInt32(2);

            //Debug.Log($"<b>Name:</b> {name}\n<b>Protons:</b> {protons}\n<b>Neutrons:</b> {neutrons}");
            Debug.Log(String.Format("<b>Name:</b> {0}\n<b>Protons:</b> {1}<b>Neutrons:</b> {2}", name, protons, neutrons));
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
	}
}
