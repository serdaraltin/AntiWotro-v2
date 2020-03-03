using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Threading;
namespace AntiWotro
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cb_scan.Text = cb_scan.Items[0].ToString();
            cb_procress.Text = cb_procress.Items[0].ToString();
        }

        private void btn_directory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog directory = new FolderBrowserDialog();
            if (directory.ShowDialog() == DialogResult.OK)
                tx_directory.Text = directory.SelectedPath;
        }

        private void dosyaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            file_add file = new file_add();
            file.ShowDialog();
        }

        private void durumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            view_data dt = new view_data();
            dt.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tx_directory.Text != "")
            {
                listView1.Items.Clear();
                try
                {
                
                    foreach (string dir in Directory.GetDirectories(tx_directory.Text, "*", SearchOption.AllDirectories))
                    {
                        foreach (string file in Directory.GetFiles(dir))
                        {
                            ListViewItem items = new ListViewItem(dir);
                            items.SubItems.Add(file.Substring(file.LastIndexOf(@"\")+1));
                            listView1.Items.Add(items);
                        }
                    }
                }
                catch (UnauthorizedAccessException uae)
                {
                    MessageBox.Show(uae.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                label6.Text = "Toplam : " + listView1.Items.Count.ToString();
            }
        }

    }
}
