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

        //Liste des fichiers
        List<File> lstFile = new List<File>();

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
        /// Lance la recherche sur le K
        /// </summary>
        public void BeginTheReasearchWord()
        {
            //Liste des mots qui contiennent la recherche
            Dictionary<string, int> lstWord = new Dictionary<string, int>();

            string[] allWords;

            try
            {
                //Pour les fichiers qui contiennent la contrainte dans leur chemin
                string[] files = Directory.GetFiles(@"K:\INF\Eleves\temp", "*", SearchOption.AllDirectories);

                //Pour les fichiers qui possèdent la contrainte dans leur contenu pdf
                string[] filePdf = Directory.GetFiles(@"K:\INF\Eleves\temp", "*" + ".pdf", SearchOption.AllDirectories);

                //Pour les fichiers qui possèdent la contrainte dans leur contenu txt
                string[] fileTxt = Directory.GetFiles(@"K:\INF\Eleves\temp", "*" + ".txt", SearchOption.AllDirectories);

                //Pour les noms de fichiers
                foreach (string s in files)
                {
                    if (!lstWord.ContainsKey(System.IO.Path.GetFileName(s)))
                    {
                        lstWord.Add(System.IO.Path.GetFileName(s), 1);
                    }
                }

                //Pour les fichiers txt qui contiennent la contrainte dans leur contenu
                foreach (string s in fileTxt)
                {
                    string text = System.IO.File.ReadAllText(s);
                    allWords = text.Split(' ');
                    foreach (string word in allWords)
                    {
                        if (word != "" && word.Length > 1 && Regex.IsMatch(word, "[A-Za-z0-9@àäéöèüêçï&]+"))
                        {
                            if (!lstWord.ContainsKey(word))
                            {
                                lstWord.Add(word, 0);
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
                        if(word != "" && word.Length > 1 && Regex.IsMatch(word, "^[A-Za-z0-9@àäéöèüêçï&]+$"))
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

            sendData(lstWord);
        }

        /// <summary>
        /// Envoie les mots trouvés
        /// </summary>
        /// <param name="words"></param>
        private void sendData(Dictionary<string, int> words)
        {
            dbd.getRequest("DELETE FROM `t_mots`;");
            foreach (KeyValuePair<string, int> word in words)
            {
                string theRequest = "INSERT INTO t_mots VALUES (NULL,'" + word.Key + "', '" + word.Value + "');";
                dbd.getRequest(theRequest);
            }
        }
    }
}
