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
using static System.Net.Mime.MediaTypeNames;
using DatosLayer;

namespace CapaConexion
{
    public partial class Form1 : Form
    {
        CustomerReposity customeReposity = new CustomerReposity();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            var custoMers = customeReposity.obtenerTodos();
            dataGrid.DataSource = custoMers;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //TextBox textBox = (TextBox)sender;
            //string text = filtrotxt.Text;

            
            //if (text.Length>0)
            //{
            //    string formateoTexto = char.ToUpper(text[0]) + text.Substring(1).ToLower();
            //    textBox.Text = formateoTexto;

            //    textBox.SelectionStart = textBox.Text.Length;

            //    var filtro = customers.FindAll(f => f.CompanyName.StartsWith(filtrotxt.Text));
            //    dataGrid.DataSource = filtro;
            //}
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
