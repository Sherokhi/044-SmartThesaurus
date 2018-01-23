using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace X_rossetlo_P_OO_loadthek
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //Créer un ensemble de fichiers situés dans le chemin spécifié

                //Pour les fichiers qui contiennent la contrainte dans leur chemin
                var files = from file in Directory.EnumerateFiles(@"H:\EssaiK", "*.txt", SearchOption.AllDirectories)
                                //On lit chaque ligne du contenu du fichier
                            from line in File.ReadLines(file)
                                //On sélectionne que certains qui possède le contenu spécifié
                            where file.Contains("asas") || line.Contains("asas")
                            select new
                            {
                                File = file,
                                Line = line
                            };

                //Pour chaque fichier dans l'ensemble de fichier, on affiche son chemin et son contenu
                foreach (var f in files)
                {
                    Console.WriteLine("{0}", f.File);
                }
                //Nombre de fichiers trouvés
                Console.WriteLine("{0} files found.", files.Count().ToString());
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }

            Console.ReadLine();
        }
    }
}
