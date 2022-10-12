using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CP_Entidades;
using CP_LogicaNegocio;
using System.Web.Services;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace Cendeisss
{
    public partial class Login : System.Web.UI.Page
    {
        private static Random random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario.Focus();

            //Navegador
            Session["navegador"] = VerificarNavegador();

            if (!string.IsNullOrEmpty((string)Session["ID"]))
            {
                Session.Clear();
            }
        }

        public string VerificarNavegador()
        {
            System.Web.HttpBrowserCapabilities browser = Request.Browser;

            string navegador = browser.Browser + " " + browser.Version;

            return navegador;
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    Encriptar encriptar = new Encriptar();
                    Asociado ObjUsuario = AsociadoLN.getInstancia().AccesoSistema(usuario.Value, encriptar.Crypto(password.Value));

                    if (ObjUsuario != null)
                    {
                        string Id = ObjUsuario.id;
                        string Nombre = ObjUsuario.nombre;
                        string Rol = ObjUsuario.rol;
                        string UltimoIngreso = ObjUsuario.f_ult_ingreso;
                        string UltimaEdicion = ObjUsuario.f_ult_edicion;

                        Session["ID"] = Id.Trim();
                        Session["Nombre"] = Nombre;
                        Session["Rol"] = Rol;
                        Session["UltimoIngreso"] = UltimoIngreso;
                        Session["UltimaEdicion"] = UltimaEdicion;


                        //*** Auditoría
                        int cod_proceso = 1;
                        string descripcion = "Inicio de sesión";

                        Proceso proceso = new Proceso
                        {
                            id_usuario = Id,
                            cod_proceso = cod_proceso,
                            descripcion = descripcion
                        };

                        try
                        {
                            ProcesoLN.getInstancia().AuditarProceso(proceso, System.Web.HttpContext.Current);
                        }
                        catch (Exception)
                        {
                        }

                        // Redirect
                        string primerInicio = Session["UltimaEdicion"].ToString();

                        if (string.IsNullOrEmpty(primerInicio))
                        {
                            Response.Redirect("~/MantenimientoPerfil.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("~/Inicio.aspx", false);
                        }
                    }
                    else
                    {
                        string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                        string mensaje = "El usuario o la contraseña son incorrectos...!";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                    }
                }
            }
            catch (Exception)
            {
                string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                string mensaje = "El servicio no está disponible, contacte al administrador del sistema.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
            }
        }

        //Agregado por Carlos Fonseca SOL271275 24/05/2019
        //Se agrega un metodo para generar un clave random y el metodo de enviar correo.
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [WebMethod]
        public static Resultado EnviarClaveEmail(String id)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje="Su contraseña fue enviada a su correo electrónico."};
            string clave = RandomString(10);
            bool respuesta = false;
            try
            {
                var EmailValidador = new EmailAddressAttribute();
                Asociado ObjUsuario = AsociadoLN.getInstancia().GetUsuario(id);

                if (ObjUsuario == null)
                {
                    resultado.Error = -1;
                    resultado.Mensaje = "El usuario no existe.";
                    return resultado;
                }

                if ((ObjUsuario.email == null) || (ObjUsuario.email.Trim() == "") || !EmailValidador.IsValid(ObjUsuario.email.Trim()))
                {
                    resultado.Error = -1;
                    resultado.Mensaje = "El usuario no tiene un correo valido asignado.";
                    return resultado;
                }

                Encriptar encriptar = new Encriptar();

                Asociado usuario = new Asociado
                {
                    id = id,
                    clave = encriptar.Crypto(clave)
                };

                respuesta = AsociadoLN.getInstancia().MantenimientoUsuario(usuario, "U");

                if (!respuesta)
                {
                    resultado.Error = -1;
                    resultado.Mensaje = "Ocurrió un error inesperado al generar la contraseña. Consulte con el administrador.";
                    return resultado;
                }

                respuesta = AsociadoLN.getInstancia().EnviarPassword(ObjUsuario.id + " - " +   ObjUsuario.nombre, clave, ObjUsuario.email);

                if (!respuesta)
                {
                    resultado.Error = -1;
                    resultado.Mensaje = "Ocurrió un error inesperado al enviar el correo. Vuelva a intentarlo o consulte con el administrador.";
                    return resultado;
                }
                else
                {
                    try
                    {
                        //*** Auditoría
                        int cod_proceso_aut = 1;
                        string descripcion = "Recuperar contraseña, usuario: " + id;

                        Proceso proceso = new Proceso
                        {
                            id_usuario = id,
                            cod_proceso = cod_proceso_aut,
                            descripcion = descripcion
                        };

                        ProcesoLN.getInstancia().AuditarProceso(proceso, System.Web.HttpContext.Current);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                resultado.Error = -1;
                resultado.Mensaje= "Ocurrió un error inesperado. Consulte con el administrador.";
            }

            return resultado;
        }
        //FIN Agregado por Carlos Fonseca SOL271275 24/05/2019


    }
}