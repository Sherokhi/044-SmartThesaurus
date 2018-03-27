///ETML
///Auteur : Loic Rosset
///Date : 13.02.2018
///Description : Remplit la base de données par rapport au k
using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace ThesaurusoIndexor
{
    public class FillK
    {
        //Singleton
        private static FillK actualResaerch;
        //Connexion à la base de données
        private DBConnect dbd = new DBConnect();
        //Taille max d'un mot
        const int MAX_SIZE_WORLD = 50;

        //Liste des fichiers et de leurs occurences par mots
        private List<OccFolder> lstOccurence = new List<OccFolder>();

        // Create a reader for the given PDF file
        PdfDocument document;


        private FillK()
        {

        }

        /// <summary>
        /// Création singleton, instance de classe
        /// </summary>
        /// <returns></returns>
        public static FillK CreateResearch()
        {
            if (actualResaerch == null)
            {
                actualResaerch = new FillK();
            }
            return actualResaerch;
        }

        /// <summary>
        /// Lance la recherche sur le K des mots
        /// </summary>
        public void BeginTheReasearchWord()
        {
            //Liste des mots qui contiennent la recherche
            Dictionary<string, int> lstWord = new Dictionary<string, int>();

            //Liste des fichiers qui contiennent la recherche
            List<string> lstFolder = new List<string>();

            string[] allWords;

            try
            {
                //Pour les fichiers qui contiennent la contrainte dans leur chemin
                string[] files = Directory.GetFiles(@"D:\DATA\fichier", "*", SearchOption.AllDirectories);

                //Pour les fichiers qui possèdent la contrainte dans leur contenu pdf
                string[] filePdf = Directory.GetFiles(@"D:\DATA\fichier", "*" + ".pdf", SearchOption.AllDirectories);

                //Pour les fichiers qui possèdent la contrainte dans leur contenu txt
                string[] fileTxt = Directory.GetFiles(@"D:\DATA\fichier", "*" + ".txt", SearchOption.AllDirectories);

                //Pour les noms de fichiers
                foreach (string s in files)
                {
                    //Ajout à la liste occurence
                    lstOccurence.Add(new OccFolder(s));

                    lstFolder.Add(@s);
                }

                //Pour les fichiers txt qui contiennent la contrainte dans leur contenu
                foreach (string s in fileTxt)
                {
                    string text = System.IO.File.ReadAllText(s);
                    allWords = text.Split(' ');
                    foreach (string word in allWords)
                    {
                        if (word != "" && word.Length > 1 && word.Length < MAX_SIZE_WORLD && Regex.IsMatch(word, "^[A-Za-z0-9@àäéöèüêçï&]+$"))
                        {
                            //Si la liste des mots ne contient pas encore le mot
                            if (!lstWord.ContainsKey(word))
                            {
                                lstWord.Add(word, 0);

                                //Pour chaque fichier dans la liste d'occurence
                                foreach (OccFolder fol in lstOccurence)
                                {
                                    //On vérifie si le nom du fichier correspond au fichier actuel
                                    if (fol.folName == s)
                                    {
                                        //On crée le mot
                                        fol.occWord.Add(word, 1);
                                    }
                                }
                            }
                            //Sinon
                            else
                            {
                                //Pour chaque fichier dans la liste d'occurence
                                foreach (OccFolder fol in lstOccurence)
                                {
                                    //On vérifie si le nom du fichier correspond au fichier actuel
                                    if (fol.folName == s)
                                    {
                                        //On incrémente l'occurence du mot
                                        fol.occWord[word]++;
                                    }
                                }
                            }
                        }
                    }

                }

                //Pour les fichiers pdf qui contiennent la contrainte dans leur contenu
                foreach (string s in filePdf)
                {
                    PdfReader reader = new PdfReader(s);
                    string text = "";
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        text += (PdfTextExtractor.GetTextFromPage(reader, i));
                    }
                    text = text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
                    allWords = text.Split(' ');
                    foreach(string word in allWords)
                    {
                        if (word != "" && word.Length > 1 && word.Length < MAX_SIZE_WORLD && Regex.IsMatch(word, "^[A-Za-z0-9@àäéöèüêçï&]+$"))
                        {
                            if (!lstWord.ContainsKey(word))
                            {
                                lstWord.Add(word, 0);
                            }
                        }
                    }

                }
            }
            catch (UnauthorizedAccessException UAEx)
            {

            }
            catch (PathTooLongException PathEx)
            {

            }
            catch
            {

            }


            sendDataFolder(lstFolder);
            sendDataWord(lstWord);
            sendDataOccurence(lstOccurence);
        }

        /// <summary>
        /// Envoie les mots trouvés
        /// </summary>
        /// <param name="words"></param>
        private void sendDataWord(Dictionary<string, int> words)
        {
            dbd.getRequest("DELETE FROM `t_mots`;");
            foreach (KeyValuePair<string, int> word in words)
            {
                string theRequest = "INSERT INTO t_mots VALUES (NULL,'" + word.Key + "', '" + word.Value + "');";
                dbd.getRequest(theRequest);
            }
        }

        /// <summary>
        /// Envoie les fichiers trouvés
        /// </summary>
        /// <param name="folders"></param>
        private void sendDataFolder(List<string> folders)
        {
            dbd.getRequest("DELETE FROM `t_folder`;");
            foreach (string folder in folders)
            {
                string[] pathTab = folder.Split('\\');
                string name = pathTab[pathTab.Length - 1];
                string theRequest = "INSERT INTO t_folder VALUES (NULL,'" + folder.Replace("\\", "\\\\") + "', '" + name + "', NULL , NULL);";
                dbd.getRequest(theRequest);
            }
        }

        /// <summary>
        /// Envoie les fichiers trouvés ainsi que leurs mots dans la table occurence
        /// </summary>
        /// <param name="folders"></param>
        private void sendDataOccurence(List<OccFolder> folders)
        {
            //Liste qui va remplacé le nom des fichiers et des mots par les ID
            List<OccFolder> lstFolderID = new List<OccFolder>();

            dbd.getRequest("DELETE FROM `t_occurencefolder`;");
            foreach (OccFolder folder in folders)
            {
                //Dictionnaire égal à chaque dico des fichiers mais avec les ID
                Dictionary<string, int> dicID = new Dictionary<string, int>();

                foreach (KeyValuePair<string, int> entry in folder.occWord)
                {
                    //Requète pour ceux qui le possède dans leur contenu
                    string selectRequestWord = "SELECT motID FROM t_mots WHERE motContenu = \"" + entry.Key + "\";";

                    
                    dicID.Add(dbd.sendRequest(selectRequestWord, 0)[0], entry.Value);
                }

                //Requète pour ceux qui le possède dans leur contenu
                string selectRequestFolder = "SELECT folID FROM t_folder WHERE folUrl = \"" + folder.folName + "\";";

                //Problème lors de la prise de l'id du fichier par rapport a l'url
                //On récupère l'id du fichier
                lstFolderID.Add(new OccFolder(dbd.sendRequest(selectRequestFolder, 0)[0], dicID));
            }

            foreach(OccFolder folder in lstFolderID)
            {
                foreach(KeyValuePair<string, int> entry in folder.occWord)
                {
                    string theRequest = "INSERT INTO t_occurencefolder VALUES (" + entry.Value + ", " + folder.folName + " , " + entry.Key + ");";
                    dbd.getRequest(theRequest);
                }
            }
        }
    }
}
