$(function (e) {

    

    $("#fmrRegister").on("submit", function (e) {
        e.preventDefault();
    });
    
  

    $("#btnRegister").click(function () {
        if (validForm() == 0) {

           
            fnSave();
        } else {

            fnAlertAdvertencia("Ingrese Todos Sus Datos");
          
            return;
        }    
    });

    $("#agreeTerms").on("click", function () {
        $("#modalTerminos").modal("show");
    });

    $("#fmrRegister").validate({
        rules: {
            Fullname: {
                required: true
            },
            DucumentNumber: {
                required: true,
                minlength: 8
 
            },
            Email: {
                required: true

            },
            Password: {
                required: true,
                minlength: 6
            },
            Password2: {
                equalTo: "#Password"
            }
        }
    });


});




function validForm() {
    var flagValid = 0;

    if ($("#Fullname").val() == "") {
      return flagValid = 1;
    }
    if ($("#DucumentNumber").val() == "") {
        return flagValid = 1;
    }
    if ($("#Email").val() == "") {
        return flagValid = 1;
    }
    if ($("#Password").val() == "") {
        return flagValid = 1;
    }
    var valid = camposIguales();
    if (!valid) {
        flagValid = 1;
        e.preventDefault();
    }
    return flagValid; 
}

function camposIguales() {
    var p1 = document.getElementById("Password").value;
    var p2 = document.getElementById("Password2").value;
    if (p1 != p2) {

        fnAlertAdvertencia("Los Password Deben Ser Iguales");
        return false;
    } else {
        
        return true;
    }
}


function fnSave() {

    var Fullname = $('#Fullname').val();
    var DucumentNumber = $('#DucumentNumber').val();
    var Email = $('#Email').val();
    var Password = $('#Password').val();

    var parametro = new Object();
    parametro.Fullname = Fullname;
    parametro.DucumentNumber = DucumentNumber;
    parametro.Email = Email;
    parametro.Password = Password;

    Post("User/SaveUser", parametro).done(function (response) {
        var result = response.data;
        console.log(result)

        if (result.code == 0) {
            $("#divRegister").html("<p class='login-box-msg'>Gracias Por Registrarse , Se le envio un Correo de Bienvenida</p>")
        //    window.location = fnBaseUrlWeb("Login/Index");
          
        } else {    
            fnAlertAdvertencia(result.message);
        }
    });

}



if (Dni != "") {
    if (ApellidoPaterno != "") {
        if (ApellidoMaterno != "") {
            if (PrimerNombre != "") {
                if (FechaNacimiento != "") {
                    if (Idsexo != "") {
                        if (idestadoCivil != "") {
                            if (Telefonocelular != "") {
                                if (idactividadEconomica != "") {
                                    if (MontoIngreso != "") {
                                        if (idtipoIngreso != "") {
                                            if (idDepartamentoDomicilio != "") {
                                                if (idProvinciaDomicilio != "") {
                                                    if (idDistritoDomicilio != "") {
                                                        if (calleVives != "") {
                                                            if (numero != "") {
                                                                if (AntiguedadDomiciliaria != "") {
                                                                    if (nombreEmpresa != "") {
                                                                        if (idgiro != "") {
                                                                            if (CalleTrabajo != "") {
                                                                                if (idDepartamento != "") {
                                                                                    if (idProvincia != "") {
                                                                                        if (idDistrito != "") {
                                                                                            if (idtipoVivienda != "") {
                                                                                                if (idDequienEs != "") {
                                                                                                    if (Ref1ApellidoPaterno != "") {
                                                                                                        if (Ref1PrimerNombre != "") {
                                                                                                            if (Ref1Telefonoparticular != "") {
                                                                                                                if (Ref2ApellidoPaterno != "") {
                                                                                                                    if (Ref2PrimerNombre != "") {
                                                                                                                        if (Ref2Telefonoparticular != "") {
                                                                                                                            if (idTurnoInvestigacion != "") {
                                                                                                                                if (idSucursal != "") {
                                                                                                                                    if (Ref1parentesco != "") {
                                                                                                                                        if (Ref2parentesco != "") {








                                                                                                                                        } else {
                                                                                                                                            fnAlertAdvertencia("debe seleccionar el parentesco de la Referencia 2");
                                                                                                                                        }

                                                                                                                                    } else {
                                                                                                                                        fnAlertAdvertencia("debe seleccionar el parentesco de la referencia 1");
                                                                                                                                    }

                                                                                                                                } else {
                                                                                                                                    fnAlertAdvertencia("debe Seleccionar Sucursal");
                                                                                                                                }
                                                                                                                            } else {
                                                                                                                                fnAlertAdvertencia("debe seleecionar el turno de la investigacion");
                                                                                                                            }

                                                                                                                        } else {
                                                                                                                            fnAlertAdvertencia("debe poner el telefono particular de la Rf2");
                                                                                                                        }

                                                                                                                    } else {
                                                                                                                        fnAlertAdvertencia("debe ingresar el primer nombre de la Rf2");
                                                                                                                    }
                                                                                                                } else {
                                                                                                                    fnAlertAdvertencia("debe ingresar el apellido paterno de la Rf2");
                                                                                                                }
                                                                                                            } else {
                                                                                                                fnAlertAdvertencia("debe ingresar el telefono particular de la Rf1");
                                                                                                            }
                                                                                                        } else {
                                                                                                            fnAlertAdvertencia("debe ingresar el primer nombre de la Rf1");
                                                                                                        }
                                                                                                    } else {
                                                                                                        fnAlertAdvertencia("debe ingresar apellido paterno de la Rf1");
                                                                                                    }
                                                                                                } else {
                                                                                                    fnAlertAdvertencia("debe seleccionar de quien es");
                                                                                                }
                                                                                            } else {
                                                                                                fnAlertAdvertencia("debe seleccionar tipo de vivienda");
                                                                                            }
                                                                                        } else {
                                                                                            fnAlertAdvertencia("debe seleccionar el distrito, Datos Laborales");
                                                                                        }

                                                                                    } else {
                                                                                        fnAlertAdvertencia("Debe Ingresar la Provincia, Datos Laborales");
                                                                                    }

                                                                                } else {
                                                                                    fnAlertAdvertencia("Debe Ingresar el Departamento, Datos Laborales");
                                                                                }
                                                                            } else {
                                                                                fnAlertAdvertencia("Debe Ingresar la calle del trabajo, Datos Laborales");
                                                                            }
                                                                        } else {
                                                                            fnAlertAdvertencia("Debe Ingresar el Giro del Negocio");
                                                                        }
                                                                    } else {
                                                                        fnAlertAdvertencia("Debe Ingresar el Nombre de la Empresa");
                                                                    }
                                                                } else {
                                                                    fnAlertAdvertencia("Debe Ingresar la Antiguedad Domiciliaria");
                                                                }
                                                            } else {
                                                                fnAlertAdvertencia("Debe Ingresar el Numero");
                                                            }
                                                        } else {
                                                            fnAlertAdvertencia("Debe Ingresar la Calle donde Vives");
                                                        }
                                                    } else {
                                                        fnAlertAdvertencia("Debe Ingresar el Distrito, Domicilio");
                                                    }
                                                } else {
                                                    fnAlertAdvertencia("Debe Ingresar la Provincia, Domicilio");
                                                }
                                            } else {
                                                fnAlertAdvertencia("Debe Ingresar el Departamento, Domicilio");
                                            }
                                        } else {
                                            fnAlertAdvertencia("Debe Ingresar el Tipo de Ingreso, Datos Economicos");
                                        }
                                    } else {
                                        fnAlertAdvertencia("Debe Ingresar el Monto de Ingreso, Datos Economicos");
                                    }
                                } else {
                                    fnAlertAdvertencia("Debe Ingresar la Actividad Economica, Datos Economicos");
                                }
                            }
                            else {
                                fnAlertAdvertencia("Debe Ingresar el Celular, Datos Basicos");
                            }
                        }

                        else {
                            fnAlertAdvertencia("Debe Ingresar el Estado Civil, Datos Basicos");
                        }
                    }
                    else {
                        fnAlertAdvertencia("Debe Ingresar el  tipo de Sexo, Datos Basicos");
                    }
                }
                else {
                    fnAlertAdvertencia("Debe Ingresar la Fecha de Nacimiento, Datos Basicos");
                }
            }
            else {
                fnAlertAdvertencia("Debe Ingresar el Primer Nombre, Datos Basicos");
            }
        }
        else {
            fnAlertAdvertencia("Debe Ingresar el Apellido Materno, Datos Basicos");
        }
    }
    else {
        fnAlertAdvertencia("Debe Ingresar el Apellido Paterno, Datos Basicos");
    }
} else {
    fnAlertAdvertencia("Debe Ingresar el DNI, Datos Basicos");
}