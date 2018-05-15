using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesaurusoIndexor
{
    public class OccFolder
    {
        private string folName;
        private Dictionary<string, int> occWord;

        //Constructeur de base
        public OccFolder(string name)
        {
            FolName = name;
            OccWord = new Dictionary<string, int>();
        }

        //Constructeur complet pour un fichier
        public OccFolder(string name, Dictionary<string, int> dic)
        {
            FolName = name;
            OccWord = dic;
        }

        public string FolName { get => folName; set => folName = value; }
        public Dictionary<string, int> OccWord { get => occWord; set => occWord = value; }
    }
}
