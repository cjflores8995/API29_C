using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic.BthPos
{
    class ExtractFiles
    {

        StreamReader srArchivo = null;
        string linea = string.Empty;

        /// <summary>
        /// Extrae los archivos provenientes de datafast
        /// </summary>
        public void ExtractDatafastFiles(String path, bool flag, string error, string glbPathOrigen, string subcarpeta)
        {
            try
            {

                DirectoryInfo filesDatafast = new DirectoryInfo(path);

                foreach (var file in filesDatafast.EnumerateFiles())
                {
                    switch (Path.GetExtension(file.FullName).ToLower())
                    {
                        case ".zip":
                            if (file.Name.Split('_').ToArray()[0] == "COOP")
                            {
                                string[] parametros = ConfigurationManager.AppSettings["datafast"].ToString().Split(';');
                                flag = Util.DescomprimirArchivo(path + @"\", file.Name, parametros[6], out error);
                                if (flag && error == "OK")
                                    File.Delete(file.FullName);
                            }
                            else
                            {
                                flag = Util.DescomprimirArchivo(path + @"\", file.Name, null, out error);
                                if (flag && error == "OK")
                                    File.Delete(file.FullName);
                            }
                            break;
                        case ".cop":
                            string rutaNueva = file.Name.Substring(4, 4) + file.Name.Substring(0, 2) + file.Name.Substring(2, 2);
                            rutaNueva = string.Format(glbPathOrigen, rutaNueva, subcarpeta);
                            if (!Directory.Exists(rutaNueva))
                                Directory.CreateDirectory(rutaNueva);
                            File.Copy(file.FullName, rutaNueva + file.Name, true);
                            File.Delete(file.FullName);
                            break;
                    }
                }

            } catch(Exception e)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, e.Message);
            }
        }

        /// <summary>
        /// Extrae los archivos provenientes de Datafast
        /// </summary>
        public void ExtractLafavoritaFiles(String path, bool flag, string error, string glbPathOrigen, string subcarpeta)
        {
            DirectoryInfo filesFavorita = new DirectoryInfo(path);
            foreach (var file in filesFavorita.EnumerateFiles())
            {
                switch (Path.GetExtension(file.FullName).ToLower())
                {
                    case ".zip":
                        flag = Util.DescomprimirArchivo(path + @"\", file.Name, null, out error);
                        if (flag && error == "OK")
                            File.Delete(file.FullName);
                        break;
                    default:
                        try
                        {
                            srArchivo = new StreamReader(file.FullName, System.Text.Encoding.Default);
                            linea = srArchivo.ReadLine();
                            srArchivo.Close();
                            string rutaNueva = linea.Substring(1, 4) + linea.Substring(5, 2) + linea.Substring(7, 2);
                            rutaNueva = string.Format(glbPathOrigen, rutaNueva, subcarpeta);
                            if (!Directory.Exists(rutaNueva))
                                Directory.CreateDirectory(rutaNueva);
                            File.Copy(file.FullName, rutaNueva + file.Name, true);
                            File.Delete(file.FullName);
                        }
                        catch (Exception ex)
                        {
                            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ARCHIVO: " + file.FullName + " ", ex, "ERR");
                        }
                        finally
                        {
                            if (srArchivo != null)
                                srArchivo.Close();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Extrae los archivos provenientes de Econofarm
        /// </summary>
        public void ExtractEconofarmFiles(String path, bool flag, string error, string glbPathOrigen, string subcarpeta)
        {
            DirectoryInfo filesEconofarm = new DirectoryInfo(path);
            foreach (var file in filesEconofarm.EnumerateFiles())
            {
                switch (Path.GetExtension(file.FullName).ToLower())
                {
                    case ".zip":
                        flag = Util.DescomprimirArchivo(path + @"\", file.Name, null, out error);
                        if (flag && error == "OK")
                            File.Delete(file.FullName);
                        break;
                    default:
                        string rutaNueva = file.Name.Substring(0, 4) + file.Name.Substring(4, 2) + file.Name.Substring(6, 2);
                        rutaNueva = string.Format(glbPathOrigen, rutaNueva, subcarpeta);
                        if (!Directory.Exists(rutaNueva))
                            Directory.CreateDirectory(rutaNueva);
                        File.Copy(file.FullName, rutaNueva + file.Name, true);
                        File.Delete(file.FullName);
                        break;
                }
            }
        }

        /// <summary>
        /// Extrae los archivos provenientes de Farcomed
        /// </summary>
        public void ExtractFarcomedFiles(String path, bool flag, string error, string glbPathOrigen, string subcarpeta)
        {
            DirectoryInfo filesFarcomed = new DirectoryInfo(path);
            foreach (var file in filesFarcomed.EnumerateFiles())
            {
                switch (Path.GetExtension(file.FullName).ToLower())
                {
                    case ".zip":
                        flag = Util.DescomprimirArchivo(path + @"\", file.Name, null, out error);
                        if (flag && error == "OK")
                            File.Delete(file.FullName);
                        break;
                    default:
                        string rutaNueva = file.Name.Substring(0, 4) + file.Name.Substring(4, 2) + file.Name.Substring(6, 2);
                        rutaNueva = string.Format(glbPathOrigen, rutaNueva, subcarpeta);
                        if (!Directory.Exists(rutaNueva))
                            Directory.CreateDirectory(rutaNueva);
                        File.Copy(file.FullName, rutaNueva + file.Name, true);
                        File.Delete(file.FullName);
                        break;
                }
            }
        }
    }
}
