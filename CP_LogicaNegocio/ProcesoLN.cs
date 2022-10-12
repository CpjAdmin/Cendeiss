using CP_Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CP_LogicaNegocio
{
    public class ProcesoLN
    {

        #region "Logica de Negocio Proceso"
        private static ProcesoLN ObjProceso = null;
        private ProcesoLN() { }
        public static ProcesoLN getInstancia()
        {
            if (ObjProceso == null)
            {
                ObjProceso = new ProcesoLN();
            }
            return ObjProceso;
        }
        #endregion

        //Auditoria
        public void AuditarProceso(Proceso proceso, HttpContext contexto)
        {
            try
            {
                string terminal_id;

                //Parametros Auditoría
                string navegador = (contexto.Session["navegador"].ToString());
                string pagina = Path.GetFileName(contexto.Request.Url.AbsolutePath);
                int cod_proceso = proceso.cod_proceso;
                string descripcion = proceso.descripcion;
                string id_usuario = proceso.id_usuario;

                //Resolver Error:  System.Net.Sockets.SocketException: No such host is known
                try
                {
                    string[] computer_name = Dns.GetHostEntry(contexto.Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
                    terminal_id = computer_name[0].ToString().ToUpper();
                }
                catch (Exception)
                {
                    terminal_id = GetIPAddress(contexto);

                    if (terminal_id == "")
                    {
                        terminal_id = "Desconocido";
                    }
                }

                //Auditoría
                AsociadoLN.getInstancia().AuditoriaSistema(cod_proceso, id_usuario, navegador, pagina, descripcion, terminal_id);

            }
            catch (Exception)
            {
                throw;
            }

        }

        protected string GetIPAddress(HttpContext context)
        {
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

    }
}
