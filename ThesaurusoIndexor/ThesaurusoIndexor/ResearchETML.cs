using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace algoResearch
{
    class ResearchETML
    {
        /// <summary>
        /// Dictionnaire pour le résultat des pages
        /// </summary>
        private Dictionary<string, string> PagesResult = new Dictionary<string, string>();

        /// <summary>
        /// URL de 
        /// </summary>
        string startURL = "https://www.etml.ch/vie-de-lecole/menus-du-restaurant.html";

        /// <summary>
        /// instance de classe pour le singleton
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
            string research = String.Empty;
            string[] keyWords = textToSearch.Split(' ');
            return research;
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
                html = new WebClient().DownloadString(url);
                //string htmlTagPattern = "<.*?>";
                var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                html = regexCss.Replace(html, string.Empty);
                //html = Regex.Replace(html, htmlTagPattern, string.Empty);
                //html = Regex.Replace(html, @"^\s+$[\r\n]*", String.Empty, RegexOptions.Multiline);
                html = html.Replace("&nbsp;", string.Empty);
                Regex regEx = new Regex("<[^>]*>", RegexOptions.IgnoreCase);
                html = regEx.Replace(html, String.Empty);
                //html = Regex.Replace(html, @"(\<!--\s*.*?((--\>)|$))", String.Empty);
                html = Regex.Replace(html, @"\n", String.Empty);
                html = Regex.Replace(html, @"\r", " ");
                html = Regex.Replace(html, @"\t", " ");
                //html = Regex.Replace(html, @"&copy;", String.Empty);
                //\s*href\s *=\s*(\"([^"]*\")|'[^']*'|([^'">\s]+))
                //<a([^>]?)>(.?)<\/a>
                html = Regex.Replace(html, @"^((\<!--\s*.*?((--\>)|$))|\\n|<.*?>)", String.Empty);
                byte[] bytes = Encoding.Default.GetBytes(html);
                html = Encoding.UTF8.GetString(bytes);
                html = Regex.Replace(html, @"[ ]{2,}", " ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Thread.Sleep(10000);
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

                for (int i = 0; i < links.Length; i++)
                {
                    links[i] = Regex.Replace(Regex.Match(links[i].ToString(), "\\s*href\\s*=\\s*(\"([^\"]*\")|'[^']*'|([^'\">\\s]+))").ToString(), "href=\"", String.Empty);
                    links[i] = Regex.Replace(links[i].ToString(), "\\s", String.Empty);
                    links[i] = Regex.Replace(links[i].ToString(), "\"", String.Empty);
                }


                string[] allLinks = new string[links.Length];

                for (int i = 0; i < links.Length; i++)
                {
                    allLinks[i] = links[i].ToString();
                }

                int nbrEtmlLinks = 0;
                foreach (string link in allLinks)
                {
                    if (link[0] == '/' && link.Length > 2)
                    {
                        finalLinks.Add(link);
                        if (IsNotIn(allLinksFinal, link))
                        {
                            allLinksFinal.Add(link);
                        }
                        nbrEtmlLinks++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Thread.Sleep(10000);
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
            foreach (string word in getTextinHTML(url).Split(' '))
            {
                if (IsNotIn(allETMLWords, word))
                {
                    allETMLWords.Add(word);
                    Console.WriteLine(word);
                }
            }

            if(url == "https://www.etml.ch")
            {
                //url += "/index.php";
            }
            if (url.Contains("https://www.etml.ch"))
            {
                newURL =url.Remove(0, 19);
            }
            if (newURL != "")
            {
                pagesChecked.Add(newURL);
            }
            Console.WriteLine(newURL);

            foreach (string link in getLinks(url))
            {
                if (IsNotIn(pagesChecked, link))
                {
                    Console.WriteLine(link);
                    RecoverAllWords("https://www.etml.ch" + link);
                }
            }
        }

    }
}
