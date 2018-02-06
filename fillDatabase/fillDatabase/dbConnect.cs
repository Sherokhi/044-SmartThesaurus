using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Data;

namespace fillDatabase
{
    public class dbConnect
    {
        /// <summary>
        /// Connexion à la base de donnée MySQL
        /// </summary>
        private MySqlConnection _Connection = new MySqlConnection(); //nouvelle connection à MySQL

        /// <summary>
        /// Nom de la base de donnée
        /// </summary>
        private const string _DATABASENAME = "db_thesaurus";

        /// <summary>
        /// Source du réseau externe
        /// </summary>
        private const string _SOURCE = "localhost";

        /// <summary>
        /// Username de l'admin
        /// </summary>
        private const string _ADMINSUSER = "root";

        /// <summary>
        /// Mot de passe de l'admin
        /// </summary>
        private const string _ADMINPASSWORD = "root";


        /// <summary>
        /// Constructeur de classe
        /// </summary>
        public dbConnect()
        {
            ConnectionSQL();
        }

        /// <summary>
        /// Méthode de connexion à la base de donnée
        /// </summary>
        public void ConnectionSQL()
        {
            string _ConnectionString = "Database=" + _DATABASENAME + ";Data Source=" + _SOURCE + ";User Id=" + _ADMINSUSER + ";Password=" + _ADMINPASSWORD;

            _Connection.ConnectionString = _ConnectionString;

            try
            {
                _Connection.Open();
                MessageBox.Show("Connexion établie !");

            }
            catch (ArgumentException e)

            {
                MessageBox.Show("Connexion refusée !" +
                    "erreur : " + e.Message);
            }

        }

        public void CloseConnection()
        {
            if (_Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }
        }

        /// <summary>
        /// Execute la requète donnée
        /// </summary>
        /// <param name="request"></param>
        public void getRequest(string request)
        {
            MySqlCommand mySqlRequest = new MySqlCommand(request, _Connection);
            try
            {
                mySqlRequest.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
