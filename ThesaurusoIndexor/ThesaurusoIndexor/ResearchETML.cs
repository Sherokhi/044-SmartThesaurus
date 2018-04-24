using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using ThesaurusoIndexor;
using System.Windows.Forms;

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
        string startURL = "https://www.etml.ch/index.php";

        /// <summary>
        /// Instance de classe pour le singleton
        /// </summary>
        private static ResearchETML instance;

        /// <summary>
        /// Liste de tous les liens présents sur un site, toute pages confondues
        /// </summary>
        public List<string> allLinksFinal = new List<string>();

        /// <summary>
        /// Tous les mots contenu sur le site de l'ETML, toutes pages confondues
        /// </summary>
        private List<wordETML> allETMLWords = new List<wordETML>();

        /// <summary>
        /// Listes des pages analysées durant le processus de recherche
        /// </summary>
        private List<string> pagesChecked = new List<string>();

        /// <summary>
        /// Liste contenant les pages avec des pdf
        /// </summary>
        private List<string> pdfETML = new List<string>();

        private Random r = new Random();

        private DBConnect bdd;

        /// <summary>
        /// Constructeur de classe
        /// </summary>
        public ResearchETML()
        {
            this.bdd = new DBConnect();
        }

        /// <summary>
        /// Retourne l'instanc de classe
        /// </summary>
        /// <returns>Instance de classe</returns>
        public static ResearchETML getInstance()
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
            SyncBddData();
            //PrintAllWords();
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
                html = Regex.Replace(html, "[,.;:()+-?`^{}[]]{1,}", String.Empty);
                html = Regex.Replace(html, "[\'\"]", " ");
                html = Regex.Replace(html, "&eacute", "é");
                html = Regex.Replace(html, "&amp", String.Empty);
                html = Regex.Replace(html, "&copy", String.Empty);
                html = Regex.Replace(html, "&nbsp", " ");
                html = Regex.Replace(html, "&agrave", "à");
                html = Regex.Replace(html, "[\\d]", String.Empty);
                html = Regex.Replace(html, "|", String.Empty);
                //html = Regex.Replace(html, "?", "é");
                //Suppression des balises commentaires
                html = Regex.Replace(html, @"^((\<!--\s*.*?((--\>)|$))|\\n|<.*?>)", String.Empty);

                //Conversion en bytes du texte ainsi obtenu
                byte[] bytes = Encoding.Default.GetBytes(html);

                //Reconversion en UTF-8
                html = Encoding.UTF8.GetString(bytes);

                //Suppression des espaces en trop
                while(Regex.IsMatch(html, @"[\s]{2,}"))
                {
                    html = Regex.Replace(html, @"[\s]{2,}", " ");
                }
                html = html.ToLower();
            }
            catch (Exception e)
            {
                pagesChecked.Add(url);
                //MessageBox.Show("Une erreur s'est produite : " + e.Message);
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
                                    NewWebPage(link);
                                }
                                nbrEtmlLinks++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (IsNotIn(pagesChecked, url))
                    pagesChecked.Add(url);
                int rroor = e.Data.Count;
                //MessageBox.Show("Une erreur s'est produite : " + e.Message);
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

        public bool IsNotIn(List<wordETML> list, wordETML elementToCheck)
        {
            return !list.Contains(elementToCheck);
        }
	
        /// <summary>
        /// Sépare et affiche tous les mots d'une page html
        /// </summary>
        /// <param name="url">URL de la page cible</param>
        /// <param name="url">URL de la page cible</param>
        public void RecoverAllWords(string url)
        {
            string newURL = "";
            if (IsNotIn(allLinksFinal, url))
            {
                NewWebPage(url);
                allLinksFinal.Add(url);
            }
            //Pour chaque mots trouvé sur la page
            foreach (string word in getTextinHTML(url).Split(' '))
            {
                string query;
                wordETML newWord = new wordETML(url, word);
                //Si le mot n'est pas dans la liste --> ajout
                if(word.Contains(" "))
                {
                    word.Remove(' ');
                }
                if(word.Length > 1)
                {
                    if (IsNotIn(allETMLWords, newWord))
                    {
                        query = "INSERT INTO `t_mots` (`motContenu`, `motIsFromETML`) VALUES ('" + word + "', 1);";
                        bdd.getRequest(query);
                        NewWordOccurence(word, url);
                        allETMLWords.Add(newWord);
                    }
                    else
                    {
                        addOccurence(word);
                    }
                }
            }
            //Si l'url contient
            if (url.Contains("https://www.etml.ch"))
            {
                newURL = url.Remove(0, 19);
            }

            //Si l'url n'est pas vide
            if (newURL != "")
            {
                pagesChecked.Add(newURL);

            }
            //Pour tous les liens dans la liste
            foreach (string link in getLinks(url))
            {
                int finished = pagesChecked.Count * 100 / allLinksFinal.Count;
                if(finished <= 100)
                {
                    Console.Clear();
                    Console.WriteLine("{0}% terminé", finished);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("99% terminé, veuillez patientez !");
                }

                //Si le lien n'a pas encore été checké
                if (IsNotIn(pagesChecked, link))
                {
                    //Relancer une recherche avec ce lien
                    RecoverAllWords("https://www.etml.ch" + link);

                }
            }
        }

        private void NewWordOccurence(string word, string url)
        {
            string query = "";
            query = "SELECT `motID` FROM `t_mots` WHERE `motContenu` = '" + word + "'";
            Console.WriteLine(query);
            List<string> idResults = bdd.sendRequest(query, 0);
            query = "INSERT INTO t_occurenceweb (occNumber, fkWebURL, motID) VALUES (1, '"+url+"', "+idResults[0]+")";
            bdd.getRequest(query);
        }

        private void NewWebPage(string url)
        {
            string query = "INSERT INTO t_web (webURL) VALUES ('" + url + "')";
            bdd.getRequest(query);
        }

        private void addOccurence(string word)
        {
            string query = "";
            query = "SELECT `motID` FROM `t_mots` WHERE `motContenu` = '" + word + "'";
            List<string> idResults = bdd.sendRequest(query, 0);
            query = "SET FOREIGN_KEY_CHECKS=0";
            bdd.getRequest(query);
            query = "UPDATE t_occurenceweb SET occNumber = ((SELECT occNumber WHERE motID = '" + idResults[0] + "') +1) WHERE motID = '" + idResults[0] + "'; ";
            bdd.getRequest(query);
        }




        /// <summary>
        /// Affiche tous les mots trouvé sur le site de l'etml
        /// </summary>
        public void PrintAllWords()
        {
            Console.Clear();
            Console.WriteLine("Mots trouvés sur le site de l'ETML :");
            foreach (wordETML word in allETMLWords)
            {
                Console.WriteLine(word.Value);
            }
        }

        /// <summary>
        /// Méthode pour lire tous les pdf trouvés
        /// </summary>
        public void ReadAllPDF()
        {
            foreach (string fileLink in pdfETML)
            {
                WebClient wc = new WebClient();
                wc.DownloadFile("https://www.etml.ch" + fileLink, fileLink.Split('/')[fileLink.Split('/').Length - 1]);
            }
        }

        private void SyncBddData()
        {
            string query = "DELETE FROM t_mots WHERE motIsFromETML = '1';";
            bdd.getRequest(query);
            query = "ALTER TABLE t_mots AUTO_INCREMENT=0;";
            bdd.getRequest(query);
            query = "DELETE FROM t_web";
            bdd.getRequest(query);
            query = "ALTER TABLE t_web AUTO_INCREMENT=0;";
            bdd.getRequest(query);
            RecoverAllWords(startURL);
            
            Console.Clear();
            Console.WriteLine("100% terminé !");
        }

        //private bool RemoteFileExists(string url)
        //{
        //    try
        //    {
        //        HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
        //        request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
        //        request.Method = "HEAD"; //Get only the header information -- no need to download any content

        //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        //        int statusCode = (int)response.StatusCode;
        //        if (statusCode >= 100 && statusCode < 400) //Good requests
        //        {
        //            return true;
        //        }
        //        else if (statusCode >= 500 && statusCode <= 510) //Server Errors
        //        {
        //            return false;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return false;
        //}
    }
}
