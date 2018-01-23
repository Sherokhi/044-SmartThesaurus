///ETML
///Auteur : Jérémie Perret
///Date : 16.01.2018
///Description : Classe de connection et de gestion SQL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Threading;

namespace ThesaurusoIndexor
{
    public class DBConnect
    {
        /// <summary>
        /// Connexion à la base de donnée MySQL
        /// </summary>
        private MySqlConnection _Connection = new MySqlConnection(); //nouvelle connection à MySQL

        /// <summary>
        /// Nom de la base de donnée
        /// </summary>
        private const string _DATABASENAME ="db_thesaurus";

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
        private const string _ADMINPASSWORD = "";


        /// <summary>
        /// Constructeur de classe
        /// </summary>
        public DBConnect()
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
            catch(ArgumentException e)

            {
                MessageBox.Show("Connexion refusée !" +
                    "erreur : " + e.Message());
            }



            Thread.Sleep(1000);
            _Connection.Close();

            MessageBox.Show("Connexion fermé !");
        }

    }
}
