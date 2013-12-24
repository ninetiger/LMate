﻿function docReady() {
    $("#ReceiptViewModel_PurchaseDate").datepicker({
        changeMonth: true,
        changeYear: true
    });

    $('#ReceiptViewModel_AccountTypeId').select2({
        placeholder: "Select a Type"
        , allowClear: true
    });

    $('#ReceiptViewModel_CurrencyId').select2({
        placeholder: "Select a Currency"
        , allowClear: true
    });

    $('#ReceiptViewModel_ReceiptCategoryId').select2({
        placeholder: "Select a Category"
    , allowClear: true
    });

    InitialViewFiles();
}

function InitialViewFiles() {
    var placeHolder = '<div id="viewFilesHolder" class="hidden"><div id="fileList"></div></div>';
    $('body').append(placeHolder);

    $('#btnViewFiles')
        .popover({
            placement: 'bottom'
            , html: true
            , content: $('#fileList')
            , trigger: 'manual'
            , container: 'body'
        })
        .click(function (e) {

            GetImages();

            e.stopPropagation();
        });

    $('html').on('click', function (e) {
        if ($(e.target).closest('.popover').length < 1) {
            $('#btnViewFiles').popover('hide');
        }
    });

    $(window).resize(function () {
        if ($('div.popover').hasClass('in')) {
            $('#btnViewFiles').popover('show');
        }
    });
}

function GetImages() {
    $.ajax({
        url: "/receipts/GetImageAddrsByReceiptId",
        dataType: "text",
        cache: false,
        data: {
            receiptId: $('#ReceiptViewModel_Id').val()
        },
        success: function (data) {
            ////for debug
            //var popoverContent = '<div id="viewFilesHolder" class="hidden"><div id="fileList"><table class="table-condensed table-hover"><tbody><tr><td style="width:10%"><img width="60" height="60" class="img-thumbnail"src="/Images/orderedList2.png" alt="ReceiptImage" /></td><td style="width:80%"><p style="vertical-align:middle">dasfafafafs.jpg</p></td><td style="width:10%">Edit<br />Cancel<br />Delete</td></tr></tbody></table></div></div>';
            var popoverContent = '<table class="table-condensed table-hover"><tbody>';
            var imageArray = data.split(";");
            for (var i = 0; i < imageArray.length - 1; i++) {
                var arr = imageArray[i].split(",");
                popoverContent += '<tr><td style="width:10%"><img width="60" height="60" class="img-rounded" src="/Receipts/GetImage?imageId=';
                popoverContent += arr[0] + '" alt="ReceiptImage" /></td><td style="width:80%"><p>' + arr[1] + '</p></td><td style="width:10%">Update<br />Delete</td></tr>';
            }
            popoverContent += '</tbody></table>';
            $('div#fileList').empty().append(popoverContent);
            $('#btnViewFiles').popover('toggle');
        }
        , fail: function () {
            alert("fail");
        }
    });
}