///ETML
///Auteur : Jérémie Perret
///Date : 16.01.2018
///Description : Classe pour le WinForm
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using algoResearch;

namespace ThesaurusoIndexor
{
    public partial class Form1 : Form
    {
        DBConnect database;
        ResearchK searchK;
        public Form1()
        {
            InitializeComponent();
            //database = new DBConnect();
            //cbResearchLocation.SelectedIndex = 1;
        }

        private void btn_Research_Click(object sender, EventArgs e)
        {
            switch (cbResearchLocation.SelectedIndex)
            {
                case 0: //Si la recherche doit chercher partout

                    break;
                case 1: //Si la recherche doit chercher sur etml.ch
                    ETMLRes(txb_Research.Text);
                    break;
                case 2: //Si la recherche doit chercher sur educanet2.ch

                    break;
                case 3: //Si la recherche doit chercher sur K:\INF\ELEVE\Temp
                    searchK = ResearchK.CreateResearch();
                    searchK.GetResearch(txb_Research.Text);
                    searchK.BeginTheReasearch();

                    break;
            }
        }

        private void txb_Research_TextChanged(object sender, EventArgs e)
        {
            btn_Research.Enabled = txb_Research.Text.Length > 0;
        }

        private void ETMLRes(string wordsToSearch)
        {
            WebClient client = new WebClient();
            string[] contenu = new string[2000];

            string[] subsStrings = wordsToSearch.Split(' ');
            //Uri urlPath = "https://www.etml.ch/";

            //string codeHtml = client.DownloadString(urlPath);

            //Create the WebBrowser control
            WebBrowser wb = new WebBrowser();
            //Add a new event to process document when download is completed   
            wb.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(DisplayText);
            //Download the webpage
            //wb.Url = urlPath;



        }

        private void DisplayText(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            wb.Document.ExecCommand("SelectAll", false, null);
            wb.Document.ExecCommand("Copy", false, null);
            //txbTest.Text = CleanText(Clipboard.GetText());
        }
    }
}
