using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algoResearch
{
    class wordETML
    {
        private string url;
        private string value;

        public wordETML(string pURL, string pValue)
        {
            url = pURL;
            value = pValue;
        }

        public string Url { get => url;}
        public string Value { get => value;}
    }
}
