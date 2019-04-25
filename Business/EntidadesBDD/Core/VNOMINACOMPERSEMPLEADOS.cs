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
    public class VNOMINACOMPERSEMPLEADOS
    {
        #region variables

        public Int32? PERSOSEC { get; set; }
        public Int32? AREASEC { get; set; }
        public Int32? CARGOSEC { get; set; }
        public String PERSONOMBRE { get; set; }
        public String PERSOAPELLIDO { get; set; }
        public String PERSOCI { get; set; }
        public Int32? PERSOSUPDIRECTO { get; set; }
        public DateTime? PERSOFECHANACIMIENTO { get; set; }
        public Int32? PERSOGENERO { get; set; }
        public String PERSODIRECCION { get; set; }
        public String PERSOTELEFONODOMICILIO { get; set; }
        public String PERSOTELEFONOMOVIL { get; set; }
        public String PERSOTELEFONOLABORAL { get; set; }
        public String PERSOEXTENSION { get; set; }
        public DateTime? PERSOFECHAINGRESO { get; set; }
        public DateTime? PERSOFECHASALIDA { get; set; }
        public String PERSOCODIGOINSTITUCION { get; set; }
        public Decimal? PERSOSUELDOBASICO { get; set; }
        public String PERSOEMAIL { get; set; }
        public String PERSOESTADO { get; set; }
        public DateTime? FACTUALIZADIRECCION { get; set; }
        public DateTime? FACTUALIZAMAIL { get; set; }
        public DateTime? FACTUALIZAMOVIL { get; set; }
        public DateTime? FACTUALIZAFONODOMICILIO { get; set; }
        public DateTime? FACTUALIZAFONOOFICINA { get; set; }
        public DateTime? FACTUALIZAEMPLEADO { get; set; }
        public DateTime? FACTUALIZAPERSONA { get; set; }
        public DateTime? FACTUALIZATRABAJO { get; set; }
        public DateTime? FACTUALIZAINFOBASICA { get; set; }

        #endregion variables

        #region metodos

        public List<VNOMINACOMPERSEMPLEADOS> ListarEmpleados(string tipo)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VNOMINACOMPERSEMPLEADOS> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" PERSOSEC, ");
                query.Append(" AREASEC, ");
                query.Append(" CARGOSEC, ");
                query.Append(" PERSONOMBRE, ");
                query.Append(" PERSOAPELLIDO, ");
                query.Append(" PERSOCI, ");
                query.Append(" PERSOSUPDIRECTO, ");
                query.Append(" PERSOFECHANACIMIENTO, ");
                query.Append(" PERSOGENERO, ");
                query.Append(" PERSODIRECCION, ");
                query.Append(" PERSOTELEFONODOMICILIO, ");
                query.Append(" PERSOTELEFONOMOVIL, ");
                query.Append(" PERSOTELEFONOLABORAL, ");
                query.Append(" PERSOEXTENSION, ");
                query.Append(" PERSOFECHAINGRESO, ");
                query.Append(" PERSOFECHASALIDA, ");
                query.Append(" PERSOCODIGOINSTITUCION, ");
                query.Append(" PERSOSUELDOBASICO, ");
                query.Append(" PERSOEMAIL, ");
                query.Append(" PERSOESTADO ");
                query.Append(" FROM VNOMINACOMPERSEMPLEADOS ");
                query.Append(" WHERE 1 = 1 ");

                if (tipo == "V")
                {
                    query.Append(" AND (PERSOFECHASALIDA IS NULL OR PERSOFECHASALIDA >= TO_DATE('20170101', 'YYYYMMDD')) ");
                }
                else if (tipo == "M")
                {
                    query.Append(" AND TRUNC (FACTUALIZADIRECCION) = TRUNC (SYSDATE) ");
                    query.Append(" OR TRUNC (FACTUALIZAMAIL) = TRUNC (SYSDATE) ");
                    query.Append(" OR TRUNC (FACTUALIZAMOVIL) = TRUNC (SYSDATE) ");
                    query.Append(" OR TRUNC (FACTUALIZAFONODOMICILIO) = TRUNC (SYSDATE) ");
                    query.Append(" OR TRUNC (FACTUALIZAFONOOFICINA) = TRUNC (SYSDATE) ");
                    query.Append(" OR TRUNC (FACTUALIZAEMPLEADO) = TRUNC (SYSDATE) ");
                    query.Append(" OR TRUNC (FACTUALIZAPERSONA) = TRUNC (SYSDATE) ");
                    query.Append(" OR TRUNC (FACTUALIZATRABAJO) = TRUNC (SYSDATE) ");
                    query.Append(" OR TRUNC (FACTUALIZAINFOBASICA) = TRUNC (SYSDATE) ");
                }

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VNOMINACOMPERSEMPLEADOS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VNOMINACOMPERSEMPLEADOS
                        {
                            PERSOSEC = Util.ConvertirNumero(reader["PERSOSEC"].ToString()),
                            AREASEC = Util.ConvertirNumero(reader["AREASEC"].ToString()),
                            CARGOSEC = Util.ConvertirNumero(reader["CARGOSEC"].ToString()),
                            PERSONOMBRE = reader["PERSONOMBRE"].ToString(),
                            PERSOAPELLIDO = reader["PERSOAPELLIDO"].ToString(),
                            PERSOCI = reader["PERSOCI"].ToString(),
                            PERSOSUPDIRECTO = Util.ConvertirNumero(reader["PERSOSUPDIRECTO"].ToString()),
                            PERSOFECHANACIMIENTO = Util.ConvertirFecha(reader["PERSOFECHANACIMIENTO"].ToString()),
                            PERSOGENERO = Util.ConvertirNumero(reader["PERSOGENERO"].ToString()),
                            PERSODIRECCION = reader["PERSODIRECCION"].ToString(),
                            PERSOTELEFONODOMICILIO = reader["PERSOTELEFONODOMICILIO"].ToString(),
                            PERSOTELEFONOMOVIL = reader["PERSOTELEFONOMOVIL"].ToString(),
                            PERSOTELEFONOLABORAL = reader["PERSOTELEFONOLABORAL"].ToString(),
                            PERSOEXTENSION = reader["PERSOEXTENSION"].ToString(),
                            PERSOFECHAINGRESO = Util.ConvertirFecha(reader["PERSOFECHAINGRESO"].ToString()),
                            PERSOFECHASALIDA = Util.ConvertirFecha(reader["PERSOFECHASALIDA"].ToString()),
                            PERSOCODIGOINSTITUCION = reader["PERSOCODIGOINSTITUCION"].ToString(),
                            PERSOSUELDOBASICO = Util.ConvertirDecimal(reader["PERSOSUELDOBASICO"].ToString()),
                            PERSOEMAIL = reader["PERSOEMAIL"].ToString(),
                            PERSOESTADO = reader["PERSOESTADO"].ToString()
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

        //query.Append(" PERSOSEC, ");
        //query.Append(" AREASEC, ");
        //query.Append(" CARGOSEC, ");
        //query.Append(" PERSONOMBRE, ");
        //query.Append(" PERSOAPELLIDO, ");
        //query.Append(" PERSOCI, ");
        //query.Append(" PERSOSUPDIRECTO, ");
        //query.Append(" PERSOFECHANACIMIENTO, ");
        //query.Append(" PERSOGENERO, ");
        //query.Append(" PERSODIRECCION, ");
        //query.Append(" PERSOTELEFONODOMICILIO, ");
        //query.Append(" PERSOTELEFONOMOVIL, ");
        //query.Append(" PERSOTELEFONOLABORAL, ");
        //query.Append(" PERSOEXTENSION, ");
        //query.Append(" PERSOFECHAINGRESO, ");
        //query.Append(" PERSOFECHASALIDA, ");
        //query.Append(" PERSOCODIGOINSTITUCION, ");
        //query.Append(" PERSOSUELDOBASICO, ");
        //query.Append(" PERSOEMAIL, ");
        //query.Append(" PERSOESTADO, ");
        //query.Append(" FACTUALIZADIRECCION, ");
        //query.Append(" FACTUALIZAMAIL, ");
        //query.Append(" FACTUALIZAMOVIL, ");
        //query.Append(" FACTUALIZAFONODOMICILIO, ");
        //query.Append(" FACTUALIZAFONOOFICINA, ");
        //query.Append(" FACTUALIZAEMPLEADO, ");
        //query.Append(" FACTUALIZAPERSONA, ");
        //query.Append(" FACTUALIZATRABAJO, ");
        //query.Append(" FACTUALIZAINFOBASICA, ");

        //PERSOSEC = ado.ConvertirNumero(reader["PERSOSEC"].ToString()),
        //AREASEC = reader["AREASEC"].ToString(),
        //CARGOSEC = reader["CARGOSEC"].ToString(),
        //PERSONOMBRE = reader["PERSONOMBRE"].ToString(),
        //PERSOAPELLIDO = reader["PERSOAPELLIDO"].ToString(),
        //PERSOCI = reader["PERSOCI"].ToString(),
        //PERSOSUPDIRECTO = reader["PERSOSUPDIRECTO"].ToString(),
        //PERSOFECHANACIMIENTO = ado.ConvertirFecha(reader["PERSOFECHANACIMIENTO"].ToString()),
        //PERSOGENERO = ado.ConvertirNumero(reader["PERSOGENERO"].ToString()),
        //PERSODIRECCION = reader["PERSODIRECCION"].ToString(),
        //PERSOTELEFONODOMICILIO = reader["PERSOTELEFONODOMICILIO"].ToString(),
        //PERSOTELEFONOMOVIL = reader["PERSOTELEFONOMOVIL"].ToString(),
        //PERSOTELEFONOLABORAL = reader["PERSOTELEFONOLABORAL"].ToString(),
        //PERSOEXTENSION = reader["PERSOEXTENSION"].ToString(),
        //PERSOFECHAINGRESO = ado.ConvertirFecha(reader["PERSOFECHAINGRESO"].ToString()),
        //PERSOFECHASALIDA = ado.ConvertirFecha(reader["PERSOFECHASALIDA"].ToString()),
        //PERSOCODIGOINSTITUCION = reader["PERSOCODIGOINSTITUCION"].ToString(),
        //PERSOSUELDOBASICO = ado.ConvertirNumero(reader["PERSOSUELDOBASICO"].ToString()),
        //PERSOEMAIL = reader["PERSOEMAIL"].ToString(),
        //PERSOESTADO = reader["PERSOESTADO"].ToString(),
        //FACTUALIZADIRECCION = ado.ConvertirFecha(reader["FACTUALIZADIRECCION"].ToString()),
        //FACTUALIZAMAIL = ado.ConvertirFecha(reader["FACTUALIZAMAIL"].ToString()),
        //FACTUALIZAMOVIL = ado.ConvertirFecha(reader["FACTUALIZAMOVIL"].ToString()),
        //FACTUALIZAFONODOMICILIO = ado.ConvertirFecha(reader["FACTUALIZAFONODOMICILIO"].ToString()),
        //FACTUALIZAFONOOFICINA = ado.ConvertirFecha(reader["FACTUALIZAFONOOFICINA"].ToString()),
        //FACTUALIZAEMPLEADO = ado.ConvertirFecha(reader["FACTUALIZAEMPLEADO"].ToString()),
        //FACTUALIZAPERSONA = ado.ConvertirFecha(reader["FACTUALIZAPERSONA"].ToString()),
        //FACTUALIZATRABAJO = ado.ConvertirFecha(reader["FACTUALIZATRABAJO"].ToString()),
        //FACTUALIZAINFOBASICA = ado.ConvertirFecha(reader["FACTUALIZAINFOBASICA"].ToString()),

        //query.Append(" PERSOSEC = :PERSOSEC, ");
        //query.Append(" AREASEC = :AREASEC, ");
        //query.Append(" CARGOSEC = :CARGOSEC, ");
        //query.Append(" PERSONOMBRE = :PERSONOMBRE, ");
        //query.Append(" PERSOAPELLIDO = :PERSOAPELLIDO, ");
        //query.Append(" PERSOCI = :PERSOCI, ");
        //query.Append(" PERSOSUPDIRECTO = :PERSOSUPDIRECTO, ");
        //query.Append(" PERSOFECHANACIMIENTO = :PERSOFECHANACIMIENTO, ");
        //query.Append(" PERSOGENERO = :PERSOGENERO, ");
        //query.Append(" PERSODIRECCION = :PERSODIRECCION, ");
        //query.Append(" PERSOTELEFONODOMICILIO = :PERSOTELEFONODOMICILIO, ");
        //query.Append(" PERSOTELEFONOMOVIL = :PERSOTELEFONOMOVIL, ");
        //query.Append(" PERSOTELEFONOLABORAL = :PERSOTELEFONOLABORAL, ");
        //query.Append(" PERSOEXTENSION = :PERSOEXTENSION, ");
        //query.Append(" PERSOFECHAINGRESO = :PERSOFECHAINGRESO, ");
        //query.Append(" PERSOFECHASALIDA = :PERSOFECHASALIDA, ");
        //query.Append(" PERSOCODIGOINSTITUCION = :PERSOCODIGOINSTITUCION, ");
        //query.Append(" PERSOSUELDOBASICO = :PERSOSUELDOBASICO, ");
        //query.Append(" PERSOEMAIL = :PERSOEMAIL, ");
        //query.Append(" PERSOESTADO = :PERSOESTADO, ");
        //query.Append(" FACTUALIZADIRECCION = :FACTUALIZADIRECCION, ");
        //query.Append(" FACTUALIZAMAIL = :FACTUALIZAMAIL, ");
        //query.Append(" FACTUALIZAMOVIL = :FACTUALIZAMOVIL, ");
        //query.Append(" FACTUALIZAFONODOMICILIO = :FACTUALIZAFONODOMICILIO, ");
        //query.Append(" FACTUALIZAFONOOFICINA = :FACTUALIZAFONOOFICINA, ");
        //query.Append(" FACTUALIZAEMPLEADO = :FACTUALIZAEMPLEADO, ");
        //query.Append(" FACTUALIZAPERSONA = :FACTUALIZAPERSONA, ");
        //query.Append(" FACTUALIZATRABAJO = :FACTUALIZATRABAJO, ");
        //query.Append(" FACTUALIZAINFOBASICA = :FACTUALIZAINFOBASICA, ");
    }
}
