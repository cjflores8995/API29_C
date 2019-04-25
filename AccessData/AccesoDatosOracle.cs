using Crypto;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace AccessData
{
    public class AccesoDatosOracle
    {
        private string connectionString = string.Empty;
        private Int32 timeout = 90;

        public OracleConnection oraConexion;

        public AccesoDatosOracle()
        {
            try
            {
                string Host = ConfigurationManager.AppSettings["OraHost"].ToString();
                string Port = ConfigurationManager.AppSettings["OraPort"].ToString();
                string Sid = ConfigurationManager.AppSettings["OraSid"].ToString();
                string User = ConfigurationManager.AppSettings["OraUser"].ToString();
                string Pass = CryptoUtil.Decrypt(ConfigurationManager.AppSettings["OraPass"].ToString(), "S29-2017");

                connectionString = string.Format(ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString, Host, Port, Sid, User, Pass);


                timeout = Convert.ToInt32(ConfigurationManager.AppSettings["OraTime"].ToString());
                oraConexion = new OracleConnection();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public AccesoDatosOracle(string conexion)
        {
            string Host = ConfigurationManager.AppSettings[conexion + "Host"].ToString();
            string Port = ConfigurationManager.AppSettings[conexion + "Port"].ToString();
            string Sid = ConfigurationManager.AppSettings[conexion + "Sid"].ToString();
            string User = ConfigurationManager.AppSettings[conexion + "User"].ToString();
            string Pass = CryptoUtil.Decrypt(ConfigurationManager.AppSettings[conexion + "Pass"].ToString(), "S29-2017");

            connectionString = string.Format(ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString, Host, Port, Sid, User, Pass);
            timeout = Convert.ToInt32(ConfigurationManager.AppSettings[conexion + "Time"].ToString());
            oraConexion = new OracleConnection();
        }

        public void AbrirConexion()
        {
            if (!(this.oraConexion.State == ConnectionState.Open))
            {
                this.oraConexion.ConnectionString = this.connectionString;
                this.oraConexion.Open();
            }
        }

        public void CerrarConexion()
        {
            if (!(this.oraConexion.State == ConnectionState.Closed))
            {
                this.oraConexion.Close();
            }
        }

        public bool EjecutarComando(OracleCommand comando)
        {
            bool resp = false;
            Int32 registros = 0;

            comando.Connection = oraConexion;
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

        public bool EjecutarComando(ref OracleCommand comando)
        {
            bool resp = false;
            Int32 registros = 0;

            comando.Connection = oraConexion;
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

        public OracleDataReader EjecutarSentencia(OracleCommand comando)
        {
            OracleDataReader reader = null;

            comando.Connection = oraConexion;
            comando.CommandTimeout = timeout;
            reader = comando.ExecuteReader();

            return reader;
        }
    }
}
