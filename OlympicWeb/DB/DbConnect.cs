using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;
using OlympicWeb.Models;
using System.Security.Cryptography;

namespace OlympicWeb.DB
{
    public class DBConnect
    {
        private MySqlConnection connection;

        //Initialize values
        public bool OpenConnection()
        {
            string connectionString = "Server=127.0.0.1;Database=olympicapp;User Id=root;Password=2691995";
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException)
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
            catch (MySqlException)
            {
                return false;
            }

        }

        //getter connection
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }

}
