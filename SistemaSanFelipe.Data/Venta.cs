using SistemaSanFelipe.Entity.Request;
using SistemaSanFelipe.Entity.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SistemaSanFelipe.Entity.Response.VentasReporteLista;

namespace SistemaSanFelipe.Data
{
    public  class Venta : IDisposable
    {
        private string ConStr;
        public Venta(string ConStr)
        {
            this.ConStr = ConStr;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public NumeroVentaResponse GetNumeroGuiaVenta()
        {
            NumeroVentaResponse Entity = null;

            using (var Ado = new SQLServer(ConStr))
            {
                //var Parameters = new SqlParameter[]
                //{
                //    new SqlParameter{ParameterName="@NroGuiaSalidaNumero2",SqlDbType=SqlDbType.Int,SqlValue=Venta.NroGuiaSalidaNumero2}
                   
                //};
                var Dr = Ado.ExecDataReader("usp_Existe_Venta_Pollo", null);

                if (!Dr.HasRows) return Entity;
                while (Dr.Read())
                {
                    Entity = new NumeroVentaResponse();
                    Entity.NroGuia = Convert.ToInt32(Dr["NroGuia"]);
                    
                    break;
                }
                Dr.Close();
            }
            return Entity;
        }

        public ClientesVentasResponse GetClienteId(int idCliente)
        {
            ClientesVentasResponse Entity = null;

            using (var Ado = new SQLServer(ConStr))
            {
                var Parameters = new SqlParameter[]
                {
                    new SqlParameter{ParameterName="@idcliente",SqlDbType=SqlDbType.Int,SqlValue=idCliente}

                };
                var Dr = Ado.ExecDataReader("usp_obtener_cliente_id @idcliente", Parameters);

                if (!Dr.HasRows) return Entity;
                while (Dr.Read())
                {
                    Entity = new ClientesVentasResponse();

                    Entity.idcliente = Convert.ToInt32(Dr["idcliente"]);
                    Entity.idzonaUbigeo = Convert.ToInt32(Dr["idzonaUbigeo"]);
                    Entity.nombreCliente = Convert.ToString(Dr["nombreCliente"]);

                    break;
                }
                Dr.Close();
            }
            return Entity;
        }
        
        public List<ClientesVentasResponse> GetClientesVentas()
        {
            using (var Ado = new SQLServer(ConStr))
            {
                try
                {
                    List<ClientesVentasResponse> List = new List<ClientesVentasResponse>();
                    var Dr = Ado.ExecDataReaderProc("usp_obtener_clientes", null);
                    {
                        if (!Dr.HasRows) { return List; }
                        while (Dr.Read())
                        {
                            var Entity = new ClientesVentasResponse();
                            if (Dr["idcliente"] != DBNull.Value) { Entity.idcliente = (int)Dr["idcliente"]; }
                            if (Dr["idzonaUbigeo"] != DBNull.Value) { Entity.idzonaUbigeo = (int)Dr["idzonaUbigeo"]; }
                            if (Dr["nombreCliente"] != DBNull.Value) { Entity.nombreCliente = (string)Dr["nombreCliente"]; }
                            if (Dr["fechaCreacion"] != DBNull.Value) { Entity.fechaCreacion = (DateTime)Dr["fechaCreacion"]; }
                            if (Dr["estado"] != DBNull.Value) { Entity.estado = (bool)Dr["estado"]; }
                         
                            List.Add(Entity);
                        }
                        return List;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public VentasReporteLista GetVentasReporteLista()
        {
          
            using (var Ado = new SQLServer(ConStr))
            {
                try
                {
                    VentasReporteLista entidad = new VentasReporteLista();
                    List<VentasReporteListaResponse> List = new List<VentasReporteListaResponse>();
                    var Dr = Ado.ExecDataReaderProc("usp_listado_Reporte_venta_pollos", null);
                    {
                        if (!Dr.HasRows) { return entidad; }
                        while (Dr.Read())
                        {
                            var Entity = new VentasReporteListaResponse();

                            if (Dr["nombreCliente"] != DBNull.Value) { Entity.nombreCliente = (string)Dr["nombreCliente"]; }
                            if (Dr["nombreUbigeo"] != DBNull.Value) { Entity.nombreUbigeo = (string)Dr["nombreUbigeo"]; }
                            if (Dr["JABAS"] != DBNull.Value) { Entity.JABAS = (string)Dr["JABAS"]; }
                            if (Dr["AVES"] != DBNull.Value) { Entity.AVES = (string)Dr["AVES"]; }
                            if (Dr["pesoBRUTO"] != DBNull.Value) { Entity.pesoBRUTO = (decimal)Dr["pesoBRUTO"]; }
                            if (Dr["TARA"] != DBNull.Value) { Entity.TARA = (decimal)Dr["TARA"]; }
                            if (Dr["PESONETOG"] != DBNull.Value) { Entity.PESONETOG = (decimal)Dr["PESONETOG"]; }
                            if (Dr["UNITARIO"] != DBNull.Value) { Entity.UNITARIO = (decimal)Dr["UNITARIO"]; }
                            if (Dr["IMPORTE"] != DBNull.Value) { Entity.IMPORTE = (decimal)Dr["IMPORTE"]; }
                            if (Dr["nro_cargo"] != DBNull.Value) { Entity.nro_cargo = (decimal)Dr["nro_cargo"]; }
                            if (Dr["nro_Abono"] != DBNull.Value) { Entity.nro_Abono = (decimal)Dr["nro_Abono"]; }
                            if (Dr["porCobrar"] != DBNull.Value) { Entity.porCobrar = (string)Dr["porCobrar"]; }
                            if (Dr["fechaSalidaVenta"] != DBNull.Value) { Entity.fechaSalidaVenta = (DateTime)Dr["fechaSalidaVenta"]; }
                             
                            List.Add(Entity);
                        }
                        Dr.Close();

                        var DrMain = Ado.ExecDataReaderProc("usp_listado_Reporte_venta_total_pollos", null);

                        List<TotalVenta> listTotalVenta = new List<TotalVenta>();

                        while (DrMain.Read())
                        {
                              TotalVenta TotalVenta = new TotalVenta();

                            if (DrMain["nombreUbigeo"] != DBNull.Value)
                                TotalVenta.nombreUbigeo = (string)DrMain["nombreUbigeo"];
                            if (DrMain["totaljabas"] != DBNull.Value)
                                TotalVenta.totaljabas = (decimal)DrMain["totaljabas"];
                            if (DrMain["totalaves"] != DBNull.Value)
                                TotalVenta.totalaves = (decimal)DrMain["totalaves"];
                            if (DrMain["totalpesobruto"] != DBNull.Value)
                                TotalVenta.totalpesobruto = (decimal)DrMain["totalpesobruto"];
                            if (DrMain["totaltara"] != DBNull.Value)
                                TotalVenta.totaltara = (decimal)DrMain["totaltara"];
                            if (DrMain["totalpesoneto"] != DBNull.Value)
                                TotalVenta.totalpesoneto = (decimal)DrMain["totalpesoneto"];
                            if (DrMain["totalunitario"] != DBNull.Value)
                                TotalVenta.totalunitario = (decimal)DrMain["totalunitario"];
                            if (DrMain["totalimporte"] != DBNull.Value)
                                TotalVenta.totalimporte = (decimal)DrMain["totalimporte"];
                            if (DrMain["totalnrocargo"] != DBNull.Value)
                                TotalVenta.totalnrocargo = (decimal)DrMain["totalnrocargo"];
                            if (DrMain["totalnroabono"] != DBNull.Value)
                                TotalVenta.totalnroabono = (decimal)DrMain["totalnroabono"];
                            if (DrMain["totalporcobrar"] != DBNull.Value)
                                TotalVenta.totalporcobrar = (decimal)DrMain["totalporcobrar"];
                            if (DrMain["fechaSalidaVenta"] != DBNull.Value)
                                TotalVenta.fechaSalidaVenta = (DateTime)DrMain["fechaSalidaVenta"];

                            listTotalVenta.Add(TotalVenta);
                        }

                        DrMain.Close();

                        entidad.ListaVentaGuiaReporte = List;
                        entidad.ListaTotalVenta = listTotalVenta;

                        return entidad;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }

        public List<ListadoPollosResponse> ObtenerListadoPollos()
        {
            using (var Ado = new SQLServer(ConStr))
            {
                try
                {
                    List<ListadoPollosResponse> List = new List<ListadoPollosResponse>();
                    var Dr = Ado.ExecDataReaderProc("usp_Obtener_Listado_Venta_Pollos", null);
                    {
                        if (!Dr.HasRows) { return List; }
                        while (Dr.Read())
                        {
                            var Entity = new ListadoPollosResponse();
                            if (Dr["idventa"] != DBNull.Value) { Entity.idventa = (string)Dr["idventa"]; }
                            if (Dr["idcliente"] != DBNull.Value) { Entity.idcliente = (int)Dr["idcliente"]; }
                            if (Dr["idusuario"] != DBNull.Value) { Entity.idusuario = (int)Dr["idusuario"]; }
                            if (Dr["codigoventa"] != DBNull.Value) { Entity.codigoventa = (string)Dr["codigoventa"]; }
                            if (Dr["guidcodeventa"] != DBNull.Value) { Entity.guidcodeventa = (Guid)Dr["guidcodeventa"]; }
                            if (Dr["fechaVenta"] != DBNull.Value) { Entity.fechaVenta = (DateTime)Dr["fechaVenta"]; }
                            if (Dr["iddetalleVenta"] != DBNull.Value) { Entity.iddetalleVenta = (int)Dr["iddetalleVenta"]; }
                            if (Dr["idproducto"] != DBNull.Value) { Entity.idproducto = (int)Dr["idproducto"]; }
                            if (Dr["NroGuiaSalida"] != DBNull.Value) { Entity.NroGuiaSalida = (string)Dr["NroGuiaSalida"]; }
                            if (Dr["NroGuiaSalidaNumero"] != DBNull.Value) { Entity.NroGuiaSalidaNumero = (int)Dr["NroGuiaSalidaNumero"]; }
                            if (Dr["NroGuiaSalidaNumero2"] != DBNull.Value) { Entity.NroGuiaSalidaNumero2 = (int)Dr["NroGuiaSalidaNumero2"]; }
                            if (Dr["fechaSalidaVenta"] != DBNull.Value) { Entity.fechaSalidaVenta = (DateTime)Dr["fechaSalidaVenta"]; }
                            if (Dr["Mnd"] != DBNull.Value) { Entity.Mnd = (string)Dr["Mnd"]; }
                            if (Dr["T_Cam"] != DBNull.Value) { Entity.T_Cam = (decimal)Dr["T_Cam"]; }
                            if (Dr["RUCCobranza"] != DBNull.Value) { Entity.RUCCobranza = (string)Dr["RUCCobranza"]; }
                            if (Dr["RUCCobranzaNombre"] != DBNull.Value) { Entity.RUCCobranzaNombre = (string)Dr["RUCCobranzaNombre"]; }
                            if (Dr["Motivo"] != DBNull.Value) { Entity.Motivo = (string)Dr["Motivo"]; }
                            if (Dr["ID"] != DBNull.Value) { Entity.ID = (int)Dr["ID"]; }
                            if (Dr["CODIGO"] != DBNull.Value) { Entity.CODIGO = (string)Dr["CODIGO"]; }
                            if (Dr["DESCRIPCION"] != DBNull.Value) { Entity.DESCRIPCION = (string)Dr["DESCRIPCION"]; }
                            if (Dr["AVES"] != DBNull.Value) { Entity.AVES = (string)Dr["AVES"]; }
                            if (Dr["JABAS"] != DBNull.Value) { Entity.JABAS = (string)Dr["JABAS"]; }
                            if (Dr["pesoBRUTO"] != DBNull.Value) { Entity.pesoBRUTO = (decimal)Dr["pesoBRUTO"]; }
                            if (Dr["TARA"] != DBNull.Value) { Entity.TARA = (decimal)Dr["TARA"]; }
                            if (Dr["PESONETOG"] != DBNull.Value) { Entity.PESONETOG = (decimal)Dr["PESONETOG"]; }
                            if (Dr["UNITARIO"] != DBNull.Value) { Entity.UNITARIO = (decimal)Dr["UNITARIO"]; }
                            if (Dr["IMPORTE"] != DBNull.Value) { Entity.IMPORTE = (decimal)Dr["IMPORTE"]; }
                            if (Dr["Status"] != DBNull.Value) { Entity.Status = (string)Dr["Status"]; }
                            if (Dr["PesoPromedio"] != DBNull.Value) { Entity.PesoPromedio = (string)Dr["PesoPromedio"]; }
                            if (Dr["Exonerado"] != DBNull.Value) { Entity.Exonerado = (string)Dr["Exonerado"]; }
                            if (Dr["IGV"] != DBNull.Value) { Entity.IGV = (string)Dr["IGV"]; }
                            if (Dr["Cobrado"] != DBNull.Value) { Entity.Cobrado = (string)Dr["Cobrado"]; }
                            if (Dr["Cancelacion"] != DBNull.Value) { Entity.Cancelacion = (string)Dr["Cancelacion"]; }
                            if (Dr["PromedioDato"] != DBNull.Value) { Entity.PromedioDato = (string)Dr["PromedioDato"]; }
                            if (Dr["Afecto"] != DBNull.Value) { Entity.Afecto = (string)Dr["Afecto"]; }
                            if (Dr["Total"] != DBNull.Value) { Entity.Total = (string)Dr["Total"]; }
                            if (Dr["porCobrar"] != DBNull.Value) { Entity.porCobrar = (string)Dr["porCobrar"]; }

                            if (Dr["nombreCliente"] != DBNull.Value) { Entity.nombreCliente = (string)Dr["nombreCliente"]; }
                            if (Dr["nombreUbigeo"] != DBNull.Value) { Entity.nombreUbigeo = (string)Dr["nombreUbigeo"]; }
                             

                            List.Add(Entity);
                        }
                        return List;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
         
        public int GuardarGuiaVenta(GuiaVentaPolloRequest datos)
        {
            using (var Ado = new SQLServer(ConStr))
                try
                {
                    Ado.BeginTransaction();
                    string strsql = "usp_Guardar_Venta";
                    var parametros = new SqlParameter[]
                    {
                       new SqlParameter { ParameterName = "@idventa", SqlDbType = SqlDbType.Int, Direction= ParameterDirection.Output},
                       new SqlParameter { ParameterName = "@mensaje", SqlDbType = SqlDbType.Int, Direction= ParameterDirection.Output},
                       new SqlParameter { ParameterName = "@idcliente", SqlDbType = SqlDbType.Int, SqlValue = datos.txtRUCCobranza },
                       new SqlParameter { ParameterName = "@idusuario", SqlDbType = SqlDbType.Int , SqlValue = datos.UsuarioId },
                       new SqlParameter { ParameterName = "@codigoventa", SqlDbType = SqlDbType.VarChar, Size = 100, SqlValue= datos.txtNroGuiaSalidaNumero },
                     };
                    Ado.ExecNonQueryProcTransaction(strsql, parametros);
                    var valor = parametros[0].Value;
                    var idventa = Convert.ToInt32(parametros[0].Value.ToString());
                    var mensaje = Convert.ToInt32(parametros[1].Value.ToString());

                    if (idventa != 0)
                    {
                        string strsql2 = "usp_Guardar_DetalleVenta";
                        var parametros2 = new SqlParameter[]
                        {

                        new SqlParameter { ParameterName = "@idventa", SqlDbType = SqlDbType.Int, SqlValue = idventa},
                        new SqlParameter { ParameterName = "@idproducto", SqlDbType = SqlDbType.Int, SqlValue = datos.idproducto},
                        new SqlParameter { ParameterName = "@NroGuiaSalida", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtNroGuiaSalida},
                        new SqlParameter { ParameterName = "@NroGuiaSalidaNumero", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtNroGuiaSalidaNumero},
                        new SqlParameter { ParameterName = "@NroGuiaSalidaNumero2", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtNroGuiaSalidaNumero2},
                        new SqlParameter { ParameterName = "@fechaSalidaVenta", SqlDbType = SqlDbType.DateTime ,Size=200, SqlValue = datos.txtFecha},
                        new SqlParameter { ParameterName = "@Mnd", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtMnd},
                        new SqlParameter { ParameterName = "@T_Cam", SqlDbType = SqlDbType.Decimal, SqlValue = datos.txtTCam},
                        new SqlParameter { ParameterName = "@RUCCobranza", SqlDbType = SqlDbType.Int,  SqlValue = datos.txtRUCCobranza},
                       /* new SqlParameter { ParameterName = "@RUCCobranzaNombre", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtRUCCobranzaNombre},*///no va
                        new SqlParameter { ParameterName = "@Motivo", SqlDbType = SqlDbType.Int, SqlValue = datos.cboMotivo},
                        new SqlParameter { ParameterName = "@ID", SqlDbType = SqlDbType.Int,  SqlValue = datos.txtID},
                        new SqlParameter { ParameterName = "@CODIGO", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtCODIGO},
                        new SqlParameter { ParameterName = "@DESCRIPCION", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtDESCRIPCION},
                        new SqlParameter { ParameterName = "@AVES", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtAVES},
                        new SqlParameter { ParameterName = "@JABAS", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtJABAS},
                        new SqlParameter { ParameterName = "@pesoBRUTO", SqlDbType = SqlDbType.Decimal,  SqlValue = datos.txtpesoBRUTO},
                        new SqlParameter { ParameterName = "@TARA", SqlDbType = SqlDbType.Decimal,  SqlValue = datos.txtTARA},
                        new SqlParameter { ParameterName = "@PESONETOG", SqlDbType = SqlDbType.Decimal,  SqlValue = datos.txtPESONETOG},
                        new SqlParameter { ParameterName = "@UNITARIO", SqlDbType = SqlDbType.Decimal,  SqlValue = datos.txtUNITARIO},
                        new SqlParameter { ParameterName = "@IMPORTE", SqlDbType = SqlDbType.Decimal,  SqlValue = datos.txtIMPORTE},
                        new SqlParameter { ParameterName = "@Status", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtStatus},
                        new SqlParameter { ParameterName = "@PesoPromedio", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtpesoPromedioDato},
                        new SqlParameter { ParameterName = "@Exonerado", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtExonerado},
                        new SqlParameter { ParameterName = "@IGV", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtIGV},
                        new SqlParameter { ParameterName = "@Cobrado", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtCobrado},
                        new SqlParameter { ParameterName = "@Cancelacion", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtCancelacion},
                        new SqlParameter { ParameterName = "@PromedioDato", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtpesoPromedioDato},
                        new SqlParameter { ParameterName = "@Afecto", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtAfecto},
                        new SqlParameter { ParameterName = "@Total", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtTotal},
                        new SqlParameter { ParameterName = "@porCobrar", SqlDbType = SqlDbType.VarChar,Size=200, SqlValue = datos.txtporCobrar}

                        };
                        Ado.ExecNonQueryProc(strsql2, parametros2);            
                    }
                    Ado.Commit();
                    return mensaje;
                }
                catch (Exception ex)
                {
                    Ado.Rollback();
                    throw ex;
                }
        }




    }
}
