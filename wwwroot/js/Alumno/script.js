function Validar(imprimir) {
    //Obteniendo los valores de los inputs del form
    var carnet = document.getElementById("txtCarnet").value;
    var nombre = document.getElementById("txtNombre").value;
    var Correo = document.getElementById("txtCorreo").value;
    var fecha = document.getElementById("txtFecha").value;

    var respuesta = "";
    var resultado = true;


    //Codigo que valida si el formato del carnet es el correcto
    var carnetDividido = carnet.split("-");
    if (carnetDividido.length == 3) {
        if (isNaN(carnetDividido[0]) || isNaN(carnetDividido[1]) || isNaN(carnetDividido[2])) {
            respuesta += "Carnet No valido Ingreso un caracter que no es un digito.\n";
            resultado = false;
        }
        else {
            respuesta += "CARNET VALIDO\n";
            resultado = true;
        }
    }
    else {
        respuesta += "Carnet No valido Ingreso mal la estructura del carnet.\n";
        resultado = false;
    }

    //Codigo que verifica si ingresaron un nombre
    if (nombre.length > 0) respuesta += "Nombre Valido\n";
    else {
        respuesta += "Nombre no valido\n";
        resultado = false;
    }

    //Codigo que verifica si ingresaron una fecha
    if (fecha.length > 0) respuesta += "Fecha Nacimiento Valida\n";
    else {
        respuesta += "Fecha Nacimiento No Valida Por favor Ingrese un valor\n";
        resultado = false;
    }

    //Codigo que verifica si el correo tiene formato correcto
    if (Correo.includes("@") && Correo.includes(".")) {
        respuesta += "Correo valido\n";
    }
    else {
        respuesta += "Correo No valido\n";
        resultado = false;
    }

    if (imprimir == true) alert(respuesta);

    return resultado;
}

function Limpiar()
{
    document.getElementById("txtCarnet").value = "";
    document.getElementById("txtNombre").value ="";
    document.getElementById("txtCorreo").value ="";
    document.getElementById("txtFecha").value ="";
}
