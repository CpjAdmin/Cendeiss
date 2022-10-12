using CP_Entidades;
using CP_LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cendeisss
{
    public partial class DescargaInformes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String RolUsuario = Session["Rol"].ToString();

            if (Session["Id"] == null || RolUsuario == "USUARIO")
            {
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        if (RolUsuario != "ADMINISTRADOR")
                        {
                            FileUploadControl.Visible = false;
                            btnGuardarArchivo.Visible = false;
                            lbEstado.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                        string mensaje = "Contecte al administrador del sistema! - " + ex.Message.Replace("'", "\""); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                    }

                }
            }
        }

        // WebMethod - Cargar Archivos
        [WebMethod]
        public static List<String[]> CargarArchivosInformes()
        {
            try
            {
                String rutaArchivos = ConfigurationManager.AppSettings["RutaDirectorioInformes"];

                //CrearDirectorio
                CrearDirectorio(rutaArchivos);

                //Tipos de Archivo
                var extensiones = new List<string> { "*.xls", "*.doc", "*.pdf" };

                //Ruta y Extracción
                DirectoryInfo directorio = new DirectoryInfo(rutaArchivos);

                //Lista de Archivos
                List<String[]> listaArchivos = new List<String[]>();


                //Recorrer el directorio
                foreach (var item in extensiones)
                {
                    FileInfo[] listaArchivosInfo = directorio.GetFiles(item);

                    foreach (var info in listaArchivosInfo)
                    {
                        String[] valores = { info.Name.ToString(), info.Extension.ToString(), info.CreationTime.ToString(), info.LastWriteTime.ToString(), Math.Round(info.Length / 1024.00, 2).ToString() };
                        listaArchivos.Add(valores);
                    }
                }

                return listaArchivos;
            }
            catch (Exception ex)
            {

                HttpContext.Current.Response.StatusCode = 500;

                if (HttpContext.Current.Session["ID"] != null)
                {
                    throw new Exception("Se ha producido un error al cargar el informe ( " + ex.Message + " )", ex);
                }
                else
                {
                    throw new Exception("Su sesion ha expirado, cerrando la sesión.");
                }
            }
           
        }

        //********** Crear Directorio
        protected static bool CrearDirectorio(String path)
        {
            bool creado = false;
            try
            {
                //*** Verificar no Existe.
                if (!Directory.Exists(path))
                {
                    // Crear el directorio.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    creado = true;
                }

                return creado;
            }
            catch (Exception)
            {
                return creado;
            }
            finally { }
        }

        protected void btnGuardarInforme_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadControl.HasFile)
                {
                    string rutaArchivos = ConfigurationManager.AppSettings["RutaDirectorioInformes"];

                    if (FileUploadControl.PostedFile.ContentType == "application/vnd.ms-excel"
                        || FileUploadControl.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        || FileUploadControl.PostedFile.ContentType == "application/msword"
                        || FileUploadControl.PostedFile.ContentType == "application/pdf"
                        //|| FileUploadControl.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.presentationml.presentation"
                       )
                    {
                        if ((FileUploadControl.PostedFile.ContentLength / 1024) < 1048576)
                        {
                            string nombreArchivo = Path.GetFileName(FileUploadControl.FileName);
                            FileUploadControl.SaveAs(rutaArchivos + "\\" + nombreArchivo);
                            lbEstado.Text = "Archivo Cargado!";

                            //*** Auditoría
                            string Id = Session["Id"].ToString();
                            int cod_proceso = 6;
                            string descripcion = "Carga - " + nombreArchivo;

                            Proceso proceso = new Proceso
                            {
                                id_usuario = Id,
                                cod_proceso = cod_proceso,
                                descripcion = descripcion
                            };

                            ProcesoLN.getInstancia().AuditarProceso(proceso, System.Web.HttpContext.Current);

                        }
                        else
                        {
                            lbEstado.Text = "Archivo No Cargado!  (Tamaño máximo 1GB)";
                        }
                    }
                    else
                    {
                        lbEstado.Text = "Archivo No Cargado! (Solo Archivos PDF, Excel y Word)";
                    }
                }
                else
                    lbEstado.Text = "";

                FileUploadControl.PostedFile.InputStream.Dispose();

            }
            catch (Exception ex)
            {
                string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                string mensaje = "No se puede descargar el Archivo! - " + ex.Message.Replace("'", "\""); ;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
            }
        }

        protected void btnDescargarInforme_Click(object sender, EventArgs e)
        {
            try
            {


                string rutaArchivos = ConfigurationManager.AppSettings["RutaDirectorioInformes"];
                string archivo = archivoNombre.Value;
                string path = rutaArchivos + "\\" + archivo;

                if (archivo != string.Empty)
                {
                    //*** Auditoría
                    string Id = Session["Id"].ToString();
                    int cod_proceso = 7;
                    string descripcion = "Descarga - " + archivo;

                    Proceso proceso = new Proceso
                    {
                        id_usuario = Id,
                        cod_proceso = cod_proceso,
                        descripcion = descripcion
                    };

                    ProcesoLN.getInstancia().AuditarProceso(proceso, System.Web.HttpContext.Current);


                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    string filePath = path;
                    response.Clear();
                    response.ClearHeaders();
                    response.ClearContent();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition",string.Format("attachment; filename = \"{0}\"",Path.GetFileName(archivo)));
                    byte[] data = req.DownloadData(filePath);
                    response.BinaryWrite(data);
                    response.End();
                }

            }
            catch (Exception ex)
            {
                string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                string mensaje = "No se puede descargar el Archivo! - " + ex.Message.Replace("'", "\""); ;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
            }
        }
    }
}