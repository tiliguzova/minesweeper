using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.OleDb;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeTextBoxes();
            InitializeButtons();
            trackBar1.Scroll += trackBar1_Scroll;
        }
        Form f2;
        private void InitializeButtons()
        {
            button1.Click += button_Click;
            button2.Click += button_Click;
        }
        private void InitializeTextBoxes()
        {
            textBox1.Click += textBox_Click;
            textBox2.Click += textBox_Click;
            textBox3.Click += textBox_Click;
            textBox4.Click += textBox_Click;
            textBox5.Click += textBox_Click;
        }
        private void textBox_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.ReadOnly = false;
            textBox.Text = "";
        }
        private void trackBar1_Scroll(object sender, EventArgs e) {
            if (trackBar1.Value == trackBar1.Minimum)
            {
                groupBox1.Visible = true;
                groupBox2.Visible = false;
            }
            else
            {
                groupBox1.Visible = false;
                groupBox2.Visible = true;
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null)
                return;

            GroupBox groupBox = null;
            if (clickedButton == button1)
            {
                groupBox = groupBox1;
            }
            else if (clickedButton == button2)
            {
                groupBox = groupBox2;
            }

            if (groupBox != null)
            {
                foreach (Control control in groupBox.Controls)
                {
                    if (control is TextBox)
                    {
                        if (string.IsNullOrEmpty(((TextBox)control).Text) || ((TextBox)control).ReadOnly)
                        {
                            MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else if (((TextBox)control).Name == "textBox5" && !IsValidEmail(((TextBox)control).Text))
                        {
                            MessageBox.Show("Некорректный адрес почты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            if (groupBox == groupBox2 && clickedButton == button2)
            {
                string login = textBox3.Text;
                string password = textBox4.Text;
                string email = textBox5.Text;

                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO table_name (Login, Password, Email) VALUES (?, ?, ?)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@p1", login);
                        command.Parameters.AddWithValue("@p2", password);
                        command.Parameters.AddWithValue("@p3", email);
                        command.ExecuteNonQuery();
                    }
                }
            }
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                f2 = new Form2();
                f2.Owner = this; 
                f2.Show();
                this.Hide();
            }
        }
                private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
