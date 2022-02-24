$(function (e) {

 
    $("#frmLogin").on("submit", function (e) {
        e.preventDefault();
    });
    //$("#frmLogin").validate();

    $("#btnLogin").click(function () {
        if (validForm() == true) { 
            fnLogin();
        }
        else
        {
            fnAlertAdvertencia("Ingrese Todos Sus Datos");
            return;
        }
    });

    $("#messageError").hide();

});


function validForm() {
    var flagValid = true;
    if ($("#Login").val() == "") {
        return flagValid = false;
    }
    if ($("#Password").val() == "") {
        return flagValid = false;
    }
    return flagValid;
}



function fnLogin() {
     var parametro = new Object();
     parametro.Login = $("#Login").val();
     parametro.Password = $("#Password").val();
    Post("Login/Validate", parametro).done(function (response) {
        var result = response.data;
        console.log(result);
        if (result.code == 0) {
        
            window.location = fnBaseUrlWeb("Main/Index");
            
        } else {
            $("#messageError").html(response.message);
            $("#messageError").show();
        }

        
    });
}