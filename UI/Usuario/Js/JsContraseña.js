
function codigoIngresado() {
    let contraseña = document.getElementById("txtCodigo");
    let smallError = document.getElementById("smallError");
    if (contraseña.value.trim() === "") {
        smallError.textContent = "Debes ingresar el código enviado al mail";
        return false;
    }
    smallError.textContent = "";
    return true;
}

function soloNumeros(evento) {
    let ASCIICode = (evento.which) ? evento.which : evento.keyCode;
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}
