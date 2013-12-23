// Helper function that formats the file sizes
function formatFileSize(bytes) {
    if (typeof bytes !== 'number') {
        return '';
    }

    if (bytes >= 1073741824) {
        return (bytes / 1073741824).toFixed(2) + ' GB';
    }

    if (bytes >= 1048576) {
        return (bytes / 1048576).toFixed(2) + ' MB';
    }

    return (bytes / 1024).toFixed(2) + ' KB';
}

var submitCount = 1, addCount = 1;

function AfterCancelUpload() {
    if ($('#fileListTBody').find('tr').length <= 0) {
        $('#desc').removeClass('hide');
        $('#filelistholder').addClass('hide');

        $('#btnName').text('Add files...');
        var addFiles = $('#btnAddFiles').detach();
        $('#footer-left').empty();

        $('#btnUploadAll').remove();
        $('#footer-right').prepend(addFiles);

        submitCount = 1;
        addCount = 1;
    }
}

//return type can be 'i' for image; 'p' for PDF //todo need to allow docx, excel as well
function IsImageFile(type) {
    var pattern = /^image\/(gif|jpeg|png)$/i;
    return (pattern.test(type));
}

function IsPdfFile(name) {
    var pattern = /.\.pdf$/i;
    return (pattern.test(name));
}

function IsAllowedFileTypes(file) {
    return '<span class="glyphicon glyphicon-picture fontSize16"></span>';
    //var imageIcon = '<span class="glyphicon glyphicon-picture fontSize16"></span>';
    //var pdfIcon = '<span class="glyphicon glyphicon-list-alt fontSize16"></span>';
    //var icon = '', type = file.type;
    //if (type.length > 0 && IsImageFile(type)) {
    //    icon = imageIcon;
    //} else if (IsPdfFile(file.name)) {
    //    icon = pdfIcon;
    //}
    //return icon;
}

//todo need to ass abort, see knob() for example
function ReceiptUpload() {
    $('#fileupload').fileupload({
        dataType: "text",
        dropZone: $('#dropzone'),
        url: "/receipts/UploadFiles",
        limitConcurrentUploads: 1,
        sequentialUploads: true,
        progressInterval: 5,
        maxChunkSize: 1048576000, //1GB //TODO limit to 20MB
        //loadImageMaxFileSize: 1073741824,
        //loadImageFileTypesk /^image\/(gif|jpeg|png)$/ 
        //formData: { receiptId: $('#ReceiptViewModel_Id').val(), desc: 'aa'},
        add: function (e, data) {
            var file = data.files[0];
            var icon = IsAllowedFileTypes(file);
            if (icon === '') return;
            $('#filelistholder').removeClass('hide');
            var descId = 'desc' + addCount++;

            var tpl = $('<tr><td style="width:5%;">' + icon + '</td>' +
                '<td style="width: 40%;"><input id="' + descId + '" class="form-control input-sm filename" placeholder="' + file.name + '" value="' + file.name + '"/>' + //use placeholder as the hidden field for watermark
                '<td style="width: 15%;"><span id="size" class="gray">' + formatFileSize(file.size) + '</span></td>' +
                '<td id="tdProgress" style="width: 35%;">' +
                    '<div class="progress"><div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%"><span class="sr-only">0% Complete</span></div></div>' +
                '</td><td style="width: 5%;">' +
                    '<a class="btnCancel" href="#"><span class="glyphicon glyphicon-remove-circle red fontSize16"></span></a>' +
                '</td></tr>');
            data.context = tpl.appendTo('#fileListTBody');

            if (!$('#desc').hasClass('hide')) {
                $('#desc').addClass('hide');
                $('#btnName').text('Add more files...');
                var addFiles = $('#btnAddFiles').detach();
                var btnUpload = '<button id="btnUploadAll" class="btn btn-success btn-sm" type="button"><i class="icon-upload icon-white"></i><span>Upload</span></button>';

                $('#footer-right').prepend(btnUpload);
                $('#footer-left').append(addFiles);

            }
            $('#overallbar').addClass("noTransition");
            $('#overallbar').css('width', 0);

            //use button to upload all
            $('#btnUploadAll').on('click', function () {
                $('#overallbar').removeClass("noTransition");
                data.submit();
            });

            $('.btnCancel').on('click', function () {
                var closetTr = $(this).closest('tr');
                closetTr.fadeOut(function () {  //todo need to test fadeout()
                    closetTr.remove();
                });
                AfterCancelUpload();
            });

            //for watermark effect of the upload file description
            $('.filename').focusin(function () {
                $(this).val('');
            });
            $('.filename').focusout(function () {
                if ($(this).val().length < 1) {
                    $(this).val($(this).prop('placeholder'));
                } else {
                    $(this).prop('placeholder', $(this).val());
                }
            });
        }
        , submit: function (e, data) {
            var id = '#desc' + submitCount++;
            if (!$(id).val()) { //dont submit if has been canceled
                e.preventDefault();
            }
            data.formData = { receiptId: $('#ReceiptViewModel_Id').val(), desc: $(id).val() };
        }
        , done: function (e, data) {
            $('#btnUploadAll').unbind('click');
            data.context.find('div.progress').addClass('fade');
            data.context.find('.btnCancel').replaceWith('<span class="glyphicon glyphicon-upload green fontSize16"></span>');

            data.context.find('input').addClass('fade');
            var input = data.context.find('input');
            var fileName = input.val();
            input.replaceWith('<span>' + fileName + '</span>');
        }
        , progressall: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $('#overallbar').css('width', progress + '%');
        }
        , progress: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            data.context.find('.progress-bar').css('width', progress + '%');
            data.context.find('.sr-only').text(progress + '% Complete');
        }
        , fail: function (e, data) {
            alert('fail: ' + data.files[0].name);
            alert('errorThrown:' + data.errorThrown
            + '\r\n' + 'status:' + data.textStatus
            + '\r\n' + 'jqXHR:' + data.jqXHR);
            // data.errorThrown
            // data.textStatus;
            // data.jqXHR;
        }
        , always: function (e, data) {
            //alert('always');
            //$('#globalProgressBar').addClass('fade');
        }
        , dragover: function (e) {
            e.preventDefault();
            var dropZone = $('#dropzone'),
            timeout = window.dropZoneTimeout;
            if (timeout) {
                clearTimeout(timeout);
            }
            var found = false,
                node = e.target;
            do {
                if (node === dropZone[0]) {
                    found = true;
                    break;
                }
                node = node.parentNode;
            } while (node != null);
            if (!found) {
                dropZone.addClass('in fade');
            }
        }
    });

    // Prevent the default action when a file is dropped on the window
    $(document)
        .on('drop dragover', function (e) {
            e.preventDefault();

            var timeout = window.dropZoneTimeout;
            if (timeout) {
                $('#bodyContent').addClass('hide');
                $('#dropzone').removeClass('hide');
                $('#dropzone').removeClass('in');
                clearTimeout(timeout);
            }

            window.dropZoneTimeout = setTimeout(function () {
                window.dropZoneTimeout = null;
                $('#bodyContent').removeClass('hide');
                $('#dropzone').addClass('hide');
            }, 100);
        });
}