
///ETML
///Auteur : Jérémie Perret
///Date : 16.01.2018
///Description : Classe de connection et de gestion SQL
using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

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
        private const string _ADMINPASSWORD = "root";


        /// <summary>
        /// Constructeur de classe
        /// </summary>
        public DBConnect()
        {
            this.ConnectionSQL();
        }

        /// <summary>
        /// Méthode de connexion à la base de donnée
        /// </summary>
        public void ConnectionSQL()
        {
            //String de connexion à la base de données
            string _ConnectionString = "Database=" + _DATABASENAME + ";Data Source=" + _SOURCE + ";User Id=" + _ADMINSUSER + ";Password=" + _ADMINPASSWORD;

            _Connection.ConnectionString = _ConnectionString;

            //On essaie d'ouvrir la connexion
            try
            {
                _Connection.Open();
                MessageBox.Show("Connexion établie !");

            }
            catch(ArgumentException e)

            {
                MessageBox.Show("Connexion refusée !" +
                    "erreur : " + e.Message);
            }

        }

        public void CloseConnection()
        {
            if(_Connection.State == ConnectionState.Open)
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
            //Commande mysql
            MySqlCommand mySqlRequest = new MySqlCommand(request, _Connection);
            //Essai de lancer la requête
            mySqlRequest.ExecuteNonQuery();

        }

        /// <summary>
        /// Execute la requète et renvoi un tableau de string
        /// </summary>
        /// <param name="request"></param>
        public string[] sendRequest(string request, int colonne)
        {
            //Commande mysql
            MySqlCommand mySqlRequest = new MySqlCommand(request, _Connection);
            //Datareader qui permet de faire un select
            MySqlDataReader dataReader = mySqlRequest.ExecuteReader();
            //Comptteur pour le tableau
            int compteur = 0;
            //Tableau de string qui va contenir les mots
            string[] tabString = new string[dataReader.FieldCount];


            //Tant qu'on peut lire le reader
            while (dataReader.Read())
            {
                //Sélectionne motcontenu
                tabString[compteur] = dataReader.GetString(colonne);
                compteur++;
            }
            dataReader.Close();
            dataReader.Dispose();
            return tabString;

        }

    }
}
