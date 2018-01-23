using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static ThesaurusoIndexor.Program;
using System.Threading;

namespace algoResearch
{
    public class ResearchK
    {
        private string[] userSearch;
        private static ResearchK actualResaerch;

        public ResearchK()
        {
            
        }

        public void GetResearch(string strResearch)
        {
            userSearch = strResearch.Split(' ');
        }

        public static ResearchK CreateResearch()
        {
            if(actualResaerch == null)
            {
                actualResaerch = new ResearchK();
            }
            return actualResaerch;
        }

        public void BeginTheReasearch()
        {
            string allData = "";
            int counter = 0;
            form1.rtbResult.Text = "";
            form1.pbLoad.Value = 0;
            foreach (string str in userSearch)
            {
                try
                {
                    //Pour les fichiers qui contiennent la contrainte dans leur chemin
                    string[] files = Directory.GetFiles(@"K:\INF\Eleves\temp", "*" + str + "*", SearchOption.AllDirectories);
                    form1.pbLoad.Maximum = files.Count();
                    form1.btn_Research.Enabled = false;
                    foreach (string s in files)
                    {
                        //Thread.Sleep(20);
                        counter++;
                        allData += s + "\r\n";
                        form1.rtbResult.Text = allData;
                        if(form1.pbLoad.Value < form1.pbLoad.Maximum)
                        {
                            form1.pbLoad.Value++;
                        }
                        form1.Update();
                        form1.lblResearchNumber.Text = counter.ToString();
                    }
                    form1.Cursor = Cursors.Arrow;
                    form1.btn_Research.Enabled = true;
                    ////Nombre de fichiers trouvés
                    form1.lblResearchNumber.Text = files.Count().ToString();
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

