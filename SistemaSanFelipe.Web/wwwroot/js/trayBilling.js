var flagEnviar = 0;

$(function () {

    $("#tabTrayBilling").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        columns: [
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
                //return "<a class='btn btn-default subirArchivo' href='javascript:void(0)' data-code='" + data.ReceiptCode + "' title='Subir archivos'/><i class='fa fa-upload' aria-hidden='true'></i></a>";
                var btndelete = "";
                var btnview = "";
                if (full.HasFiles) {
                    btndelete = "<a class='btn btn-default QuestionDelete' href='javascript:void(0)' data-code='" + data.ReceiptCode + "' title='Eliminar Archivos'/><i class='fa fa-trash' aria-hidden='true'></i></a>";
                    btnview = "<a class='btn btn-default PoppupDetail' href='javascript:void(0)' data-code='" + data.ReceiptCode + "' data-pdf='" + data.FilenamePDF + "' title='Ver Detalle'/><i class='fa fa-search-plus' aria-hidden='true'></i></a>";
                }

                var btnupload = "<a class='btn btn-default subirArchivo' href='javascript:void(0)' data-code='" + data.ReceiptCode + "' title='Subir archivos'/><i class='fa fa-upload' aria-hidden='true'></i></a>";

                return btnupload + " " + btndelete + " " + btnview;
            }
        },
        {
            "targets": 1,
            "orderable": false,
            "data": "ReceiptCode",
            "render": function (data, type, full, meta) {
                if (full.HasFiles) {
                    return "<a href='" + fnBaseUrlWeb("Receipt/Download?filename=" + full.FilenamePDF) + "' title='Descargar PDF'>PDF</a>" + " " +
                        "<a href='" + fnBaseUrlWeb("Receipt/Download?filename=" + full.FilenameXML) + "' title='Descargar XML'>XML</a>";
                }
                return "";
            }
        }   
        ]
    });

    $("#cboSubsidiary").select2();


    $("#frmExport").on("submit", function (e) {
        if (flagEnviar === 1) {
            flagEnviar = 0;
        }
        else {
            e.preventDefault();
        }
    });

    $("#btnExport").on("click", function (e) {
        
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
        flagEnviar = 1;
        $('#hdnStartDate').val(startDate);
        $('#hdnEndDate').val(endDate);
        $("#frmExport").submit();
    });

    $("#txtStartDate, #txtEndDate").datepicker({
        autoclose: true,
        format: "dd/mm/yyyy"
    });

    $("#btnRefresh").on("click", function () {
        fnRefreshTrayBilling();
    });

    fnLoadSubsidiary();
 
    // PARA EL PDF
    $(".browse-btn").on("click", function (a) {
        a.preventDefault();
        $("#real-input").click();
    });

    // PARA EL XML
    $(".browse-btnxml").on("click", function (a) {
        a.preventDefault();
        $("#real-inputxml").click();
    });

    // PARA EL PDF
    $("#real-input").on("change", function () {
        const name = $("#real-input").val().split(/\\|\//).pop();
        console.log(name);
        var htmlName = name.length > 20 ? name.substr(name.length - 20) : name;
        $(".file-info").text(htmlName);
    });

     // PARA EL XML
    $("#real-inputxml").on("change", function () {
        const name = $("#real-inputxml").val().split(/\\|\//).pop();
        console.log(name);
        var htmlName = name.length > 20 ? name.substr(name.length - 20) : name;
        $(".file-infoxml").text(htmlName);
    });

    $("#EnviarReq").on("click", function (e) {
        e.preventDefault();
        var code = $("#ReceiptCode").val();
        var file = $("#real-input")[0].files;
        var NumSunat = $("#numreceiptsunat").val();
        console.log(file);
        var filexml = $("#real-inputxml")[0].files;
        var fdata = new FormData();
        fdata.append("FormFile", file[0]);
        fdata.append("FormFile", filexml[0]);
        fdata.append("receiptCode", code);
        fdata.append("NumReceiptSunat", NumSunat)

        if (file.length > 0 && filexml.length > 0 && NumSunat != "" ) {

            // Validacion de extension PDF
            var extension = "";
            var ext = file[0].type;
            var ext2 = ext.split("/");

            // Validacion de extension XML
            var extensionxml = "";
            var extxml = filexml[0].type;
            var extxml2 = extxml.split("/");

            if (ext2.length > 1 && extxml2.length > 1) {
                extension = ext2[1];
                extensionxml = extxml2[1];

                console.log(extension);
                console.log(extensionxml);
                console.log(fdata);
                if ((extension == "pdf" || extension == "PDF") && (extensionxml == "xml" || extensionxml == "XML")) {

                    var parametros = new Object();

                    parametros.data = fdata;

                    PostUpload("Main/UploadFile",fdata).done(function (response) {
                        if (response.code == 0) {
                            $("#modalEnviarCorreo").modal("hide");
                            fnAlertSuccess(response.message);
                            fnRefreshTrayBilling();
                        }
                    });

                } else {
                    fnAlertAdvertencia("No Ingreso Un Formato pdf y xml");
                }
            }
        } else {
            fnAlertAdvertencia("Debe Seleccionar Dos Archivos y Ingresar una Boleta de Sunat");
        }   
    });
});


function eventShowPopup() {
    $(".subirArchivo").each(function(i,e) {

        $(this).on("click", function (f) {
            console.log($(f));
            f.preventDefault();
            $("#real-input").val("");
            $("#real-inputxml").val("");
            $(".file-infoxml").text("");
            $(".file-info").text("");
            var code = $(e).data("code");
            console.log(code);
            $("#ReceiptCode").val(code);

            $("#modalEnviarCorreo").modal("show");
        });
 
    });
}

function eventDetail() {
    $(".PoppupDetail").each(function (i, e) {
        $(this).on("click", function (f) {
            f.preventDefault();
            var code = $(e).data("code");
            var pdf = $(e).data("pdf");

            $("#divFrame").html("<iframe src='" + fnBaseUrlWeb("files/" + pdf) + "' frameborder='0' style='width: 100%; height: 400px;'></iframe>");
            $("#modalDetail").modal("show");
        });

    });
}

function eventDelete() {
    $(".QuestionDelete").each(function (i, e) {



        $(this).on("click", function (f) {
            f.preventDefault();
            var code = $(e).data("code");

            swal({
                title: "¿Estas Seguro?",
                text: "Si eliminas no podras recuperar La Informacion",
                type: "warning",
                showCancelButton: true,
                cancelButtonText: "Cancelar",
                confirmButtonColor: "#007bff",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                closeOnCancel: true
            }, function (isConfirm) {
                if (isConfirm) {
                    PostUpload("Main/DeleteReceiptFileName?ReceiptCode=" + code).done(function (response) {
                        if (response.code == 0) {
                            fnRefreshTrayBilling();
                            fnAlertSuccess("Se elimino Correctamente");

                        }
                    });
                }
            });
        });
    });
}


function fnLoadSubsidiary() {

    Get("Main/GetSubsidiary").done(function (response) {
        console.log(response.data);
        if (response.data != null) {
            var select = document.getElementById('cboSubsidiary');
            var options = '<option value="">Seleccionar Subsidiario</option>';
            for (let item of response.data) {
                options += `<option value="${item.SubsidiaryId}">${item.Name}</option>`;
            }
            select.innerHTML = options;
        }
    });
}


function fnRefreshTrayBilling() {

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
    

    Get("Receipt/GetReceiptBilling?startdate=" + startDate + "&enddate=" + endDate + "&subsidiaryId=" + subsidiaryId + "&numorder=" + numorder + "&documentNumber=" + documentNumber).done(function (response) {
        console.log(response.data);
        if (response.data != null) {
           
            fnClearTable($('#tabTrayBilling').dataTable());
            if (response.data.length > 0) {
                $('#tabTrayBilling').dataTable().fnAddData(response.data);  
                eventShowPopup();
                eventDetail();
                eventDelete();
            } else {
                fnAlertAdvertencia("No se Encontro Datos Disponibles");
            }
        }
    });

}

