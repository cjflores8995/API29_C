using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TBTHDETALLEDEBITO
    {
        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
        public Int32? SECUENCIA { get; set; }
        public DateTime? FDEBITO { get; set; }
        public Decimal? VALOR { get; set; }
        public String NUMEROMENSAJE { get; set; }

        public bool Insertar(TBTHDETALLEDEBITO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TBTHDETALLEDEBITO ( ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" FDEBITO, ");
                query.Append(" VALOR, ");
                query.Append(" NUMEROMENSAJE ");
                query.Append(" ) VALUES ( ");
                query.Append(" :FPROCESO, ");
                query.Append(" :CPROCESO, ");
                query.Append(" :SECUENCIA, ");
                query.Append(" :FDEBITO, ");
                query.Append(" :VALOR, ");
                query.Append(" :NUMEROMENSAJE ");
                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, obj.SECUENCIA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FDEBITO", OracleDbType.Date, obj.FDEBITO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("VALOR", OracleDbType.Decimal, obj.VALOR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));

                #endregion armaComando

                #region ejecuta comando

                ado.AbrirConexion();
                resp = ado.EjecutarComando(comando);

                #endregion ejecuta comando
            }
            catch (Exception ex)
            {
                resp = false;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return resp;
        }

        //query.Append(" FPROCESO, ");
        //query.Append(" CPROCESO, ");
        //query.Append(" SECUENCIA, ");
        //query.Append(" FDEBITO, ");
        //query.Append(" VALOR, ");
        //query.Append(" NUMEROMENSAJE, ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
        //FDEBITO = Util.ConvertirFecha(reader["FDEBITO"].ToString()),
        //VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
        //NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),

        //query.Append(" FPROCESO = :FPROCESO, ");
        //query.Append(" CPROCESO = :CPROCESO, ");
        //query.Append(" SECUENCIA = :SECUENCIA, ");
        //query.Append(" FDEBITO = :FDEBITO, ");
        //query.Append(" VALOR = :VALOR, ");
        //query.Append(" NUMEROMENSAJE = :NUMEROMENSAJE, ");

        //comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, obj.SECUENCIA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FDEBITO", OracleDbType.Date, obj.FDEBITO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("VALOR", OracleDbType.Decimal, obj.VALOR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));

    }
}
