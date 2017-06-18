
$(document).ready(function(){
    $(window).scrollTop(0);
});



$(document).ready(function(){
    $(window).scrollTop(0);
});




function input_clear_comment() {
    $("#article form[action*=AddComment] textarea").val('');
}



$('#search input').on('keyup', function () {
    $("#SearchString").submit();
});


$('#target').click(function () {
    var text = $("#SearchString").val();
    $("#SearchString").val("");
    $("#WRAPPERDIVID").empty();
    $.ajax({
        type: "POST",
        url: "/Search/FullSearch",
        data: {title:text},
        success: function (response) {
            $("#FullSearchDiv").html(response);
        },
        dataType: "html",
            
    });
});


$('#imageFile').on("change", function () {
    $("#form1").submit();
});



setTimeout(fade_out, 2500);

function fade_out() {
    $("#SuccessMessage").fadeOut().empty();
}