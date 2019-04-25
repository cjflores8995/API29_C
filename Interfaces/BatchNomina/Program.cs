using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using timer = System.Timers;

namespace BatchNomina
{
    class Program
    {
        public static timer.Timer timerEmpleado = null;
        public static timer.Timer timerArea = null;
        public static timer.Timer timerCargo = null;
        public static Thread esperaTimer = null;

        static void Main(string[] args)
        {
            string error = "OK";
            bool flag = true;

            try
            {
                if (flag && error == "OK")
                {
                    flag = Logging.IniciaLog();
                }

                if (flag && error == "OK")
                {
                    flag = BthNomina.CargaParametros();
                }

                if (flag && error == "OK")
                {
                    Convivencia();
                }

                //if (flag && error == "OK")
                //{
                //    timerEmpleado = new timer.Timer(BthNomina.glbTiempo);
                //    timerEmpleado.Enabled = true;
                //    timerEmpleado.Elapsed += Convivencia;

                //    Program obj = new Program();
                //    obj.IniciaProceso();
                //}

                if (!flag || error != "OK")
                {

                    Util.ImprimePantalla("ERROR GENERAL: " + error);
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Console.ReadLine();
            }
        }

        public void IniciaProceso()
        {
            esperaTimer = new Thread((ThreadStart)delegate
            {
                Inactividad();
            });

            esperaTimer.Name = "timerEspera";
            esperaTimer.Start();
        }

        public void Inactividad()
        {
            while (true)
            {
                Util.ImprimePantalla(" ... ");
                System.Threading.Thread.Sleep(300000);
            }
        }

        public static void Convivencia(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerEmpleado.Stop();

                if (BthNomina.glbAreas)
                {
                    new BthNomina().ActualizarAreas();
                }

                Thread.Sleep(BthNomina.glbSleep);

                if (BthNomina.glbCargos)
                {
                    new BthNomina().ActualizarCargos();
                }

                Thread.Sleep(BthNomina.glbSleep);

                if (BthNomina.glbEmpleados)
                {
                    new BthNomina().ActualizarEmpleados();
                }

                timerEmpleado.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void Convivencia()
        {
            try
            {
                //timerEmpleado.Stop();

                if (BthNomina.glbAreas)
                {
                    new BthNomina().ActualizarAreas();
                }

                Thread.Sleep(BthNomina.glbSleep);

                if (BthNomina.glbCargos)
                {
                    new BthNomina().ActualizarCargos();
                }

                Thread.Sleep(BthNomina.glbSleep);

                if (BthNomina.glbEmpleados)
                {
                    new BthNomina().ActualizarEmpleados();
                }

                Thread.Sleep(BthNomina.glbSleep);

                //timerEmpleado.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }
    }
}
