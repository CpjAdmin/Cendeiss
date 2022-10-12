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
    public partial class MantenimientoPerfil : System.Web.UI.Page
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
                        CargarInformacionCliente(Session["Id"].ToString());
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

        //********** DropDownList Provincias
        private void CargarProvincias()
        {
            ddlProvincia.DataSource = ProvinciaLN.getInstancia().GetProvincias();
            ddlProvincia.DataTextField = "nombre";
            ddlProvincia.DataValueField = "cod_provincia";
            ddlProvincia.DataBind();
        }

        private void CargarCantonesBase(string cod_provincia)
        {
            ddlCanton.DataSource = CantonLN.getInstancia().GetCantones(cod_provincia);
            ddlCanton.DataTextField = "nombre";
            ddlCanton.DataValueField = "cod_canton";
            ddlCanton.DataBind();
        }

        private void CargarDistritosBase(string cod_provincia,string cod_canton)
        {
            ddlDistrito.DataSource = DistritoLN.getInstancia().GetDistritos(cod_provincia, cod_canton);
            ddlDistrito.DataTextField = "nombre";
            ddlDistrito.DataValueField = "cod_distrito";
            ddlDistrito.DataBind();
        }

        private void CargarInformacionCliente(string id)
        {
            try
            {
                //Cargar Usuario
                Asociado ObjUsuario = AsociadoLN.getInstancia().GetUsuario(id);

                if (ObjUsuario != null)
                {
                    txtId.Value = ObjUsuario.id;
                    txtPerfilRol.Value = ObjUsuario.rol;
                    txtNombre.Value = ObjUsuario.nombre;
                    fechaNacimiento.Value = ObjUsuario.f_nacimiento;
                    txtTelHabitacion.Value = ObjUsuario.tel_habitacion;
                    txtTelCelular.Value = ObjUsuario.tel_celular;
                    txtTelTrabajo.Value = ObjUsuario.tel_trabajo;
                    txtCorreoPrin.Value = ObjUsuario.email;
                    txtCorreoSec.Value = ObjUsuario.email2;
                    txtOtrasS.Value = ObjUsuario.direccion;

                    //Provincia
                    int geoSize = ObjUsuario.ubicacion_geografica.Length;
                    CargarProvincias();

                    if (geoSize >= 7) // A053+
                    {
                        string cod_provincia = ObjUsuario.ubicacion_geografica.Substring(4, 3);
                        ddlProvincia.SelectedValue = cod_provincia;
                        CargarCantonesBase(cod_provincia);

                        if (geoSize >= 10)
                        {
                            string cod_canton = ObjUsuario.ubicacion_geografica.Substring(7, 3);
                            ddlCanton.SelectedValue = cod_canton;
                            CargarDistritosBase(cod_provincia, cod_canton);

                            if (geoSize == 13)
                            {
                                string cod_distrito = ObjUsuario.ubicacion_geografica.Substring(10, 3);
                                ddlDistrito.SelectedValue = cod_distrito;
                            }
                        }
                        else
                        {
                            ddlCanton.SelectedValue = "0";
                            ddlDistrito.SelectedValue = "0";
                        }
                    }
                    else
                    {
                        ddlProvincia.SelectedValue = "0";
                        ddlCanton.SelectedValue = "0";
                        ddlDistrito.SelectedValue = "0";
                    }

                }
            }
            catch (Exception ex)
            {
                string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                string mensaje = "Contecte al administrador del sistema! - " + ex.Message.Replace("'", "\""); ;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
            }
        }
        // WebMethod - Cargar Cantones
        [WebMethod]
        public static List<Canton> CargarCantones(string provinciaId)
        {
            List<Canton> listaCantones = new List<Canton>();

            listaCantones = CantonLN.getInstancia().GetCantones(provinciaId);

            return listaCantones;
        }

        // WebMethod - Cargar Distritos
        [WebMethod]
        public static List<Distrito> CargarDistritos(string provinciaId, string cantonId)
        {
            List<Distrito> listaDistritoes = new List<Distrito>();

            listaDistritoes = DistritoLN.getInstancia().GetDistritos(provinciaId, cantonId);

            return listaDistritoes;
        }

        // Procesar Perfil de Usuario
        [WebMethod]
        public static bool ProcesarUsuario(string p_id, string p_nombre, string p_email, string p_email2,string p_tel_habitacion
            ,string p_tel_celular, string p_tel_trabajo, string p_direccion, string p_ubicacion_geografica, string p_f_nacimiento)
        {
            bool resultado;
            string p_proceso = "W";

            try
            {
                Asociado asociado = new Asociado()
                {
                    id = p_id,
                    nombre = p_nombre,
                    email = p_email,
                    email2 = p_email2,
                    tel_habitacion = p_tel_habitacion,
                    tel_celular = p_tel_celular,
                    tel_trabajo = p_tel_trabajo,
                    direccion = p_direccion,
                    ubicacion_geografica = p_ubicacion_geografica,
                    f_nacimiento = p_f_nacimiento
                };

                //Actualizar Usuario  
                resultado = AsociadoLN.getInstancia().MantenimientoUsuario(asociado, p_proceso);

                if (resultado)
                {
                    //Actualizar Variable Sesión 
                    HttpContext.Current.Session["UltimaEdicion"] = DateTime.Now.ToString("yyyy/MM/dd");
                    
                    //*** Auditoría
                    string Id = p_id;
                    int cod_proceso = 10;
                    string descripcion = "Actualización de Perfil: " + Id; ;

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
