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

        public ResearchDatabase()
        {

        }

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
        /// 
        /// </summary>
        public void BeginTheResearchFolder()
        {

        }

        /// <summary>
        /// Lance la recherche sur le K pour les mots
        /// </summary>
        public void BeginTheReasearchWord()
        {
            string allData = "";
            string allData2 = "";
            int counter = 0;
            string text;
            //Liste des mots qui contiennent la recherche
            List<string> lstWord = new List<string>();

            form1.pbLoad.Value = 0;

            form1.dgvData.Rows.Clear();

            allData += "====================================================";
            allData += "\r\n";
            allData += "Fichiers qui possèdent la recherche dans leur chemin";
            allData += "\r\n";
            allData += "====================================================";
            allData += "\r\n";

            allData2 += "====================================================";
            allData2 += "\r\n";
            allData2 += "Fichiers qui possèdent la recherche dans leur contenu";
            allData2 += "\r\n";
            allData2 += "====================================================";
            allData2 += "\r\n";

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
                    theRequest2 = "SELECT DISTINCT * FROM t_folder WHERE folName = '" + str + "';";
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
                    counter++;
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
                    if (form1.pbLoad.Value < form1.pbLoad.Maximum)
                    {
                        form1.pbLoad.Value++;
                    }
                    form1.Update();
                    try
                    {
                        text = System.IO.File.ReadAllText(s);
                        //Prend tous les fichiers qui possèdent le string dans leur contenu
                        if (text.Contains(str))
                        {
                            counter++;
                            if(!allData2.Contains(s))
                            {
                                allData2 += s + "\r\n";
                                form1.dgvData.Rows.Add();
                                form1.dgvData[0, i].Value = s;
                                form1.dgvData[1, i].Value = "Contenu";
                                i++;
                            }
                        }
                    }

                    catch
                    {

                    }
                    if (!lstWord.Contains(Path.GetFileName(s)))
                    {
                        lstWord.Add(Path.GetFileName(s));
                    }
                }

                form1.Cursor = Cursors.Arrow;
                ////Nombre de fichiers trouvés
                form1.lblResearchNumber.Text = files.Count().ToString();
                form1.btn_Research.Enabled = true;
            }
        }
    }
}
