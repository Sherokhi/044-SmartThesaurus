﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace ThesaurusoIndexor
{
    public class FillK
    {
        private string[] userSearch;
        private static FillK actualResaerch;
        private DBConnect dbd = new DBConnect();
        string text = "";

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
        public void BeginTheReasearch()
        {
            //Liste des mots qui contiennent la recherche
            List<string> lstWord = new List<string>();
            string[] allWords;

            try
            {
                //Pour les fichiers qui contiennent la contrainte dans leur chemin
                string[] files = Directory.GetFiles(@"K:\INF\Eleves\temp\fichier", "*", SearchOption.AllDirectories);

                //Pour les fichiers qui possèdent la contrainte dans leur contenu pdf
                string[] filePdf = Directory.GetFiles(@"K:\INF\Eleves\temp\fichier", "*" + ".pdf", SearchOption.AllDirectories);

                //Pour les fichiers qui possèdent la contrainte dans leur contenu txt
                string[] fileTxt = Directory.GetFiles(@"K:\INF\Eleves\temp\fichier", "*" + ".txt", SearchOption.AllDirectories);

                //Pour les noms de fichiers
                foreach (string s in files)
                {
                    lstWord.Add(System.IO.Path.GetFileName(s));
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
                            lstWord.Add(word);
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
                        if(word != "" && word.Length > 1 && Regex.IsMatch(word, "[A-Za-z0-9@àäéöèüêçï&]+"))
                        {
                            lstWord.Add(word);
                        }
                    }

                }

                //Pour les fichiers qui contiennent la contrainte dans leur contenu
                foreach (string s in fileTxt)
                {
                    

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
        private void sendData(List<string> words)
        {
            dbd.getRequest("DELETE FROM `t_mots`;");
            foreach (string word in words)
            {
                string theRequest = "INSERT INTO t_mots VALUES (NULL,'" + word + "');";
                dbd.getRequest(theRequest);
            }
        }
    }
}
