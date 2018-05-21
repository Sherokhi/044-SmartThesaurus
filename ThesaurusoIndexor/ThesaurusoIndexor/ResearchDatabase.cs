///ETML
///Auteur : Loïc Rosset
///Date : 27.02.2018
///Description : Classe qui va recherchée les mots dans la base de données et l'affiche sur le programme
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static ThesaurusoIndexor.Program;

namespace ThesaurusoIndexor
{
    public class ResearchDatabase
    {
        private string[] userSearch;
        private static ResearchDatabase actualResaerch;
        private DBConnect dbd = new DBConnect();

        /// <summary>
        /// Initialise la recherche de l'utilisateur
        /// </summary>
        /// <param name="strResearch"></param>
        public void GetResearch(string strResearch)
        {
            //Si la personne veut faire une recherche avec des espaces
            if (strResearch.Contains("\""))
            {
                userSearch = strResearch.Split('"');
            }
            else
            {
                userSearch = strResearch.Split(' ');
            }
        }

        /// <summary>
        /// Création singleton, instance de classe
        /// </summary>
        /// <returns></returns>
        public static ResearchDatabase CreateResearch()
        {
            if (actualResaerch == null)
            {
                actualResaerch = new ResearchDatabase();
            }
            return actualResaerch;
        }

        /// <summary>
        /// Lance la recherche sur le K
        /// </summary>
        public void BeginTheReasearchWord()
        {
            string allData = "";
            //Liste des mots qui contiennent la recherche
            List<string> lstWord = new List<string>();

            //Réinitilisation de la barre de rechrche
            form1.pbLoad.Value = 0;

            //Effacement du datagridview
            form1.dgvData.Rows.Clear();

            foreach (string str in userSearch)
            {
                string theRequest = "";
                string theRequest2 = "";
                
                if (form1.txb_Research.Text == "")
                {
                    //Requète pour ceux qui le possède dans leur contenu
                    theRequest2 = "SELECT DISTINCT * FROM t_folder;";
                }
                else
                {
                    //Requète pour ceux qui le possède dans leur contenu
                    theRequest = "SELECT DISTINCT t_folder.* FROM t_occurencefolder, t_folder, t_mots WHERE t_mots.motContenu = '" + str + "' AND t_occurencefolder.folID = t_folder.folID AND t_occurencefolder.motID = t_mots.motID AND t_mots.motIsTitle = false ORDER BY t_occurencefolder.occNumber ASC ;";
                    //Requète pour ceux qui le possède dans leur nom de fichier
                    theRequest2 = "SELECT DISTINCT * FROM t_folder WHERE folName LIKE '" + str + ".%' OR folName = '" + str + "' ;";
                }
                //On récupère le contenu du mot
                List<string> files = dbd.sendRequest(theRequest, 1);
                //On récupère aussi le bool qui dit si c'est un nom de fichiers
                List<string> filesTitle = dbd.sendRequest(theRequest2, 1);

                form1.pbLoad.Maximum = files.Count() + filesTitle.Count();
                form1.btn_Research.Enabled = false;

                int i = 0;

                //Pour les noms de fichiers
                foreach (string s in filesTitle)
                {

                    if (s != null)
                    {
                        if (!allData.Contains(s))
                        {
                            allData += s + "\r\n";
                            form1.dgvData.Rows.Add();
                            form1.dgvData[0, i].Value = s;
                            form1.dgvData[1, i].Value = "Nom";
                            i++;
                        }
                    }

                    if (form1.pbLoad.Value < form1.pbLoad.Maximum)
                    {
                        form1.pbLoad.Value++;
                    }
                    form1.Update();

                    lstWord.Add(Path.GetFileName(s));
                }

                //Pour les fichiers qui contiennent la contrainte dans leur contenu
                foreach (string s in files)
                {
                    if (s != null)
                    {
                        if (!allData.Contains(s))
                        {
                            allData += s + "\r\n";
                            form1.dgvData.Rows.Add();
                            form1.dgvData[0, i].Value = s;
                            form1.dgvData[1, i].Value = "Contenu";
                            i++;
                        }
                    }
                    if (form1.pbLoad.Value < form1.pbLoad.Maximum)
                    {
                        form1.pbLoad.Value++;
                    }
                    form1.Update();

                    if (!lstWord.Contains(Path.GetFileName(s)))
                    {
                        lstWord.Add(Path.GetFileName(s));
                    }
                }

                form1.Cursor = Cursors.Arrow;
                ////Nombre de fichiers trouvés
                form1.lblResearchNumber.Text = form1.pbLoad.Value.ToString();
                form1.btn_Research.Enabled = true;
            }
        }
    }
}
