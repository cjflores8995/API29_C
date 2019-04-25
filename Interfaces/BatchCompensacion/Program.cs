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

namespace BatchCompensacion
{
    class Program
    {
        public static timer.Timer timerExtraccion = null;
        public static timer.Timer timerLectura = null;
        public static timer.Timer timerCompensar = null;
        public static timer.Timer timerAutorizar = null;
        public static Thread timerEspera = null;

        static void Main(string[] args)
        {
            Console.Title = "Batch de Compensaciones";

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
                    flag = BthPos.CargaParametros(out error);
                }

                if (flag && error == "OK")
                {
                    #region extraccion
                    if (BthPos.glbExtraccionActiva)
                    {
                        timerExtraccion = new timer.Timer(BthPos.glbExtraccionTiempo);
                        timerExtraccion.Enabled = true;
                        timerExtraccion.Elapsed += Extraccion;
                    }
                    #endregion extraccion

                    #region lectura
                    if (BthPos.glbLecturaActiva)
                    {
                        timerLectura = new timer.Timer(BthPos.glbLecturaTiempo);
                        timerLectura.Enabled = true;
                        timerLectura.Elapsed += Lectura;
                    }
                    #endregion lectura

                    #region compensa
                    if (BthPos.glbCompensaActiva)
                    {
                        timerCompensar = new timer.Timer(BthPos.glbCompensaTiempo);
                        timerCompensar.Enabled = true;
                        timerCompensar.Elapsed += Compensar;
                    }
                    #endregion compensa

                    #region autoriza
                    if (BthPos.glbAutorizaActiva)
                    {
                        timerAutorizar = new timer.Timer(BthPos.glbAutorizaTiempo);
                        timerAutorizar.Enabled = true;
                        timerAutorizar.Elapsed += Autorizar;
                    }
                    #endregion autoriza

                    Program obj = new Program();
                    obj.IniciaProceso();
                }

                if (!flag && error != "OK")
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, error);
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Console.ReadLine();
            }
        }

        public void IniciaProceso()
        {
            timerEspera = new Thread((ThreadStart)delegate
            {
                Inactividad();
            });

            timerEspera.Name = "timerEspera";
            timerEspera.Start();
        }

        public void Inactividad()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
            }
        }

        public static void Extraccion(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerExtraccion.Stop();
                new BthPos().Extraccion();
                timerExtraccion.Start();
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
            }
        }

        public static void Lectura(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerLectura.Stop();
                new BthPos().Lectura();
                timerLectura.Start();
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
            }
        }

        public static void Compensar(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerCompensar.Stop();
                new BthPos().Compensar();
                timerCompensar.Start();
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
            }
        }

        public static void Autorizar(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerAutorizar.Stop();
                new BthPos().Autorizar();
                timerAutorizar.Start();
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
            }
        }
    }
}
