using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
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
        public ResearchETML()
        {

        }

        public string Start(string textToSearch)
        {
            string research = String.Empty;
            string[] keyWords = textToSearch.Split(' ');
            return research;
        }

        public string getTextinHTML(string url)
        {
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
            return html;
        }

        /// <summary>
        /// Recherche tous les liens des pages domaines et les renvoies.
        /// </summary>
        /// <param name="url">Page HTML cible</param>
        /// <returns>tous les liens domaines</returns>
        public List<string> getLinks(string url)
        {
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
                    }
                    nbrEtmlLinks++;
                }
            }

            return finalLinks;
        }

        public bool IsNotIn(List<string> list, string elementToCheck)
        {
            return !list.Contains(elementToCheck);
        }

        public void RecoverAllWords(string url)
        {
            foreach (string word in getTextinHTML(url).Split())
            {
                if (IsNotIn(allETMLWords, word))
                {
                    allETMLWords.Add(word);
                    Console.WriteLine(word);
                }
            }

            pagesChecked.Add(url);
            Console.WriteLine(url);

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
