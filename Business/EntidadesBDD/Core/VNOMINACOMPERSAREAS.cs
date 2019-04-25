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
    public class VNOMINACOMPERSAREAS
    {
        #region variables

        public String CDIVISION { get; set; }
        public String CDEPARTAMENTO { get; set; }
        public String CUNIDAD { get; set; }
        public Int32? CODIGO { get; set; }
        public String NOMBRE { get; set; }
        public String ESTADO { get; set; }

        #endregion variables

        #region metodos

        public List<VNOMINACOMPERSAREAS> ListarDivisiones(string tipo)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VNOMINACOMPERSAREAS> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT * FROM ( ");
                query.Append(" SELECT ");
                query.Append(" B.CDIVISION, ");
                query.Append(" B.CDEPARTAMENTO, ");
                query.Append(" B.CUNIDAD, ");
                query.Append(" B.CODIGO, ");
                query.Append(" A.NOMBRE NOMBRE, ");
                query.Append(" CASE WHEN FHASTA = FNCFHASTA THEN 'A' ELSE 'I' END ESTADO ");
                query.Append(" FROM TJURIDICODIVISIONES A, THOMOLOGACOMPERS B ");
                query.Append(" WHERE A.CODIGODIVISION = B.CDIVISION ");
                query.Append(" AND B.CDEPARTAMENTO = '0' ");
                query.Append(" AND B.CUNIDAD = '0' ");
                query.Append("  AND A.CODIGODIVISION NOT IN ('01') ");

                if (tipo == "V")
                {
                    query.Append(" AND TRUNC(FHASTA) = :FHASTA ");
                }
                else if (tipo == "M")
                {
                    query.Append(" AND TRUNC(FDESDE) = :FDESDE ");
                }

                query.Append(" ) ORDER BY CODIGO,  ESTADO DESC ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (tipo == "V")
                {
                    comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, new DateTime(2999, 12, 31), ParameterDirection.Input));
                }
                else if (tipo == "M")
                {
                    comando.Parameters.Add(new OracleParameter("FDESDE", OracleDbType.Date, DateTime.Today, ParameterDirection.Input));
                }

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VNOMINACOMPERSAREAS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VNOMINACOMPERSAREAS
                        {
                            CDIVISION = reader["CDIVISION"].ToString(),
                            CDEPARTAMENTO = reader["CDEPARTAMENTO"].ToString(),
                            CUNIDAD = reader["CUNIDAD"].ToString(),
                            CODIGO = Util.ConvertirNumero(reader["CODIGO"].ToString()),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            ESTADO = reader["ESTADO"].ToString()
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

        public List<VNOMINACOMPERSAREAS> ListarDepartamentos(string division, string tipo)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VNOMINACOMPERSAREAS> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT * FROM ( ");
                query.Append(" SELECT ");
                query.Append(" B.CDIVISION, ");
                query.Append(" B.CDEPARTAMENTO, ");
                query.Append(" B.CUNIDAD, ");
                query.Append(" B.CODIGO, ");
                query.Append(" A.NOMBRE, ");
                query.Append(" CASE WHEN A.FHASTA = FNCFHASTA THEN 'A' ELSE 'I' END ESTADO ");
                query.Append(" FROM TJURIDICODEPARTAMENTOS A, THOMOLOGACOMPERS B ");
                query.Append(" WHERE A.CODIGODIVISION = B.CDIVISION ");
                query.Append(" AND B.CDEPARTAMENTO = A.CODIGODEPARTAMENTO ");
                query.Append(" AND B.CUNIDAD = '0' ");
                query.Append(" AND B.CDIVISION = :CDIVISION ");

                if (tipo == "V")
                {
                    query.Append(" AND TRUNC(FHASTA) = :FHASTA ");
                }
                else if (tipo == "M")
                {
                    query.Append(" AND TRUNC(FDESDE) = :FDESDE ");
                }

                query.Append(" ) ORDER BY CODIGO,  ESTADO DESC ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CDIVISION", OracleDbType.Varchar2, division, ParameterDirection.Input));

                if (tipo == "V")
                {
                    comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, new DateTime(2999, 12, 31), ParameterDirection.Input));
                }
                else if (tipo == "M")
                {
                    comando.Parameters.Add(new OracleParameter("FDESDE", OracleDbType.Date, DateTime.Today, ParameterDirection.Input));
                }

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VNOMINACOMPERSAREAS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VNOMINACOMPERSAREAS
                        {
                            CDIVISION = reader["CDIVISION"].ToString(),
                            CDEPARTAMENTO = reader["CDEPARTAMENTO"].ToString(),
                            CUNIDAD = reader["CUNIDAD"].ToString(),
                            CODIGO = Util.ConvertirNumero(reader["CODIGO"].ToString()),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            ESTADO = reader["ESTADO"].ToString()
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

        public List<VNOMINACOMPERSAREAS> ListarUnidades(string division, string departamento, string tipo)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VNOMINACOMPERSAREAS> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT * FROM ( ");
                query.Append(" SELECT ");
                query.Append(" B.CDIVISION, ");
                query.Append(" B.CDEPARTAMENTO, ");
                query.Append(" B.CUNIDAD, ");
                query.Append(" B.CODIGO, ");
                query.Append(" A.NOMBRE, ");
                query.Append(" CASE WHEN A.FHASTA = FNCFHASTA THEN 'A' ELSE 'I' END ESTADO ");
                query.Append(" FROM TJURIDICOUNIDADES A, THOMOLOGACOMPERS B ");
                query.Append(" WHERE A.CODIGODIVISION = B.CDIVISION");
                query.Append(" AND B.CDEPARTAMENTO = A.CODIGODEPARTAMENTO ");
                query.Append(" AND B.CUNIDAD = A.CODIGOUNIDAD ");
                query.Append(" AND B.CDIVISION = :CDIVISION ");
                query.Append(" AND B.CDEPARTAMENTO = :CDEPARTAMENTO ");

                if (tipo == "V")
                {
                    query.Append(" AND TRUNC(FHASTA) = :FHASTA ");
                }
                else if (tipo == "M")
                {
                    query.Append(" AND TRUNC(FDESDE) = :FDESDE ");
                }

                query.Append(" ) ORDER BY CODIGO,  ESTADO DESC ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CDIVISION", OracleDbType.Varchar2, division, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CDEPARTAMENTO", OracleDbType.Varchar2, departamento, ParameterDirection.Input));

                if (tipo == "V")
                {
                    comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, new DateTime(2999, 12, 31), ParameterDirection.Input));
                }
                else if (tipo == "M")
                {
                    comando.Parameters.Add(new OracleParameter("FDESDE", OracleDbType.Date, DateTime.Today, ParameterDirection.Input));
                }

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VNOMINACOMPERSAREAS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VNOMINACOMPERSAREAS
                        {
                            CDIVISION = reader["CDIVISION"].ToString(),
                            CDEPARTAMENTO = reader["CDEPARTAMENTO"].ToString(),
                            CUNIDAD = reader["CUNIDAD"].ToString(),
                            CODIGO = Util.ConvertirNumero(reader["CODIGO"].ToString()),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            ESTADO = reader["ESTADO"].ToString()
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

        //query.Append(" CDIVISION, ");
        //query.Append(" DIVISION, ");
        //query.Append(" CDEPARTAMENTO, ");
        //query.Append(" DEPARTAMENTO, ");
        //query.Append(" CUNIDAD, ");
        //query.Append(" UNIDAD, ");
        //query.Append(" FACTUALIZADIVISION, ");
        //query.Append(" FACTUALIZADEPARTMENTO, ");
        //query.Append(" FACTUALIZAUNIDAD, ");

        //CDIVISION = ado.ConvertirNumero(reader["CDIVISION"].ToString()),
        //DIVISION = reader["DIVISION"].ToString(),
        //CDEPARTAMENTO = ado.ConvertirNumero(reader["CDEPARTAMENTO"].ToString()),
        //DEPARTAMENTO = reader["DEPARTAMENTO"].ToString(),
        //CUNIDAD = ado.ConvertirNumero(reader["CUNIDAD"].ToString()),
        //UNIDAD = reader["UNIDAD"].ToString(),
        //FACTUALIZADIVISION = ado.ConvertirFecha(reader["FACTUALIZADIVISION"].ToString()),
        //FACTUALIZADEPARTMENTO = ado.ConvertirFecha(reader["FACTUALIZADEPARTMENTO"].ToString()),
        //FACTUALIZAUNIDAD = ado.ConvertirFecha(reader["FACTUALIZAUNIDAD"].ToString()),
    }
}
