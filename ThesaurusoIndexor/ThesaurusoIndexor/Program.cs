///ETML
///Auteur : Jérémie Perret et Loïc Rosset
///Date : 16.01.2018
///Description : Classe de connection et de gestion SQL
using System;
using System.Windows.Forms;

namespace ThesaurusoIndexor
{
    static class Program
    {
        public static Form1 form1;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form1 = new Form1();
            Application.Run(form1);
        }
    }
}
