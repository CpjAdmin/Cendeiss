using CP_Entidades;
using CP_LogicaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cendeisss
{
    public partial class ConsultaSaldos : System.Web.UI.Page
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
                            optConsultaSaldosUsuario.Visible = false;
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
        public static List<Saldos> CargarSaldos(string usuario, string mes, string ano, string id)
        {
            List<Saldos> listaSaldos = new List<Saldos>();

            int nMes = Int32.Parse(mes);
            int nAno = Int32.Parse(ano);
            int cod_proceso = 1;

            try
            {
                listaSaldos = SaldosLN.getInstancia().CargarSaldos(usuario, nMes, nAno, cod_proceso);

                //*** Auditoría
                string IdConsulta = (usuario == "") ? "TODOS" : usuario;
                int cod_proceso_aut = 4;
                string descripcion = "Saldos del periodo ( Mes: " + nMes + ", Año: "+ nAno + " ) - Asociado: " + IdConsulta;

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

            return listaSaldos;
        }

    }
}