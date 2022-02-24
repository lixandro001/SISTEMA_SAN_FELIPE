
let ArrayCodes = [];

$(function () {

    $("#tabTrayManager").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        columns: [
            { data: null },
            { data: null },
            { data: null },
            { data: "SubsidiaryNeco" },
            { data: "SubsidiaryName" },
            { data: "NumReceipt" },
            { data: "NumReceiptSunat" },
            { data: "NumOrder" },
            { data: "CReceiptDate" },
            { data: "DocumentNumber" },
            { data: "NetAmount" },
            { data: "ISC" },
            { data: "CustomerName" },
            { data: "BusinessName" }
        ],
        columnDefs: [{
            "targets": 0,
            "orderable": false,
            "data": "ReceiptCode",
            "render": function (data, type, full, meta) {
                if (full.HasFiles) {
                    return "<label class='containerCheck'><input class='checkListCode' name='checkCode' data-code='" + data.ReceiptCode + "' type='checkbox'><span class='checkmark'></span></label>";
                       
                } else {
                    return "Boleta en Proceso";
                }
            }
        }, {
            "targets": 1,
            "orderable": false,
            "data": "ReceiptCode",
            "render": function (data, type, full, meta) {
                var btnview = "";
                if (full.HasFiles) {
                    btnview = "<a class='PoppupSaveMail' href='javascript:void(0)' data-code='" + data.ReceiptCode + "' title='Guardar Datos de Correo'/><i class='fa fa-pen' aria-hidden='true'></i></a>" + " " +
                        "<a class='PoppupDetail' href='javascript:void(0)' data-code='" + data.ReceiptCode + "' data-pdf='" + data.FilenamePDF + "' title='Ver Detalle'/><i class='fa fa-search-plus' aria-hidden='true'></i></a>";
                }
                return btnview;
            }
        },
        {
            "targets": 2,
            "orderable": false,
            "data": "ReceiptCode",
            "render": function (data, type, full, meta) {
                if (full.HasFiles) {
                    return "<a href='" + fnBaseUrlWeb("Receipt/Download?filename=" + full.FilenamePDF) + "'>PDF</a>" + " " +
                        "<a href='" + fnBaseUrlWeb("Receipt/Download?filename=" + full.FilenameXML) + "'>XML</a>";
                }
                return "";
            }
        }
        ]
    });

    $("#cboSubsidiary").select2();

    $("#txtStartDate, #txtEndDate").datepicker({
        autoclose: true,
        format: "dd/mm/yyyy"
    });

    $("#btnRefresh").on("click", function () {
        fnRefreshTrayManager();
    });

    fnLoadSubsidiary();
 

    $("#btnShowPoppup").on("click", function () {
        $("#mailpoppup").val("");
        ArrayCodes = [];
        $(".checkListCode").each(function (i, e) {
            if ($(e).prop("checked")) {
                ArrayCodes.push($(e).data("code"));
                $("#ListaArrays").val(ArrayCodes);
             
            }
        });

        if (ArrayCodes.length > 0) {
            $("#modalSendMail").modal("show");
        } else {
            fnAlertAdvertencia("Debe Seleccionar Uno o Varios Para enviar correo");
          
        }
    });

    $("#SendMail").on("click", function () {

        var correo = $("#mailpoppup").val();

        if (correo == "") {
            fnAlertAdvertencia("Necesitamos un Correo");
            return;
        }


        parametros = new Object();
        parametros.ReciedCode = ArrayCodes;
        parametros.mail = correo;

        Post("Receipt/SendMail", parametros).done(function (response) {
            var result = response.data;
            console.log(result);
            if (response.code == 0) {
                $("#modalSendMail").modal("hide");
                ArrayCodes = [];
                fnAlertSuccess("Se Envio Al Correo Correctamente");
                fnRefreshTrayManager();
            }
        });

    });

    $("#SaveMailData").on("click", function () {

        var receipt = $("#receiptcodehiden").val();
        var asunto = $("#mailAsunto").val();
        var adquireinte = $("#mailAdquiriente").val();
        var vin = $("#mailVIN").val();
        var titulo = $("#mailTítulo").val();
        var año = $("#mailAño").val();
        var Oficina = $("#mailOficina").val();
        var comprobante = $("#mailComprobante").val();

        parametros = new Object();

        parametros.ReceiptCode = receipt;
        parametros.maildata_subject = asunto;
        parametros.maildata_acquiring = adquireinte;
        parametros.maildata_vin = vin;
        parametros.maildata_title = titulo;
        parametros.maildata_year = año;
        parametros.maildata_office = Oficina;
        parametros.maildata_receipt = comprobante;

        if (receipt == "" || asunto == "" || adquireinte == "" || vin == "" || titulo == "" || año == ""
            || Oficina == "" || comprobante == "")
        {
            fnAlertAdvertencia("Todos los Campos son Obligatorios");
            return;
        }

        Post("Receipt/SaveMailData", parametros).done(function (response) {
            if (response.code == 0) {
                $("#modalSaveMail").modal("hide");
                fnAlertSuccess("Datos Registrados Correctamente");
                fnRefreshTrayManager();
            }
        });


    });

    $("#formSaveDataMail").validate({
        rules: {
            mailAsunto: {
                required: true
            },
            mailAdquiriente: {
                required: true
            },
            mailVIN: {
                required: true
            },
            mailTítulo: {
                required: true
            },
            mailAño: {
                required: true

            },
            mailOficina: {
                required: true
            },
            mailComprobante: {
                required: true

            }
        }
    });


});

 

function fnLoadSubsidiary() {

    Get("Main/GetSubsidiary").done(function (response) {
      
        if (response.data != null) {
            var select = document.getElementById('cboSubsidiary');
            var options = '<option value="">Seleccionar Subsidiario</option>';
            for (let item of response.data) {
                options += `<option value="${item.SubsidiaryId}">${item.Name}</option>`
            }
            select.innerHTML = options;
        }
    });
}


function fnRefreshTrayManager() {

    var params = new Object();
    var startDate = "";
    var endDate = "";
    var subsidiaryId = $("#cboSubsidiary").val();
    var numorder = $("#txtNumOrder").val();
    var documentNumber = $("#txtDocumentNumber").val();

    if ($("#chkApplyDates").prop("checked")) {
        startDate = $("#txtStartDate").val();
        endDate = $("#txtEndDate").val();
    } else {
        if (!(subsidiaryId != "" || numorder != "" || documentNumber != "")) {
            fnAlertAdvertencia("Debe proporcionar la Sucursal, Número de Pedido o Número de Documento");
            return;
        }
    }
    
    Get("Receipt/GetReceiptManager?startdate=" + startDate + "&enddate=" + endDate + "&subsidiaryId=" + subsidiaryId + "&numorder=" + numorder + "&documentNumber=" + documentNumber).done(function (response) {
        console.log(response);
        if (response.data != null) {
            fnClearTable($('#tabTrayManager').dataTable());
            if (response.data.length > 0) {
                $('#tabTrayManager').dataTable().fnAddData(response.data);
                eventDetail();
                poppupSaveMail();
            } else {
                fnAlertAdvertencia("No se Encontro Datos Disponibles");
            }
        }
    });

}

function eventDetail() {
    $(".PoppupDetail").each(function (i, e) {

        var code = $(e).data("code");
        var pdf = $(e).data("pdf");

        $(this).on("click", function (f) {
            f.preventDefault();
            $("#fraPDF").prop("src", fnBaseUrlWeb("files/" + pdf));
            $("#modalDetail").modal("show");
        });

    });
}

function poppupSaveMail() {
    $(".PoppupSaveMail").each(function (i, e) {

        var code = $(e).data("code");

        $(this).on("click", function (f) {
            f.preventDefault();

            Get("Receipt/GetDataMail?ReciedCode=" + code).done(function (response) {
                var respuesta = response.data
                console.log(respuesta);
                if (respuesta.maildata_subject == null || respuesta.maildata_acquiring == null
                    || respuesta.maildata_vin == null || respuesta.maildata_title == null
                    || respuesta.maildata_year == null || respuesta.maildata_office == null
                    || respuesta.maildata_receipt == null)
                {
                    $("#mailAsunto").val("");
                    $("#mailAdquiriente").val("");
                    $("#mailVIN").val("");
                    $("#mailTítulo").val("");
                    $("#mailAño").val("");
                    $("#mailOficina").val("");
                    $("#mailComprobante").val("");

                } else {
                    $("#mailAsunto").val(respuesta.maildata_subject);
                    $("#mailAdquiriente").val(respuesta.maildata_acquiring);
                    $("#mailVIN").val(respuesta.maildata_vin);
                    $("#mailTítulo").val(respuesta.maildata_title);
                    $("#mailAño").val(respuesta.maildata_year);
                    $("#mailOficina").val(respuesta.maildata_office);
                    $("#mailComprobante").val(respuesta.maildata_receipt);

                }
               

            });
 
            $("#receiptcodehiden").val(code);
            $("#modalSaveMail").modal("show");
        });

    });
}


