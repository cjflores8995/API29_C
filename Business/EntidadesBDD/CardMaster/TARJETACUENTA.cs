using AccessData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TARJETACUENTA
    {
        #region variables

        public String TARJETA { get; set; }
        public String CUENTA { get; set; }
        public DateTime? FENTREGA { get; set; }
        public Int32? ROWNUM { get; set; }

        #endregion variables

        #region metodos

        public TARJETACUENTA ListarTarjetaCuenta(string tarjeta)
        {
            AccesoDatosSql ado = new AccesoDatosSql();
            SqlCommand comando = new SqlCommand();
            StringBuilder query = new StringBuilder();
            TARJETACUENTA obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" TAR_NUMERO TARJETA, ");
                query.Append(" CTA_NUMERO CUENTA");
                query.Append(" FROM [CardMaster].[dbo].[TARJETA_CUENTA] ");
                query.Append(" WHERE TAR_NUMERO = '" + tarjeta + "' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                SqlDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TARJETACUENTA
                        {
                            TARJETA = reader["TARJETA"].ToString(),
                            CUENTA = reader["CUENTA"].ToString()
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

        public List<TARJETACUENTA> ListarTarjetaMantenimiento(DateTime fecha, int desde, int hasta)
        {
            AccesoDatosSql ado = new AccesoDatosSql();
            SqlCommand comando = new SqlCommand();
            StringBuilder query = new StringBuilder();
            List<TARJETACUENTA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" WITH C AS ( ");
                query.Append(" SELECT ");
                query.Append(" ROW_NUMBER() OVER(ORDER BY A.TAR_FECHA_ENTREGA ASC) AS NUM, ");
                query.Append(" A.TAR_NUMERO TARJETA, ");
                query.Append(" B.CTA_NUMERO CUENTA, ");
                query.Append(" A.TAR_FECHA_ENTREGA FENTREGA ");
                query.Append(" FROM CardMaster.dbo.TARJETA A, CardMaster.dbo.TARJETA_CUENTA B ");
                query.Append(" WHERE A.TAR_NUMERO = B.TAR_NUMERO ");
                query.Append(" AND A.EST_TAR_CODIGO = '2' ");
                query.Append(" AND CONVERT(varchar(2), DATEPART (\"MONTH\", A.TAR_FECHA_ENTREGA)) + CONVERT(varchar(2), DATEPART (\"DAY\", A.TAR_FECHA_ENTREGA)) = " + fecha.Month.ToString() + fecha.Day.ToString() + " ");
                query.Append(" AND DATEPART (\"YEAR\", A.TAR_FECHA_ENTREGA) < " + fecha.Year + " ");
                query.Append(" ) ");
                query.Append(" SELECT NUM, TARJETA, CUENTA, FENTREGA FROM C WHERE NUM BETWEEN " + desde + " AND " + hasta + " ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                SqlDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TARJETACUENTA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TARJETACUENTA
                        {
                            TARJETA = reader["TARJETA"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            FENTREGA = Util.ConvertirFecha(reader["FENTREGA"].ToString()),
                            ROWNUM = Util.ConvertirNumero(reader["NUM"].ToString())
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

        public Int32 ContarTarjetaMantenimiento(DateTime fecha)
        {
            AccesoDatosSql ado = new AccesoDatosSql();
            SqlCommand comando = new SqlCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" COUNT(*) REGISTROS ");
                query.Append(" FROM CardMaster.dbo.TARJETA A, CardMaster.dbo.TARJETA_CUENTA B ");
                query.Append(" WHERE A.TAR_NUMERO = B.TAR_NUMERO ");
                query.Append(" AND A.EST_TAR_CODIGO = '2' ");
                query.Append(" AND CONVERT(varchar(2), DATEPART (\"MONTH\", A.TAR_FECHA_ENTREGA)) + CONVERT(varchar(2), DATEPART (\"DAY\", A.TAR_FECHA_ENTREGA)) = " + fecha.Month.ToString() + fecha.Day.ToString() + " ");
                query.Append(" AND DATEPART (\"YEAR\", A.TAR_FECHA_ENTREGA) < " + fecha.Year + " ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                SqlDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        #endregion metodos
    }
}
