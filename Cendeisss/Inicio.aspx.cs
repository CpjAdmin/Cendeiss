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
    public partial class Inicio : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["ID"] == null)
            {
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        //Navegador Web
                        Session["navegador"] = VerificarNavegador();
                    }
                    catch (Exception ex)
                    {
                        string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                        string mensaje = ex.Message;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                    }
                }
            }
        }

        //****************************************************************** Actualizar Clave CENDEISSS
        [WebMethod]
        public static bool ActualizarClave(String clave, String sistema, String id)
        {
            bool resultado = false;

            try
            {
                if (sistema == "CENDEISSS")
                {
                    Encriptar encriptar = new Encriptar();

                    Asociado usuario = new Asociado
                    {
                        id = id,
                        clave = encriptar.Crypto(clave)
                    };

                    resultado = AsociadoLN.getInstancia().MantenimientoUsuario(usuario, "U");
                }

                if (resultado)
                {
                    //*** Auditoría
                    int cod_proceso = 3;
                    string descripcion = "Cambio de contraseña - " + sistema;

                    Proceso proceso = new Proceso
                    {
                        id_usuario = id,
                        cod_proceso = cod_proceso,
                        descripcion = descripcion
                    };

                    ProcesoLN.getInstancia().AuditarProceso(proceso, System.Web.HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;

                if (HttpContext.Current.Session["ID"] != null)
                {
                    throw new Exception("Ocurrio un error ( " + ex.Message + " )", ex);
                }
                else
                {
                    throw new Exception("Su sesion ha expirado, sera redireccionado a la página principal");
                }
            }

            return resultado;
        }

        public string VerificarNavegador()
        {
            System.Web.HttpBrowserCapabilities browser = Request.Browser;

            string navegador = browser.Browser + " " + browser.Version;

            return navegador;
        }
       
    } // Partial Class END 
} // namespace END

