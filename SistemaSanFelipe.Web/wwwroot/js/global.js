var TituloApp = "FACT EKT";

var urlglobal = "http://localhost:28548/files/";

$(function (e) {

});

function fnClearTable(tabla) {
    tabla.fnClearTable();
}


function Post(url, params, async) {
    return ajaxMethod(url, "POST", params, async);
}

function PostUpload(url, params, async) {
    return ajaxMethodUpload(url, "POST", params, async);
}

function Get(url, params, async) {
    return ajaxMethod(url, "GET", params, async);
}

function ajaxMethod(url, method, params, async) {
    if (async == undefined || async == null) async = true;

    $("body").loading({
        zIndex: 1050,
        message: 'Cargando...'
    });

    return $.ajax({
        url: window.appURL + url,
        method: method,
        cache: false,
        data: params,
        async: async,
        //processData: false,
        //contentType: false,
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.debug(jqXHR);
        console.debug(textStatus);
        console.debug(errorThrown);
    }).always(function () {
        $('body').loading('stop');  
    });
}

function ajaxMethodUpload(url, method, params, async) {
    if (async == undefined || async == null) async = true;

    $("body").loading({
        zIndex: 1050,
        message: 'Cargando...'
    });

    return $.ajax({
        url: window.appURL + url,
        method: method,
        cache: false,
        data: params,
        async: async,
        processData: false,
        contentType: false,
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.debug(jqXHR);
        console.debug(textStatus);
        console.debug(errorThrown);
    }).always(function () {
        $('body').loading('stop');  
    });
}

function fnBaseUrlWeb(url) {
    return window.appURL + url;
}

 

function fnAlertAdvertencia(messange,titulo) {
 
    swal({
        title: titulo || "¡Advertencia!",
        html: false,
        text: messange,
        type: "warning",
        showCancelButton: false,
        confirmButtonColor: "#007bff",
        confirmButtonText: "Aceptar",
        closeOnConfirm: true
    });
}


function fnAlertError(messange) {
    swal({
        title: '¡Error!',
        html: true,
        text: messange || 'Ocurrio Un Error Inesperado, Porfavor Vuelva a Cargar la Pagina',
        type: "error",
        showCancelButton: false,
        confirmButtonColor: "#007bff",
        confirmButtonText: "Aceptar",
        closeOnConfirm: true
    });
}

function fnAlertSuccess(message, functionOnConfirm) {
    swal({
        title: "¡Exito!",
        html: false,
        text: message || "Acción ejecutada con exito.",
        type: "success",
        showCancelButton: false,
        confirmButtonColor: "#007bff",
        confirmButtonText: "Aceptar",
        closeOnConfirm: true
    }, functionOnConfirm);
}
