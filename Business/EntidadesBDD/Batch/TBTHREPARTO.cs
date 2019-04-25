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
    public class TBTHREPARTO
    {
        #region variables

        public String CREPARTO { get; set; }
        public String DESCRIPCION { get; set; }
        public Int32? CSUCURSAL { get; set; }
        public Int32? COFICINA { get; set; }
        public String ACTIVO { get; set; }

        #endregion variables

        #region metodos

        public List<TBTHREPARTO> Listar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHREPARTO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" CREPARTO, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" ACTIVO ");
                query.Append(" FROM TBTHREPARTO ");
                query.Append(" WHERE ACTIVO = '1' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHREPARTO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHREPARTO
                        {
                            CREPARTO = reader["CREPARTO"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            ACTIVO = reader["ACTIVO"].ToString()
                        });
                    }
                }
                else
                {
                    ltObj = null;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                ltObj = null;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return ltObj;
        }

        public TBTHREPARTO ListarXCodigo(string creparto)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TBTHREPARTO obj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" CREPARTO, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" ACTIVO ");
                query.Append(" FROM TBTHREPARTO ");
                query.Append(" WHERE CREPARTO = :CREPARTO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CREPARTO", OracleDbType.Varchar2, creparto, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        obj = new TBTHREPARTO
                        {
                            CREPARTO = reader["CREPARTO"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            ACTIVO = reader["ACTIVO"].ToString()
                        };
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
