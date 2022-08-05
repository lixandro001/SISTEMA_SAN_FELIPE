
$(function (e) {
    fnObtenerClientes();
    fnObtenerNroGuiaVentaNuevo();  
    const fecha = new Date();
    const añoActual = fecha.getFullYear();
    const hoy = fecha.getDate();
    const mesActual = fecha.getMonth() + 1;
    var txtpesoBRUTO=0.0;
    var txtTARA = 0.0;
    var txtPESONETOG = 0.0;
    var txtUNITARIO = 0.0;
    var txtIMPORTE = 0.0;
    var nroaves = 0.0;
    var peso = 0.0;

    $("#txtCobrado").val('0.00');
    $("#txtCancelacion").val('00/00/0000');
    $("#txtSN").val('S/N');
    $("#txtSN1").val('000');
    $("#txtSN2").val('000000');
    $("#txtpesoBRUTO").change(function () {     
        txtpesoBRUTO = $("#txtpesoBRUTO").val();
        $("#txtPESONETOG").val(parseFloat(txtpesoBRUTO) - parseFloat(txtTARA));
    });
    $("#txtTARA").change(function () {       
        txtTARA = $("#txtTARA").val();
        $("#txtPESONETOG").val(parseFloat(txtpesoBRUTO) - parseFloat(txtTARA));   
    });
    $("#txtPESONETOG").change(function () {
        $("#txtPESONETOG").val(parseFloat(txtpesoBRUTO) - parseFloat(txtTARA));
    });
    $("#txtUNITARIO").change(function () {
        txtUNITARIO = $("#txtUNITARIO").val();
        txtPESONETOG = $("#txtPESONETOG").val();
        $("#txtIMPORTE").val(parseFloat(txtPESONETOG) * parseFloat(txtUNITARIO));             
        $("#txtTotal").val((parseFloat(txtPESONETOG) * parseFloat(txtUNITARIO)));
        $("#txtporCobrar").val((parseFloat(txtPESONETOG) * parseFloat(txtUNITARIO)));
        $("#txtAfecto").val( Number.parseFloat( ($("#txtTotal").val()) / 1.18).toFixed(3) );
        $("#txtIGV").val( Number.parseFloat( ($("#txtAfecto").val()) * 0.18).toFixed(3)  );

    });
    $("#txtIMPORTE").change(function () {
        $("#txtIMPORTE").val(parseFloat(txtPESONETOG) * parseFloat(txtUNITARIO));     
        $("#txtTotal").val((parseFloat(txtPESONETOG) * parseFloat(txtUNITARIO)));
        $("#txtporCobrar").val((parseFloat(txtPESONETOG) * parseFloat(txtUNITARIO)));
        $("#txtAfecto").val( Number.parseFloat( ($("#txtTotal").val()) / 1.18).toFixed(3) );
        $("#txtIGV").val( Number.parseFloat( ($("#txtAfecto").val()) * 0.18).toFixed(3)  );
    });

    $("#txtUNITARIO").change(function () {
        nroaves = $("#txtAVES").val();
        peso = txtPESONETOG;
        console.log(nroaves);
        console.log(peso);
        var resultado = parseFloat(peso) / parseFloat(nroaves);
        console.log( Number.parseFloat(resultado).toFixed(4) );
        $("#txtpesoPromedioDato").val( Number.parseFloat(resultado).toFixed(3)  );
    })
    $("#txtIMPORTE").change(function () {
        nroaves = $("#txtAVES").val();
        peso = txtPESONETOG;

        console.log(nroaves);
        console.log(peso);

        $("#txtpesoPromedioDato").val(Number.parseFloat(resultado).toFixed(3) );
 
    })
    $("#tabDescTablaCliente").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        columns: [
            { data: null },      
            { data: "idzonaUbigeo" },
            { data: "nombreCliente" }          
        ],
        columnDefs: [{
            "targets": 0,
            "orderable": false,
            "data": "CodeClienteDatosGenerales",
            "render": function (data, type, full, meta) {
                //if (data.IdEstadoComercial == 4) {
                //    console.log(data.IdEstadoComercial);
                //    return `<div style="text-align:center;">
                //            <a class="fa fa-pencil" href="#" onclick="event.preventDefault();fnEditRegister('${data.CodeClienteDatosGenerales}')"><i class="fa fa-edit" aria-hidden="true"></i></a>
                //        </div>`;
                //}
                    return `<div style="text-align:center;">
                            <a class="fa fa-pencil" href="#" onclick="event.preventDefault();fnViewRegister('${data.idcliente}')"><i class="fa fa-eye" aria-hidden="true"></i></a>
                        </div>`;
            }
        }
        ]
    });

    $(".DateFechaOnly").datepicker({
        autoclose: true,
        format: "dd/mm/yyyy",
    });
    $("#txtExonerado").val('0.00');
    $("#txtStatus").val('por facturar');
    $("#txtMnd").val('S/.');
    $("#txtTCam").val('1.000');
    $("#txtNroGuiaSalida").val('Guia de salida');
    $("#txtNroGuiaSalidaNumero").val('001');
    $("#txtFecha").val(hoy + "/" + mesActual + "/" + añoActual);    
    $("#txtCODIGO").change(function () {
        $("#txtID").val(01);
        $("#txtDESCRIPCION").val('POLLO CARNE');
        $("#txtCODIGO").val(zfill(1, 6));  
    });
    $("#txtRUCCobranzaNombre").on("click", function () {       
        $("#exampleModalCenter").modal("show");
    });
    $('#myModal').on('shown.bs.modal', function () {
        $('#myInput').trigger('focus')
    })  
    $("#btnRefresh").on("click", function () {
        window.location = fnBaseUrlWeb("Main/FormularioCliente");
    });

    $("#btnGuardarFormulario").on("click", function () {

        if ($("#txtAVES").val() == null || $("#txtAVES").val() == ''
            & $("#txtJABAS").val() == null || $("#txtJABAS").val() == ''
            & $("#txtpesoBRUTO").val() == null || $("#txtpesoBRUTO").val() == ''
            & $("#txtTARA").val() == null || $("#txtTARA").val() == ''
            & $("#txtUNITARIO").val() == null || $("#txtUNITARIO").val() == '') {
            fnAlertAdvertencia("Debe llenar los datos obligatorios");
            return;
        } else {
            fnGuardarGuiaVentaPollo();
        }
        
        
    });

});

function fnViewRegister(CodeRegister) {
   
    Get("Mantenimiento/GetClienteId?idCliente=" + CodeRegister).done(function (response) {
        $('body').loading('stop');
       
        if (response.data != null) {
            var data = response.data;            
            $("#txtRUCCobranza").val(zfill(data.idcliente, 11)    );
            $("#txtRUCCobranzaNombre").val(data.nombreCliente);
            $("#exampleModalCenter").modal("hide");
        }
        else {
            fnAlertAdvertencia("No se Encontro Datos Disponibles");
        }
    });
}
 

function fnObtenerClientes() {  
    Get("Mantenimiento/GetBandejaCliente").done(function (response) {
        
        if (response.data != null) {
            fnClearTable($('#tabDescTablaCliente').dataTable());
           
            if (response.data.length > 0) {
                $('#tabDescTablaCliente').dataTable().fnAddData(response.data);
            } else {
                fnAlertAdvertencia("No se Encontro Datos Disponibles");
            }
        }
    });
}


function fnLoadCategoriaCliente() {
    Get("SisInternoSelect/GetCategoriaCliente").done(function (response) {
        
        var Response = response.data.Data;
        if (Response != null) {
            var select = document.getElementById('cboMotivo');
            var options = '<option value="">Seleccionar Categoria Cliente</option>';
            for (let item of Response) {
                options += `<option value="${item.Id_S_Categoria_Cliente}">${item.Nombre_S_Categoria_Cliente}</option>`;
            }
            select.innerHTML = options;
        }
    });
}

  
function fnObtenerNroGuiaVentaNuevo() {
    Get("Mantenimiento/GetNumeroGuiaVenta").done(function (response) {
        $('body').loading('stop');
         
        if (response.data != null) {
            var data = response.data ;
            
            $("#txtNroGuiaSalidaNumero2").val(zfill(data.nroGuia, 6));
        }
        else {
            fnAlertAdvertencia("No se Encontro Datos Disponibles");
        }
    });
}

function fnGuardarGuiaVentaPollo() {
    var parametros = new Object();

    var txtNroGuiaSalida = $("#txtNroGuiaSalida").val();
    var txtNroGuiaSalidaNumero = $("#txtNroGuiaSalidaNumero").val();
    var txtNroGuiaSalidaNumero2 = $("#txtNroGuiaSalidaNumero2").val();
    var txtFecha = $("#txtFecha").val();
    var txtMnd = $("#txtMnd").val();
    var txtTCam = $("#txtTCam").val();
    var txtRUCCobranza = $("#txtRUCCobranza").val();
    var txtRUCCobranzaNombre = $("#txtRUCCobranzaNombre").val();
    var cboMotivo = $("#cboMotivo").val();
    var txtID = $("#txtID").val();
    var txtCODIGO = $("#txtCODIGO").val();
    var txtDESCRIPCION = $("#txtDESCRIPCION").val();
    var txtAVES = $("#txtAVES").val();
    var txtJABAS = $("#txtJABAS").val();
    var txtpesoBRUTO = $("#txtpesoBRUTO").val();
    var txtTARA = $("#txtTARA").val();
    var txtPESONETOG = $("#txtPESONETOG").val();
    var txtUNITARIO = $("#txtUNITARIO").val();
    var txtIMPORTE = $("#txtIMPORTE").val();
    var txtStatus = $("#txtStatus").val();
    var txtpesoPromedioDato = $("#txtpesoPromedioDato").val();
    var txtExonerado = $("#txtExonerado").val();
    var txtIGV = $("#txtIGV").val();
    var txtCobrado = $("#txtCobrado").val();
    var txtCancelacion = $("#txtCancelacion").val();
    var txtAfecto = $("#txtAfecto").val();
    var txtTotal = $("#txtTotal").val();
    var txtporCobrar = $("#txtporCobrar").val();
    var txtSN = $("#txtSN").val();
    var txtSN1 = $("#txtSN1").val();
    var txtSN2 = $("#txtSN2").val();
     
    
    parametros.txtNroGuiaSalida = txtNroGuiaSalida;
    parametros.txtNroGuiaSalidaNumero = txtNroGuiaSalidaNumero;
    parametros.txtNroGuiaSalidaNumero2 = txtNroGuiaSalidaNumero2;
    parametros.txtFecha = txtFecha;
    parametros.txtMnd = txtMnd;
    parametros.txtTCam = txtTCam;
    parametros.txtRUCCobranza = txtRUCCobranza;
    parametros.txtRUCCobranzaNombre = txtRUCCobranzaNombre;
    parametros.cboMotivo = cboMotivo;
    parametros.txtID = txtID;
    parametros.txtCODIGO = txtCODIGO;
    parametros.txtDESCRIPCION = txtDESCRIPCION;
    parametros.txtAVES = txtAVES;
    parametros.txtJABAS = txtJABAS;
    parametros.txtpesoBRUTO = txtpesoBRUTO;
    parametros.txtTARA = txtTARA;
    parametros.txtPESONETOG = txtPESONETOG;
    parametros.txtUNITARIO = txtUNITARIO;
    parametros.txtIMPORTE = txtIMPORTE;
    parametros.txtStatus = txtStatus;
    parametros.txtpesoPromedioDato = txtpesoPromedioDato;
    parametros.txtExonerado = txtExonerado;
    parametros.txtIGV = txtIGV;
    parametros.txtCobrado = txtCobrado;
    parametros.txtCancelacion = txtCancelacion;
    parametros.txtAfecto = txtAfecto;
    parametros.txtTotal = txtTotal;
    parametros.txtporCobrar = txtporCobrar;
    parametros.txtSN = txtSN;
    parametros.txtSN1 = txtSN1;
    parametros.txtSN2 = txtSN2;
   
    Post("Mantenimiento/GuardarGuiaVenta", parametros).done(function (response) {
        if (response.code == 0) {
            fnAlertSuccess(response.message, function () {
                window.location = fnBaseUrlWeb("Main/BandejaVentas");
            });
            console.log(response.data);
        } else {
            fnAlertError(response.message);
        }
    });
}
 
  
function zfill(number, width) {
    var numberOutput = Math.abs(number); /* Valor absoluto del número */
    var length = number.toString().length; /* Largo del número */
    var zero = "0"; /* String de cero */
    if (width <= length) {
        if (number < 0) {
            return ("-" + numberOutput.toString());
        } else {
            return numberOutput.toString();
        }
    } else {
        if (number < 0) {
            return ("-" + (zero.repeat(width - length)) + numberOutput.toString());
        } else {
            return ((zero.repeat(width - length)) + numberOutput.toString());
        }
    }
}

 