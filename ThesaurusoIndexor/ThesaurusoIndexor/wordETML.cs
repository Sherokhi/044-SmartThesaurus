using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algoResearch
{
    class WordETML
    {
        private string url;
        private string value;
        private int occurences;

        public WordETML(string pURL, string pValue, int pOccurences)
        {
            url = pURL;
            value = pValue;
            occurences = pOccurences;
        }

        public string Url { get => url;}
        public string Value { get => value;}

        public void AddOccurence()
        {
            occurences++;
        }
    }
}
