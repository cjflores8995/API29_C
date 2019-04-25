using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public class BthNomina
    {
        #region variables

        //StreamWriter file = null;

        public static Int32 glbProcesosBdd = 0;

        public static string glbCompersUser = string.Empty;
        public static string glbCompersPassword = string.Empty;

        public static Int32 glbTiempo = 0;
        public static Int32 glbSleep = 0;
        public static Int32 glbProcesos = 0;
        public static bool glbAreas = false;
        public static bool glbCargos = false;
        public static bool glbEmpleados = false;
        public static string glbTipoAreas = string.Empty;
        public static string glbTipoCargos = string.Empty;
        public static string glbTipoEmpleados = string.Empty;

        #endregion variables

        #region carga parametros

        public static bool CargaParametros()
        {
            bool response = true;

            try
            {
                glbProcesosBdd = Convert.ToInt32(ConfigurationManager.AppSettings["bddProcesos"]);

                glbCompersUser = ConfigurationManager.AppSettings["compersUser"];
                glbCompersPassword = ConfigurationManager.AppSettings["compersPassword"];

                glbTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["tiempo"]);
                glbSleep = Convert.ToInt32(ConfigurationManager.AppSettings["sleep"]);
                glbProcesos = Convert.ToInt32(ConfigurationManager.AppSettings["procesos"]);
                glbAreas = Convert.ToBoolean(ConfigurationManager.AppSettings["areas"]);
                glbCargos = Convert.ToBoolean(ConfigurationManager.AppSettings["cargos"]);
                glbEmpleados = Convert.ToBoolean(ConfigurationManager.AppSettings["empleados"]);
                glbTipoAreas = ConfigurationManager.AppSettings["tipoAreas"];
                glbTipoCargos = ConfigurationManager.AppSettings["tipoCargos"];
                glbTipoEmpleados = ConfigurationManager.AppSettings["tipoEmpleados"];
            }
            catch (Exception ex)
            {
                response = false;
                Util.ImprimePantalla(ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return response;
        }

        #endregion carga parametros

        #region Actualiza Empleados

        public void ActualizarEmpleados()
        {
            List<VNOMINACOMPERSEMPLEADOS> ltEmpleados = null;
            string error = string.Empty;
            int x = 0;

            try
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "INICIO PROCESO EMPLEADOS");

                ltEmpleados = new VNOMINACOMPERSEMPLEADOS().ListarEmpleados(glbTipoEmpleados);
                if (ltEmpleados != null)
                {
                    foreach (VNOMINACOMPERSEMPLEADOS obj in ltEmpleados)
                    {
                        x++;
                        try
                        {
                            wsCompers.WebService29Octubre ws = new wsCompers.WebService29Octubre();
                            wsCompers.LoginInfo credenciales = new wsCompers.LoginInfo();
                            credenciales.UserName = glbCompersUser;
                            credenciales.PassWord = glbCompersPassword;
                            ws.LoginInfoValue = credenciales;

                            if (obj.CARGOSEC != null && obj.AREASEC != null)
                            {
                                bool flag = ws.InsertaOActualizaPersona(
                                    obj.PERSOSEC.Value,
                                    obj.AREASEC.Value,
                                    obj.CARGOSEC.Value,
                                    obj.PERSONOMBRE,
                                    obj.PERSOAPELLIDO,
                                    Convert.ToInt32(obj.PERSOSUELDOBASICO.Value),
                                    obj.PERSOCI,
                                    obj.PERSOSUPDIRECTO.Value,
                                    obj.PERSOFECHANACIMIENTO.Value.ToString("yyyy/MM/dd"),
                                    obj.PERSOFECHAINGRESO.Value.ToString("yyyy/MM/dd"),
                                    obj.PERSOGENERO.Value,
                                    obj.PERSODIRECCION,
                                    obj.PERSOTELEFONODOMICILIO,
                                    obj.PERSOTELEFONOMOVIL,
                                    obj.PERSOTELEFONOLABORAL,
                                    obj.PERSOEXTENSION,
                                    obj.PERSOCODIGOINSTITUCION,
                                    obj.PERSOEMAIL,
                                    obj.PERSOESTADO);

                                if (flag)
                                {
                                    error = "OK";
                                }
                                else
                                {
                                    error = "ERROR";
                                    Logging.EscribirLog("ACTUALIZANDO CPERSONA: " + obj.PERSOSEC.Value + " => " + error, null, "ERR");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            error = ex.Message.ToString().ToUpper();
                            Logging.EscribirLog("ACTUALIZANDO CPERSONA: " + obj.PERSOSEC.Value, ex, "ERR");
                        }

                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "CPERSONA: " + obj.PERSOSEC.Value.ToString().PadLeft(10, ' ') + " " + error);

                        if (x >= glbProcesos)
                        {
                            Thread.Sleep(glbSleep);
                            x = 0;
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN EMPLEADOS: " + error);
                }

                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "FINALIZA PROCESO EMPLEADOS ");
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        #endregion Actualiza Empleados

        #region Actualiza Areas

        public void ActualizarAreas()
        {
            List<VNOMINACOMPERSAREAS> ltDivisiones = null;
            List<VNOMINACOMPERSAREAS> ltDepartamentos = null;
            List<VNOMINACOMPERSAREAS> ltUnidades = null;
            string error = string.Empty;

            try
            {
                //file = new StreamWriter(@"D:\areas.txt");

                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "INICIA CARGA AREAS ");

                if (ActualizarArea(1, 0, "COAC 29 DE OCTUBRE", 1, "A", out error))
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "EMPRESA: OK");

                    #region divisiones

                    ltDivisiones = new VNOMINACOMPERSAREAS().ListarDivisiones(glbTipoAreas);
                    if (ltDivisiones != null)
                    {
                        foreach (VNOMINACOMPERSAREAS division in ltDivisiones)
                        {
                            if (ActualizarArea(division.CODIGO.Value, 1, division.NOMBRE, 2, division.ESTADO, out error))
                            {
                                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "DIVISION: " + division.CDIVISION + " OK");

                                #region departamentos

                                ltDepartamentos = new VNOMINACOMPERSAREAS().ListarDepartamentos(division.CDIVISION, glbTipoAreas);
                                if (ltDepartamentos != null)
                                {
                                    foreach (VNOMINACOMPERSAREAS departamento in ltDepartamentos)
                                    {
                                        if (ActualizarArea(departamento.CODIGO.Value, division.CODIGO.Value, departamento.NOMBRE, 3, departamento.ESTADO, out error))
                                        {
                                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "DEPARTAMENTO: " + departamento.CDIVISION + "-" + departamento.CDEPARTAMENTO + " OK");

                                            #region unidades

                                            ltUnidades = new VNOMINACOMPERSAREAS().ListarUnidades(departamento.CDIVISION, departamento.CDEPARTAMENTO, glbTipoAreas);
                                            if (ltUnidades != null)
                                            {
                                                foreach (VNOMINACOMPERSAREAS unidad in ltUnidades)
                                                {
                                                    if (ActualizarArea(unidad.CODIGO.Value, departamento.CODIGO.Value, unidad.NOMBRE, 4, unidad.ESTADO, out error))
                                                    {
                                                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "UNIDAD: " + unidad.CDIVISION + "-" + unidad.CDEPARTAMENTO + "-" + unidad.CUNIDAD + " OK");
                                                    }
                                                    else
                                                    {
                                                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "UNIDAD: " + unidad.CDIVISION + "-" + unidad.CDEPARTAMENTO + "-" + unidad.CUNIDAD + " ERROR");
                                                        Logging.EscribirLog("ERROR ACTUALIZANDO UNIDAD: " + unidad.CDIVISION + "-" + unidad.CDEPARTAMENTO + "-" + unidad.CUNIDAD + " => " + error, null, "ERR");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN UNIDADES, DEPARTAMENTO: " + departamento.CDIVISION + "-" + departamento.CDEPARTAMENTO);
                                            }

                                            #endregion unidades
                                        }
                                        else
                                        {
                                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "DEPARTAMENTO: " + departamento.CDIVISION + "-" + departamento.CDEPARTAMENTO + " ERROR");
                                        }
                                    }
                                }
                                else
                                {
                                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN DEPARTAMENTOS, DIVISION: " + division.CDIVISION);
                                }

                                #endregion departamentos
                            }
                            else
                            {
                                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "DIVISION: " + division.CDIVISION + " ERROR");
                                Logging.EscribirLog("ERROR ACTUALIZANDO DIVISION: " + division.CDIVISION + " => " + error, null, "ERR");
                            }

                            Thread.Sleep(glbSleep);
                        }
                    }
                    else
                    {
                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN DIVISIONES");
                    }

                    #endregion divisiones
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "EMPRESA: ERROR");
                    Logging.EscribirLog("ERROR ACTUALIZANDO EMPRESA => " + error, null, "ERR");
                }

                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "FIN CARGA AREAS ");

                //file.Close();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public bool ActualizarArea(Int32 carea, Int32 cpadre, string desripcion, int nivel, string estado, out string error)
        {
            bool flag = false;
            error = string.Empty;

            try
            {
                //file.WriteLine(carea + ";" + cpadre + ";" + desripcion + ";" + nivel + ";" + estado);

                //flag = true;
                //error = "OK";

                wsCompers.WebService29Octubre ws = new wsCompers.WebService29Octubre();
                wsCompers.LoginInfo credenciales = new wsCompers.LoginInfo();
                credenciales.UserName = glbCompersUser;
                credenciales.PassWord = glbCompersPassword;
                ws.LoginInfoValue = credenciales;

                flag = ws.InsertaOActualizaArea(carea, cpadre, desripcion, nivel, estado);

                if (flag)
                {
                    error = "OK";
                }
                else
                {
                    error = "RESPUESTA FALSE";
                }
            }
            catch (Exception ex)
            {
                flag = false;
                error = ex.Message.ToString().ToUpper();
            }

            return flag;
        }

        #endregion Actualiza Areas

        #region Actualiza Cargos

        public void ActualizarCargos()
        {
            List<VNOMINACOMPERSCARGOS> ltCargos = null;
            string error = string.Empty;
            int x = 0;
            try
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "INICIO PROCESO CARGOS ");

                ltCargos = new VNOMINACOMPERSCARGOS().ListarCargos(glbTipoCargos);
                if (ltCargos != null)
                {
                    foreach (VNOMINACOMPERSCARGOS cargo in ltCargos)
                    {
                        x++;
                        try
                        {
                            wsCompers.WebService29Octubre ws = new wsCompers.WebService29Octubre();
                            wsCompers.LoginInfo credenciales = new wsCompers.LoginInfo();
                            credenciales.UserName = glbCompersUser;
                            credenciales.PassWord = glbCompersPassword;
                            ws.LoginInfoValue = credenciales;

                            if (ws.InsertaOActualizaCargo(cargo.CODIGO.Value, cargo.NOMBRE, cargo.ESTADO))
                            {
                                error = "OK";
                            }
                            else
                            {
                                error = "RETURN FALSE";
                                Logging.EscribirLog("ACTUALIZANDO CARGO: " + cargo.CODIGO.Value + " => " + error, null, "ERR");
                            }
                        }
                        catch (Exception ex)
                        {
                            error = ex.Message.ToString().ToUpper();
                            Logging.EscribirLog("ACTUALIZANDO CARGO: " + cargo.CODIGO.Value, ex, "ERR");
                        }

                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "CARGO: " + cargo.CODIGO.Value + " " + error);

                        if (x >= glbProcesos)
                        {
                            Thread.Sleep(glbSleep);
                            x = 0;
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN CARGOS: " + error);
                }

                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "FIN PROCESO CARGOS ");
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        #endregion Actualiza Cargos
    }
}
