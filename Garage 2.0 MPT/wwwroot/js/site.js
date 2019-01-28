$(document).ready(function () {

    $("#s_typ").click(function () {
        $("#i1").attr('src', '/img/arrow_down.png');    
    });
    $("#s_reg").click(function () {
        $("#i2").attr('src', '/img/arrow_down.png'); 
    });
    $("#s_col").click(function () {
        $("#i3").attr('src', '/img/arrow_down.png'); 
    });
    $("#s_mod").click(function () {
        $("#i4").attr('src', '/img/arrow_down.png'); 
    });
    $("#s_bra").click(function () {
        $("#i5").attr('src', '/img/arrow_down.png'); 
    });
    $("#seek_text01").click(function () {      
        if (window.location.href != 'https://localhost:44314/') {
            location.href = 'https://localhost:44314/';       
        }      
    });

    
  
});