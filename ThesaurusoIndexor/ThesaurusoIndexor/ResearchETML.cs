using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace algoResearch
{
    class ResearchETML
    {
        /// <summary>
        /// Dictionnaire pour le résultat des pages
        /// </summary>
        private Dictionary<string, string> PagesResult = new Dictionary<string, string>();

        /// <summary>
        /// URL de départ
        /// </summary>
        string startURL = "https://www.etml.ch/vie-de-lecole/menus-du-restaurant.html";

        /// <summary>
        /// Instance de classe pour le singleton
        /// </summary>
        private static ResearchETML instance;

        /// <summary>
        /// Tableau des liens récupérés sur une page html
        /// </summary>
        string[] links;

        /// <summary>
        /// Liste de tous les liens présents sur un site, toute pages confondues
        /// </summary>
        public List<string> allLinksFinal = new List<string>();

        /// <summary>
        /// Tous les mots contenu sur le site de l'ETML, toutes pages confondues
        /// </summary>
        List<string> allETMLWords = new List<string>();

        /// <summary>
        /// Listes des pages analysées durant le processus de recherche
        /// </summary>
        List<string> pagesChecked = new List<string>();

        /// <summary>
        /// Liste contenant les pages avec des pdf
        /// </summary>
        List<string> pdfETML = new List<string>();

        Random r = new Random();
        ConsoleColor actualColor = new ConsoleColor();
        ConsoleColor lastColor = new ConsoleColor();

        /// <summary>
        /// Constructeur de classe
        /// </summary>
        public ResearchETML()
        {

        }

        /// <summary>
        /// Retourne l'instanc de classe
        /// </summary>
        /// <returns>Instance de classe</returns>
        public ResearchETML getInstance()
        {
            if (instance == null)
            {
                instance = new ResearchETML();
            }
            return instance;
        }

        /// <summary>
        /// Démarre l'algorythme de recherche
        /// </summary>
        /// <param name="textToSearch"></param>
        /// <returns></returns>
        public string Start(string textToSearch)
        {
            return string.Empty;
        }

        /// <summary>
        /// Récupère le contenu d'une page html
        /// </summary>
        /// <param name="url">URL de la page cible</param>
        /// <returns>Le contenu de la page en un chaîne de caractère</returns>
        public string getTextinHTML(string url)
        {
            var html = "";
            try
            {
                //Téléchargement du code source de la page html
                html = new WebClient().DownloadString(url);

                //Regex pour détecter les balises script et style avec leur contenu
                var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                html = regexCss.Replace(html, string.Empty);

                Regex regEx = new Regex("<[^>]*>", RegexOptions.IgnoreCase);
                html = regEx.Replace(html, String.Empty);

                html = Regex.Replace(html, @"\n", String.Empty);
                html = Regex.Replace(html, @"\r", " ");
                html = Regex.Replace(html, @"\t", " ");

                //Suppression des balises commentaires
                html = Regex.Replace(html, @"^((\<!--\s*.*?((--\>)|$))|\\n|<.*?>)", String.Empty);

                //Conversion en bytes du texte ainsi obtenu
                byte[] bytes = Encoding.Default.GetBytes(html);

                //Reconversion en UTF-8
                html = Encoding.UTF8.GetString(bytes);

                //Suppression des espaces en trop
                html = Regex.Replace(html, @"[ ]{2,}", " ");
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //Thread.Sleep(10000);
            }
            return html;
        }

        /// <summary>
        /// Recherche tous les liens des pages domaines et les renvoies.
        /// </summary>
        /// <param name="url">Page HTML cible</param>
        /// <returns>tous les liens domaines</returns>
        public List<string> getLinks(string url)
        {
            List<string> finalLinks = new List<string>();
            string htmlCode = "";
            try
            {
                //télécharge le code source de la page
                htmlCode = new WebClient().DownloadString(url);

                //Récupère tous les liens d'une page
                var linkTags = Regex.Matches(htmlCode, "<a\\s*(.*)>(.*)</a>", RegexOptions.Multiline);
                object[] links = new object[linkTags.Count];
                linkTags.CopyTo(links, 0);

                //Récupération des liens avec le bon format
                for (int i = 0; i < links.Length; i++)
                {
                    links[i] = Regex.Replace(Regex.Match(links[i].ToString(), "\\s*href\\s*=\\s*(\"([^\"]*\")|'[^']*'|([^'\">\\s]+))").ToString(), "href=\"", String.Empty);
                    links[i] = Regex.Replace(links[i].ToString(), "\\s", String.Empty);
                    links[i] = Regex.Replace(links[i].ToString(), "\"", String.Empty);
                }

                //Nouveau tableau qui va contenir tout les liens de cette pages
                string[] allLinks = new string[links.Length];

                //Remplissae de ce tableau
                for (int i = 0; i < links.Length; i++)
                {
                    allLinks[i] = links[i].ToString();
                }

                //Nombre de liens 
                int nbrEtmlLinks = 0;

                foreach (string link in allLinks)
                {
                    //Si le lien continue sur le même domaine
                    if (link[0] == '/' && link.Length > 2)
                    {
                        //Si le lien contient ".pdf"
                        if (link.Contains(".pdf"))
                        {
                            pdfETML.Add(link);
                        }
                        else
                        {
                            //Si c'est un lien qui n'est pas une image 
                            if (!link.Contains(".img"))
                            {
                                //Ajout à la liste des liens trouvé sur la page
                                finalLinks.Add(link);

                                //Si lien pas dans la liste --> ajout
                                if (IsNotIn(allLinksFinal, link))
                                {
                                    allLinksFinal.Add(link);
                                }
                                nbrEtmlLinks++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //Thread.Sleep(1000);
            }
            return finalLinks;
        }

        /// <summary>
        /// Vérifie si un élément ne fait pas partie d'une list
        /// </summary>
        /// <param name="list">Liste de tous les éléments</param>
        /// <param name="elementToCheck">Elément cible pour la recherche</param>
        /// <returns>true si inexistant dans la liste</returns>
        public bool IsNotIn(List<string> list, string elementToCheck)
        {
            return !list.Contains(elementToCheck);
        }

        /// <summary>
        /// Sépare et affiche tous les mots d'une page html
        /// </summary>
        /// <param name="url">URL de la page cible</param>
        public void RecoverAllWords(string url)
        {
            string newURL = "";
            while (actualColor == lastColor || actualColor == ConsoleColor.Black)
            {
                actualColor = (ConsoleColor)(r.Next(15));
            }
            lastColor = actualColor;
            Console.ForegroundColor = actualColor;
            Console.WriteLine(newURL + "> start\nNouveaux mots trouvés :");
            //Pour chaque mots trouvé sur la page
            foreach (string word in getTextinHTML(url).Split(' '))
            {
                //Si le mot n'est pas dans la liste --> ajout
                if (IsNotIn(allETMLWords, word))
                {
                    allETMLWords.Add(word);

                    //Output console
                    Console.WriteLine(word);
                }
            }


            if (url.Contains("https://www.etml.ch"))
            {
                newURL = url.Remove(0, 19);
            }
            if (newURL != "")
            {
                pagesChecked.Add(newURL);
                Console.WriteLine(newURL + "> end");
            }

            foreach (string link in getLinks(url))
            {
                if (IsNotIn(pagesChecked, link))
                {
                    Console.WriteLine(link);
                    RecoverAllWords("https://www.etml.ch" + link);
                }
            }
        }


        private bool RemoteFileExists(string url)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
                request.Method = "HEAD"; //Get only the header information -- no need to download any content

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                int statusCode = (int)response.StatusCode;
                if (statusCode >= 100 && statusCode < 400) //Good requests
                {
                    return true;
                }
                else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                {
                    return false;
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public void PrintAllWords()
        {
            Console.Clear();
            Console.WriteLine("Mots trouvés sur le site de l'ETML :");
            foreach (string word in allETMLWords)
            {
                Console.WriteLine(word);
            }
        }

        public void ReadAllPDF()
        {
            foreach (string fileLink in pdfETML)
            {
                WebClient wc = new WebClient();
                wc.DownloadFile("https://www.etml.ch" + fileLink, fileLink.Split('/')[fileLink.Split('/').Length - 1]);
            }
        }

    }
}
