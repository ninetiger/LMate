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
            $('#btnViewFiles').prop('disabled', true).text('Loading...'); //todo need image
            GetImagesForPopover();
            InitDraggableViewer();
            InitImageViewer();
            e.stopPropagation();
        });

    $('html').on('click', function (e) {
        if ($(e.target).closest('.popover').length < 1
            && $(e.target).closest('div#viewDragger').length < 1) {
            $('#btnViewFiles').popover('hide');
        }
    });

    $(window).resize(function () {
        if ($('div.popover').hasClass('in')) {
            $('#btnViewFiles').popover('show');
            $('div.popover').css('left', (parseFloat($('div.popover').css('left')) + 80));
        }
    });
}

function InitDraggableViewer() {
    var offsetBottom = 75;
    var sidesMargin = 11;
    $("div#viewDragger").draggable({
        cancel: 'div#viewerBody',
        cursor: 'move',
        create: function () {
            $(this).css({ 'top': -690, 'left': 25 });
            $("div#viewerBody").css({'height': '-='+offsetBottom+'px', 'width' : '-='+sidesMargin +'px'});
        }
    }).css('z-index', 1500).resizable({
        minHeight: 250
        , minWidth: 300
        , resize: function (event, ui) {
            $("div#viewerBody")
                .height(ui.size.height - offsetBottom)
                .width(ui.size.width-sidesMargin);
        }
    });

    $('div#viewDragger div#viewerHeader button').click(function () {
        $('div#viewDragger').addClass('hidden');
    });
}

function GetImagesForPopover() {
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
            $('div.popover').css('left', (parseFloat($('div.popover').css('left')) + 80));

            // popover row click event
            $('div#fileList table tbody tr').click(function () {
                $("div#viewerBody").iviewer('loadImage', $(this).find('img').prop('src'));
                $('div#viewDragger').removeClass('hidden');
            });

        }
        , fail: function () {
            alert("fail");
        }
        , complete: function () {
            $('#btnViewFiles').text('View Files').prop('disabled', false);
        }
    });
}

function InitImageViewer() {
    var iv1 = $("div#viewerBody").iviewer({
        //src: "/images/testimage.jpg",
        update_on_resize: true,
        zoom_animation: false,
        mousewheel: false,
        onMouseMove: function (ev, coords) { },
        onStartDrag: function (ev, coords) { }, //this image will not be dragged
        onDrag: function (ev, coords) { },
        //onFinishLoad: function (ev, src) { $(this).iviewer('center'); }
    });

    //$("#in").click(function () { iv1.iviewer('zoom_by', 1); });
    //$("#out").click(function () { iv1.iviewer('zoom_by', -1); });
    //$("#fit").click(function () { iv1.iviewer('fit'); });
    //$("#orig").click(function () { iv1.iviewer('set_zoom', 100); });
    //$("#update").click(function () { iv1.iviewer('update_container_info'); });
}

//var dragger = '<div id="viewDragger" class="ui-widget-content"><div id="viewerHeader"><h5 id="title"class="pull-left">File Viewer</h5><button class="pull-right"><b>X</b></button><div class="clearfix"></div><hr /></div><div id="viewerBody"><div id="imageViewer" class="viewer"></div><input id="imageSrc" type="hidden" value=""/></div><div id="viewerFooter"><hr /></div></div>';