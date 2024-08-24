using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaConexion.Modelos;
using static System.Net.Mime.MediaTypeNames;

namespace CapaConexion
{
    public partial class Form1 : Form
    {
        List<Customers> customers = new List<Customers>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-5R5EQO8\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;");
            conexion.Open();
            MessageBox.Show("Conectado");

            String selectFrom = "";
            selectFrom = selectFrom + "SELECT [CompanyName] " + "\n";
            selectFrom = selectFrom + "      ,[ContactName] " + "\n";
            selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
            selectFrom = selectFrom + "      ,[Address] " + "\n";
            selectFrom = selectFrom + "      ,[City] " + "\n";
            selectFrom = selectFrom + "      ,[Region] " + "\n";
            selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
            selectFrom = selectFrom + "      ,[Country] " + "\n";
            selectFrom = selectFrom + "      ,[Phone] " + "\n";
            selectFrom = selectFrom + "      ,[Fax] " + "\n";
            selectFrom = selectFrom + "  FROM [dbo].[Customers]";

            SqlCommand comando = new SqlCommand(selectFrom,conexion);
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Customers customer = new Customers();
                customer.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
                customer.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
                customer.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
                customer.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
                customer.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
                customer.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
                customer.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (string)reader["PostalCode"];
                customer.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
                customer.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
                customer.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];

                customers.Add(customer);
            }
            dataGrid.DataSource = customers;
            conexion.Close();
            MessageBox.Show("Gracias, Conexion finalizada");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = filtrotxt.Text;

            
            if (text.Length>0)
            {
                string formateoTexto = char.ToUpper(text[0]) + text.Substring(1).ToLower();
                textBox.Text = formateoTexto;

                textBox.SelectionStart = textBox.Text.Length;

                var filtro = customers.FindAll(f => f.CompanyName.StartsWith(filtrotxt.Text));
                dataGrid.DataSource = filtro;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
            DatosLayer.DataBase.ConnetionTimeout = 30;
            string cadenaConexion = DatosLayer.DataBase.ConnectionString;

            var conectarDB = DatosLayer.DataBase.GetSqlConnection();
        }
    }
}
