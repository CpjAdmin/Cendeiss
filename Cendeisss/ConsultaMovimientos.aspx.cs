using CP_Entidades;
using CP_LogicaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cendeisss
{
    public partial class ConsultaMovimientos : System.Web.UI.Page
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
                        String RolUsuario = Session["Rol"].ToString();

                        if (RolUsuario == "USUARIO")
                        {
                            optConsultaMovimientosUsuario.Visible = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                        string mensaje = "Contecte al administrador del sistema...! - " + ex.Message.Replace("'", "\""); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                    }

                }
            }

        }

        [WebMethod]
        public static List<Movimientos> CargarMovimientos(string usuario, string f_ini, string f_fin, string id)
        {
            List<Movimientos> listaMovimientos = new List<Movimientos>();

            string f_inicio = f_ini;
            string f_final = f_fin;
            int cod_proceso = 1;

            try
            {
                listaMovimientos = MovimientosLN.getInstancia().CargarMovimientos(usuario, f_ini, f_fin, cod_proceso);

                //*** Auditoría
                string IdConsulta = (usuario == "") ? "TODOS" : usuario; 
                int cod_proceso_aut = 5;
                string descripcion = "Movimientos del " + DateTime.Parse(f_ini).ToString("dd-MM-yyyy") + " al " + DateTime.Parse(f_fin).ToString("dd-MM-yyyy") +" - Asociado: "+ IdConsulta;

                Proceso proceso = new Proceso
                {
                    id_usuario = id,
                    cod_proceso = cod_proceso_aut,
                    descripcion = descripcion
                };

                ProcesoLN.getInstancia().AuditarProceso(proceso, System.Web.HttpContext.Current);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;

                if (HttpContext.Current.Session["ID"] != null)
                {
                    throw new Exception("Se ha producido un error al cargar los saldos ( " + ex.Message + " )", ex);
                }
                else
                {
                    throw new Exception("Su sesion ha expirado, cerrando la sesión.");
                }
            }

            return listaMovimientos;

        }

        //protected void ExportToExcel_Click(object sender, EventArgs e)
        //{
        //    Response.ContentType = "application/force-download";
        //    Response.AddHeader("content-disposition", "attachment; filename=Print.xls");
        //    Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
        //    Response.Write("<head>");
        //    Response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-     8\">");
        //    Response.Write("<!--[if gte mso 9]><xml>");
        //    Response.Write("<x:ExcelWorkbook>");
        //    Response.Write("<x:ExcelWorksheets>");
        //    Response.Write("<x:ExcelWorksheet>");
        //    Response.Write("<x:Name>Report Data</x:Name>");
        //    Response.Write("<x:WorksheetOptions>");
        //    Response.Write("<x:Print>");
        //    Response.Write("<x:ValidPrinterInfo/>");
        //    Response.Write("</x:Print>");
        //    Response.Write("</x:WorksheetOptions>");
        //    Response.Write("</x:ExcelWorksheet>");
        //    Response.Write("</x:ExcelWorksheets>");
        //    Response.Write("</x:ExcelWorkbook>");
        //    Response.Write("</xml>");
        //    Response.Write("<![endif]--> ");
        //    StringWriter tw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(tw);
        //    tbl_movimientos.RenderControl(hw);
        //    Response.Write(tw.ToString());
        //    Response.Write("</head>");
        //    Response.Flush();
        //}

    }
}