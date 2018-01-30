using System.IO;
using System.Net;

namespace algoResearch
{
    public class ResearchETML
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
            WebRequest req = HttpWebRequest.Create("https://www.etml.ch/vie-de-lecole/menus-du-restaurant.html");
            req.Method = "GET";

            string source;
            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                source = reader.ReadToEnd();
            }


            return source;
        }
    }
}
