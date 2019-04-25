function noData(capa, texto) {
    var div = document.getElementById(capa);
    div.innerHTML = "<div class=\"titulo2\">" + texto + "</div>";
}

function cargando_small(capa) {
    var div = document.getElementById(capa);
    div.innerHTML = "<div><img src=\"../img/loading.gif\"></div>";
}

function cargando(capa, texto) {
    var div = document.getElementById(capa);
    div.innerHTML = "<div style=\"padding-top:20px\"><img src=\"../img/loadBarra.gif\"> <br /> <span style=\"color:#555\">" + texto + " </span></div>";
}

function mostrarCorrecto(texto) {
    alertify.success(texto);
    return false;
}

function mostrarError(texto) {
    alertify.error(texto);
    return false;
}

function mostrarNormal(texto) {
    alertify.log(texto);
    return false;
}

function alerta(texto) {
    var mensaje = "";
    mensaje = "<table><tr><td style=\"padding-right:5px\"><img src=\"../img/notificaciones/alerta.png\"></td><td style=\"color:#FFF\">" + texto + "</td></tr></table>";
    return mensaje;
}

function correcto(texto) {
    var mensaje = "";
    mensaje = "<table><tr><td style=\"padding-right:5px\"><img src=\"../img/notificaciones/correcto.png\"></td><td style=\"color:#FFF\">" + texto + "</td></tr></table>";
    return mensaje;
}

function incorrecto(texto) {
    var mensaje = "";
    mensaje = "<table><tr><td style=\"padding-right:5px\"><img src=\"../img/notificaciones/incorrecto.png\"></td><td style=\"color:#FFF\">" + texto + "</td></tr></table>";
    return mensaje;
}

function mostrar_alerta(valor) {
    var timer;
    $('#mensaje').slideDown('slow', function () {
        if ($('#mensaje').is(":hidden")) {
            $('#mensaje').slideUp("slow");
        }
    });
    var texto_mensaje = document.getElementById('texto_mensaje');
    texto_mensaje.innerHTML = alerta(valor);
    var cerrar = document.getElementById('cerrar_pie');
    cerrar.style.display = '';
}

function mostrar_correcto(valor) {
    var timer;
    $('#mensaje').slideDown('slow', function () {
        if ($('#mensaje').is(":hidden")) {
            $('#mensaje').slideUp("slow");
        }
    });
    var texto_mensaje = document.getElementById('texto_mensaje');
    texto_mensaje.innerHTML = correcto(valor);
    var cerrar = document.getElementById('cerrar_pie');
    cerrar.style.display = '';
}

function mostrar_incorrecto(valor) {
    var timer;
    $('#mensaje').slideDown('slow', function () {
        if ($('#mensaje').is(":hidden")) {
            $('#mensaje').slideUp("slow");
        }
    });
    var texto_mensaje = document.getElementById('texto_mensaje');
    texto_mensaje.innerHTML = incorrecto(valor);
    var cerrar = document.getElementById('cerrar_pie');
    cerrar.style.display = '';
}

function cerrar_mensaje() {
    $('#mensaje').slideDown('slow', function () {
        if ($('#mensaje').is(":visible")) {
            $('#mensaje').slideUp("slow");
        }
    });
}

function mostrarLoad(capa) {
    var div = "#" + capa;

    $(div).slideDown('slow', function () {
        if ($(div).is(":visible")) {
            $(div).slideUp("slow");
        }
    });
}

function mostarAlertaX(texto) {
    var div = document.getElementById('mensaje');
    var tabla = texto;
    div.innerHTML = tabla;
}