using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesaurusoIndexor
{
    public class OccFolder
    {
        public string folName;
        public Dictionary<string, int> occWord;

        //Constructeur de base
        public OccFolder(string name)
        {
            folName = name;
            occWord = new Dictionary<string, int>();
        }

        //Constructeur complet pour un fichier
        public OccFolder(string name, Dictionary<string, int> dic)
        {
            folName = name;
            occWord = dic;
        }
    }
}
