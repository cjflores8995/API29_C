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
    public class VCUENTASPERSONA
    {
        #region variables

        public String CSUBSISTEMA { get; set; }
        public String CGRUPOPRODUCTO { get; set; }
        public String CPRODUCTO { get; set; }
        public String CESTATUSCUENTA { get; set; }
        public String CCONDICIONOPERATIVA { get; set; }
        public String CCUENTA { get; set; }
        public String NOMBRECUENTA { get; set; }
        public Int32? CPERSONA { get; set; }
        public String IDENTIFICACION { get; set; }
        public String NOMBRELEGAL { get; set; }
        public String CRELACIONPRODUCTO { get; set; }

        #endregion variables

        #region metodos

        public VCUENTASPERSONA ListarCuentasVistaSPI(string ccuenta, string identificacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            VCUENTASPERSONA obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CGRUPOPRODUCTO, ");
                query.Append(" CPRODUCTO, ");
                query.Append(" CESTATUSCUENTA, ");
                query.Append(" CCONDICIONOPERATIVA, ");
                query.Append(" CCUENTA, ");
                query.Append(" NOMBRECUENTA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" NOMBRELEGAL, ");
                query.Append(" CRELACIONPRODUCTO ");
                query.Append(" FROM VCUENTASPERSONA ");
                query.Append(" WHERE CCUENTA = :CCUENTA ");
                query.Append(" AND IDENTIFICACION = :IDENTIFICACION ");
                query.Append(" AND CSUBSISTEMA = '04' ");
                query.Append(" AND CGRUPOPRODUCTO IN ('01', '06', '08', '10') ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CCUENTA", OracleDbType.Varchar2, ccuenta, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, identificacion, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        obj = new VCUENTASPERSONA
                        {
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CGRUPOPRODUCTO = reader["CGRUPOPRODUCTO"].ToString(),
                            CPRODUCTO = reader["CPRODUCTO"].ToString(),
                            CESTATUSCUENTA = reader["CESTATUSCUENTA"].ToString(),
                            CCONDICIONOPERATIVA = reader["CCONDICIONOPERATIVA"].ToString(),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            NOMBRECUENTA = reader["NOMBRECUENTA"].ToString(),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            NOMBRELEGAL = reader["NOMBRELEGAL"].ToString(),
                            CRELACIONPRODUCTO = reader["CRELACIONPRODUCTO"].ToString()
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

        public List<VCUENTASPERSONA> ListarCuentasVistaBCE(string identificacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VCUENTASPERSONA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CGRUPOPRODUCTO, ");
                query.Append(" CPRODUCTO, ");
                query.Append(" CESTATUSCUENTA, ");
                query.Append(" CCONDICIONOPERATIVA, ");
                query.Append(" CCUENTA, ");
                query.Append(" NOMBRECUENTA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" NOMBRELEGAL, ");
                query.Append(" CRELACIONPRODUCTO ");
                query.Append(" FROM VCUENTASPERSONA ");
                query.Append(" WHERE IDENTIFICACION = :IDENTIFICACION ");
                query.Append(" AND CSUBSISTEMA = '04' ");
                query.Append(" AND CGRUPOPRODUCTO IN ('01', '06', '08') ");
                query.Append(" AND CESTATUSCUENTA IN ('002') ");
                query.Append(" AND CCONDICIONOPERATIVA IN ('NOR') ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, identificacion, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VCUENTASPERSONA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VCUENTASPERSONA
                        {
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CGRUPOPRODUCTO = reader["CGRUPOPRODUCTO"].ToString(),
                            CPRODUCTO = reader["CPRODUCTO"].ToString(),
                            CESTATUSCUENTA = reader["CESTATUSCUENTA"].ToString(),
                            CCONDICIONOPERATIVA = reader["CCONDICIONOPERATIVA"].ToString(),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            NOMBRECUENTA = reader["NOMBRECUENTA"].ToString(),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            NOMBRELEGAL = reader["NOMBRELEGAL"].ToString(),
                            CRELACIONPRODUCTO = reader["CRELACIONPRODUCTO"].ToString()
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

        public List<VCUENTASPERSONA> ListarCuentaPersona(string identificacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VCUENTASPERSONA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CGRUPOPRODUCTO, ");
                query.Append(" CPRODUCTO, ");
                query.Append(" CESTATUSCUENTA, ");
                query.Append(" CCONDICIONOPERATIVA, ");
                query.Append(" CCUENTA, ");
                query.Append(" NOMBRECUENTA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" NOMBRELEGAL, ");
                query.Append(" CRELACIONPRODUCTO ");
                query.Append(" FROM VCUENTASPERSONA ");
                query.Append(" WHERE IDENTIFICACION = :IDENTIFICACION ");
                query.Append(" AND CSUBSISTEMA = '04' ");
                query.Append(" AND CGRUPOPRODUCTO IN ('01', '06', '08', '10') ");
                query.Append(" AND CESTATUSCUENTA IN ('002') ");
                query.Append(" AND CCONDICIONOPERATIVA IN ('NOR',  'DEB') ");
                query.Append(" AND CRELACIONPRODUCTO IN ('PRI') ");
                query.Append(" ORDER BY CGRUPOPRODUCTO, CPRODUCTO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, identificacion, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VCUENTASPERSONA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VCUENTASPERSONA
                        {
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CGRUPOPRODUCTO = reader["CGRUPOPRODUCTO"].ToString(),
                            CPRODUCTO = reader["CPRODUCTO"].ToString(),
                            CESTATUSCUENTA = reader["CESTATUSCUENTA"].ToString(),
                            CCONDICIONOPERATIVA = reader["CCONDICIONOPERATIVA"].ToString(),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            NOMBRECUENTA = reader["NOMBRECUENTA"].ToString(),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            NOMBRELEGAL = reader["NOMBRELEGAL"].ToString(),
                            CRELACIONPRODUCTO = reader["CRELACIONPRODUCTO"].ToString()
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

        #endregion metodos
    }
}
