
function validarCampos() {
    contrasenaValida();
    mailValido();
    if (contrasenaValida() && mailValido())
    {
        return true;
    }
    else {
        return false;
    }
}
function mailValido() {
    let mail = document.getElementById("txtMail");
    let mensaje = document.getElementById("mailErrorMsj");
    mensaje.textContent = ""; 
    mensaje = document.getElementById("mailCorrectoMsj");
    mensaje.textContent = "";

    if (mail.value == "") {
        let mensaje2 = document.getElementById("mailErrorMsj");
        mensaje2.textContent = "Debes ingresar un mail";
        mail.classList.add("is-invalid");
        mail.classList.remove("is-valid")
        return false; 
    }
    else {
        let mensaje2 = document.getElementById("mailCorrectoMsj");
        mensaje2.textContent = "Mail correcto";
        mail.classList.remove("is-invalid")
        mail.classList.add("is-valid");
        return true; 
    }
}

function contrasenaValida() {
    let contrasena = document.getElementById("txtPass");
    let mensaje = document.getElementById("contrasenaErrorMsj");
    mensaje.textContent = ""; 
    mensaje = document.getElementById("contrasenaCorrectaMsj");
    mensaje.textContent = ""; 

    if (contrasena.value == "") {
        let mensaje = document.getElementById("contrasenaErrorMsj");    
        mensaje.textContent = "Debes ingresar una contraseña";
        contrasena.classList.remove("is-valid")
        contrasena.classList.add("is-invalid");
        return false;
    }
    else {
        let mensaje = document.getElementById("contrasenaCorrectaMsj");
        contrasena.classList.remove("is-invalid")
        contrasena.classList.add("is-valid");
        mensaje.textContent = "Contraseña correcta";
        return true;
    }
}