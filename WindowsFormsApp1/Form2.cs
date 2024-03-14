using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\source\repos\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\Database.accdb;Persist Security Info=False;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "SELECT * FROM User";
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();

            if (dbReader.HasRows == false)
            {
                MessageBox.Show("Данных нет", "Ошибка");
            }
            else {
                while (dbReader.Read()) {
                    dataGridView1.Rows.Add(dbReader["id"], dbReader["login"], dbReader["password"], dbReader["mail"]);
                }
            }
            dbReader.Close();
            dbConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1) {
                MessageBox.Show("Выберите одну строку", "Внимание");
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            if (dataGridView1.Rows[index].Cells[0].Value == null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null) {
                MessageBox.Show("Поля не заполненны", "Внимание");
                return;
            }
            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string login = dataGridView1.Rows[index].Cells[1].Value.ToString();
            string password = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string mail = dataGridView1.Rows[index].Cells[3].Value.ToString();

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\source\repos\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\Database.accdb;Persist Security Info=False;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "INSERT INTO User VALUES (" + id + ", '" + login + "', '" + password + "', '" + mail + "'\")";
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка запроса", "Ошибка");
            else 
                MessageBox.Show("Данные добавлены", "Внимание");

            dbConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку", "Внимание");
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            if (dataGridView1.Rows[index].Cells[0].Value == null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null)
            {
                MessageBox.Show("Поля не заполненны", "Внимание");
                return;
            }
            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string login = dataGridView1.Rows[index].Cells[1].Value.ToString();
            string password = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string mail = dataGridView1.Rows[index].Cells[3].Value.ToString();

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\source\repos\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\Database.accdb;Persist Security Info=False;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "UPDATE User SET Login = '" + login + "',Password = '" + password + "',Mail = '" + mail + "' WHERE id = " + id;
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка запроса", "Ошибка");
            else
            {
                MessageBox.Show("Данные обновлены", "Внимание");
            }
            dbConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку", "Внимание");
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            if (dataGridView1.Rows[index].Cells[0].Value == null)
            {
                MessageBox.Show("Поля не заполненны", "Внимание");
                return;
            }
            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\source\repos\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\Database.accdb;Persist Security Info=False;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "DELETE FROM User WHERE id = " + id;
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка запроса", "Ошибка");
            else
            { 
                MessageBox.Show("Данные удалены", "Внимание");
                dataGridView1.Rows.RemoveAt(index);
            }
            dbConnection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
