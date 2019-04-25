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
    public class VPRESTAMOSPERSONA
    {
        #region variables

        public Int32? CPERSONA { get; set; }
        public String IDENTIFICACION { get; set; }
        public String CSUBSISTEMA { get; set; }
        public String CGRUPOPRODUCTO { get; set; }
        public String CPRODUCTO { get; set; }
        public String CCUENTA { get; set; }
        public DateTime? FEMISION { get; set; }
        public Int32? DESMES { get; set; }
        public String CCUENTADEBITO { get; set; }
        public String ORIGEN { get; set; }

        #endregion variables

        #region metodos

        public List<VPRESTAMOSPERSONA> ListarPrestamos(Int32? cpersona)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VPRESTAMOSPERSONA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CGRUPOPRODUCTO, ");
                query.Append(" CPRODUCTO, ");
                query.Append(" CCUENTA, ");
                query.Append(" FEMISION, ");
                query.Append(" DESMES, ");
                query.Append(" CCUENTADEBITO, ");
                query.Append(" ORIGEN ");
                query.Append(" FROM VPRESTAMOSPERSONA ");
                query.Append(" WHERE CPERSONA = :CPERSONA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, cpersona, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VPRESTAMOSPERSONA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VPRESTAMOSPERSONA
                        {
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CGRUPOPRODUCTO = reader["CGRUPOPRODUCTO"].ToString(),
                            CPRODUCTO = reader["CPRODUCTO"].ToString(),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            FEMISION = Util.ConvertirFecha(reader["FEMISION"].ToString()),
                            DESMES = Util.ConvertirNumero(reader["DESMES"].ToString()),
                            CCUENTADEBITO = reader["CCUENTADEBITO"].ToString(),
                            ORIGEN = reader["ORIGEN"].ToString()
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

        public List<VPRESTAMOSPERSONA> ListarPrestamos(string credito)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VPRESTAMOSPERSONA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CGRUPOPRODUCTO, ");
                query.Append(" CPRODUCTO, ");
                query.Append(" CCUENTA, ");
                query.Append(" FEMISION, ");
                query.Append(" DESMES, ");
                query.Append(" CCUENTADEBITO, ");
                query.Append(" ORIGEN ");
                query.Append(" FROM VPRESTAMOSPERSONA ");
                query.Append(" WHERE CCUENTA = :CCUENTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CCUENTA", OracleDbType.Varchar2, credito, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VPRESTAMOSPERSONA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VPRESTAMOSPERSONA
                        {
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CGRUPOPRODUCTO = reader["CGRUPOPRODUCTO"].ToString(),
                            CPRODUCTO = reader["CPRODUCTO"].ToString(),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            FEMISION = Util.ConvertirFecha(reader["FEMISION"].ToString()),
                            DESMES = Util.ConvertirNumero(reader["DESMES"].ToString()),
                            CCUENTADEBITO = reader["CCUENTADEBITO"].ToString(),
                            ORIGEN = reader["ORIGEN"].ToString()
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
