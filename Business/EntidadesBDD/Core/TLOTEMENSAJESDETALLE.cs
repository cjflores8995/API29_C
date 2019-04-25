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
    public class TLOTEMENSAJESDETALLE
    {
        #region variables

        public Int32? NUMEROLOTE { get; set; }
        public DateTime? FECHALOTE { get; set; }
        public Int32? STRANSACCION { get; set; }
        public String CODIGORESULTADO { get; set; }
        public String TEXTOERROR { get; set; }
        public String MENSAJEORIGINAL { get; set; }
        public String NUMEROMENSAJE { get; set; }

        #endregion variables

        #region metodos

        public TLOTEMENSAJESDETALLE ConsultaMensaje(DateTime? fechalote, Int32? NUMEROLOTE, string mensaje)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TLOTEMENSAJESDETALLE obj = null;

            try
            {
                #region arma comando

                query.Append(" SELECT ");
                query.Append(" NUMEROLOTE, ");
                query.Append(" FECHALOTE, ");
                query.Append(" STRANSACCION, ");
                query.Append(" CODIGORESULTADO, ");
                query.Append(" TEXTOERROR, ");
                query.Append(" MENSAJEORIGINAL, ");
                query.Append(" NUMEROMENSAJE ");
                query.Append(" FROM TLOTEMENSAJESDETALLE ");
                query.Append(" WHERE FECHALOTE = :FECHALOTE ");
                query.Append(" AND NUMEROLOTE = :NUMEROLOTE ");
                query.Append(" AND MENSAJEORIGINAL LIKE :MENSAJEORIGINAL ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FECHALOTE", OracleDbType.Date, fechalote, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROLOTE", OracleDbType.Int32, NUMEROLOTE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("MENSAJEORIGINAL", OracleDbType.Varchar2, mensaje, ParameterDirection.Input));

                #endregion arma comando

                #region ejecuta comando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TLOTEMENSAJESDETALLE
                        {
                            NUMEROLOTE = Util.ConvertirNumero(reader["NUMEROLOTE"].ToString()),
                            FECHALOTE = Util.ConvertirFecha(reader["FECHALOTE"].ToString()),
                            STRANSACCION = Util.ConvertirNumero(reader["STRANSACCION"].ToString()),
                            CODIGORESULTADO = reader["CODIGORESULTADO"].ToString(),
                            TEXTOERROR = reader["TEXTOERROR"].ToString(),
                            MENSAJEORIGINAL = reader["MENSAJEORIGINAL"].ToString(),
                            NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString()
                        };
                        break;
                    }
                }
                else
                {
                    obj = null;
                }

                #endregion ejecuta comando
            }
            catch (Exception ex)
            {
                obj = null;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return obj;
        }

        #endregion metodos

        //query.Append(" NUMEROLOTE, ");
        //query.Append(" FECHALOTE, ");
        //query.Append(" STRANSACCION, ");
        //query.Append(" CODIGORESULTADO, ");
        //query.Append(" TEXTOERROR, ");
        //query.Append(" MENSAJEORIGINAL, ");
        //query.Append(" NUMEROMENSAJE, ");

        //NUMEROLOTE = Util.ConvertirNumero(reader["NUMEROLOTE"].ToString()),
        //FECHALOTE = Util.ConvertirFecha(reader["FECHALOTE"].ToString()),
        //STRANSACCION = Util.ConvertirNumero(reader["STRANSACCION"].ToString()),
        //CODIGORESULTADO = reader["CODIGORESULTADO"].ToString(),
        //TEXTOERROR = reader["TEXTOERROR"].ToString(),
        //MENSAJEORIGINAL = reader["MENSAJEORIGINAL"].ToString(),
        //NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),

        //query.Append(" NUMEROLOTE = :NUMEROLOTE, ");
        //query.Append(" FECHALOTE = :FECHALOTE, ");
        //query.Append(" STRANSACCION = :STRANSACCION, ");
        //query.Append(" CODIGORESULTADO = :CODIGORESULTADO, ");
        //query.Append(" TEXTOERROR = :TEXTOERROR, ");
        //query.Append(" MENSAJEORIGINAL = :MENSAJEORIGINAL, ");
        //query.Append(" NUMEROMENSAJE = :NUMEROMENSAJE, ");

        //comando.Parameters.Add(new OracleParameter("NUMEROLOTE", OracleDbType.Int32, obj.NUMEROLOTE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FECHALOTE", OracleDbType.Date, obj.FECHALOTE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("STRANSACCION", OracleDbType.Int32, obj.STRANSACCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CODIGORESULTADO", OracleDbType.Varchar2, obj.CODIGORESULTADO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TEXTOERROR", OracleDbType.Varchar2, obj.TEXTOERROR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("MENSAJEORIGINAL", OracleDbType.Varchar2, obj.MENSAJEORIGINAL, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));

    }
}
