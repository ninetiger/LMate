function docReady() {
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

    //$('#btnViewFiles').click(function() {
    GetImages();
    GetViewFiles();
    //});
}

function GetViewFiles() {
    $('#btnViewFiles')
    .popover({
        placement: 'bottom'
        , html: true
        , content: $('#fileList')
        , trigger: 'manual'
        //, container: '#btnViewFiles'
        //, selector: true
    })
    .click(function (e) {
        e.stopPropagation();
        $('#btnViewFiles').popover('toggle');
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
        async: false,
        data: {
            receiptId: $('#ReceiptViewModel_Id').val()
        },
        success: function (data) {
            ////for debug
            //var popoverContent = '<div id="viewFilesHolder" class="hidden"><div id="fileList"><table class="table-condensed table-hover"><tbody><tr><td style="width:10%"><img width="60" height="60" class="img-thumbnail"src="/Images/orderedList2.png" alt="ReceiptImage" /></td><td style="width:80%"><p style="vertical-align:middle">dasfafafafs.jpg</p></td><td style="width:10%">Edit<br />Cancel<br />Delete</td></tr></tbody></table></div></div>';
            var popoverContent = '<div id="viewFilesHolder" class="hidden"><div id="fileList"><table class="table-condensed table-hover"><tbody>';
            var imageArray = data.split(";");
            for (var i = 0; i < imageArray.length - 1; i++) {
                var arr = imageArray[i].split(",");
                popoverContent += '<tr><td style="width:10%"><img width="60" height="60" class="img-rounded" src="/Receipts/GetImage?imageId=';
                popoverContent += arr[0] + '" alt="ReceiptImage" /></td><td style="width:80%"><p>' + arr[1] + '</p></td><td style="width:10%">Update<br />Delete</td></tr>';
            }
            popoverContent += '</tbody></table></div></div>';
            $('body').append(popoverContent);
        }
    });
}

//function GetImages() {
//    $.ajax({
//        url: "/receipts/GetImage",
//        dataType: "json",
//        cache: false,
//        data: {
//            receiptId: 15//$('#Receipts_Desc').val()
//        },
//        success: function (data) {
//            alert('in: ');
//            //var image = '<img width="100%" height="100%" class="img-rounded" src = "@imageFile" /  >';
//            //$('#receiptImages').html(image);
//            response($.map(data.list, function (item) {
//                alert('inn');
//                return {
//                    label: item,
//                    value: item
//                };
//            }));
//        }
//    });
//}