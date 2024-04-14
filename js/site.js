$(document).ready(function () {

    $('.menu_bar').click(function () {
        $('#nav').toggle("slow");
    });


    $(window).resize(function () {
        var w = $(window).width();
        //console.log(w);
        if (w > 1157) {
            $('#nav').removeAttr('style');
            $("div.nav-container > ul#nav > li:first").addClass("first");
            $("div.nav-container > ul#nav > li:last").addClass("last");
        } else {
            $("div.nav-container > ul#nav > li:first").removeClass("first");
            $("div.nav-container > ul#nav > li:last").removeClass("last");
        }

    });

});

function validaLongitud(idTxt, longitud) {
    $("#" + idTxt).keyup(function () {
        if ((this.value).length > longitud) {
            this.value = (this.value).substring(0, longitud);
        }
    });
}
