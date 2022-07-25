using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity.Request
{
   public class GuiaVentaPolloRequest
    {
        public string txtNroGuiaSalida { get; set; }
        public int idproducto { get; set; }
        public string txtNroGuiaSalidaNumero { get; set; }
        public string txtNroGuiaSalidaNumero2 { get; set; }
        public DateTime txtFecha { get; set; }
        public string txtMnd { get; set; }
        public decimal txtTCam { get; set; }
        public int txtRUCCobranza { get; set; }
        public string txtRUCCobranzaNombre { get; set; }
        public int cboMotivo { get; set; }
        public int txtID { get; set; }
        public string txtCODIGO { get; set; }
        public string txtDESCRIPCION { get; set; }
        public string txtAVES { get; set; }
        public string txtJABAS { get; set; }
        public decimal txtpesoBRUTO { get; set; }
        public decimal txtTARA { get; set; }
        public decimal txtPESONETOG { get; set; }
        public decimal txtUNITARIO { get; set; }
        public decimal txtIMPORTE { get; set; }
        public string txtStatus { get; set; }
        public string txtpesoPromedioDato { get; set; }
        public string txtExonerado { get; set; }
        public string txtIGV { get; set; }
        public string txtCobrado { get; set; }
        public string txtCancelacion { get; set; }
        public string txtAfecto { get; set; }
        public string txtTotal { get; set; }
        public string txtporCobrar { get; set; }
        public string txtSN { get; set; }
        public string txtSN1 { get; set; }
        public string txtSN2 { get; set; }
        public int UsuarioId { get; set; }
    }
}
