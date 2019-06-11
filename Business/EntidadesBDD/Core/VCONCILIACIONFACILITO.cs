using System;
using AccessData;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class VCONCILIACIONFACILITO
    {
        #region PROPIEDADES
        public DateTime FCONTABLE { get; set; }
        public string REFERENCIA { get; set; }
        public string NUMEROMOVIMIENTO { get; set; }
        public string NUMEROCUENTAORIGEN { get; set; }
        public string NUMEROCUENTADESTINO { get; set; }
        public string CODIGODECLIENTE { get; set; }
        public string ESTADO { get; set; }
        public string TIPO { get; set; }
        public string SUBTIPO { get; set; }
        public DateTime FECHAHORATRANSACCION { get; set; }
        public double VALOR { get; set; }
        public double COMISIONTOTAL { get; set; }
        #endregion PROPIEDADES

        #region METODOS

        public List<VCONCILIACIONFACILITO> ListarElementos(string fdesde, string fhasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("Fitbank");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VCONCILIACIONFACILITO> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FCONTABLE, ");
                query.Append(" REFERENCIA, ");
                query.Append(" NUMEROMOVIMIENTO, ");
                query.Append(" NUMEROCUENTAORIGEN, ");
                query.Append(" NUMEROCUENTADESTINO, ");
                query.Append(" CODIGODECLIENTE, ");
                query.Append(" ESTADO, ");
                query.Append(" TIPO, ");
                query.Append(" SUBTIPO, ");
                query.Append(" FECHAHORATRANSACCION, ");
                query.Append(" VALOR, ");
                query.Append(" COMISIONTOTAL ");
                query.Append(" FROM VCONCILIACIONFACILITO ");
                query.Append(" WHERE 1 = 1 ");
                query.Append(" AND FCONTABLE BETWEEN :FDESDE AND :FHASTA ");

                //query.Append(" SELECT * FROM FROM VCONCILIACIONFACILITO  ");// WHERE FCONTABLE BETWEEN :FDESDE AND :FHASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (!string.IsNullOrEmpty(fdesde) && !string.IsNullOrEmpty(fhasta))
                {
                    comando.Parameters.Add(new OracleParameter("FDESDE", OracleDbType.Date, Convert.ToDateTime(fdesde), ParameterDirection.Input));
                    comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, Convert.ToDateTime(fhasta), ParameterDirection.Input));
                }

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VCONCILIACIONFACILITO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VCONCILIACIONFACILITO
                        {
                            FCONTABLE = Convert.ToDateTime(reader["FCONTABLE"]),
                            REFERENCIA = reader["REFERENCIA"].ToString(),
                            NUMEROMOVIMIENTO = reader["NUMEROMOVIMIENTO"].ToString(),
                            NUMEROCUENTAORIGEN = reader["NUMEROCUENTAORIGEN"].ToString(),
                            NUMEROCUENTADESTINO = reader["NUMEROCUENTADESTINO"].ToString(),
                            CODIGODECLIENTE = reader["CODIGODECLIENTE"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            TIPO = reader["TIPO"].ToString(),
                            SUBTIPO = reader["SUBTIPO"].ToString(),
                            FECHAHORATRANSACCION = Convert.ToDateTime(reader["FECHAHORATRANSACCION"]),
                            VALOR = Convert.ToDouble(reader["VALOR"].ToString()),
                            COMISIONTOTAL = Convert.ToDouble(reader["COMISIONTOTAL"].ToString())
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

        #endregion METODOS

    }
}
