using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace algoResearch
{
    class ResearchETML
    {
        private Dictionary<string, string> PagesResult = new Dictionary<string, string>();
        string startURL = "https://www.etml.ch/vie-de-lecole/menus-du-restaurant.html";
        string[] links;
        public List<string> allLinksFinal = new List<string>();
        List<string> allETMLWords = new List<string>();
        List<string> pagesChecked = new List<string>();
<<<<<<< HEAD

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
=======
>>>>>>> 1a9eb25f5f46de42b5bcd8c2e6090db10d5b3823
        public ResearchETML()
        {

        }

<<<<<<< HEAD
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
=======
>>>>>>> 1a9eb25f5f46de42b5bcd8c2e6090db10d5b3823
        public string Start(string textToSearch)
        {
            return string.Empty;
        }

        public string getTextinHTML(string url)
        {
<<<<<<< HEAD
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

                //reconversion en UTF-8
                html = Encoding.UTF8.GetString(bytes);

                //Suppression des espaces en trop
                html = Regex.Replace(html, @"[ ]{2,}", " ");
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //Thread.Sleep(10000);
            }
=======
            var html = new WebClient().DownloadString(url);
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
>>>>>>> 1a9eb25f5f46de42b5bcd8c2e6090db10d5b3823
            return html;
        }

        /// <summary>
        /// Recherche tous les liens des pages domaines et les renvoies.
        /// </summary>
        /// <param name="url">Page HTML cible</param>
        /// <returns>tous les liens domaines</returns>
        public List<string> getLinks(string url)
        {
<<<<<<< HEAD
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
                    if(link[0] == '/' && link.Length > 2)
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
=======
            string htmlCode = new WebClient().DownloadString(url);
            var linkTags = Regex.Matches(htmlCode, "<a\\s*(.*)>(.*)</a>", RegexOptions.Multiline);
            object[] links = new object[linkTags.Count];
            linkTags.CopyTo(links, 0);

            for (int i = 0; i < links.Length; i++)
            {
                links[i] = Regex.Replace(Regex.Match(links[i].ToString(), "\\s*href\\s*=\\s*(\"([^\"]*\")|'[^']*'|([^'\">\\s]+))").ToString(), "href=\"", String.Empty);
                links[i] = Regex.Replace(links[i].ToString(), "\\s", String.Empty);
                links[i] = Regex.Replace(links[i].ToString(), "\"", String.Empty);
            }

            List<string> finalLinks = new List<string>();
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
>>>>>>> 1a9eb25f5f46de42b5bcd8c2e6090db10d5b3823
                    }
                    nbrEtmlLinks++;
                }
            }
<<<<<<< HEAD
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //Thread.Sleep(1000);
            }
=======

>>>>>>> 1a9eb25f5f46de42b5bcd8c2e6090db10d5b3823
            return finalLinks;
        }

        public bool IsNotIn(List<string> list, string elementToCheck)
        {
            return !list.Contains(elementToCheck);
        }

        public void RecoverAllWords(string url)
        {
<<<<<<< HEAD
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
=======
            foreach (string word in getTextinHTML(url).Split())
>>>>>>> 1a9eb25f5f46de42b5bcd8c2e6090db10d5b3823
            {
                //Si le mot n'est pas dans la liste --> ajout
                if (IsNotIn(allETMLWords, word))
                {
                    allETMLWords.Add(word);

                    //Output console
                    Console.WriteLine(word);
                }
            }

<<<<<<< HEAD

            if (url.Contains("https://www.etml.ch"))
            {
                newURL = url.Remove(0, 19);
            }
            if (newURL != "")
            {
                pagesChecked.Add(newURL);
                Console.WriteLine(newURL + "> end");
            }
=======
            pagesChecked.Add(url);
            Console.WriteLine(url);
>>>>>>> 1a9eb25f5f46de42b5bcd8c2e6090db10d5b3823

            foreach (string link in getLinks(url))
            {
                if (IsNotIn(pagesChecked, link))
                {
                    Console.WriteLine(link);
                    RecoverAllWords("https://www.etml.ch" + link);
                }
            }
        }
<<<<<<< HEAD


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
            foreach(string fileLink in pdfETML)
            {
                WebClient wc = new WebClient();
                wc.DownloadFile("https://www.etml.ch" + fileLink, fileLink.Split('/')[fileLink.Split('/').Length - 1]);
            }
        }

=======
>>>>>>> 1a9eb25f5f46de42b5bcd8c2e6090db10d5b3823
    }
}
