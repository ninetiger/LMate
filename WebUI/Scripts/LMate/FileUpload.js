function fileUploadInit() {
    $('#fileupload').fileupload({
        dataType: 'text',
        url: "/FileUpload/UploadFiles",
        //limitConcurrentUploads: 1,
        //sequentialUploads: true,
        //progressInterval: 100,
        //maxChunkSize: 10000000,
        //done: function (e, data) {
        //    alert('in');
        //    //$.each(data.result.files, function (index, file) {
        //    //    $('<p/>').text(file.name).appendTo(document.body);
        //    //});
        //}
    });

    // Load existing files by an initial ajax request to the server after page loads up
    // This is done by a simple jQuery ajax call, not by the FIle Upload plugin.,
    // but the results are passed to the plugin with the help of the context parameter: 
    // context: $('#fileupload')[0] and the $(this)... call in the done handler. 
    // With ajax.context you can pass a JQuery object to the event handler and use "this".
    $('#fileupload').addClass('fileupload-processing');
    $.ajax({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: "/FileUpload/UploadFiles",
        dataType: 'json',
        context: $('#fileupload')[0]
    }).always(function () {
        $(this).removeClass('fileupload-processing');
    }).done(function (result) {
        $(this).fileupload('option', 'done')
            .call(this, null, { result: result });
    });

    //$('#fileupload').fileupload({
    //    dataType: "json",
    //    //url: "/api/upload",
    //    url: "/FileUpload/UploadFiles",
    //    limitConcurrentUploads: 1,
    //    sequentialUploads: true,
    //    progressInterval: 100,
    //    maxChunkSize: 10000000,
    //    add: function (e, data) {
    //        $('#filelistholder').removeClass('hide');
    //        data.context = $('<div />').text(data.files[0].name).appendTo('#filelistholder');
    //        $('</div><div class="progress"><div class="bar" style="width:0%"></div></div>').appendTo(data.context);
    //        $('#btnUploadAll').click(function () {
    //            $('#overallbarWrap').removeClass('fade');
    //            data.submit();
    //        });
    //    }
    //    , done: function (e, data) {
    //        alert('in');

    //        data.context.text(data.files[0].name + '... Completed');
    //        $('</div><div class="progress"><div class="bar" style="width:100%"></div></div>').appendTo(data.context);
    //        $('#overallbarWrap').addClass('fade').hide();
    //    }
    //    , progressall: function (e, data) {
    //        alert('i1');
    //        var progress = parseInt(data.loaded / data.total * 100, 10);
    //        $('#overallbar').css('width', progress + '%');
    //    }
    //    , progress: function (e, data) {
    //        var progress = parseInt(data.loaded / data.total * 100, 10);
    //        data.context.find('.bar').css('width', progress + '%');
    //    }
    //});
}

function fileUploadInit1() {
    $(function () {
        $('#fileupload').fileupload({
            dataType: "text",
            //url: "/api/upload",
            url: "/FileUpload/UploadFiles",
            limitConcurrentUploads: 1,
            //sequentialUploads: true,
            progressInterval: 5,
            maxChunkSize: 1073741824,
            add: function (e, data) {
                $('#filelistholder').removeClass('hide');
                var tpl = $('<tr><td style="width:5%;vertical-align: middle"><span class="glyphicon glyphicon-picture fontSize16"></span></td>' +
                    '<td style="width: 40%; vertical-align: middle"><input class="form-control input-sm" id="name" value="002.jpg" /></td>' +
                    '<td style="width: 15%; vertical-align: middle"><span id="size" class="gray">300kb</span></td>' +
                    ' <td style="width: 40%; text-align: right"><div class="progress"><div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%"><span class="sr-only">0% Complete</span></div></div></td></tr>');
                //var tpl = $('<tr><td><span class="glyphicon glyphicon-picture fontSize16"></span></td>' +
                //    '<td><span id="name"></span> - <span id="size" class="gray"></span></td>' +
                //    '<td><div class="progress"><div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%"><span class="sr-only">0% Complete</span></div></div></td></tr>');
                //tpl.find('#name').text(data.files[0].name);
                tpl.find('#name').val(data.files[0].name);
                tpl.find('#size').text(formatFileSize(data.files[0].size));
                data.context = tpl.appendTo('#fileListTBody');
                //use button to upload all
                $('#btnUploadAll').click(function () {
                    data.submit();
                });
            }
            , done: function (e, data) {
                $('#btnUploadAll').unbind('click');
                data.context.find('div.progress').addClass('fade');
                //data.context.find('#size').append('<span class="glyphicon glyphicon-ok-sign green fontSize16"></span>');
                data.context.find('div.progress').replaceWith('<span class="glyphicon glyphicon-ok-sign green fontSize16"></span>');

                data.context.find('input').addClass('fade');
                data.context.find('input').replaceWith('<span>' + data.files[0].name + '</span>');

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
            }
            , always: function (e, data) {
                //alert('always');
                //$('#globalProgressBar').addClass('fade');
            }
        });

        //$('#fileupload').click(function (e, data) {
        //    var uploadAll = '<button id="btnUploadAll" class="btn btn-success btn-sm" type="button"><i class="icon-upload icon-white"></i><span>Upload</span></button>';

        //    $('#btnName').text('Add more files...');
        //    var btnAddMoreFiles = $.find('#btnAddFiles');


        //    $('#btnAddFiles').replaceWith(uploadAll);
        //    $('#btnUploadAll').replaceWith(btnAddMoreFiles);
        //});
    });
}

function knob() {
    var ul = $('#upload ul');

    $('#drop a').click(function () {
        // Simulate a click on the file input button
        // to show the file browser dialog
        $(this).parent().find('input').click();
    });

    // Initialize the jQuery File Upload plugin
    $('#upload').fileupload({
        url: "/FileUpload/UploadFiles",
        // This element will accept file drag/drop uploading
        dropZone: $('#drop'),

        // This function is called when a file is added to the queue;
        // either via the browse button, or via drag/drop:
        add: function (e, data) {
            var tpl = $('<li class="working"><input type="text" value="0" data-width="48" data-height="48"' +
                ' data-fgColor="#0788a5" data-readOnly="1" data-bgColor="#3e4043" /><p></p><span></span></li>');

            // Append the file name and file size
            tpl.find('p').text(data.files[0].name)
                .append('<i>' + formatFileSize(data.files[0].size) + '</i>');

            // Add the HTML to the UL element
            data.context = tpl.appendTo(ul);

            // Initialize the knob plugin
            tpl.find('input').knob();

            // Listen for clicks on the cancel icon
            tpl.find('span').click(function () {

                if (tpl.hasClass('working')) {
                    jqXHR.abort();
                }

                tpl.fadeOut(function () {
                    tpl.remove();
                });

            });

            // Automatically upload the file once it is added to the queue
            var jqXHR = data.submit();
        },
        progress: function (e, data) {
            // Calculate the completion percentage of the upload
            var progress = parseInt(data.loaded / data.total * 100, 10);

            // Update the hidden input field and trigger a change
            // so that the jQuery knob plugin knows to update the dial
            data.context.find('input').val(progress).change();

            if (progress == 100) {
                data.context.removeClass('working');
            }
        },
        fail: function (e, data) {
            // Something has gone wrong!
            data.context.addClass('error');
        }
    });

    // Prevent the default action when a file is dropped on the window
    $(document).on('drop dragover', function (e) {
        e.preventDefault();
    });


}

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

var submitCount = 1;
var addCount = 1;
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
            $('#filelistholder').removeClass('hide');
            var tpl = $('<tr><td style="width:5%;vertical-align: middle"><span class="glyphicon glyphicon-picture fontSize16"></span></td>' +
                '<td style="width: 40%; vertical-align: middle"><input class="form-control input-sm filename" />' +
                '<td style="width: 15%; vertical-align: middle"><span id="size" class="gray">300kb</span></td>' +
                ' <td style="width: 40%; text-align: right"><div class="progress"><div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%"><span class="sr-only">0% Complete</span></div></div></td></tr>');
            //var tpl = $('<tr><td><span class="glyphicon glyphicon-picture fontSize16"></span></td>' +
            //    '<td><span id="name"></span> - <span id="size" class="gray"></span></td>' +
            //    '<td><div class="progress"><div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%"><span class="sr-only">0% Complete</span></div></div></td></tr>');
            //tpl.find('#name').text(data.files[0].name);
            tpl.find('input.filename').first().val(data.files[0].name);
            tpl.find('#size').text(formatFileSize(data.files[0].size));
            //var descId = 'desc' + ($('#filelistholder').find('tr').length + 1);
            var descId = 'desc' + addCount++;
            tpl.find('input.filename').prop('id', descId);
            //$('<input type="hidden" id="' + data.files[0].name + data.files[0].size + '" value="' + descId + '" />').insertAfter(tpl.find('input.filename'));
            data.context = tpl.appendTo('#fileListTBody');
       

            if (!$('#desc').hasClass('hide')) {
                $('#desc').addClass('hide');
                $('#btnName').text('Add more files...');
                var addFiles = $('#btnAddFiles').detach();
                var btnUpload = '<button id="btnUploadAll" class="btn btn-success btn-sm" type="button"><i class="icon-upload icon-white"></i><span>Upload</span></button>';

                $('#footer-right').prepend(btnUpload);
                $('#footer-left').append(addFiles);
                addFiles = null;
            }

            //use button to upload all
            $('#btnUploadAll').on('click', function () {
                data.submit();
            });
        }
        , submit: function (e, data) {
            var id = '#desc' + submitCount++;
            data.formData = { receiptId: $('#ReceiptViewModel_Id').val(), desc: $(id).val() };
        }
        , done: function (e, data) {
            $('#btnUploadAll').unbind('click');
            data.context.find('div.progress').addClass('fade');
            //data.context.find('#size').append('<span class="glyphicon glyphicon-ok-sign green fontSize16"></span>');
            data.context.find('div.progress').replaceWith('<span class="glyphicon glyphicon-ok-sign green fontSize16"></span>');

            data.context.find('input').addClass('fade');
            data.context.find('input').replaceWith('<span>' + data.files[0].name + '</span>');

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
        , drop: function (e, data) {
            $('#dropzone').addClass('active');
        }
        , dragover: function (e) {
            $('#btnName7').text('in dragov:' + ++cc);
            e.preventDefault();
            var dropZone = $('#dropzone'),
            timeout = window.dropZoneTimeout;
            if (!timeout) {
                $('#btnName').text('aaaaaaaaaa:');
                dropZone.addClass('in');
            } else {
                $('#btnName').text('bbbbbbbbbbbb:');
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
            if (found) {
                $('#btnName').text('cccccccccccccc:');
                dropZone.removeClass('hover');
            } else {
                $('#btnName').text('dddddddddddddddd:');
                dropZone.addClass('in');
            }
            window.dropZoneTimeout = setTimeout(function () {
                window.dropZoneTimeout = null;
                $('#btnName').text('eeeeeeeeeeeeeee:');
                dropZone.removeClass('in hover');
            }, 100);
        }
    });

    // Prevent the default action when a file is dropped on the window
    $(document)
        .on('drop dragover', function (e) {
                $('#btnName1').text('in:' + ++a);
            modelEnter = true;
            e.preventDefault();

            var dropZone = $(this), timeout = window.dropZoneTimeout;
            if (!timeout) {
                $('#btnName').text('ffffffffffff:');
            } else {
                $('#bodyContent').addClass('hide');
                $('#dropzone').removeClass('hide');
                $('#dropzone').removeClass('in');
                $('#btnName').text('ggggggggggggg:');
                clearTimeout(timeout);
            }

            window.dropZoneTimeout = setTimeout(function () {
                window.dropZoneTimeout = null;
                $('#btnName').text('hhhhhhhhhhhh:');
                $('#bodyContent').removeClass('hide');
                $('#dropzone').addClass('hide');
            }, 100);
        })
        .on('drop', function () {
            if ($('#dropzone').hasClass('active')) {
                $('#desc').addClass('hide');
            }
            $('#bodyContent').removeClass('hide');
            $('#dropzone').addClass('hide');
        })

        .on('dragleave', function (e) {
            $('#btnName2').text('doc leave:' + ++b);

            if (modelEnter == true) return;
            //$('#bodyContent').removeClass('hide');
            //$('#dropzone').addClass('hide');
        })
    ;

    $('#fileUploadModal')
        .on('dragleave', function (e) {
            $('#btnName3').text('model leave:' + ++c);

            e.stopPropagation();
            e.preventDefault();
            modelEnter = false;
        })
        .on('dragenter', function (e) {
            $('#btnName4').text('model enter:' + ++d);
            //$('#bodyContent').addClass('hide');
            //$('#dropzone').removeClass('hide');
            modelEnter = true;
        });
    ;
    $('.modal-dialog')
        .on('dragleave', function (e) {
            $('#btnName5').text('dialog leave:' + ++aa);
            e.stopPropagation();
            e.preventDefault();
        })
        .on('dragenter', function (e) {
            $('#btnName6').text('dialog enter:' + ++bb);
            e.stopPropagation();
            e.preventDefault();
        });
    ;

    $('.modal-content')
  .on('dragleave', function (e) {
      e.stopPropagation();
      e.preventDefault();

  })
            .on('dragenter', function (e) {
                e.stopPropagation();
                e.preventDefault();
            });
    ;

}

var a = 0, b = 0, c = 0, d = 0, aa=0, bb=0, cc=0, ee=0;
var modelEnter = false, pageEnter = false;