using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class CustomerReposity
    {
        List<Customers> customers = new List<Customers>();
        public List<Customers> obtenerTodos()
        {
            var conexion = DataBase.GetSqlConnection();

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

            SqlCommand comando = new SqlCommand(selectFrom, conexion);
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
            conexion.Close();

            return customers;
        }
    }
}
