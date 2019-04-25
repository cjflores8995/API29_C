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
    public class VPOSCONVENIO
    {
        #region variables

        public Int32? CCONVENIO { get; set; }
        public String IDENTIFICACION { get; set; }
        public String NOMBRE { get; set; }
        public String COMPENSA { get; set; }
        public String NOTIFICA { get; set; }
        public Decimal? COMISION { get; set; }
        public String ARCHIVOORIGEN { get; set; }
        public String ARCHIVORETRONO { get; set; }
        public String NOTIFICATIPO { get; set; }
        public String NOTIFICAPARAMETROS { get; set; }
        public String TIPOLIQUIDACION { get; set; }
        public String TIPOPAGO { get; set; }
        public String CUENTADEBITO { get; set; }
        public String CUENTACREDITO { get; set; }
        public String CUENTACREDITOTIPO { get; set; }
        public Int32? CUENTACREDITOBANCO { get; set; }
        public String CUENTACREDITOBANCONOMBRE { get; set; }
        public String CUENTACREDITOBANCOBCE { get; set; }
        public Int32? EXISTEARCHIVO { get; set; }
        public String COMANDOCALCULO { get; set; }
        public String COMANDOTRANSACCION { get; set; }
        public String COMANDOARCHIVO { get; set; }

        #endregion variables

        #region metodos

        public List<VPOSCONVENIO> ListarTodos()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VPOSCONVENIO> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CCONVENIO, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" NOMBRE, ");
                query.Append(" COMPENSA, ");
                query.Append(" NOTIFICA, ");
                query.Append(" COMISION, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORETRONO, ");
                query.Append(" NOTIFICATIPO, ");
                query.Append(" NOTIFICAPARAMETROS, ");
                query.Append(" TIPOLIQUIDACION, ");
                query.Append(" TIPOPAGO, ");
                query.Append(" CUENTADEBITO, ");
                query.Append(" CUENTACREDITO, ");
                query.Append(" CUENTACREDITOTIPO, ");
                query.Append(" CUENTACREDITOBANCO, ");
                query.Append(" CUENTACREDITOBANCONOMBRE, ");
                query.Append(" CUENTACREDITOBANCOBCE, ");
                query.Append(" EXISTEARCHIVO, ");
                query.Append(" COMANDOCALCULO, ");
                query.Append(" COMANDOTRANSACCION, ");
                query.Append(" COMANDOARCHIVO ");
                query.Append(" FROM VPOSCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VPOSCONVENIO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VPOSCONVENIO
                        {
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            COMPENSA = reader["COMPENSA"].ToString(),
                            NOTIFICA = reader["NOTIFICA"].ToString(),
                            COMISION = Util.ConvertirDecimal(reader["COMISION"].ToString()),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORETRONO = reader["ARCHIVORETRONO"].ToString(),
                            NOTIFICATIPO = reader["NOTIFICATIPO"].ToString(),
                            NOTIFICAPARAMETROS = reader["NOTIFICAPARAMETROS"].ToString(),
                            TIPOLIQUIDACION = reader["TIPOLIQUIDACION"].ToString(),
                            TIPOPAGO = reader["TIPOPAGO"].ToString(),
                            CUENTADEBITO = reader["CUENTADEBITO"].ToString(),
                            CUENTACREDITO = reader["CUENTACREDITO"].ToString(),
                            CUENTACREDITOTIPO = reader["CUENTACREDITOTIPO"].ToString(),
                            CUENTACREDITOBANCO = Util.ConvertirNumero(reader["CUENTACREDITOBANCO"].ToString()),
                            CUENTACREDITOBANCONOMBRE = reader["CUENTACREDITOBANCONOMBRE"].ToString(),
                            CUENTACREDITOBANCOBCE = reader["CUENTACREDITOBANCOBCE"].ToString(),
                            EXISTEARCHIVO = Util.ConvertirNumero(reader["EXISTEARCHIVO"].ToString()),
                            COMANDOCALCULO = reader["COMANDOCALCULO"].ToString(),
                            COMANDOTRANSACCION = reader["COMANDOTRANSACCION"].ToString(),
                            COMANDOARCHIVO = reader["COMANDOARCHIVO"].ToString()
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

        public List<VPOSCONVENIO> ListarConveniosCompensar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VPOSCONVENIO> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CCONVENIO, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" NOMBRE, ");
                query.Append(" COMPENSA, ");
                query.Append(" NOTIFICA, ");
                query.Append(" COMISION, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORETRONO, ");
                query.Append(" NOTIFICATIPO, ");
                query.Append(" NOTIFICAPARAMETROS, ");
                query.Append(" TIPOLIQUIDACION, ");
                query.Append(" TIPOPAGO, ");
                query.Append(" CUENTADEBITO, ");
                query.Append(" CUENTACREDITO, ");
                query.Append(" CUENTACREDITOTIPO, ");
                query.Append(" CUENTACREDITOBANCO, ");
                query.Append(" CUENTACREDITOBANCONOMBRE, ");
                query.Append(" CUENTACREDITOBANCOBCE, ");
                query.Append(" EXISTEARCHIVO, ");
                query.Append(" COMANDOCALCULO, ");
                query.Append(" COMANDOTRANSACCION, ");
                query.Append(" COMANDOARCHIVO ");
                query.Append(" FROM VPOSCONVENIO ");
                query.Append(" WHERE COMPENSA = '1' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VPOSCONVENIO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VPOSCONVENIO
                        {
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            COMPENSA = reader["COMPENSA"].ToString(),
                            NOTIFICA = reader["NOTIFICA"].ToString(),
                            COMISION = Util.ConvertirDecimal(reader["COMISION"].ToString()),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORETRONO = reader["ARCHIVORETRONO"].ToString(),
                            NOTIFICATIPO = reader["NOTIFICATIPO"].ToString(),
                            NOTIFICAPARAMETROS = reader["NOTIFICAPARAMETROS"].ToString(),
                            TIPOLIQUIDACION = reader["TIPOLIQUIDACION"].ToString(),
                            TIPOPAGO = reader["TIPOPAGO"].ToString(),
                            CUENTADEBITO = reader["CUENTADEBITO"].ToString(),
                            CUENTACREDITO = reader["CUENTACREDITO"].ToString(),
                            CUENTACREDITOTIPO = reader["CUENTACREDITOTIPO"].ToString(),
                            CUENTACREDITOBANCO = Util.ConvertirNumero(reader["CUENTACREDITOBANCO"].ToString()),
                            CUENTACREDITOBANCONOMBRE = reader["CUENTACREDITOBANCONOMBRE"].ToString(),
                            CUENTACREDITOBANCOBCE = reader["CUENTACREDITOBANCOBCE"].ToString(),
                            EXISTEARCHIVO = Util.ConvertirNumero(reader["EXISTEARCHIVO"].ToString()),
                            COMANDOCALCULO = reader["COMANDOCALCULO"].ToString(),
                            COMANDOTRANSACCION = reader["COMANDOTRANSACCION"].ToString(),
                            COMANDOARCHIVO = reader["COMANDOARCHIVO"].ToString()
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

        public VPOSCONVENIO ListarXConvenio(Int32 cconvenio)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            VPOSCONVENIO obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CCONVENIO, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" NOMBRE, ");
                query.Append(" COMPENSA, ");
                query.Append(" NOTIFICA, ");
                query.Append(" COMISION, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORETRONO, ");
                query.Append(" NOTIFICATIPO, ");
                query.Append(" NOTIFICAPARAMETROS, ");
                query.Append(" TIPOLIQUIDACION, ");
                query.Append(" TIPOPAGO, ");
                query.Append(" CUENTADEBITO, ");
                query.Append(" CUENTACREDITO, ");
                query.Append(" CUENTACREDITOTIPO, ");
                query.Append(" CUENTACREDITOBANCO, ");
                query.Append(" CUENTACREDITOBANCONOMBRE, ");
                query.Append(" CUENTACREDITOBANCOBCE, ");
                query.Append(" EXISTEARCHIVO, ");
                query.Append(" COMANDOCALCULO, ");
                query.Append(" COMANDOTRANSACCION, ");
                query.Append(" COMANDOARCHIVO ");
                query.Append(" FROM VPOSCONVENIO ");
                query.Append(" WHERE CCONVENIO = :CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, cconvenio, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new VPOSCONVENIO
                        {
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            COMPENSA = reader["COMPENSA"].ToString(),
                            NOTIFICA = reader["NOTIFICA"].ToString(),
                            COMISION = Util.ConvertirDecimal(reader["COMISION"].ToString()),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORETRONO = reader["ARCHIVORETRONO"].ToString(),
                            NOTIFICATIPO = reader["NOTIFICATIPO"].ToString(),
                            NOTIFICAPARAMETROS = reader["NOTIFICAPARAMETROS"].ToString(),
                            TIPOLIQUIDACION = reader["TIPOLIQUIDACION"].ToString(),
                            TIPOPAGO = reader["TIPOPAGO"].ToString(),
                            CUENTADEBITO = reader["CUENTADEBITO"].ToString(),
                            CUENTACREDITO = reader["CUENTACREDITO"].ToString(),
                            CUENTACREDITOTIPO = reader["CUENTACREDITOTIPO"].ToString(),
                            CUENTACREDITOBANCO = Util.ConvertirNumero(reader["CUENTACREDITOBANCO"].ToString()),
                            CUENTACREDITOBANCONOMBRE = reader["CUENTACREDITOBANCONOMBRE"].ToString(),
                            CUENTACREDITOBANCOBCE = reader["CUENTACREDITOBANCOBCE"].ToString(),
                            EXISTEARCHIVO = Util.ConvertirNumero(reader["EXISTEARCHIVO"].ToString()),
                            COMANDOCALCULO = reader["COMANDOCALCULO"].ToString(),
                            COMANDOTRANSACCION = reader["COMANDOTRANSACCION"].ToString(),
                            COMANDOARCHIVO = reader["COMANDOARCHIVO"].ToString()
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

        //query.Append(" CCONVENIO, ");
        //query.Append(" IDENTIFICACION, ");
        //query.Append(" NOMBRE, ");
        //query.Append(" COMPENSA, ");
        //query.Append(" NOTIFICA, ");
        //query.Append(" COMISION, ");
        //query.Append(" ARCHIVOORIGEN, ");
        //query.Append(" ARCHIVORETRONO, ");
        //query.Append(" NOTIFICATIPO, ");
        //query.Append(" NOTIFICAPARAMETROS, ");
        //query.Append(" TIPOLIQUIDACION, ");
        //query.Append(" CUENTADEBITO, ");
        //query.Append(" CUENTACREDITO, ");
        //query.Append(" CUENTACREDITOTIPO, ");
        //query.Append(" CUENTACREDITOBANCO, ");
        //query.Append(" CUENTACREDITOBANCONOMBRE, ");
        //query.Append(" CUENTACREDITOBANCOBCE, ");
        //query.Append(" EXISTEARCHIVO, ");
        //query.Append(" COMANDOCALCULO, ");
        //query.Append(" COMANDOTRANSACCION, ");
        //query.Append(" COMANDOARCHIVO, ");

        //CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
        //IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
        //NOMBRE = reader["NOMBRE"].ToString(),
        //COMPENSA = reader["COMPENSA"].ToString(),
        //NOTIFICA = reader["NOTIFICA"].ToString(),
        //COMISION = Util.ConvertirDecimal(reader["COMISION"].ToString()),
        //ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
        //ARCHIVORETRONO = reader["ARCHIVORETRONO"].ToString(),
        //NOTIFICATIPO = reader["NOTIFICATIPO"].ToString(),
        //NOTIFICAPARAMETROS = reader["NOTIFICAPARAMETROS"].ToString(),
        //TIPOLIQUIDACION = reader["TIPOLIQUIDACION"].ToString(),
        //CUENTADEBITO = reader["CUENTADEBITO"].ToString(),
        //CUENTACREDITO = reader["CUENTACREDITO"].ToString(),
        //CUENTACREDITOTIPO = reader["CUENTACREDITOTIPO"].ToString(),
        //CUENTACREDITOBANCO = Util.ConvertirNumero(reader["CUENTACREDITOBANCO"].ToString()),
        //CUENTACREDITOBANCONOMBRE = reader["CUENTACREDITOBANCONOMBRE"].ToString(),
        //CUENTACREDITOBANCOBCE = reader["CUENTACREDITOBANCOBCE"].ToString(),
        //EXISTEARCHIVO = Util.ConvertirNumero(reader["EXISTEARCHIVO"].ToString()),
        //COMANDOCALCULO = reader["COMANDOCALCULO"].ToString(),
        //COMANDOTRANSACCION = reader["COMANDOTRANSACCION"].ToString(),
        //COMANDOARCHIVO = reader["COMANDOARCHIVO"].ToString(),

        //query.Append(" CCONVENIO = :CCONVENIO, ");
        //query.Append(" IDENTIFICACION = :IDENTIFICACION, ");
        //query.Append(" NOMBRE = :NOMBRE, ");
        //query.Append(" COMPENSA = :COMPENSA, ");
        //query.Append(" NOTIFICA = :NOTIFICA, ");
        //query.Append(" COMISION = :COMISION, ");
        //query.Append(" ARCHIVOORIGEN = :ARCHIVOORIGEN, ");
        //query.Append(" ARCHIVORETRONO = :ARCHIVORETRONO, ");
        //query.Append(" NOTIFICATIPO = :NOTIFICATIPO, ");
        //query.Append(" NOTIFICAPARAMETROS = :NOTIFICAPARAMETROS, ");
        //query.Append(" TIPOLIQUIDACION = :TIPOLIQUIDACION, ");
        //query.Append(" CUENTADEBITO = :CUENTADEBITO, ");
        //query.Append(" CUENTACREDITO = :CUENTACREDITO, ");
        //query.Append(" CUENTACREDITOTIPO = :CUENTACREDITOTIPO, ");
        //query.Append(" CUENTACREDITOBANCO = :CUENTACREDITOBANCO, ");
        //query.Append(" CUENTACREDITOBANCONOMBRE = :CUENTACREDITOBANCONOMBRE, ");
        //query.Append(" CUENTACREDITOBANCOBCE = :CUENTACREDITOBANCOBCE, ");
        //query.Append(" EXISTEARCHIVO = :EXISTEARCHIVO, ");
        //query.Append(" COMANDOCALCULO = :COMANDOCALCULO, ");
        //query.Append(" COMANDOTRANSACCION = :COMANDOTRANSACCION, ");
        //query.Append(" COMANDOARCHIVO = :COMANDOARCHIVO, ");

    }
}
