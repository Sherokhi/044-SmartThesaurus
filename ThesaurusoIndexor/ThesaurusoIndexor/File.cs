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

        //Chemin du fichier
        private string path;

        //Extension du fichier
        private string extension;

        //Date de la dernière modification
        private DateTime dateModif;

        //Poid du fichier
        private int filePoids;
        
        public int FilePoids { get => filePoids; set => filePoids = value; }
        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
        public string Extension { get => extension; set => extension = value; }
        public DateTime DateModif { get => dateModif; set => dateModif = value; }

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
