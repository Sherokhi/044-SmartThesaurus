﻿///ETML
///Auteur : Jérémie Perret
///Date : 16.01.2018
///Description : Classe pour le WinForm
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using algoResearch;

namespace ThesaurusoIndexor
{
    public partial class Form1 : Form
    {
        ResearchDatabase searchK;
        ResearchETML researchETML;

        public Form1()
        {
            InitializeComponent();
            //database = new DBConnect();
            //cbResearchLocation.SelectedIndex = 1;
        }

        private void btn_Research_Click(object sender, EventArgs e)
        {
            //db = new DBConnect();
            switch (cbResearchLocation.SelectedIndex)
            {
                case 0: //Si la recherche doit chercher partout

                    break;
                case 1: //Si la recherche doit chercher sur etml.ch
                    //researchETML = ResearchETML.CreateResearch();
                    //researchETML.GetAllData();
                    break;
                case 2: //Si la recherche doit chercher sur educanet2.ch
                    break;
                case 3: //Si la recherche doit chercher sur K:\INF\ELEVE\Temp

                    //Disparition du bouton car possibilité de spammer
                    btn_Research.Location = new Point(100000, 100000);
                    timerButton.Enabled = true;

                    Cursor = Cursors.WaitCursor;

                    //Création de la recherche dans laa base de données
                    searchK = ResearchDatabase.CreateResearch();
                    //Initialisation de la recherche
                    searchK.GetResearch(txb_Research.Text);
                    //Lancement de la recherche
                    searchK.BeginTheReasearchWord();
                    break;
            }

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

        private void Form1_Load(object sender, EventArgs e)
        {
            //On remplit le datagridview
            dgvData.Columns.Add("urlFolder", "URL");

            dgvData.Columns.Add("provFolder", "Provenance");
        }

        private void timerButton_Tick(object sender, EventArgs e)
        {
            //On réaffiche le bouton
            btn_Research.Location = new Point(884, 9);
            timerButton.Enabled = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //Rafraichissement de la base de données
            FillK recherche = FillK.CreateResearch();
            recherche.BeginTheReasearchWord();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //Ajout du clique pour le datagridview (à tester)
            if (e.ColumnIndex == 0)
            {
                String file = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(file, "");
                try
                {
                    System.Diagnostics.Process.Start(psi);
                }

                catch
                {
                    MessageBox.Show("Erreur survenue, le fichier n'existe peut être plus.");
                }
            }
        }
    }
}
