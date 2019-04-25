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
    public class TCUENTACHEQUESLOCALES
    {

        #region propiedades
        public String CUENTAGIRADA { get; set; }
        public Int32? NUMEROCHEQUE { get; set; }
        public String RUTATRANSITO { get; set; }
        public String PARTICION { get; set; }
        public DateTime? FCARGA { get; set; }
        public DateTime? FREAL { get; set; }
        public DateTime? FCONTABLEDEPOSITO { get; set; }
        public Int32? CPERSONA_COMPANIA { get; set; }
        public String CCUENTA { get; set; }
        public Int32? COFICINA { get; set; }
        public Int32? CSUCURSAL { get; set; }
        public Decimal? VALORCHEQUE { get; set; }
        public DateTime? FALIBERAR { get; set; }
        public DateTime? FCONFIRMACION { get; set; }
        public String CONFIRMADO { get; set; }
        public DateTime? FDEVOLUCION { get; set; }
        public String DEVUELTO { get; set; }
        public String CODIGOINSTITUCION { get; set; }
        public DateTime? FECHALOTE_CAMARA { get; set; }
        public Int32? NUMEROLOTE_CAMARA { get; set; }
        public Int32? SECUENCIALOTE_CAMARA { get; set; }
        public Int32? CTIPOCUENTACAMARA { get; set; }
        public String NUMEROMENSAJE { get; set; }
        public String TIPODOCUMENTO { get; set; }
        public String INDICADORDEPOSITO { get; set; }
        public String ESTADOPROCESO { get; set; }
        public String CESTATUSCHEQUE { get; set; }
        public Int32? CMOTIVOESTATUSCHEQUE { get; set; }
        public String CSUBSISTEMA_TRANSACCION { get; set; }
        public String CTRANSACCION { get; set; }
        public String VERSIONTRANSACCION { get; set; }
        public Int32? CORDENDEPOSITO { get; set; }
        public String CPERIODO { get; set; }
        public Int32? CORDENDEVOLUCION { get; set; }
        public DateTime? FCHEQUE { get; set; }
        public String BENEFICIARIO { get; set; }
        #endregion propiedades

        #region metodos

        public TCUENTACHEQUESLOCALES ConsultaCheque(string cuentaGirada, Int32 numeroCheque, string ccuenta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TCUENTACHEQUESLOCALES obj = null;

            try
            {
                #region arma comando

                query.Append(" SELECT ");
                query.Append(" CCUENTA, ");
                query.Append(" RUTATRANSITO, ");
                query.Append(" CUENTAGIRADA, ");
                query.Append(" NUMEROCHEQUE, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" VALORCHEQUE, ");
                query.Append(" FCONTABLEDEPOSITO, ");
                query.Append(" FCARGA, ");
                query.Append(" FALIBERAR, ");
                query.Append(" PARTICION, ");
                query.Append(" FREAL, ");
                query.Append(" CPERSONA_COMPANIA ");
                query.Append(" FROM TCUENTACHEQUESLOCALES ");
                query.Append(" WHERE TO_NUMBER(CUENTAGIRADA) = :CUENTAGIRADA ");
                query.Append(" AND NUMEROCHEQUE = :NUMEROCHEQUE ");
                query.Append(" AND CCUENTA = :CCUENTA ");
                query.Append(" AND CONFIRMADO = '0' ");
                query.Append(" AND DEVUELTO = '0' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CUENTAGIRADA", OracleDbType.Varchar2, cuentaGirada, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROCHEQUE", OracleDbType.Int32, numeroCheque, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCUENTA", OracleDbType.Varchar2, ccuenta, ParameterDirection.Input));

                #endregion arma comando

                #region ejecuta comando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TCUENTACHEQUESLOCALES
                        {
                            CCUENTA = reader["CCUENTA"].ToString(),
                            RUTATRANSITO = reader["RUTATRANSITO"].ToString(),
                            CUENTAGIRADA = reader["CUENTAGIRADA"].ToString(),
                            NUMEROCHEQUE = Util.ConvertirNumero(reader["NUMEROCHEQUE"].ToString()),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            VALORCHEQUE = Util.ConvertirDecimal(reader["VALORCHEQUE"].ToString()),
                            FCONTABLEDEPOSITO = Util.ConvertirFecha(reader["FCONTABLEDEPOSITO"].ToString()),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FALIBERAR = Util.ConvertirFecha(reader["FALIBERAR"].ToString()),
                            PARTICION = reader["PARTICION"].ToString(),
                            FREAL = Util.ConvertirFecha(reader["FREAL"].ToString()),
                            CPERSONA_COMPANIA = Util.ConvertirNumero(reader["CPERSONA_COMPANIA"].ToString())
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

        //query.Append(" CUENTAGIRADA, ");
        //query.Append(" NUMEROCHEQUE, ");
        //query.Append(" RUTATRANSITO, ");
        //query.Append(" PARTICION, ");
        //query.Append(" FCARGA, ");
        //query.Append(" FREAL, ");
        //query.Append(" FCONTABLEDEPOSITO, ");
        //query.Append(" CPERSONA_COMPANIA, ");
        //query.Append(" CCUENTA, ");
        //query.Append(" COFICINA, ");
        //query.Append(" CSUCURSAL, ");
        //query.Append(" VALORCHEQUE, ");
        //query.Append(" FALIBERAR, ");
        //query.Append(" FCONFIRMACION, ");
        //query.Append(" CONFIRMADO, ");
        //query.Append(" FDEVOLUCION, ");
        //query.Append(" DEVUELTO, ");
        //query.Append(" CODIGOINSTITUCION, ");
        //query.Append(" FECHALOTE_CAMARA, ");
        //query.Append(" NUMEROLOTE_CAMARA, ");
        //query.Append(" SECUENCIALOTE_CAMARA, ");
        //query.Append(" CTIPOCUENTACAMARA, ");
        //query.Append(" NUMEROMENSAJE, ");
        //query.Append(" TIPODOCUMENTO, ");
        //query.Append(" INDICADORDEPOSITO, ");
        //query.Append(" ESTADOPROCESO, ");
        //query.Append(" CESTATUSCHEQUE, ");
        //query.Append(" CMOTIVOESTATUSCHEQUE, ");
        //query.Append(" CSUBSISTEMA_TRANSACCION, ");
        //query.Append(" CTRANSACCION, ");
        //query.Append(" VERSIONTRANSACCION, ");
        //query.Append(" CORDENDEPOSITO, ");
        //query.Append(" CPERIODO, ");
        //query.Append(" CORDENDEVOLUCION, ");
        //query.Append(" FCHEQUE, ");
        //query.Append(" BENEFICIARIO, ");

        //CUENTAGIRADA = reader["CUENTAGIRADA"].ToString(),
        //NUMEROCHEQUE = ado.ConvertirNumero(reader["NUMEROCHEQUE"].ToString()),
        //RUTATRANSITO = reader["RUTATRANSITO"].ToString(),
        //PARTICION = reader["PARTICION"].ToString(),
        //FCARGA = ado.ConvertirFecha(reader["FCARGA"].ToString()),
        //FREAL = ado.ConvertirFecha(reader["FREAL"].ToString()),
        //FCONTABLEDEPOSITO = ado.ConvertirFecha(reader["FCONTABLEDEPOSITO"].ToString()),
        //CPERSONA_COMPANIA = ado.ConvertirNumero(reader["CPERSONA_COMPANIA"].ToString()),
        //CCUENTA = reader["CCUENTA"].ToString(),
        //COFICINA = ado.ConvertirNumero(reader["COFICINA"].ToString()),
        //CSUCURSAL = ado.ConvertirNumero(reader["CSUCURSAL"].ToString()),
        //VALORCHEQUE = ado.ConvertirDecimal(reader["VALORCHEQUE"].ToString()),
        //FALIBERAR = ado.ConvertirFecha(reader["FALIBERAR"].ToString()),
        //FCONFIRMACION = ado.ConvertirFecha(reader["FCONFIRMACION"].ToString()),
        //CONFIRMADO = reader["CONFIRMADO"].ToString(),
        //FDEVOLUCION = ado.ConvertirFecha(reader["FDEVOLUCION"].ToString()),
        //DEVUELTO = reader["DEVUELTO"].ToString(),
        //CODIGOINSTITUCION = reader["CODIGOINSTITUCION"].ToString(),
        //FECHALOTE_CAMARA = ado.ConvertirFecha(reader["FECHALOTE_CAMARA"].ToString()),
        //NUMEROLOTE_CAMARA = ado.ConvertirNumero(reader["NUMEROLOTE_CAMARA"].ToString()),
        //SECUENCIALOTE_CAMARA = ado.ConvertirNumero(reader["SECUENCIALOTE_CAMARA"].ToString()),
        //CTIPOCUENTACAMARA = ado.ConvertirNumero(reader["CTIPOCUENTACAMARA"].ToString()),
        //NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
        //TIPODOCUMENTO = reader["TIPODOCUMENTO"].ToString(),
        //INDICADORDEPOSITO = reader["INDICADORDEPOSITO"].ToString(),
        //ESTADOPROCESO = reader["ESTADOPROCESO"].ToString(),
        //CESTATUSCHEQUE = reader["CESTATUSCHEQUE"].ToString(),
        //CMOTIVOESTATUSCHEQUE = ado.ConvertirNumero(reader["CMOTIVOESTATUSCHEQUE"].ToString()),
        //CSUBSISTEMA_TRANSACCION = reader["CSUBSISTEMA_TRANSACCION"].ToString(),
        //CTRANSACCION = reader["CTRANSACCION"].ToString(),
        //VERSIONTRANSACCION = reader["VERSIONTRANSACCION"].ToString(),
        //CORDENDEPOSITO = ado.ConvertirNumero(reader["CORDENDEPOSITO"].ToString()),
        //CPERIODO = reader["CPERIODO"].ToString(),
        //CORDENDEVOLUCION = ado.ConvertirNumero(reader["CORDENDEVOLUCION"].ToString()),
        //FCHEQUE = ado.ConvertirFecha(reader["FCHEQUE"].ToString()),
        //BENEFICIARIO = reader["BENEFICIARIO"].ToString(),

        //query.Append(" CUENTAGIRADA = :CUENTAGIRADA, ");
        //query.Append(" NUMEROCHEQUE = :NUMEROCHEQUE, ");
        //query.Append(" RUTATRANSITO = :RUTATRANSITO, ");
        //query.Append(" PARTICION = :PARTICION, ");
        //query.Append(" FCARGA = :FCARGA, ");
        //query.Append(" FREAL = :FREAL, ");
        //query.Append(" FCONTABLEDEPOSITO = :FCONTABLEDEPOSITO, ");
        //query.Append(" CPERSONA_COMPANIA = :CPERSONA_COMPANIA, ");
        //query.Append(" CCUENTA = :CCUENTA, ");
        //query.Append(" COFICINA = :COFICINA, ");
        //query.Append(" CSUCURSAL = :CSUCURSAL, ");
        //query.Append(" VALORCHEQUE = :VALORCHEQUE, ");
        //query.Append(" FALIBERAR = :FALIBERAR, ");
        //query.Append(" FCONFIRMACION = :FCONFIRMACION, ");
        //query.Append(" CONFIRMADO = :CONFIRMADO, ");
        //query.Append(" FDEVOLUCION = :FDEVOLUCION, ");
        //query.Append(" DEVUELTO = :DEVUELTO, ");
        //query.Append(" CODIGOINSTITUCION = :CODIGOINSTITUCION, ");
        //query.Append(" FECHALOTE_CAMARA = :FECHALOTE_CAMARA, ");
        //query.Append(" NUMEROLOTE_CAMARA = :NUMEROLOTE_CAMARA, ");
        //query.Append(" SECUENCIALOTE_CAMARA = :SECUENCIALOTE_CAMARA, ");
        //query.Append(" CTIPOCUENTACAMARA = :CTIPOCUENTACAMARA, ");
        //query.Append(" NUMEROMENSAJE = :NUMEROMENSAJE, ");
        //query.Append(" TIPODOCUMENTO = :TIPODOCUMENTO, ");
        //query.Append(" INDICADORDEPOSITO = :INDICADORDEPOSITO, ");
        //query.Append(" ESTADOPROCESO = :ESTADOPROCESO, ");
        //query.Append(" CESTATUSCHEQUE = :CESTATUSCHEQUE, ");
        //query.Append(" CMOTIVOESTATUSCHEQUE = :CMOTIVOESTATUSCHEQUE, ");
        //query.Append(" CSUBSISTEMA_TRANSACCION = :CSUBSISTEMA_TRANSACCION, ");
        //query.Append(" CTRANSACCION = :CTRANSACCION, ");
        //query.Append(" VERSIONTRANSACCION = :VERSIONTRANSACCION, ");
        //query.Append(" CORDENDEPOSITO = :CORDENDEPOSITO, ");
        //query.Append(" CPERIODO = :CPERIODO, ");
        //query.Append(" CORDENDEVOLUCION = :CORDENDEVOLUCION, ");
        //query.Append(" FCHEQUE = :FCHEQUE, ");
        //query.Append(" BENEFICIARIO = :BENEFICIARIO, ");

        //comando.Parameters.Add(new OracleParameter("CUENTAGIRADA", OracleDbType.Varchar2, obj.CUENTAGIRADA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMEROCHEQUE", OracleDbType.Int32, obj.NUMEROCHEQUE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("RUTATRANSITO", OracleDbType.Varchar2, obj.RUTATRANSITO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("PARTICION", OracleDbType.Varchar2, obj.PARTICION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FREAL", OracleDbType.Date, obj.FREAL, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FCONTABLEDEPOSITO", OracleDbType.Date, obj.FCONTABLEDEPOSITO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CPERSONA_COMPANIA", OracleDbType.Int32, obj.CPERSONA_COMPANIA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CCUENTA", OracleDbType.Varchar2, obj.CCUENTA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("COFICINA", OracleDbType.Int32, obj.COFICINA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CSUCURSAL", OracleDbType.Int32, obj.CSUCURSAL, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("VALORCHEQUE", OracleDbType.Decimal, obj.VALORCHEQUE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FALIBERAR", OracleDbType.Date, obj.FALIBERAR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FCONFIRMACION", OracleDbType.Date, obj.FCONFIRMACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CONFIRMADO", OracleDbType.Varchar2, obj.CONFIRMADO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FDEVOLUCION", OracleDbType.Date, obj.FDEVOLUCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("DEVUELTO", OracleDbType.Varchar2, obj.DEVUELTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CODIGOINSTITUCION", OracleDbType.Varchar2, obj.CODIGOINSTITUCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FECHALOTE_CAMARA", OracleDbType.Date, obj.FECHALOTE_CAMARA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMEROLOTE_CAMARA", OracleDbType.Int32, obj.NUMEROLOTE_CAMARA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("SECUENCIALOTE_CAMARA", OracleDbType.Int32, obj.SECUENCIALOTE_CAMARA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CTIPOCUENTACAMARA", OracleDbType.Int32, obj.CTIPOCUENTACAMARA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TIPODOCUMENTO", OracleDbType.Varchar2, obj.TIPODOCUMENTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("INDICADORDEPOSITO", OracleDbType.Varchar2, obj.INDICADORDEPOSITO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ESTADOPROCESO", OracleDbType.Varchar2, obj.ESTADOPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CESTATUSCHEQUE", OracleDbType.Varchar2, obj.CESTATUSCHEQUE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CMOTIVOESTATUSCHEQUE", OracleDbType.Int32, obj.CMOTIVOESTATUSCHEQUE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CSUBSISTEMA_TRANSACCION", OracleDbType.Varchar2, obj.CSUBSISTEMA_TRANSACCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CTRANSACCION", OracleDbType.Varchar2, obj.CTRANSACCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("VERSIONTRANSACCION", OracleDbType.Varchar2, obj.VERSIONTRANSACCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CORDENDEPOSITO", OracleDbType.Int32, obj.CORDENDEPOSITO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CPERIODO", OracleDbType.Varchar2, obj.CPERIODO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CORDENDEVOLUCION", OracleDbType.Int32, obj.CORDENDEVOLUCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FCHEQUE", OracleDbType.Date, obj.FCHEQUE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("BENEFICIARIO", OracleDbType.Varchar2, obj.BENEFICIARIO, ParameterDirection.Input));

    }
}
