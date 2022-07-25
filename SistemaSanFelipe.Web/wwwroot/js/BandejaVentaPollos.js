
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

    $("#btnConsultar").on("click", function () {
        fnObtenerGuiaVenta();
    });

    $("#btnNuevo").on("click", function () {
        fnViewForm();
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

function fnViewRegister(CodeRegister) {
    location.href = '/Main/FormularioPerfilComercial/?code=' + CodeRegister;
}

function fnEditRegister(CodeRegister) {
    location.href = '/Main/EditFormularioPerfilComercial/?code=' + CodeRegister;
}

function fnViewForm() {
    location.href = '/Main/FichaVenta';
}
