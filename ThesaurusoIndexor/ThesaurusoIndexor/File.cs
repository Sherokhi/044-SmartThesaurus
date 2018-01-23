using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesaurusoIndexor
{
    public class File
    {
        private string name;
        public string Name { get => name; set => name = value; }
        private string path;
        public string Path { get => path; set => path = value; }
        private string extension;
        public string Extension { get => extension; set => extension = value; }
        private DateTime dateModif;
        public DateTime DateModif { get => dateModif; set => dateModif = value; }
        private int filePoids;
        public int FilePoids { get => filePoids; set => filePoids = value; }
        

        public File(string strName, string strPath, string strExt, DateTime dateTimeModif, int intFilePoids)
        {
            name = strName;
            path = strPath;
            extension = strExt;
            dateModif = dateTimeModif;
            filePoids = intFilePoids;
        }

        public string ReturnFullName()
        {
            return this.name + this.extension;
        }
    }
}
