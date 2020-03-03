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
namespace AntiWotro
{
    public partial class file_add : Form
    {
        public file_add()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "/data/data.mdb");
        string name = null;
        string base64 = null;
        string app = null;
        string info = null;
        private void button2_Click(object sender, EventArgs e)
        {
           try
           {
               string kind = null;
               if (comboBox1.SelectedIndex == 0)
                   kind = "worm";
               else if (comboBox1.SelectedIndex == 1)
                   kind = "keylogger";
               else if (comboBox1.SelectedIndex == 2)
                   kind = "rat";
               else if (comboBox1.SelectedIndex == 3)
                   kind = "adware";

                baglan.Open();
                OleDbCommand komut = new OleDbCommand("insert into added (kind,name,base64,app) values(@kind,@name,@base64,@app) ", baglan);
                komut.Parameters.AddWithValue("@kind", comboBox1.Text);
                komut.Parameters.AddWithValue("@name",name);
                komut.Parameters.AddWithValue("@base64", base64);
                komut.Parameters.AddWithValue("@app", app);
                komut.ExecuteNonQuery();
                baglan.Close();
              

                    baglan.Open();
                    OleDbCommand worm = new OleDbCommand("insert into "+kind+" (name,base64,app) values(@name,@base64,@app) ", baglan);
                    worm.Parameters.AddWithValue("@name", name);
                    worm.Parameters.AddWithValue("@base64", base64);
                    worm.Parameters.AddWithValue("@app", app);
                    worm.ExecuteNonQuery();
                    baglan.Close();
                MessageBox.Show("VeriTabanına Ekleme başarılı", "AntiWotro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button2.Enabled = false;
                button4.Enabled = false;
                richTextBox1.Text = "";
                textBox1.Text = "";
                name = null;
                base64 = null;
                app = null;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "AntiWotro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void file_add_Load(object sender, EventArgs e)
        {
            comboBox1.Text = comboBox1.Items[0].ToString();
            try
            {
                this.Text = "VeriTabanına Bağlanıldı.";
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "AntiWotro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Title = "Dosya Seç";
            dosya.Filter = "Tüm Dosyalar|*.*";
            if (dosya.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dosya.FileName;
                FileInfo inf = new FileInfo(dosya.FileName);
                byte[] bytes=File.ReadAllBytes(dosya.FileName);
                richTextBox1.Text = "İsim : " + dosya.SafeFileName + Environment.NewLine +
                    "Boyut : " + inf.Length.ToString()+" byte"+  Environment.NewLine +
                    "Son Değişiklik : " + inf.LastWriteTime.ToShortDateString() + Environment.NewLine +
                    "Oluşturma : " + inf.CreationTime.ToShortDateString() + Environment.NewLine +
                    "Nitelikler : " + inf.Attributes.ToString() + Environment.NewLine +
                    "Base64 : " + bytes.Length.ToString()+" karekter";
                info = richTextBox1.Text;
                name = dosya.SafeFileName;
                base64 = Convert.ToBase64String(bytes);
                app = comboBox1.Text;
                button4.Enabled = true;
                button2.Enabled = true;
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
