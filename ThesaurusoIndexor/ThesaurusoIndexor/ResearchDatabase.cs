///ETML
///Auteur : Loïc Rosset
///Date : 27.02.2018
///Description : Classe qui va recherchée les mots dans la base de données et l'affiche sur le programme
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
            userSearch = strResearch.Split(' ');
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
            List<string> lstHaveText = new List<string>();
            List<string> lstText = new List<string>();
            //Liste des mots qui contiennent la recherche
            List<string> lstWord = new List<string>();

            form1.rtbResult.Text = "";
            form1.rtbResultData.Text = "";
            form1.pbLoad.Value = 0;
            foreach (string str in userSearch)
            {
                //Requète pour ceux qui le possède dans leur contenu
                string theRequest = "SELECT t_folder.* FROM t_occurencefolder, t_folder, t_mots WHERE t_occurencefolder.folID = t_folder.folID AND t_occurencefolder.motID = t_mots.motID AND t_mots.motIsTitle = false;";
                //On récupère le contenu du mot
                string[] files = dbd.sendRequest(theRequest, 1);

                //Requète pour ceux qui le possède dans leur nom de fichier
                string theRequest2 = "SELECT t_folder.* FROM t_occurencefolder, t_folder, t_mots WHERE t_occurencefolder.folID = t_folder.folID AND t_occurencefolder.motID = t_mots.motID AND t_mots.motIsTitle = true;";
                //On récupère aussi le bool qui dit si c'est un nom de fichiers
                string[] filesTitle = dbd.sendRequest(theRequest2, 2);

                form1.pbLoad.Maximum = files.Count();
                form1.btn_Research.Enabled = false;

                allData += "====================================================";
                allData += "\r\n";
                allData += "Fichiers qui possèdent la recherche uniquement dans leur chemin";
                allData += "\r\n";
                allData += "====================================================";
                allData += "\r\n";

                //Pour les noms de fichiers
                foreach (string s in filesTitle)
                {
                    counter++;
                    if (s != null)
                    {
                        if (!allData.Contains(s))
                        {
                            allData += s + "\r\n";
                        }
                    }

                    if (form1.pbLoad.Value < form1.pbLoad.Maximum)
                    {
                        form1.pbLoad.Value++;
                    }
                    form1.Update();
                    form1.lblResearchNumber.Text = counter.ToString();

                    lstWord.Add(Path.GetFileName(s));
                }

                //Pour les fichiers qui contiennent la contrainte dans leur contenu
                allData2 += "====================================================";
                allData2 += "\r\n";
                allData2 += "Fichiers qui possèdent la recherche uniquement dans leur contenu";
                allData2 += "\r\n";
                allData2 += "====================================================";
                allData2 += "\r\n";

                foreach (string s in files)
                {
                    try
                    {
                        text = System.IO.File.ReadAllText(s);
                        //Prend tous les fichiers qui possèdent le string dans leur contenu
                        if (text.Contains(str))
                        {
                            //Contient le lien du fichier
                            lstHaveText.Add(s);
                            //Contient le contenu du fichier
                            lstText.Add(text);

                            counter++;
                            allData2 += s + "\r\n";
                            if (form1.pbLoad.Value < form1.pbLoad.Maximum)
                            {
                                form1.pbLoad.Value++;
                            }
                            form1.Update();
                            form1.lblResearchNumber.Text = counter.ToString();
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

                form1.rtbResult.Text = allData;
                form1.rtbResultData.Text = allData2;
                form1.Cursor = Cursors.Arrow;
                ////Nombre de fichiers trouvés
                form1.lblResearchNumber.Text = files.Count().ToString();
                form1.btn_Research.Enabled = true;
            }
        }
    }
}
