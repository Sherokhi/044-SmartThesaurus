///ETML
///Auteur : Loic Rosset
///Date : 23.01.2018
///Description : Classe pour les objets fichiers
using System;

namespace ThesaurusoIndexor
{
    public class File
    {
        //Nom du fichier
        private string name;
        public string Name { get => name; set => name = value; }
        //Chemin du fichier
        private string path;
        public string Path { get => path; set => path = value; }
        //Extension du fichier
        private string extension;
        public string Extension { get => extension; set => extension = value; }
        //Date de la dernière modification
        private DateTime dateModif;
        public DateTime DateModif { get => dateModif; set => dateModif = value; }
        //Poid du fichier
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

        /// <summary>
        /// Retourne le nom complet du fichier
        /// </summary>
        /// <returns></returns>
        public string ReturnFullName()
        {
            return this.name + this.extension;
        }
    }
}
