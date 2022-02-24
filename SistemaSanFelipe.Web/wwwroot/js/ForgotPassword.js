$(function () {

    $("#BtnforgotPassword").on("click", function () {

        var correo = $("#txtcorreo").val();

        if (correo == "") {
            fnAlertAdvertencia("Necesitamos un Correo");
            return;
        }

        

        Post("Login/ForgotPassword?email=" + correo).done(function (response) {
            var result = response.data;
            if (response.code == 0) {
                $("#hidenForgotPassword").html("<p class='login-box-msg'>Se le envio un Correo para que pueda Recuperar su Contraseña</p>")
            }
            
            
        });

    });

});