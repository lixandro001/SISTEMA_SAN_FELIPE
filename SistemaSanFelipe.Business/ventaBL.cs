using SistemaSanFelipe.Data;
using SistemaSanFelipe.Entity.Request;
using SistemaSanFelipe.Entity.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Business
{
    public  class ventaBL: IDisposable
    {
        private Venta objVenta;

        public ventaBL(string ConStr)
        {
            objVenta = new Venta(ConStr);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public NumeroVentaResponse GetNumeroGuiaVenta()
        {
            return objVenta.GetNumeroGuiaVenta();        
        }

        public List<ClientesVentasResponse> GetClientesVentas()
        {
            return objVenta.GetClientesVentas();
        }

        public ClientesVentasResponse GetClienteId(int idCliente)
        {
            return objVenta.GetClienteId(idCliente);
        }
         
        public int GuardarGuiaVenta(GuiaVentaPolloRequest datos)
        {
            return objVenta.GuardarGuiaVenta(datos);
        }

        public List<ListadoPollosResponse> ObtenerListadoPollos(DateTime StartDate, DateTime EndDate)
        {
            return objVenta.ObtenerListadoPollos(StartDate, EndDate);
        }

        public VentasReporteLista GetVentasReporteLista(DateTime fechaIniDate, DateTime fechaFinDate)
        {
            return objVenta.GetVentasReporteLista(fechaIniDate, fechaFinDate);
        }

    }
}
