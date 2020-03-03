using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace AntiWotro
{
    public partial class view_data : Form
    {
        public view_data()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "/data/data.mdb");
        private void view_data_Load(object sender, EventArgs e)
        {
            baglan.Open();
            OleDbCommand komut = new OleDbCommand("select *from added", baglan);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem items = new ListViewItem(oku["kind"].ToString());
                items.SubItems.Add(oku["name"].ToString());
                listView1.Items.Add(items);
            }
            baglan.Close();
        }
    }
}
