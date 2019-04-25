using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class TSISTERMINAL
    {
        #region variables

        public String CTERMINAL { get; set; }
        public Int32? CSUCURSAL { get; set; }
        public Int32? COFICINA { get; set; }
        public String IPADRESS { get; set; }

        #endregion variables

        #region metodos

        public TSISTERMINAL Listar(string cterminal)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TSISTERMINAL obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT  ");
                query.Append(" CTERMINAL, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" IPADRESS ");
                query.Append(" FROM TSISTERMINAL ");
                query.Append(" WHERE 1 = 1 ");
                query.Append(" AND CTERMINAL = :CTERMINAL ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CTERMINAL", OracleDbType.Varchar2, cterminal, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TSISTERMINAL
                        {
                            CTERMINAL = reader["CTERMINAL"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            IPADRESS = reader["IPADRESS"].ToString()
                        };
                        break;
                    }
                }
                else
                {
                    obj = null;
                }

                #endregion ejecutaComando
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
    }
}
