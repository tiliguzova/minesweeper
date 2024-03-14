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
        Form f3;
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
                string mail = textBox5.Text;

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\source\repos\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\Database.accdb;Persist Security Info=False;";

                using (OleDbConnection dbConnection = new OleDbConnection(connectionString))
                {
                    dbConnection.Open();
                    string query = "SELECT COUNT(*) FROM User WHERE login = ? OR mail = ?";
                    using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                    {
                        dbCommand.Parameters.AddWithValue("@login", login);
                        dbCommand.Parameters.AddWithValue("@mail", mail);
                        int count = (int)dbCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Пользователь с таким логином или адресом почты уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; 
                        }
                    }

               
                    query = "INSERT INTO [User] (login, password, mail) VALUES (?, ?, ?)";
                    using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                    {
                        dbCommand.Parameters.AddWithValue("@login", login);
                        dbCommand.Parameters.AddWithValue("@password", password);
                        dbCommand.Parameters.AddWithValue("@mail", mail);
                        dbCommand.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Успешная регистрация! Можете авторизироваться. Приятной игры!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (groupBox == groupBox1 && clickedButton == button1)
            {
                string login = textBox1.Text;
                string password = textBox2.Text;
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\source\repos\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\Database.accdb;Persist Security Info=False;";

                using (OleDbConnection dbConnection = new OleDbConnection(connectionString))
                {
                    dbConnection.Open();
               
                    string query = "SELECT COUNT(*) FROM [User] WHERE [login] = ? AND [password] = ?";
                    using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                    {
                        dbCommand.Parameters.AddWithValue("@login", login);
                        dbCommand.Parameters.AddWithValue("@password", password);
                        int count = (int)dbCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Успешная авторизация!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            f3 = new Form3();
                            f3.Owner = this;
                            f3.Show();
                            this.Hide();
                        }
                        else if (login == "admin" && password == "admin")
                        {
                            f2 = new Form2();
                            f2.Owner = this;
                            f2.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
