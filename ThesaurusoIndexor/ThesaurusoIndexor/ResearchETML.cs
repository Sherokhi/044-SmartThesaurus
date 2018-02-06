using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace algoResearch
{
    class ResearchETML
    {
        private string[] userSearch;
        private static ResearchETML actualResearch;
        private List<string> linkList = new List<string>();
        public ResearchETML()
        {

        }


        public static ResearchETML CreateResearch()
        {
            if (actualResearch == null)
            {
                actualResearch = new ResearchETML();
            }
            return actualResearch;
        }
       

        public string GetAllData()
        {
            string html = "";
            try
            {
                html= new WebClient().DownloadString("https://www.etml.ch/vie-de-lecole/menus-du-restaurant.html");
            }
            catch
            {
                throw new Exception("");
            }
            //string htmlTagPattern = "<.*?>";
            Match links = Regex.Match(html, "<a href=\".* \".*>");
            var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = regexCss.Replace(html, string.Empty);
            //html = Regex.Replace(html, htmlTagPattern, string.Empty);
            html = Regex.Replace(html, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            html = html.Replace("&nbsp;", string.Empty);
            Regex regEx = new Regex("<[^>]*>", RegexOptions.IgnoreCase);
            html = regEx.Replace(html, "");
            html = Regex.Replace(html, @"(\<!--\s*.*?((--\>)|$))", String.Empty);
            html = Regex.Replace(html, @"\n", "");
            html = Regex.Replace(html, @"&copy;", "");
            //html = Regex.Replace(html, @"^((\<!--\s*.*?((--\>)|$))|\\n|<.*?>|&copy;)", String.Empty);
            byte[] bytes = Encoding.Default.GetBytes(html);
            html = Encoding.UTF8.GetString(bytes);
            return html;
        }






    }
}
