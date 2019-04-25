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
    public class TCOSPAGOS
    {
        #region variables

        public String IDCOSEDE { get; set; }
        public String IDENTIFICACION { get; set; }
        public String TIPOIDENTIFICACION { get; set; }
        public String NOMBRE { get; set; }
        public String COMPROBANTE { get; set; }
        public String INSTITUCION { get; set; }
        public Decimal? MONTO { get; set; }
        public String DIRECCION { get; set; }
        public String TELEFONO1 { get; set; }
        public String TELEFONO2 { get; set; }
        public String CORREO { get; set; }
        public String REFERENCIA { get; set; }
        public DateTime? FORDENPAGO { get; set; }
        public DateTime? FPAGOCOSEDE { get; set; }
        public DateTime? FPAGOREAL { get; set; }
        public String CUSUARIOPAGO { get; set; }
        public Int32? CSUCURSAL { get; set; }
        public Int32? COFICINA { get; set; }
        public String CTERMINAL { get; set; }

        public string lnk { get; set; }

        #endregion variables

        #region metodos

        public bool Insertar(TCOSPAGOS obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TCOSPAGOS ( ");

                query.Append(" IDCOSEDE, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" TIPOIDENTIFICACION, ");
                query.Append(" NOMBRE, ");
                query.Append(" COMPROBANTE, ");
                query.Append(" INSTITUCION, ");
                query.Append(" MONTO, ");
                query.Append(" DIRECCION, ");
                query.Append(" TELEFONO1, ");
                query.Append(" TELEFONO2, ");
                query.Append(" CORREO, ");
                query.Append(" REFERENCIA, ");
                query.Append(" FORDENPAGO, ");
                query.Append(" FPAGOCOSEDE, ");
                query.Append(" FPAGOREAL, ");
                query.Append(" CUSUARIOPAGO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CTERMINAL ");

                query.Append(" ) VALUES ( ");

                query.Append(" :IDCOSEDE, ");
                query.Append(" :IDENTIFICACION, ");
                query.Append(" :TIPOIDENTIFICACION, ");
                query.Append(" :NOMBRE, ");
                query.Append(" :COMPROBANTE, ");
                query.Append(" :INSTITUCION, ");
                query.Append(" :MONTO, ");
                query.Append(" :DIRECCION, ");
                query.Append(" :TELEFONO1, ");
                query.Append(" :TELEFONO2, ");
                query.Append(" :CORREO, ");
                query.Append(" :REFERENCIA, ");
                query.Append(" :FORDENPAGO, ");
                query.Append(" :FPAGOCOSEDE, ");
                query.Append(" :FPAGOREAL, ");
                query.Append(" :CUSUARIOPAGO, ");
                query.Append(" :CSUCURSAL, ");
                query.Append(" :COFICINA, ");
                query.Append(" :CTERMINAL ");

                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("IDCOSEDE", OracleDbType.Varchar2, obj.IDCOSEDE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, obj.IDENTIFICACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TIPOIDENTIFICACION", OracleDbType.Varchar2, obj.TIPOIDENTIFICACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NOMBRE", OracleDbType.Varchar2, obj.NOMBRE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("COMPROBANTE", OracleDbType.Varchar2, obj.COMPROBANTE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("INSTITUCION", OracleDbType.Varchar2, obj.INSTITUCION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("MONTO", OracleDbType.Decimal, obj.MONTO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DIRECCION", OracleDbType.Varchar2, obj.DIRECCION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TELEFONO1", OracleDbType.Varchar2, obj.TELEFONO1, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TELEFONO2", OracleDbType.Varchar2, obj.TELEFONO2, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CORREO", OracleDbType.Varchar2, obj.CORREO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("REFERENCIA", OracleDbType.Varchar2, obj.REFERENCIA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FORDENPAGO", OracleDbType.Date, obj.FORDENPAGO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPAGOCOSEDE", OracleDbType.Date, obj.FPAGOCOSEDE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPAGOREAL", OracleDbType.Date, obj.FPAGOREAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIOPAGO", OracleDbType.Varchar2, obj.CUSUARIOPAGO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUCURSAL", OracleDbType.Int32, obj.CSUCURSAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("COFICINA", OracleDbType.Int32, obj.COFICINA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CTERMINAL", OracleDbType.Varchar2, obj.CTERMINAL, ParameterDirection.Input));
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

        public TCOSPAGOS ListarPagoUnitario(string idcosede, string identificacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TCOSPAGOS obj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" IDCOSEDE, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" TIPOIDENTIFICACION, ");
                query.Append(" NOMBRE, ");
                query.Append(" COMPROBANTE, ");
                query.Append(" INSTITUCION, ");
                query.Append(" MONTO, ");
                query.Append(" DIRECCION, ");
                query.Append(" TELEFONO1, ");
                query.Append(" TELEFONO2, ");
                query.Append(" CORREO, ");
                query.Append(" REFERENCIA, ");
                query.Append(" FORDENPAGO, ");
                query.Append(" FPAGOCOSEDE, ");
                query.Append(" FPAGOREAL, ");
                query.Append(" CUSUARIOPAGO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CTERMINAL ");
                query.Append(" FROM TCOSPAGOS ");
                query.Append(" WHERE IDCOSEDE = :IDCOSEDE ");
                query.Append(" AND IDENTIFICACION = :IDENTIFICACION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("IDCOSEDE", OracleDbType.Varchar2, idcosede, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, identificacion, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TCOSPAGOS
                        {
                            IDCOSEDE = reader["IDCOSEDE"].ToString(),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            TIPOIDENTIFICACION = reader["TIPOIDENTIFICACION"].ToString(),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            COMPROBANTE = reader["COMPROBANTE"].ToString(),
                            INSTITUCION = reader["INSTITUCION"].ToString(),
                            MONTO = Util.ConvertirDecimal(reader["MONTO"].ToString()),
                            DIRECCION = reader["DIRECCION"].ToString(),
                            TELEFONO1 = reader["TELEFONO1"].ToString(),
                            TELEFONO2 = reader["TELEFONO2"].ToString(),
                            CORREO = reader["CORREO"].ToString(),
                            REFERENCIA = reader["REFERENCIA"].ToString(),
                            FORDENPAGO = Util.ConvertirFecha(reader["FORDENPAGO"].ToString()),
                            FPAGOCOSEDE = Util.ConvertirFecha(reader["FPAGOCOSEDE"].ToString()),
                            FPAGOREAL = Util.ConvertirFecha(reader["FPAGOREAL"].ToString()),
                            CUSUARIOPAGO = reader["CUSUARIOPAGO"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            CTERMINAL = reader["CTERMINAL"].ToString()
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

        public List<TCOSPAGOS> ListarPagosUsuario(string identificacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TCOSPAGOS> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" IDCOSEDE, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" TIPOIDENTIFICACION, ");
                query.Append(" NOMBRE, ");
                query.Append(" COMPROBANTE, ");
                query.Append(" INSTITUCION, ");
                query.Append(" MONTO, ");
                query.Append(" DIRECCION, ");
                query.Append(" TELEFONO1, ");
                query.Append(" TELEFONO2, ");
                query.Append(" CORREO, ");
                query.Append(" REFERENCIA, ");
                query.Append(" FORDENPAGO, ");
                query.Append(" FPAGOCOSEDE, ");
                query.Append(" FPAGOREAL, ");
                query.Append(" CUSUARIOPAGO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CTERMINAL ");
                query.Append(" FROM TCOSPAGOS ");
                query.Append(" WHERE IDENTIFICACION = :IDENTIFICACION ");
                //query.Append(" WHERE TRUNC(FPAGOREAL) = TRUNC(:FPAGOREAL) ");
                //query.Append(" AND CUSUARIOPAGO = :CUSUARIOPAGO ");

                /*if (!string.IsNullOrEmpty(identificacion))
                {
                    query.Append(" AND IDENTIFICACION = :IDENTIFICACION ");
                }*/

                comando.Connection = ado.oraConexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, identificacion, ParameterDirection.Input));

                //comando.Parameters.Add(new OracleParameter("FPAGOREAL", OracleDbType.Date, fecha, ParameterDirection.Input));
                //comando.Parameters.Add(new OracleParameter("CUSUARIOPAGO", OracleDbType.Varchar2, cusuario, ParameterDirection.Input));
                /*if (!string.IsNullOrEmpty(identificacion))
                {
                    comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, identificacion, ParameterDirection.Input));
                }*/

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TCOSPAGOS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TCOSPAGOS
                        {
                            IDCOSEDE = reader["IDCOSEDE"].ToString(),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            TIPOIDENTIFICACION = reader["TIPOIDENTIFICACION"].ToString(),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            COMPROBANTE = reader["COMPROBANTE"].ToString(),
                            INSTITUCION = reader["INSTITUCION"].ToString(),
                            MONTO = Util.ConvertirDecimal(reader["MONTO"].ToString()),
                            DIRECCION = reader["DIRECCION"].ToString(),
                            TELEFONO1 = reader["TELEFONO1"].ToString(),
                            TELEFONO2 = reader["TELEFONO2"].ToString(),
                            CORREO = reader["CORREO"].ToString(),
                            REFERENCIA = reader["REFERENCIA"].ToString(),
                            FORDENPAGO = Util.ConvertirFecha(reader["FORDENPAGO"].ToString()),
                            FPAGOCOSEDE = Util.ConvertirFecha(reader["FPAGOCOSEDE"].ToString()),
                            FPAGOREAL = Util.ConvertirFecha(reader["FPAGOREAL"].ToString()),
                            CUSUARIOPAGO = reader["CUSUARIOPAGO"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            CTERMINAL = reader["CTERMINAL"].ToString(),
                            lnk = reader["IDENTIFICACION"].ToString() + ";" + reader["IDCOSEDE"].ToString()
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

        //query.Append(" IDCOSEDE, ");
        //query.Append(" IDENTIFICACION, ");
        //query.Append(" TIPOIDENTIFICACION, ");
        //query.Append(" NOMBRE, ");
        //query.Append(" COMPROBANTE, ");
        //query.Append(" INSTITUCION, ");
        //query.Append(" MONTO, ");
        //query.Append(" DIRECCION, ");
        //query.Append(" TELEFONO1, ");
        //query.Append(" TELEFONO2, ");
        //query.Append(" CORREO, ");
        //query.Append(" REFERENCIA, ");
        //query.Append(" FORDENPAGO, ");
        //query.Append(" FPAGOCOSEDE, ");
        //query.Append(" FPAGOREAL, ");
        //query.Append(" CUSUARIOPAGO, ");
        //query.Append(" CSUCURSAL, ");
        //query.Append(" COFICINA, ");
        //query.Append(" CTERMINAL, ");

        //IDCOSEDE = reader["IDCOSEDE"].ToString(),
        //IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
        //TIPOIDENTIFICACION = reader["TIPOIDENTIFICACION"].ToString(),
        //NOMBRE = reader["NOMBRE"].ToString(),
        //COMPROBANTE = reader["COMPROBANTE"].ToString(),
        //INSTITUCION = reader["INSTITUCION"].ToString(),
        //MONTO = ado.ConvertirDecimal(reader["MONTO"].ToString()),
        //DIRECCION = reader["DIRECCION"].ToString(),
        //TELEFONO1 = reader["TELEFONO1"].ToString(),
        //TELEFONO2 = reader["TELEFONO2"].ToString(),
        //CORREO = reader["CORREO"].ToString(),
        //REFERENCIA = reader["REFERENCIA"].ToString(),
        //FORDENPAGO = ado.ConvertirFecha(reader["FORDENPAGO"].ToString()),
        //FPAGOCOSEDE = ado.ConvertirFecha(reader["FPAGOCOSEDE"].ToString()),
        //FPAGOREAL = ado.ConvertirFecha(reader["FPAGOREAL"].ToString()),
        //CUSUARIOPAGO = reader["CUSUARIOPAGO"].ToString(),
        //CSUCURSAL = ado.ConvertirNumero(reader["CSUCURSAL"].ToString()),
        //COFICINA = ado.ConvertirNumero(reader["COFICINA"].ToString()),
        //CTERMINAL = reader["CTERMINAL"].ToString(),

        //query.Append(" IDCOSEDE = :IDCOSEDE, ");
        //query.Append(" IDENTIFICACION = :IDENTIFICACION, ");
        //query.Append(" TIPOIDENTIFICACION = :TIPOIDENTIFICACION, ");
        //query.Append(" NOMBRE = :NOMBRE, ");
        //query.Append(" COMPROBANTE = :COMPROBANTE, ");
        //query.Append(" INSTITUCION = :INSTITUCION, ");
        //query.Append(" MONTO = :MONTO, ");
        //query.Append(" DIRECCION = :DIRECCION, ");
        //query.Append(" TELEFONO1 = :TELEFONO1, ");
        //query.Append(" TELEFONO2 = :TELEFONO2, ");
        //query.Append(" CORREO = :CORREO, ");
        //query.Append(" REFERENCIA = :REFERENCIA, ");
        //query.Append(" FORDENPAGO = :FORDENPAGO, ");
        //query.Append(" FPAGOCOSEDE = :FPAGOCOSEDE, ");
        //query.Append(" FPAGOREAL = :FPAGOREAL, ");
        //query.Append(" CUSUARIOPAGO = :CUSUARIOPAGO, ");
        //query.Append(" CSUCURSAL = :CSUCURSAL, ");
        //query.Append(" COFICINA = :COFICINA, ");
        //query.Append(" CTERMINAL = :CTERMINAL, ");

        //comando.Parameters.Add(new OracleParameter("IDCOSEDE", OracleDbType.Varchar2, obj.IDCOSEDE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, obj.IDENTIFICACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TIPOIDENTIFICACION", OracleDbType.Varchar2, obj.TIPOIDENTIFICACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NOMBRE", OracleDbType.Varchar2, obj.NOMBRE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("COMPROBANTE", OracleDbType.Varchar2, obj.COMPROBANTE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("INSTITUCION", OracleDbType.Varchar2, obj.INSTITUCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("MONTO", OracleDbType.Decimal, obj.MONTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("DIRECCION", OracleDbType.Varchar2, obj.DIRECCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TELEFONO1", OracleDbType.Varchar2, obj.TELEFONO1, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TELEFONO2", OracleDbType.Varchar2, obj.TELEFONO2, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CORREO", OracleDbType.Varchar2, obj.CORREO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("REFERENCIA", OracleDbType.Varchar2, obj.REFERENCIA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FORDENPAGO", OracleDbType.Date, obj.FORDENPAGO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FPAGOCOSEDE", OracleDbType.Date, obj.FPAGOCOSEDE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FPAGOREAL", OracleDbType.Date, obj.FPAGOREAL, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CUSUARIOPAGO", OracleDbType.Varchar2, obj.CUSUARIOPAGO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CSUCURSAL", OracleDbType.Int32, obj.CSUCURSAL, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("COFICINA", OracleDbType.Int32, obj.COFICINA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CTERMINAL", OracleDbType.Varchar2, obj.CTERMINAL, ParameterDirection.Input));

    }
}
