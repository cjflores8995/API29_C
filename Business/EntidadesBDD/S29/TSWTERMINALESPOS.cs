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
    public class TSWTERMINALESPOS
    {
        #region variables

        public Int32? CCONVENIO { get; set; }
        public String MID { get; set; }
        public String NOMBRE { get; set; }
        public String CTACONTABLE { get; set; }
        public String DIRECCION { get; set; }
        public String CIUDAD { get; set; }
        public String ESTADO { get; set; }
        public String CODIGOALTERNO { get; set; }

        #endregion variables

        #region metodos

        public List<TSWTERMINALESPOS> ListarTerminalesXConvenio(Int32? cconvenio)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("S29");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TSWTERMINALESPOS> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CCONVENIO, ");
                query.Append(" MID, ");
                query.Append(" NOMBRE, ");
                query.Append(" CTACONTABLE, ");
                query.Append(" DIRECCION, ");
                query.Append(" CIUDAD, ");
                query.Append(" ESTADO, ");
                query.Append(" CODIGOALTERNO ");
                query.Append(" FROM TSWTERMINALESPOS ");
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
                    ltObj = new List<TSWTERMINALESPOS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TSWTERMINALESPOS
                        {
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            MID = reader["MID"].ToString(),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            CTACONTABLE = reader["CTACONTABLE"].ToString(),
                            DIRECCION = reader["DIRECCION"].ToString(),
                            CIUDAD = reader["CIUDAD"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            CODIGOALTERNO = reader["CODIGOALTERNO"].ToString()
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

        //query.Append(" CCONVENIO, ");
        //query.Append(" MID, ");
        //query.Append(" NOMBRE, ");
        //query.Append(" CTACONTABLE, ");
        //query.Append(" DIRECCION, ");
        //query.Append(" CIUDAD, ");
        //query.Append(" ESTADO, ");
        //query.Append(" CODIGOALTERNO, ");

        //CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
        //MID = reader["MID"].ToString(),
        //NOMBRE = reader["NOMBRE"].ToString(),
        //CTACONTABLE = reader["CTACONTABLE"].ToString(),
        //DIRECCION = reader["DIRECCION"].ToString(),
        //CIUDAD = reader["CIUDAD"].ToString(),
        //ESTADO = reader["ESTADO"].ToString(),
        //CODIGOALTERNO = reader["CODIGOALTERNO"].ToString(),
    }
}
