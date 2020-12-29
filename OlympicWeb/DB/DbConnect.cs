using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace OlympicWeb.DB
{
    public class DBConnect
    {
        private MySqlConnection connection;
        private MySqlDataReader dataReader;
        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {

            string connectionString = "Server=127.0.0.1;Database=olympic_app;User Id=root;Password=6u6fwn8S9";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                return false;

            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }

        }


        //Select statement
        public List<string>[] Select()
        {
            List<string>[] list = new List<string>[2];
            list[0] = new List<string>();
            list[1] = new List<string>();

            var queryString = "SELECT Athlete_Id,Name FROM olympic_app.athletes";
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["Athlete_Id"] + "");
                list[1].Add(dataReader["Name"] + "");
                //list[2].Add(dataReader["age"] + "");
            }
            //close Data Reader
            dataReader.Close();

            return list;

        }

        //filter statement
        public List<string>[] BasicFilter(string table, List<string> atributes)
        {
            List<string>[] list = new List<string>[atributes.Count];
            for (int i = 0; i < atributes.Count; i++)
            {
                list[i] = new List<string>();
            }

            string atributesStr = "";

            foreach (string item in atributes)
            {
                atributesStr += item;
                atributesStr += ",";
            }
            atributesStr = atributesStr.Remove(atributesStr.Length - 1);

            var queryString = "SELECT " + atributesStr + " FROM " + table + ";";
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                for (int i = 0; i < atributes.Count; i++)
                {
                    list[i].Add(dataReader[atributes[i]] + "");
                }
            }
            //close Data Reader
            dataReader.Close();

            return list;

        }


        public List<string>[] FeedPosts()
        {

            List<string>[] posts = new List<string>[2];
            posts[0].Add(TheBestAthlete("Basketball"));
            posts[0].Add(TheBestAthlete("Swimming"));
            return posts;

        }


        //the best athlete in specific sport

        public string TheBestAthlete(string sport)
        {

            var queryString = "SELECT Name FROM olympic_app.athletes WHERE Athlete_Id = (SELECT Athlete_Id FROM (" +
            "SELECT Athlete_Id, COUNT(*) AS magnitude FROM (SELECT Athlete_Id, Medal FROM olympic_app.medals WHERE ((event_id IN " +
            "(SELECT event_id FROM olympic_app.event_types WHERE sport = \"" + sport + "\"" + ")) AND  medal <> \"NA\")) AS temp " +
            "GROUP BY Athlete_Id " +
             "ORDER BY magnitude DESC " +
            "LIMIT 1) AS temp2);";
            string result = "";
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            dataReader = cmd.ExecuteReader();

            //Read the data and store the name in string
            while (dataReader.Read())
            {
                result = dataReader["Name"] + "";

            }
            //close Data Reader
            dataReader.Close();

            return result;

        }

    }
}
