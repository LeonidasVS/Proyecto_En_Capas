﻿using System;
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
using System.Reflection;

namespace CapaConexion
{
    public partial class Form1 : Form
    {
        CustomerReposity customeReposity = new CustomerReposity();
        public Form1()
        {
            InitializeComponent();
            cargarInformacion();
            btnModificar.Enabled= false;
            btnEliminar.Enabled= false;

        }

        public void cargarInformacion()
        {
            var custoMers = customeReposity.ObtenerTodos();
            dataGrid.DataSource = custoMers;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var ObtenerTodo = customeReposity.ObtenerTodos();
            var filtro = ObtenerTodo.FindAll(f => f.CustomerID.StartsWith(filtrotxt.Text));
            dataGrid.DataSource = filtro;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
            //DatosLayer.DataBase.ConnetionTimeout = 30;
            //string cadenaConexion = DatosLayer.DataBase.ConnectionString;

            //var conectarDB = DatosLayer.DataBase.GetSqlConnection();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var cliente = customeReposity.ObtenerPorID(textBuscar.Text);
            if (cliente!=null)
            {
                txtCustomerID.Text = cliente.CustomerID;
                txtCompanyName.Text = cliente.CompanyName;
                txtContactName.Text = cliente.ContactName;
                txtContactTitle.Text = cliente.ContactTitle;
                txtAddress.Text = cliente.Address;
                txtCity.Text = cliente.City;
                txtCustomerID.Enabled = false;
                btnIngresar.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
  
            var resultado = 0;
            var nuevoCliente = ObtenerNUevoCLiente();
            if (validarCampoNull(nuevoCliente) == false)
            {
                resultado = customeReposity.InsertarCliente(nuevoCliente);

                if (resultado>0)
                {
                    MessageBox.Show("Cliente insertado con EXITO", "Insercion de clientes",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Borrar();
                }
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos por favor","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

        }

        public Boolean validarCampoNull(Object objeto)
        {
            foreach (PropertyInfo property in objeto.GetType().GetProperties())
            {
                object value = property.GetValue(objeto, null);
                if ((string)value == "")
                {
                    return true;
                }
            }
            return false;
        }

        public void Borrar()
        {
            txtCustomerID.Text = "";
            txtCompanyName.Text = "";
            txtContactName.Text = "";
            txtCity.Text = "";
            txtAddress.Text = "";
            txtContactTitle.Text = "";
            textBuscar.Text = "";
        }
        private void clear_Click(object sender, EventArgs e)
        {
            Borrar();
            txtCustomerID.Enabled = true;
            btnIngresar.Enabled = true;
            textBuscar.Text = "";
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            textBuscar.Text= "";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            var actualizarCliente = ObtenerNUevoCLiente();
            int actualizadas=customeReposity.ActualizarCliente(actualizarCliente);
            MessageBox.Show($"Filas actualizadas = {actualizadas}");
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            txtCustomerID.Enabled = true;
            btnIngresar.Enabled = true;
            Borrar();
            cargarInformacion();
            
        }

        private Customers ObtenerNUevoCLiente()
        {
            var nuevoCliente = new Customers
            {
                CustomerID= txtCustomerID.Text,
                CompanyName= txtCompanyName.Text,
                ContactName= txtContactName.Text,
                ContactTitle= txtContactTitle.Text,
                Address= txtAddress.Text,
                City= txtCity.Text,
            };
            return nuevoCliente;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int eliminados = customeReposity.EliminarCliente(textBuscar.Text);

            if (eliminados>0)
            {
                MessageBox.Show("Usuario Elimiando", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Borrar();
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
                txtCustomerID.Enabled = true;
                btnIngresar.Enabled = true;
                cargarInformacion();
            }
        }
    }
}
