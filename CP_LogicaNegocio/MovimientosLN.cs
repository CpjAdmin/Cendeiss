using CP_AccesoDatos;
using CP_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_LogicaNegocio
{
    public class MovimientosLN
    {
        #region "Logica de Negocio Saldo"
        private static MovimientosLN ObjSaldo = null;
        private MovimientosLN() { }
        public static MovimientosLN getInstancia()
        {
            if (ObjSaldo == null)
            {
                ObjSaldo = new MovimientosLN();
            }
            return ObjSaldo;
        }
        #endregion

        public List<Movimientos> CargarMovimientos(string usuario, string f_ini, string f_fin, int cod_proceso)
        {
            try
            {
                return MovimientosDAO.getInstancia().CargarMovimientos(usuario, f_ini, f_fin, cod_proceso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
