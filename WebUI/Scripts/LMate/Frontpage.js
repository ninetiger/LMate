function docReady() {
    $('#navigation li').removeClass('active');
    $('#navigation li a[href="' + window.location.pathname + '"]').parent().addClass('active');

    //disable the button during register process
    $('input#register').click(function () {
        var isValid = $('form#register').valid();
        if (isValid) {
            setButtonLoading($('input#register'));
        }
    });
}

function setButtonLoading(obj) {
    var w = obj.css('width'); //when text is set to empty, have to set w and h
    var h = obj.css('height');
    obj.prop('disabled', true).addClass('loading20').css({ 'width': w, 'height': h });
}