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

            Console.WriteLine("Entrer le mot à rechercher");
            string []entre = Console.ReadLine().Split(' ');

            foreach(string str in entre)
            {
                try
                {
                    //Pour les fichiers qui contiennent la contrainte dans leur chemin
                    string[] files = Directory.GetFiles(@"K:\INF\eleves", "*" + str + "*", SearchOption.AllDirectories);
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                    }
                    ////Nombre de fichiers trouvés
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
            }
            Console.ReadLine();
        }
    }
}
