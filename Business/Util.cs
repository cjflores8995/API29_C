using Crypto;
using Ionic.Zip;
using Org.Mentalis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tamir.SharpSsh;

namespace Business
{
    public static class Util
    {
        public static string semilla = "S29-2017";

        public static string key = "ABAD2771878497543F87328C10E399EC";

        /// <summary>
        /// Convierte la cadena recibida en un SHA256
        /// </summary>
        /// <param name="dato"></param>
        /// <returns>String</returns>
        public static String HashSHA256(String dato)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(dato));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString().ToUpper();
            }
        }

        public static String validarTarjetaHash(String tarjetaSinHash, String tarjetaConHash)
        {
            String resultado = String.Empty;
            String tarjetaProcesoHash = HashSHA256(tarjetaSinHash);

            if(tarjetaProcesoHash.Equals(tarjetaConHash))
            {
                resultado = tarjetaSinHash;
            }

            return resultado;
        }

        public static void CalculaVeces(Int32 numeroProcesos, Int32 numeroRegistros, ref Int32 veces, ref Int32 ultimo)
        {
            try
            {
                Int32 modulo = numeroRegistros % numeroProcesos;

                if (modulo == 0)
                {
                    veces = numeroRegistros / numeroProcesos;
                    ultimo = numeroProcesos;
                }
                else if (modulo > 0)
                {
                    veces = (numeroRegistros / numeroProcesos) + 1;
                    ultimo = modulo;
                }
                else
                {
                    veces = 0;
                    ultimo = 0;
                }
            }
            catch
            {
                veces = -1;
                ultimo = -1;
            }
        }

        public static bool ExtraerSftp(string conexionFtp, string pathFtp, string pathLocal, out string error)
        {
            bool resp = true;
            error = "OK";
            SshTransferProtocolBase sftp = null;

            try
            {
                string[] parametros = conexionFtp.Split(';');
                sftp = new Scp(parametros[0], parametros[2]);
                sftp.Password = parametros[3].ToString();
                sftp.Connect();
                sftp.Get(pathFtp, pathLocal);
            }
            catch (Tamir.SharpSsh.jsch.SftpException ex1)
            {
                resp = false;
                error = ex1.message.ToString();
            }
            catch (Tamir.SharpSsh.jsch.JSchException ex2)
            {
                resp = false;
                error = "ERROR DE CONEXION/TIMEOUT, " + ex2.Message.ToString().ToUpper();
            }
            catch (Exception ex3)
            {
                resp = false;
                error = ex3.Message.ToString();
            }
            finally
            {
                if (sftp != null)
                {
                    sftp.Close();
                }
            }
            return resp;
        }

        public static bool ExtraerFtp(string conexionFtp, string pathFtp, string pathLocal, out string error)
        {
            bool flag = true;
            error = string.Empty;
            string responseDescription = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(conexionFtp))
                {
                    if (File.Exists(pathLocal))
                    {
                        File.Delete(pathLocal);
                    }

                    string[] parametros = conexionFtp.Split(';');
                    string url = "ftp://" + parametros[0] + ":" + parametros[1] + "/" + pathFtp;
                    FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(url);
                    req.Method = WebRequestMethods.Ftp.DownloadFile;
                    req.Credentials = new NetworkCredential(parametros[2], parametros[3], parametros[4]);
                    req.UseBinary = true;
                    req.Proxy = null;
                    FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                    Stream stream = response.GetResponseStream();
                    byte[] buffer = new byte[2048];
                    FileStream fs = new FileStream(pathLocal, FileMode.Create);
                    int ReadCount = stream.Read(buffer, 0, buffer.Length);
                    while (ReadCount > 0)
                    {
                        fs.Write(buffer, 0, ReadCount);
                        ReadCount = stream.Read(buffer, 0, buffer.Length);
                    }
                    responseDescription = response.StatusDescription;
                    fs.Close();
                    stream.Close();
                }
                else
                {
                    flag = false;
                    error = "NO EXISTEN PARAMETROS CONEXION FTP";
                }
            }
            catch (Exception ex)
            {
                flag = false;
                error = ex.Message.ToString().ToUpper();
            }
            return flag;
        }

        public static bool UploadFtp(string conexionFtp, string pathFtp, string pathLocal, string nombreArchivo, out string error)
        {
            bool flag = true;
            error = "OK";
            string responseDescription = string.Empty;
            try
            {
                string[] parametros = conexionFtp.Split(';');
                string url = "ftp://" + parametros[0] + ":" + parametros[1] + "/" + pathFtp + "/" + nombreArchivo;
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                if (!string.IsNullOrEmpty(parametros[4]))
                {
                    request.Credentials = new NetworkCredential(parametros[2], parametros[3], parametros[4]);
                }
                else
                {
                    request.Credentials = new NetworkCredential(parametros[2], parametros[3]);
                }
                StreamReader sourceStream = new StreamReader(pathLocal + nombreArchivo);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                flag = false;
                error = ex.Message.ToString().ToUpper();
            }
            return flag;
        }

        public static bool UploadSftp(string conexionFtp, string pathFtp, string pathLocal, string nombreArchivo, out string error)
        {
            bool resp = true;
            error = "OK";
            SshTransferProtocolBase sftp = null;

            try
            {
                string[] parametros = conexionFtp.Split(';');
                sftp = new Scp(parametros[0], parametros[2]);
                sftp.Password = parametros[3].ToString();
                sftp.Connect(Convert.ToInt16(parametros[1]));
                sftp.Put(pathLocal + nombreArchivo, pathFtp + nombreArchivo);
            }
            catch (Tamir.SharpSsh.jsch.SftpException ex1)
            {
                resp = false;
                error = ex1.message.ToString();
            }
            catch (Tamir.SharpSsh.jsch.JSchException ex2)
            {
                resp = false;
                error = "ERROR DE CONEXION/TIMEOUT, " + ex2.Message.ToString().ToUpper();
            }
            catch (Exception ex3)
            {
                resp = false;
                error = ex3.Message.ToString();
            }
            finally
            {
                if (sftp != null)
                {
                    sftp.Close();
                }
            }
            return resp;
        }

        public static bool BorrarFtp(string conexionFtp, string pathFtp, out string error)
        {
            bool flag = true;
            error = string.Empty;

            try
            {
                string[] parametros = conexionFtp.Split(';');
                string url = "ftp://" + parametros[0] + ":" + parametros[1] + "/" + pathFtp;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Credentials = new NetworkCredential(parametros[2], parametros[3], parametros[4]);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                flag = false;
                error = ex.Message.ToString();
            }
            return flag;
        }

        public static List<string> ListarArchivosFtp(string conexionFtp, string pathFtp, out string error)
        {
            List<string> directories = new List<string>();
            error = string.Empty;

            try
            {
                string[] parametros = conexionFtp.Split(';');
                string url = "ftp://" + parametros[0] + ":" + parametros[1] + "/" + pathFtp;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(parametros[2], parametros[3], parametros[4]);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    directories.Add(line);
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                directories = null;
                error = ex.Message.ToString();
            }

            return directories;
        }

        public static bool DescomprimirArchivo(string ruta, string archivo, string password, out string error)
        {
            bool resp = true;
            error = "OK";

            try
            {
                using (ZipFile archivoComprimido = ZipFile.Read(ruta + archivo))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        archivoComprimido.Password = password;
                    }

                    archivoComprimido.ExtractAll(ruta, true);
                }
            }
            catch (Exception ex)
            {
                resp = false;
                error = ex.Message.ToString();
            }
            return resp;
        }

        public static string ArchivoToString(string ruta, string archivo)
        {
            string result = string.Empty;
            try
            {
                StreamReader reader = new StreamReader(ruta + archivo);
                BinaryReader binReader = new BinaryReader(reader.BaseStream);
                byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
                binReader.Close();
                reader.Close();
                result = ByteArrayToString(binFile);
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }

        public static string ArchivoToBase64String(string ruta, string archivo)
        {
            string result = string.Empty;
            try
            {
                StreamReader reader = new StreamReader(ruta + archivo);
                BinaryReader binReader = new BinaryReader(reader.BaseStream);
                byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
                result = Convert.ToBase64String(binFile);
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string ObjToString(Object obj)
        {
            string data = null;
            try
            {
                XmlSerializer op1 = new XmlSerializer(obj.GetType());
                System.IO.StringWriter ss1 = new System.IO.StringWriter();
                XmlSerializerNamespaces ns_app = new XmlSerializerNamespaces();
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(String.Empty, String.Empty);
                op1.Serialize(ss1, obj, ns);
                var xml1 = ss1.ToString().Replace("xmlns=\"http://tempuri.org/\"", "");
                data = xml1.ToString();
            }
            catch (Exception)
            {
            }
            return data;
        }

        public static Object StringToObj(string xml, Type t)
        {
            try
            {
                XmlSerializer s_dsl = new XmlSerializer(t);
                Object iso = (Object)s_dsl.Deserialize(new System.IO.StringReader(xml));
                return iso;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetSecuencial(int length)
        {
            try
            {
                byte[] buffer = Guid.NewGuid().ToByteArray();
                string number = BitConverter.ToUInt32(buffer, length).ToString();
                return number.PadLeft(length, '0').Substring(0, length);
            }
            catch
            {
                string n = "1";
                return n.PadLeft(length, '0');
            }
        }

        public static void ImprimePantalla(string mensaje)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + mensaje);
        }

        public static void ImprimePantalla(string tipo, string mensaje)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " [" + tipo + "] " + mensaje);
        }

        public static string ReturnExceptionString(Exception ex)
        {
            return ex.Message.ToString().Replace(Environment.NewLine, " ").Replace("\n", " ").Replace("\\", "-").Replace("'", "");
        }

        public static string ReturnExceptionString(string texto)
        {
            return texto.Replace(Environment.NewLine, " ").Replace("\n", " ").Replace("\\", "-").Replace("'", "");
        }

        public static string DecimalToString(Decimal? valor)
        {
            string resp = string.Empty;
            try
            {
                if (valor != null)
                    resp = valor.Value.ToString("F2").Replace(",", "").Replace(".", "");
                else
                    resp = "0";
            }
            catch
            {
                resp = "0";
            }
            return resp;
        }

        public static decimal StringToDecimal(string texto)
        {
            decimal valor = 0;

            try
            {
                valor = Convert.ToDecimal(texto) / 100;
            }
            catch
            {
                valor = 0;
            }

            return valor;
        }

        public static DateTime FHasta()
        {
            return new DateTime(2999, 12, 31, 0, 0, 0);
        }

        public static DateTime FSysdate()
        {
            return DateTime.Now;
        }

        public static DateTime? ConvertirFecha(string texto)
        {
            try
            {
                return Convert.ToDateTime(texto);
            }
            catch
            {
                return null;
            }
        }

        public static Int32? ConvertirNumero(string texto)
        {
            try
            {
                return Convert.ToInt32(texto);
            }
            catch
            {
                return null;
            }
        }

        public static Decimal? ConvertirDecimal(string texto)
        {
            try
            {
                return Convert.ToDecimal(texto);
            }
            catch
            {
                return null;
            }
        }

        public static Double? ConvertirDouble(string texto)
        {
            try
            {
                return Convert.ToDouble(texto);
            }
            catch
            {
                return null;
            }
        }

        public static string OfuscaTarjeta(string texto)
        {
            string ofusca = string.Empty;
            try
            {
                ofusca = texto.Substring(0, 4);
                ofusca = ofusca.PadRight(texto.Length - 4, 'X');
                ofusca += texto.Substring(texto.Length - 4, 4);
            }
            catch
            {
                ofusca = ("").PadLeft(texto.Length, 'X');
            }
            return ofusca;
        }

        public static string OfuscaTarjeta(string texto, int inicio, int fin)
        {
            string ofusca = string.Empty;
            try
            {
                ofusca = texto.Substring(0, inicio);
                ofusca = ofusca.PadRight(texto.Length - fin, 'X');
                ofusca += texto.Substring(texto.Length - fin, fin);
            }
            catch
            {
                ofusca = ("").PadLeft(texto.Length, 'X');
            }
            return ofusca;
        }

        public static string EncriptaFitBank(string texto)
        {
            cryptoFit.ProcessEncription crypto = new cryptoFit.ProcessEncription();
            return Convert.ToString(crypto.EncryptPassword(texto, "FIT-2008"));
        }

        public static int RestarFechasEnDias(DateTime oldDate, DateTime newDate)
        {
            TimeSpan ts = newDate - oldDate;
            return ts.Days;
        }

        public static string MostarAlerta(string titulo, string mensaje, string tipo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=text/javascript>");
            sb.Append("swal({ ");
            sb.Append("title: \"" + titulo + "\", ");
            sb.Append("text: \"" + mensaje + "\", ");
            switch (tipo)
            {
                case "OK":
                    sb.Append("type: \"success\" ");
                    break;
                case "ER":
                    sb.Append("type: \"error\" ");
                    break;
                case "WR":
                    sb.Append("type: \"warning\" ");
                    break;
                case "IN":
                    sb.Append("type: \"info\" ");
                    break;
                default:
                    sb.Append("type: \"info\" ");
                    break;
            }
            sb.Append("});");
            sb.Append("</script>");
            return sb.ToString();
        }

        public static string MostarAlertaFormularios(string titulo, string mensaje, string tipo)
        {
            return "mostrarAlerta('" + titulo + "', '" + mensaje + "', '" + tipo + "')";
        }

        public static string EncryptKey(string texto)
        {
            return CryptoUtil.Encrypt(texto, semilla);
            //string result = string.Empty;

            //try
            //{
            //    //arreglo de bytes donde guardaremos la llave
            //    byte[] keyArray;

            //    //arreglo de bytes donde guardaremos el texto
            //    //que vamos a encriptar

            //    byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

            //    //se utilizan las clases de encriptación
            //    //provistas por el Framework
            //    //Algoritmo MD5

            //    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            //    //se guarda la llave para que se le realice
            //    //hashing

            //    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            //    hashmd5.Clear();

            //    //Algoritmo 3DAS
            //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //    tdes.Key = keyArray;
            //    tdes.Mode = CipherMode.ECB;
            //    tdes.Padding = PaddingMode.PKCS7;

            //    //se empieza con la transformación de la cadena
            //    ICryptoTransform cTransform = tdes.CreateEncryptor();

            //    //arreglo de bytes donde se guarda la
            //    //cadena cifrada
            //    byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            //    tdes.Clear();

            //    //se regresa el resultado en forma de una cadena
            //    result = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            //}
            //catch (Exception ex)
            //{
            //    result = "";
            //    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            //}
            //return result;
        }

        public static string DecryptKey(string texto)
        {
            return CryptoUtil.Decrypt(texto, semilla);

            //string result = string.Empty;

            //try
            //{
            //    texto = texto.Replace(" ", "+");

            //    byte[] keyArray;

            //    //convierte el texto en una secuencia de bytes
            //    byte[] Array_a_Descifrar = Convert.FromBase64String(texto);

            //    //se llama a las clases que tienen los algoritmos
            //    //de encriptación se le aplica hashing
            //    //algoritmo MD5
            //    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //    hashmd5.Clear();
            //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //    tdes.Key = keyArray;
            //    tdes.Mode = CipherMode.ECB;
            //    tdes.Padding = PaddingMode.PKCS7;
            //    ICryptoTransform cTransform = tdes.CreateDecryptor();
            //    byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
            //    tdes.Clear();

            //    //se regresa en forma de cadena
            //    result = UTF8Encoding.UTF8.GetString(resultArray);
            //}
            //catch (Exception ex)
            //{
            //    result = "";
            //    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            //}

            //return result;
        }

        /// <summary>
        /// Recibe la fecha actual y la convierte en un strinf con el formato yyyyMMdd
        /// <developer>Carlos Flores</developer>
        /// </summary>
        /// <param name="FECHA"></param>
        /// <returns></returns>
        public static string ConvertToYearMonthDay(DateTime FECHA)
        {
            string FECHA_RETORNO = string.Empty;

            try
            {
                FECHA_RETORNO = FECHA.ToString("yyyyMMdd");

            } catch(Exception ex)
            {
                FECHA_RETORNO = ex.Message.ToUpper().ToString();
            }

            return FECHA_RETORNO;
        }

        public static bool ValidaFechas(string fecha)
        {
            bool isDate = true;
            try
            {
                DateTime dateValue;
                dateValue = DateTime.ParseExact(fecha, "dd/MM/yyyy", null);
            }
            catch
            {
                isDate = false;
            }
            return isDate;
        }

        public static string Encriptar(string texto, string semilla)
        {
            return CryptoUtil.Encrypt(texto, semilla);
        }

        public static string Desencriptar(string texto, string semilla)
        {
            return CryptoUtil.Decrypt(texto, semilla);
        }
    }

    public class csEstructuraNotificacion
    {
        [XmlRoot("notificacion")]
        public class notificacion
        {
            private object[] _items = null;
            [XmlElementAttribute("mensaje", typeof(mensaje), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            [XmlElementAttribute("adjuntos", typeof(archivo[]), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public object[] items { get { return _items; } set { _items = value; } }
        }
        public class mensaje
        {
            string _from = string.Empty;
            [XmlElementAttribute("from", Order = 1)]
            public string from { get { return _from; } set { _from = value; } }

            string _to = string.Empty;
            [XmlElementAttribute("to", Order = 2)]
            public string to { get { return _to; } set { _to = value; } }

            string _cc = string.Empty;
            [XmlElementAttribute("cc", Order = 3)]
            public string cc { get { return _cc; } set { _cc = value; } }

            string _asunto = string.Empty;
            [XmlElementAttribute("asunto", Order = 4)]
            public string asunto { get { return _asunto; } set { _asunto = value; } }

            string _parametros = string.Empty;
            [XmlElementAttribute("parametros", Order = 5)]
            public string parametros { get { return _parametros; } set { _parametros = value; } }

            string _imagen = string.Empty;
            [XmlElementAttribute("imagen", Order = 6)]
            public string imagen { get { return _imagen; } set { _imagen = value; } }
        }
        [XmlTypeAttribute("archivo")]
        public class archivo
        {
            string _nombre = string.Empty;
            [XmlElementAttribute("nombre", Order = 1)]
            public string nombre { get { return _nombre; } set { _nombre = value; } }

            string _contenido = string.Empty;
            [XmlElementAttribute("contenido", Order = 2)]
            public string contenido { get { return _contenido; } set { _contenido = value; } }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class respuesta
        {
            private string _cod_respuesta;
            private string _des_respuesta;
            private string _id_transaccion;

            [System.Xml.Serialization.XmlElementAttribute("cod_respuesta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string cod_respuesta
            {
                get { return _cod_respuesta; }
                set { _cod_respuesta = value; }
            }

            [System.Xml.Serialization.XmlElementAttribute("des_respuesta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string des_respuesta
            {
                get { return _des_respuesta; }
                set { _des_respuesta = value; }
            }

            [System.Xml.Serialization.XmlElementAttribute("id_transaccion", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string id_transaccion
            {
                get { return _id_transaccion; }
                set { _id_transaccion = value; }
            }
        }
    }
}
