using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using CP_Entidades;
using CP_LogicaNegocio;

namespace Cendeisss
{
    public partial class MasterCC : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] == null)
            {
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        String IdUsuario = Session["Id"].ToString().Trim();
                        String RolUsuario = Session["Rol"].ToString();
                        String NombreUsuario = Session["Nombre"].ToString();

                        Id.Value = IdUsuario;
                        Nombre.Value = NombreUsuario;
                        Rol.Value = RolUsuario;

                        string primerInicio = Session["UltimaEdicion"].ToString();
                        
                        if (string.IsNullOrEmpty(primerInicio))
                        {
                            optSaldos.Visible = false;
                            optMovimientos.Visible = false;
                            optProcesos.Visible = false;
                            optMenuMantenimiento.Visible = false;
                            optDescargaInformes.Visible = false;

                            string titulo = "ACTUALICE SUS DATOS";
                            string mensaje = "Actualice sus datos para poder visualizar las opciones de la página.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                        }
                        else
                        {
                            if (RolUsuario != "ADMINISTRADOR")
                            {
                                optMenuMantenimiento.Visible = false;
                            }
                            if (RolUsuario == "USUARIO")
                            {
                                optDescargaInformes.Visible = false;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        string pagina = HttpContext.Current.Request.UrlReferrer.PathAndQuery.ToString();
                        string titulo = pagina.Replace("/", "");
                        string mensaje = "Contacte al administrador del sistema...!";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                    }
                }
            }
        }

        //********** ObtenerPDF
        protected void ObtenerPDF(object Source, EventArgs e)
        {
            {
                try
                {
                    string id = pdfMov.Value;

                    int n;
                    bool isNumeric = int.TryParse(id.Substring(0, 1), out n);

                    if (isNumeric)
                    {
                        if (id.Substring(0, 1) != "0")
                        {
                            id = "0" + id;
                        }

                        Asociado ObjUsuario = AsociadoLN.getInstancia().GetUsuario(id);

                        if (ObjUsuario == null)
                        {
                            string titulo = "ESTADO DE CUENTA";
                            string mensaje = "El asociado con cédula <b> " + id + " </b> no existe.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                        }
                        else
                        {
                            // ReportServer 
                            string ReportServer = ConfigurationManager.AppSettings["ReportServer"];
                            string reporte = ConfigurationManager.AppSettings["rpt_EstadoCuenta"];

                            //Llamado al Reporte
                            string URLReport = ReportServer + "/Pages/ReportViewer.aspx?" + reporte + "&rs:Command=Render&rs:Format=pdf";
                            string paramCedula = id;
                            string URL = URLReport + "&P_CEDULA=" + paramCedula;

                            HttpWebRequest Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
                            Req.Credentials = ICredenciales();

                            Req.UseDefaultCredentials = true;
                            Req.PreAuthenticate = true;
                            Req.Method = "GET";
                            HttpWebResponse HttpWResp = (HttpWebResponse)Req.GetResponse();
                            Stream fStream = HttpWResp.GetResponseStream();

                            byte[] pdfBytes = ReadFully(fStream);

                            HttpWResp.Close();

                            if (pdfBytes != null && pdfBytes.Length > 0)
                            {
                                //Nombre PDF
                                string pdfNombre = "EstadoCuenta_" + id + @".pdf";

                                Response.Clear();
                                Response.ClearContent();
                                Response.ClearHeaders();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "inline; filename=\"" + pdfNombre + "\"");
                                Response.BinaryWrite(pdfBytes);
                                Response.Flush();
                                Response.SuppressContent = true;
                                HttpContext.Current.ApplicationInstance.CompleteRequest();

                                //*** Auditoría
                                string idUsuario = Session["Id"].ToString().Trim();
                                int cod_proceso_aut = 11;
                                string descripcion = "Estado de Cuenta: " + id + " - Generado por ( " + idUsuario + " )";

                                Proceso proceso = new Proceso
                                {
                                    id_usuario = id,
                                    cod_proceso = cod_proceso_aut,
                                    descripcion = descripcion
                                };

                                ProcesoLN.getInstancia().AuditarProceso(proceso, System.Web.HttpContext.Current);
                            }
                        }
                    }
                    else
                    {
                        string titulo = "ESTADO DE CUENTA";
                        string mensaje = "El usuario <b> " + id + " </b> es administrativo.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                    }
                }
                catch (Exception ex)
                {
                    string titulo = "ESTADO DE CUENTA";
                    string mensaje = ex.Message.Replace("'", "\"");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                }
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static ICredentials ICredenciales()
        {
            // Usuario
            string usuario = ConfigurationManager.AppSettings["ReportViewerUsuario"];

            if (string.IsNullOrEmpty(usuario))
                throw new Exception(
                    "Falta el usuario del archivo web.config");
            // Clave
            string clave = ConfigurationManager.AppSettings["ReportViewerClave"];

            if (string.IsNullOrEmpty(clave))
                throw new Exception(
                    "Falta la contraseña del archivo web.config");
            // Dominio
            string dominio = ConfigurationManager.AppSettings["ReportViewerDominio"];

            if (string.IsNullOrEmpty(dominio))
                throw new Exception(
                    "Falta el dominio del archivo web.config");

            return new NetworkCredential(usuario, clave, dominio);

        }

    }
}