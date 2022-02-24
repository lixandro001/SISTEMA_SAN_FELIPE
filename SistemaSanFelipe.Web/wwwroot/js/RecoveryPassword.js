$(function (e) {


    $("#formrecovery").validate({
        rules: {
          
            Password: {
                required: true,
                minlength: 6
            },
            ConfirPassword: {
                equalTo: "#Password"
            }
        }
    });

    $("#btnChangePassword").on("click", function () {

       var contraseña =  $("#Password").val();
       var Confirmcontraseña = $("#ConfirPassword").val();
        var TK = $("#TK").val();


        if (contraseña != Confirmcontraseña) {
            fnAlertAdvertencia("Las Contraseña no Coinciden");
            return;
        } 

        var parametros = new Object();

        parametros.Password = contraseña;
        parametros.Token = TK;

        Post("Request/ChangePassword", parametros).done(function (response) {

            var result = response.data;
            console.log(result);

            $("#hidenChangePassword").html("<p class='login-box-msg'>" + result.message + "</p>")


        });

    });

});