using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity.Response
{
   public class VentasReporteLista
    {
        public VentasReporteLista()
        {
            ListaVentaGuiaReporte = new List<VentasReporteListaResponse>();
            ListaTotalVenta = new List<TotalVenta>();
        }
        public List<VentasReporteListaResponse> ListaVentaGuiaReporte { get; set; }
        public List<TotalVenta> ListaTotalVenta { get; set; }
 
    }
    public class VentasReporteListaResponse
    {
        public string nombreCliente { get; set; }
        public string nombreUbigeo { get; set; }
        public string JABAS { get; set; }
        public string AVES { get; set; }
        public decimal pesoBRUTO { get; set; }
        public decimal TARA { get; set; }
        public decimal PESONETOG { get; set; }
        public decimal UNITARIO { get; set; }
        public decimal IMPORTE { get; set; }
        public decimal nro_cargo { get; set; }
        public decimal nro_Abono { get; set; }
        public string porCobrar { get; set; }
        public DateTime fechaSalidaVenta { get; set; }
    }
    public class TotalVenta
    {
        public string nombreUbigeo { get; set; }
        public decimal totaljabas { get; set; }
        public decimal totalaves { get; set; }
        public decimal totalpesobruto { get; set; }
        public decimal totaltara { get; set; }
        public decimal totalpesoneto { get; set; }
        public decimal totalunitario { get; set; }
        public decimal totalimporte { get; set; }
        public decimal totalnrocargo { get; set; }
        public decimal totalnroabono { get; set; }
        public decimal totalporcobrar { get; set; }
        public DateTime fechaSalidaVenta { get; set; }
    }
}
