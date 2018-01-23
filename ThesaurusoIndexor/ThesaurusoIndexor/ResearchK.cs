using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace algoResearch
{
    public class ResearchK
    {
        private string[] userSearch;

        public ResearchK()
        {

        }

        public void GetResearch(string strResearch)
        {
            userSearch = strResearch.Split(' ');
        }

        public void BeginTheReasearch()
        {
            foreach (string str in userSearch)
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
                    MessageBox.Show(UAEx.Message);
                }
                catch (PathTooLongException PathEx)
                {
                    MessageBox.Show(PathEx.Message);
                }
            }
        }
    }
}

