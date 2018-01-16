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

namespace ThesaurusoIndexor
{
    public partial class Form1 : Form
    {
        DBConnect database;
        public Form1()
        {
            InitializeComponent();
            database = new DBConnect();
        }

        private void btn_Research_Click(object sender, EventArgs e)
        {

        }

        private void txb_Research_TextChanged(object sender, EventArgs e)
        {
            btn_Research.Enabled = txb_Research.Text.Length > 0;
        }
    }
}
