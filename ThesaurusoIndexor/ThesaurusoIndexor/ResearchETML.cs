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
        public ResearchETML()
        {

        }

        public string Start(string textToSearch)
        {
            string research = "";
            string[] keyWords = textToSearch.Split(' ');
            return research;
        }

        public string getTextinHTML()
        {
            var html = new WebClient().DownloadString("https://www.etml.ch/vie-de-lecole/menus-du-restaurant.html");
            //string htmlTagPattern = "<.*?>";
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
