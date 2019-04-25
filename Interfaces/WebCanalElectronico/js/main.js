function entornoCargaFormulario(url) {

    var frame = document.getElementById('formulario');
    frame.src = url;

}

function entornoNuevaVentana() {

    var height = screen.availHeight - 30;
    var width = screen.availWidth - 10;
    var left = 0;
    var top = 0;
    settings = 'fullscreen=no,resizable=yes,location=no,toolbar=no,menubar=no';
    settings = settings + ',status=no,directories=no,scrollbars=yes';
    settings = settings + ',width=' + width + ',height=' + height;
    settings = settings + ',top=' + top + ',left=' + left;
    settings = settings + ',charset=iso-8859-1';
    var win = window.open('entorno.aspx', '', settings);
    win.resizeTo(screen.availWidth, screen.availHeight);
    if (!win.focus) {
        win.focus();
    }

    return win;
}

function alertaMostarError(mensaje) {
    var div = document.getElementById('barraEstado');
    div.innerHTML = "<div style=\"padding-top:20px\"><img src=\"../img/loadBarra.gif\"> <br /> <span style=\"color:#555\">" + mensaje + " </span></div>";
}

function abreLogin() {
    var height = screen.availHeight - 30;
    var width = screen.availWidth - 10;

    var left = 0;
    var top = 0;

    settings = 'fullscreen=no,resizable=yes,location=no,toolbar=no,menubar=no';
    settings = settings + ',status=no,directories=no,scrollbars=yes';
    settings = settings + ',width=' + width + ',height=' + height;
    settings = settings + ',top=' + top + ',left=' + left;
    settings = settings + ',charset=iso-8859-1';
    var win = window.open('ingreso.aspx', '', settings);

    win.outerHeight = screen.availHeight;
    win.outerWidth = screen.availWidth;

    win.resizeTo(screen.availWidth, screen.availHeight);

    if (!win.focus) {
        win.focus();
    }

    return win;
}

function mostrarAlerta(titulo, mensaje, tipo) {

    var vtipo;

    switch (tipo) {
        case "OK":
            vtipo = "success";
            break;
        case "ER":
            vtipo = "error";
            break;
        case "WR":
            vtipo = "warning";
            break;
        case "IN":
            vtipo = "info";
            break;
        default:
            vtipo = "info";
            break;
    }

    swal({
        title: titulo,
        text: mensaje,
        type: vtipo
    });

}

function mostrarPopup(titulo, mensaje) {
    var div = document.getElementById('popup');
    var popup = "";

    popup += "<div class=\"content-popup\">";
    popup += "<div class=\"close\"><a onclick=\"cerrarPopup();\"><img src=\"../images/iconos/cerrar.png\" width=\"16\" height=\"16\"/></a></div>";
    popup += "<div>";
    popup += "<h2>" + titulo + "</h2>";
    popup += "<p><textarea rows=\"15\" cols=\"60\" readonly=\"readonly\">" + mensaje + "</textarea></p>";
    popup += "</div>";
    popup += "</div>";

    div.innerHTML = popup;

    $('#popup').fadeIn('slow');
    $('.popup-overlay').fadeIn('slow');
    $('.popup-overlay').height($(window).height());
    return false;
}

function cerrarPopup() {
    $('#popup').fadeOut('slow');
    $('.popup-overlay').fadeOut('slow');
    return false;
}

function AbrirLnk(elemento) {
    document.getElementById(elemento).click();
}

//////////////// VALIDACIONES ////////////////

function IsNumeric(valor) {
    var log = valor.length; var sw = "S";
    for (x = 0; x < log; x++) {
        v1 = valor.substr(x, 1);
        v2 = parseInt(v1);
        //Compruebo si es un valor numérico 
        if (isNaN(v2)) { sw = "N"; }
    }
    if (sw == "S") { return true; } else { return false; }
}

var primerslap = false;
var segundoslap = false;

function formateafecha(fecha) {
    var long = fecha.length;
    var dia;
    var mes;
    var ano;

    if ((long >= 2) && (primerslap == false)) {
        dia = fecha.substr(0, 2);
        if ((IsNumeric(dia) == true) && (dia <= 31) && (dia != "00")) { fecha = fecha.substr(0, 2) + "/" + fecha.substr(3, 7); primerslap = true; }
        else { fecha = ""; primerslap = false; }
    }
    else {
        dia = fecha.substr(0, 1);
        if (IsNumeric(dia) == false)
        { fecha = ""; }
        if ((long <= 2) && (primerslap = true)) { fecha = fecha.substr(0, 1); primerslap = false; }
    }
    if ((long >= 5) && (segundoslap == false)) {
        mes = fecha.substr(3, 2);
        if ((IsNumeric(mes) == true) && (mes <= 12) && (mes != "00")) { fecha = fecha.substr(0, 5) + "/" + fecha.substr(6, 4); segundoslap = true; }
        else { fecha = fecha.substr(0, 3);; segundoslap = false; }
    }
    else { if ((long <= 5) && (segundoslap = true)) { fecha = fecha.substr(0, 4); segundoslap = false; } }
    if (long >= 7) {
        ano = fecha.substr(6, 4);
        if (IsNumeric(ano) == false) { fecha = fecha.substr(0, 6); }
        else { if (long == 10) { if ((ano == 0) || (ano < 1900) || (ano > 2999)) { fecha = fecha.substr(0, 6); } } }
    }
    if (long >= 10) {
        fecha = fecha.substr(0, 10);
        dia = fecha.substr(0, 2);
        mes = fecha.substr(3, 2);
        ano = fecha.substr(6, 4);
        // Año no viciesto y es febrero y el dia es mayor a 28 
        if ((ano % 4 != 0) && (mes == 02) && (dia > 28)) { fecha = fecha.substr(0, 2) + "/"; }
    }
    return (fecha);
}

function formateaComprobante(numero) {
    var long = numero.length;
    var est;
    var put;
    var num;

    if ((long >= 2) && (primerslap == false)) {
        est = numero.substr(0, 2);
        if (IsNumeric(est) == true) {
            numero = fecha.substr(0, 3) + "-" + numero.substr(4, 7);
            primerslap = true;
        }
        else {
            numero = "";
            primerslap = false;
        }
    }
    else {
        est = numero.substr(0, 1);
        if (IsNumeric(est) == false) {
            numero = "";
        }
        if ((long <= 3) && (primerslap = true)) {
            numero = numero.substr(0, 1);
            primerslap = false;
        }
    }

    return (numero);
}

function validaFecha(fecha) {
    var dtCh = "/";
    var minYear = 1900;
    var maxYear = 2100;
    function isInteger(s) {
        var i;
        for (i = 0; i < s.length; i++) {
            var c = s.charAt(i);
            if (((c < "0") || (c > "9"))) return false;
        }
        return true;
    }

    function stripCharsInBag(s, bag) {
        var i;
        var returnString = "";
        for (i = 0; i < s.length; i++) {
            var c = s.charAt(i);
            if (bag.indexOf(c) == -1) returnString += c;
        }
        return returnString;
    }

    function daysInFebruary(year) {
        return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
    }

    function DaysArray(n) {
        for (var i = 1; i <= n; i++) {
            this[i] = 31
            if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
            if (i == 2) { this[i] = 29 }
        }
        return this
    }

    function isDate(dtStr) {
        var daysInMonth = DaysArray(12)
        var pos1 = dtStr.indexOf(dtCh)
        var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
        var strDay = dtStr.substring(0, pos1)
        var strMonth = dtStr.substring(pos1 + 1, pos2)
        var strYear = dtStr.substring(pos2 + 1)
        strYr = strYear
        if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
        if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
        for (var i = 1; i <= 3; i++) {
            if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
        }
        month = parseInt(strMonth)
        day = parseInt(strDay)
        year = parseInt(strYr)
        if (pos1 == -1 || pos2 == -1) {
            return false
        }
        if (strMonth.length < 1 || month < 1 || month > 12) {
            return false
        }
        if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
            return false
        }
        if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
            return false
        }
        if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
            return false
        }
        return true
    }
    if (isDate(fecha)) {
        return true;
    } else {
        return false;
    }
}

function justNumbers(e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if (keynum == 8) return true;
    return /\d/.test(String.fromCharCode(keynum));
}

function justNumbersLetters(e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if (keynum == 8) return true;
    return /[\d-\s-A-Za-zñÑ]/.test(String.fromCharCode(keynum));
}

function convertirMayusculas(v) {
    v = v.replace(v, v.toUpperCase());
    return v
}

function formateaNumerico(v) {
    v = v.replace(/\D/g, "");
    return v
}

function nombre_mes(mes) {
    var respuesta = "";
    if (mes == 1) {
        respuesta = "enero";
    }
    else if (mes == 2) {
        respuesta = "febrero";
    }
    else if (mes == 3) {
        respuesta = "marzo";
    }
    else if (mes == 4) {
        respuesta = "abril";
    }
    else if (mes == 5) {
        respuesta = "mayo";
    }
    else if (mes == 6) {
        respuesta = "junio";
    }
    else if (mes == 7) {
        respuesta = "julio";
    }
    else if (mes == 8) {
        respuesta = "agosto";
    }
    else if (mes == 9) {
        respuesta = "septiembre";
    }
    else if (mes == 10) {
        respuesta = "octubre";
    }
    else if (mes == 11) {
        respuesta = "noviembre";
    }
    else if (mes == 12) {
        respuesta = "diciembre";
    }
    else {
        respuesta = "indefinido";
    }

    return respuesta;
}

function validar_cedula(cedula) {
    array = cedula.split("");
    num = array.length;
    if (num == 10) {
        total = 0;
        digito = (array[9] * 1);
        for (i = 0; i < (num - 1) ; i++) {
            mult = 0;
            if ((i % 2) != 0) {
                total = total + (array[i] * 1);
            }
            else {
                mult = array[i] * 2;
                if (mult > 9)
                    total = total + (mult - 9);
                else
                    total = total + mult;
            }
        }
        decena = total / 10;
        decena = Math.floor(decena);
        decena = (decena + 1) * 10;
        final = (decena - total);
        if ((final == 10 && digito == 0) || (final == digito)) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}

function formateavalor(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{2})$/, ".$1");
    v = v.replace(/(\d+)(\d{3},\d{2})$/g, "$1.$2");
    var qtdLoop = (v.length - 3) / 3; var count = 0;
    while (qtdLoop > count) {
        count++;
        v = v.replace(/(\d+)(\d{3}.*)/, "$1.$2");
    } v = v.replace(/^(0)(\d)/g, "$2");
    return v
}

function validarEmail(email) {
    var respuesta;
    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (!expr.test(email)) {
        respuesta = true;
    }
    else {
        respuesta = false;
    }

    return respuesta;
}

$()