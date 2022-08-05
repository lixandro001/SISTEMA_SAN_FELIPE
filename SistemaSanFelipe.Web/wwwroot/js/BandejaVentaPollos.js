
$(function () {
    fnObtenerGuiaVenta();

     

    $("#tabDescTable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        columns: [
            { data: null },
            { data: "codigoventa"},
            { data: "idventa" },
            { data: "cfechaSalidaVenta"},
            { data: "nombreUbigeo"},
            { data: "nombreCliente" },
            { data: "jabas" },
            { data: "aves" },
            { data: "pesoBRUTO" },
            { data: "tara" },
            { data: "pesonetog" },
            { data: "pesoPromedio" },
            { data: "unitario" },
            { data: "total" },
            { data: "porCobrar" }
          
        ],
        columnDefs: [{
            "targets": 0,
            "orderable": false,
            "data": "idventa",
            "render": function (data, type, full, meta) {
             
               
                    return `<div style="text-align:center;">
                            <a class="fa fa-pencil" href="#"  ;fnViewRegister('${data.idventa}')">GSA</a>
                        </div>`;
            }
        }
        ]
    });

    $("#txtStartDate, #txtEndDate").datepicker({
        autoclose: true,
        format: "dd/mm/yyyy"
    });


    $("#btnReporteExcel").on("click", function () {

        var fechaInicial = "";
        var fechaFinal = "";

        fechaInicial = $("#txtStartDate").val();
        fechaFinal = $("#txtEndDate").val();
         
        location.href = '/Mantenimiento/ExportExcel?StartDate=' + fechaInicial + "&EndDate=" + fechaFinal;
        /*$("#frmExport").submit();*/
    });
     

    $("#btnConsultar").on("click", function () {
        fnObtenerGuiaVenta();
    });

    $("#btnNuevo").on("click", function () {
        fnViewForm();
    });

     

    $("#btnReportePdf").on("click", function () {
        consultarPdf();
    });




});


function fnObtenerGuiaVenta() {
    var startDate = "";
    var endDate = "";
    startDate = $("#txtStartDate").val();
    endDate = $("#txtEndDate").val();
    /*PerfilId = $("#perfil").val();*/
    Get("Mantenimiento/ObtenerListadoPollos?StartDate=" + startDate + "&EndDate=" + endDate).done(function (response) {
        console.log(response);

        if (response.data != null) {
            fnClearTable($('#tabDescTable').dataTable());
            console.log(response.data);
            if (response.data.length > 0) {
                $('#tabDescTable').dataTable().fnAddData(response.data);
            } else {
                fnAlertAdvertencia("No se Encontro Datos Disponibles");
            }
        }
    });
}

function consultarPdf() {
    var startDate = "";
    var endDate = "";

    startDate = $("#txtStartDate").val();
    endDate = $("#txtEndDate").val();

    //var replaceSlashes = $(this).val().replace(/\\/g, "/");
    //$(this).val(replaceSlashes);

    var nuevofechaInicial = startDate.replace("/", "-").replace("/", "-");
    var nuevofechaFinal = endDate.replace("/", "-").replace("/", "-");

     
    console.log(nuevofechaInicial);
    console.log(nuevofechaFinal);

    location.href = '/Mantenimiento/DescargarPdf?fechaInicio=' + nuevofechaInicial + "&FechaFinal=" + nuevofechaFinal;
}




function fnViewRegister(CodeRegister) {
    location.href = '/Main/FormularioPerfilComercial/?code=' + CodeRegister;
}

function fnEditRegister(CodeRegister) {
    location.href = '/Main/EditFormularioPerfilComercial/?code=' + CodeRegister;
}

function fnViewForm() {
    location.href = '/Main/FichaVenta';
}
