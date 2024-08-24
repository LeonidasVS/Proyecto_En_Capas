using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatosLayer
{
    public class DataBase
    {
        public static int ConnetionTimeout { get; set; }
        public static string ApplicationName { get; set; }
        public static string ConnectionString
        {
            get
            {
                string CadenaConexion= ConfigurationManager.ConnectionStrings["NWConnection"].ConnectionString;
                SqlConnectionStringBuilder conexionBuilder =new SqlConnectionStringBuilder(CadenaConexion);

                conexionBuilder.ApplicationName =
                ApplicationName ?? conexionBuilder.ApplicationName;
                conexionBuilder.ConnectTimeout = (ConnetionTimeout > 0)
                    ? ConnetionTimeout : conexionBuilder.ConnectTimeout;
                return conexionBuilder.ToString();


            }
        }

        public static SqlConnection GetSqlConnection()
        {

            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            return conexion;
        }
    }
}
