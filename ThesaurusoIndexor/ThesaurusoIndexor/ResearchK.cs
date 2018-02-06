﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static ThesaurusoIndexor.Program;
using ThesaurusoIndexor;
using System.Threading;

namespace algoResearch
{
    public class ResearchK
    {
        private string[] userSearch;
        private static ResearchK actualResaerch;
        private DBConnect dbd = new DBConnect();

        public ResearchK()
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
        public static ResearchK CreateResearch()
        {
            if(actualResaerch == null)
            {
                actualResaerch = new ResearchK();
            }
            return actualResaerch;
        }

        /// <summary>
        /// Lance la recherche sur le K
        /// </summary>
        public void BeginTheReasearch()
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
                try
                {
                    //Pour les fichiers qui contiennent la contrainte dans leur chemin
                    string[] files = Directory.GetFiles(@"K:\INF\Eleves\temp", "*" + str + "*", SearchOption.AllDirectories);

                    //Pour les fichiers qui possèdent la contrainte dans leur contenu
                    string[] allFiles = Directory.GetFiles(@"K:\INF\Eleves\temp", "*", SearchOption.AllDirectories);

                    form1.pbLoad.Maximum = files.Count();
                    form1.btn_Research.Enabled = false;

                    allData += "====================================================";
                    allData += "\r\n";
                    allData += "Fichiers qui possèdent la recherche uniquement dans leur chemin";
                    allData += "\r\n";
                    allData += "====================================================";
                    allData += "\r\n";

                    //Pour les noms de fichiers
                    foreach (string s in files)
                    {

                        //Thread.Sleep(20);
                        counter++;
                        allData += s + "\r\n";
                        if(form1.pbLoad.Value < form1.pbLoad.Maximum)
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

                    foreach (string s in allFiles)
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
                        if(!lstWord.Contains(Path.GetFileName(s)))
                        {
                            lstWord.Add(Path.GetFileName(s));
                        }
                    }

                    form1.rtbResult.Text = allData;
                    form1.rtbResultData.Text = allData2;
                    form1.Cursor = Cursors.Arrow;
                    ////Nombre de fichiers trouvés
                    form1.lblResearchNumber.Text = files.Count().ToString();
                }
                catch (UnauthorizedAccessException UAEx)
                {
                    MessageBox.Show(UAEx.Message);
                }
                catch (PathTooLongException PathEx)
                {
                    MessageBox.Show(PathEx.Message);
                }
                catch
                {

                }
                form1.btn_Research.Enabled = true;

                sendData(lstWord);
            }
        }

        /// <summary>
        /// Envoie les mots trouvés
        /// </summary>
        /// <param name="words"></param>
        public void sendData(List<string> words)
        {
            dbd.getRequest("DELETE FROM t_mots;");
            foreach (string word in words)
            {
                string theRequest = "INSERT INTO t_mots VALUES (NULL,'" + word + "');";
                dbd.getRequest(theRequest);
            }
        }
    }
}

