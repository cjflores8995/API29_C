using Crypto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessData
{
    public class AccesoDatosSql
    {
        private string connectionString = string.Empty;
        private Int32 timeout = 90;

        private SqlConnection objConexion;

        public AccesoDatosSql()
        {
            string Host = ConfigurationManager.AppSettings["SqlHost"].ToString();
            string User = ConfigurationManager.AppSettings["SqlUser"].ToString();
            string Pass = CryptoUtil.Decrypt(ConfigurationManager.AppSettings["SqlPass"].ToString(), "S29-2017");

            connectionString = string.Format(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString, Host, User, Pass);
            timeout = Convert.ToInt32(ConfigurationManager.AppSettings["SqlTime"].ToString());
            objConexion = new SqlConnection();
        }

        public AccesoDatosSql(string conexion)
        {
            string Host = ConfigurationManager.AppSettings[conexion + "Host"].ToString();
            string User = ConfigurationManager.AppSettings[conexion + "User"].ToString();
            string Pass = CryptoUtil.Decrypt(ConfigurationManager.AppSettings[conexion + "Pass"].ToString(), "S29-2017");

            connectionString = string.Format(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString, Host, User, Pass);
            timeout = Convert.ToInt32(ConfigurationManager.AppSettings[conexion + "Time"].ToString());
            objConexion = new SqlConnection();
        }

        public void AbrirConexion()
        {
            if (!(this.objConexion.State == ConnectionState.Open))
            {
                this.objConexion.ConnectionString = this.connectionString;
                this.objConexion.Open();
            }
        }

        public void CerrarConexion()
        {
            if (!(this.objConexion.State == ConnectionState.Closed))
            {
                this.objConexion.Close();
            }
        }

        public bool EjecutarComando(SqlCommand comando)
        {
            bool resp = false;
            Int32 registros = 0;

            comando.Connection = objConexion;
            comando.CommandTimeout = timeout;
            registros = comando.ExecuteNonQuery();

            if (registros >= 0 || registros == -1)
            {
                resp = true;
            }
            else
            {
                resp = false;
            }

            return resp;
        }

        public bool EjecutarComando(ref SqlCommand comando)
        {
            bool resp = false;
            Int32 registros = 0;

            comando.Connection = objConexion;
            comando.CommandTimeout = timeout;
            registros = comando.ExecuteNonQuery();

            if (registros >= 0 || registros == -1)
            {
                resp = true;
            }
            else
            {
                resp = false;
            }

            return resp;
        }

        public SqlDataReader EjecutarSentencia(SqlCommand comando)
        {
            SqlDataReader reader = null;

            comando.Connection = objConexion;
            comando.CommandTimeout = timeout;
            reader = comando.ExecuteReader();

            return reader;
        }
    }
}
