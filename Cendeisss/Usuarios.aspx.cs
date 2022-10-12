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
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String RolUsuario = Session["Rol"].ToString();

            if (Session["ID"] == null || RolUsuario != "ADMINISTRADOR")
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
                            btnRegistrar.Visible = false;
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

        [WebMethod]
        public static List<Asociado> CargarRegistros()
        {
            List<Asociado> lista = null;

            try
            {
                lista = AsociadoLN.getInstancia().GetUsuarios();

                //*** Actualización Masiva de Contraseñas ( Temporal )
                //for (int i = 0; i < lista.Count; i++)
                //{
                //    AsociadoLN.getInstancia().MantenimientoUsuario(lista[i], "U");
                //}
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;

                if (HttpContext.Current.Session["ID"] != null)
                {
                    throw new Exception("Se ha producido un error al cargar los usuarios ( " + ex.Message + " )", ex);
                }
                else
                {
                    throw new Exception("Su sesion ha expirado, sera redireccionado a la página principal");
                }
            }

            return lista;
        }

        [WebMethod]
        public static bool ProcesarUsuario(String p_usuario, String p_nombre, String p_cod_rol,String p_email, String p_clave, String p_i_estado, String p_proceso, String p_id)
        {
            bool resultado;

            try
            {
                Encriptar encriptar = new Encriptar();

                Asociado asociado = new Asociado()
                {
                    id = p_usuario,
                    nombre = p_nombre,
                    email = p_email,
                    clave = p_clave.Trim() == "" ? "" : encriptar.Crypto(p_clave),
                    rol = p_cod_rol,
                    estado = p_i_estado
                };

                //Actualizar Usuario  
                resultado = AsociadoLN.getInstancia().MantenimientoUsuario(asociado, p_proceso);

                if (resultado)
                {
                    //*** Auditoría
                    string Id = p_id;
                    int cod_proceso;
                    string descripcion;


                    if (p_proceso == "U") //Actualización
                    {
                        cod_proceso = 8;
                        descripcion = "Actualización de Usuario: " + p_usuario;
                    }
                    else  //R = Registro
                    {
                        cod_proceso = 9;
                        descripcion = "Registro de Usuario: " + p_usuario;
                    }

                    Proceso proceso = new Proceso
                    {
                        id_usuario = Id,
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
                    throw new Exception("Se ha producido un error al procesar el usuario ( " + ex.Message + " )", ex);
                }
                else
                {
                    throw new Exception("Su sesion ha expirado, sera redireccionado a la página principal");
                }
            }
            return resultado;
        }


    }
}