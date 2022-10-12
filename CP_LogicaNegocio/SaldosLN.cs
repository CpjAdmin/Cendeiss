using CP_Entidades;
using CP_AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_LogicaNegocio
{
    public class SaldosLN
    {

        #region "Logica de Negocio Saldo"
        private static SaldosLN ObjSaldo = null;
        private SaldosLN() { }
        public static SaldosLN getInstancia()
        {
            if (ObjSaldo == null)
            {
                ObjSaldo = new SaldosLN();
            }
            return ObjSaldo;
        }
        #endregion

        public List<Saldos> CargarSaldos(string usuario, int mes, int ano, int cod_proceso)
        {
            try
            {
                return SaldosDAO.getInstancia().CargarSaldos(usuario, mes, ano, cod_proceso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
