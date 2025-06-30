function soloNumeros(e) {
    var key = e.keyCode || e.which;
    return (key >= 48 && key <= 57) || key === 8;
}

//function validarCP(input) {
//    const mensaje = document.getElementById("cpErrorMsj");
//    const valor = input.value;

//    if (!/^\d*$/.test(valor)) {
//        mensaje.textContent = "Solo se permiten números.";
//    } else if (valor.length >= 9) {
//        mensaje.textContent = "Haz alcanzado el límite de 9 dígitos.";
//    } else {
//        mensaje.textContent = "";
//    }
//}
function validarLongitud(input, maxLength, mensajeId) {
    const mensaje = document.getElementById(mensajeId);
    const caracteresDisponibles = maxLength - input.value.length;
    const caracteresPorcentaje = (maxLength / 6);
    const valor = input.value;

    if (caracteresDisponibles <= 5 && caracteresDisponibles > 0) {
        mensaje.textContent = `Te quedan ${caracteresDisponibles} caracteres.`;
        mensaje.classList.remove("text-danger");
        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
    } else if (input.value.length === maxLength) {
        mensaje.textContent = `Has alcanzado el límite de ${maxLength} caracteres.`;
        mensaje.classList.add("text-danger");
        //input.classList.remove("is-valid");
        //input.classList.add("is-invalid");
    } else {
        mensaje.textContent = "";
        mensaje.classList.remove("text-danger");
        input.classList.remove("is-invalid");
        input.classList.remove("is-valid");
    }

    if (valor.length >= caracteresPorcentaje-1) {
        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
    } else {
        input.classList.remove("is-valid");
        input.classList.remove("is-invalid");
    }
}
function validarLongitudYCaracteresEspeciales(input, maxLength, mensajeId) {
    const mensaje = document.getElementById(mensajeId);
    const valor = input.value;
    const caracteresDisponibles = maxLength - valor.length;
    const regexLetrasEspeciales = /^[A-Za-zÁÉÍÓÚáéíóúÜüÑñ\s]+$/;

    // Validar caracteres inválidos primero
    if (valor !== "" && !regexLetrasEspeciales.test(valor)) {
        mensaje.textContent = "Solo se permiten letras, espacios y caracteres especiales (á, é, í, ó, ú, ü, ñ).";
        mensaje.classList.add("text-danger");
        input.classList.remove("is-valid");
        input.classList.add("is-invalid");
        return;
    }

    // Validar longitud máxima
    if (valor.length === maxLength) {
        mensaje.textContent = `Has alcanzado el límite de ${maxLength} caracteres.`;
        mensaje.classList.remove("text-danger");
        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
        return;
    }

    // Validar caracteres disponibles restantes
    if (caracteresDisponibles <= 5 && caracteresDisponibles > 0) {
        mensaje.textContent = `Te quedan ${caracteresDisponibles} caracteres.`;
        mensaje.classList.remove("text-danger");
    } else {
        mensaje.textContent = "";
        mensaje.classList.remove("text-danger");
    }

    // Validar cantidad mínima de caracteres
    if (valor.length >= 3) {
        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
    } else {
        input.classList.remove("is-valid");
        input.classList.remove("is-invalid");
    }
}

function validarPrecio(input, mensajeId) {
    const mensaje = document.getElementById(mensajeId);
    const valor = input.value.trim();

    // Hasta 8 cifras enteras, opcionalmente con punto o coma y hasta 2 decimales
    const regexPrecio = /^(?!0\d)\d{1,8}(?:[.,]\d{1,2})?$/;

    if (valor === "") {
        mensaje.textContent = "";
        input.classList.remove("is-invalid");
        input.classList.remove("is-valid");
        return;
    }

    if (regexPrecio.test(valor)) {
        mensaje.textContent = "";
        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
    } else {
        mensaje.textContent = "Ingrese un precio válido (hasta 8 cifras enteras y 2 decimales).";
        mensaje.classList.add("text-danger");
        input.classList.remove("is-valid");
        input.classList.add("is-invalid");
    }
}

function validarEmail(input, mensajeId) {
    const mensaje = document.getElementById(mensajeId);
    const valor = input.value.trim();
    const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (valor === "") {
        mensaje.textContent = "";
        input.classList.remove("is-valid", "is-invalid");
        return;
    }

    if (regexEmail.test(valor)) {
        mensaje.textContent = "";
        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
    } else {
        mensaje.textContent = "Ingrese un email válido.";
        mensaje.classList.add("text-danger");
        input.classList.add("is-invalid");
        input.classList.remove("is-valid");
    }
}
