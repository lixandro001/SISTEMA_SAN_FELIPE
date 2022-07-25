using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity.Response
{
   public class ListadoPollosResponse
    {
        public string  idventa { get; set; } 
        public int  idcliente { get; set; }
        public int idusuario { get; set; }
        public string codigoventa { get; set; }
        public Guid guidcodeventa { get; set; }
        public DateTime fechaVenta { get; set; }
        public int iddetalleVenta { get; set; }
        public int idproducto { get; set; }
        public string NroGuiaSalida { get; set; }
        public int NroGuiaSalidaNumero { get; set; }
        public int NroGuiaSalidaNumero2 { get; set; }
        public DateTime fechaSalidaVenta { get; set; }

        public string CfechaSalidaVenta
        {
            get
            {
                if (fechaSalidaVenta != null)
                {
                    return string.Format("{0:dd/MM/yyyy}", fechaSalidaVenta);
                }
                return string.Empty;
            }
        }

        public string Mnd { get; set; }
        public decimal T_Cam { get; set; }
        public string RUCCobranza { get; set; }
        public string RUCCobranzaNombre { get; set; }
        public string Motivo { get; set; }
        public int ID { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public string AVES { get; set; }
        public string JABAS { get; set; }
        public decimal pesoBRUTO { get; set; }
        public decimal TARA { get; set; }
        public decimal PESONETOG { get; set; }
        public decimal UNITARIO { get; set; }
        public decimal IMPORTE { get; set; }
        public string Status { get; set; }
        public string PesoPromedio { get; set; }
        public string Exonerado { get; set; }
        public string IGV { get; set; }
        public string Cobrado { get; set; }
        public string Cancelacion { get; set; }
        public string PromedioDato { get; set; }
        public string Afecto { get; set; }
        public string Total { get; set; }
        public string porCobrar { get; set; }

        public string nombreCliente { get; set; }
        public string nombreUbigeo { get; set; }

    }
}
