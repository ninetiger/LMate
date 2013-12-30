function docReady() {
    $("#ReceiptViewModel_PurchaseDate").datepicker({
        changeMonth: true
        , changeYear: true
        , dateFormat: 'd M yy'
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

    AutoCompleteVendor();

    InitialViewFiles();

    //DraggablePannel(); //todo future feature
}

function DraggablePannel() {
    $("div#panel").draggable({
        cancel: 'div.panel-body',
        cursor: 'move',
        create: function () { }
    }).css('z-index', 1500).resizable({
        minHeight: 350
    , minWidth: 250
    });
}

function InitialViewFiles() {
    var placeHolder = '<div id="viewFilesHolder" class="hidden"><div id="fileList"></div></div>';
    $('body').append(placeHolder);
    $('button#btnViewFiles')
        .popover({
            placement: 'bottom'
            , html: true
            , content: $('#fileList')
            , trigger: 'manual'
            , container: 'body'
        })
        .click(function (e) {
            if ($('div.popover').hasClass('in')) {
                $('button#btnViewFiles').popover('hide');

            } else {
                setButtonLoading(true);
                GetImagesForPopover();
                InitDraggableViewer();
                InitImageViewer();
            }
            e.stopPropagation();
        });

    $('html').on('click', function (e) {
        if ($('div.popover').hasClass('in')) { //hide the popover if clicked elsewhere
            if ($(e.target).closest('.popover').length < 1
                && $(e.target).closest('div#viewDragger').length < 1) {
                $('button#btnViewFiles').popover('hide');
            }
        }

        //hide the vendor autocomplete
        if ($('ul#ui-id-1').css('display') != 'none') {
            $('ul#ui-id-1').css('display', 'none');
        }
    });

    $(window).resize(function () {
        if ($('div.popover').hasClass('in')) {
            $('button#btnViewFiles').popover('show');
            $('div.popover').css('left', (parseFloat($('div.popover').css('left')) + 80));
        }
    });
}

function InitDraggableViewer() {
    var offsetBottom = 85;
    var sidesMargin = 11;
    $("div#viewDragger").draggable({
        cancel: 'div#viewerBody',
        cursor: 'move',
        create: function () {
            var w = $(this).width(), h = $(this).height();

            //set pop up the the center of the screen
            var left = ($(window).width() - w) / 2, top = ($(window).height() - h) / 2;
            $(this).css({ 'position': 'fixed', 'left': left + 'px', 'top': top + 'px' });

            $("div#viewerBody").css({ 'height': h - offsetBottom, 'width': w - sidesMargin });
        }
    }).css('z-index', 1500).resizable({
        minHeight: 350
        , minWidth: 250
        , resize: function (event, ui) {
            $("div#viewerBody")
                .height(ui.size.height - offsetBottom)
                .width(ui.size.width - sidesMargin);
        }
    });

    $('div#viewerFooter button#prev').click(function () {
        navigateFiles(false);
    });

    $('div#viewerFooter button#next').click(function () {
        navigateFiles(true);
    });

    $('div#viewDragger div#viewerHeader button').click(function () {
        $('div#viewDragger').addClass('hidden');
    });
}

//used for image navigation
var popoverImageArray = [];

function getCurrentImageIndex() {
    var currentImg = $('div#viewerBody img').prop('src');
    var currentImageId = currentImg.substr(currentImg.lastIndexOf('=') + 1);
    var index = -1;

    for (var i = 0; i < popoverImageArray.length; i++) {
        if (popoverImageArray[i][0] == currentImageId) {
            index = i;
            break;
        }
    }
    return index;
}

function navigateFiles(isNext) {
    var index = getCurrentImageIndex();
    if (index == -1) return;

    var arrLen = popoverImageArray.length;
    var next = index;

    if (isNext) {
        next += 1;
        if (next >= arrLen) {
            next = 0;
        }
    } else {
        next -= 1;
        if (next < 0) {
            next = arrLen -1;
        }
    }

    $("div#viewerBody").iviewer('loadImage', '/Receipts/GetImage?imageId=' + popoverImageArray[next][0]);
    $('div#viewerFooter label#fileDesc').text(popoverImageArray[next][1]);
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
            $('button#btnViewFiles').popover('show');
            popoverImageArray = [];

            ////for debug
            //var popoverContent = '<div id="viewFilesHolder" class="hidden"><div id="fileList"><table class="table-condensed table-hover"><tbody><tr><td style="width:10%"><img width="60" height="60" class="img-thumbnail"src="/Images/orderedList2.png" alt="ReceiptImage" /></td><td style="width:80%"><p style="vertical-align:middle">dasfafafafs.jpg</p></td><td style="width:10%">Edit<br />Cancel<br />Delete</td></tr></tbody></table></div></div>';
            var popoverContent = '<table class="table-condensed table-hover"><tbody>';
            var imageArray = data.split(";");  //todo need change to json, as ; & , in desc break this code
            for (var i = 0; i < imageArray.length - 1; i++) {
                var arr = imageArray[i].split(",");
                popoverImageArray.push(arr);
                popoverContent += '<tr><td style="width:10%"><img width="60" height="60" class="img-rounded" src="/Receipts/GetImage?imageId=';
                popoverContent += arr[0] + '" alt="ReceiptImage" /></td><td style="width:80%"><p>'
                    + arr[1] + '</p></td><td style="width:10%"><a class="" id="' + arr[0] + '" href="#">Update</a><br /><a class="imageDel" id="' + arr[0] + '" href="#">Delete</a></td></tr>';
                //todo alt should set to desc, but need to test for invalid chars not break html
            }
            popoverContent += '</tbody></table>';
            $('div#fileList').empty().append(popoverContent);

            $('div.popover').css('left', '+=80px');

            //update event
            $('div#fileList table tbody tr td a.imageUpdate').click(function (e) {
                alert('update clicked');
                var newDesc = 'new desc';

                var index = getCurrentImageIndex();
                popoverImageArray[index][0] = newDesc;
                $('div#viewerFooter label#fileDesc').text(newDesc);
                e.stopPropagation();
            });

            //delete event
            $('div#fileList table tbody tr td a.imageDel').click(function (e) {
                deleteImage($(this));

                var index = getCurrentImageIndex();
                navigateFiles(true);
                popoverImageArray.splice(index, 1);
                e.stopPropagation();
            });

            // popover row click event
            $('div#fileList table tbody tr').click(function () {
                $("div#viewerBody").iviewer('loadImage', $(this).find('img').prop('src'));
                $('div#viewerFooter label#fileDesc').text($(this).find('p').text());
                $('div#viewDragger').removeClass('hidden');
            });
        }
        , fail: function () {
            alert("fail");
        }
        , complete: function () {
            setButtonLoading(false);
        }
    });
}

function deleteImage(obj) {
    DetachAnImage(obj.prop('id'));

    var popUp = $('div#viewDragger');
    if (popUp != null && !popUp.hasClass('hidden')) {
        navigateFiles(true);
    }

    obj.closest('tr').remove();
}

function setButtonLoading(isLoading) {
    if (isLoading) {
        var w = $('button#btnViewFiles').css('width'); //when text is set to empty, have to set w and h
        var h = $('button#btnViewFiles').css('height');
        $('button#btnViewFiles').prop('disabled', true).addClass('loading20').css({ 'width': w, 'height': h }).text('');
    } else {
        $('button#btnViewFiles').removeClass('loading20').text('View Files').prop('disabled', false);
    }
}

function InitImageViewer() {
    var iv1 = $("div#viewerBody").iviewer({
        //src: "/images/testimage.jpg",
        update_on_resize: true,
        zoom_animation: false,
        mousewheel: false,
        onMouseMove: function (ev, coords) { },
        onStartDrag: function (ev, coords) { }, //this image will not be dragged if set: return false;
        onDrag: function (ev, coords) { }
        //onFinishLoad: function (ev, src) { $(this).iviewer('center'); }
    });

    //$("#in").click(function () { iv1.iviewer('zoom_by', 1); });
    //$("#out").click(function () { iv1.iviewer('zoom_by', -1); });
    //$("#fit").click(function () { iv1.iviewer('fit'); });
    //$("#orig").click(function () { iv1.iviewer('set_zoom', 100); });
    //$("#update").click(function () { iv1.iviewer('update_container_info'); });
}

function DetachAnImage(imgId) {
    $.ajax({
        url: "/receipts/DetachAnImage",
        data: {
            receiptId: $('#ReceiptViewModel_Id').val(),
            imageId: imgId
        }
        , fail: function () {
            alert("fail");
        }
        , error: function () {
            alert("an error has happened");
        }
    });
}

function AutoCompleteVendor() {
    var auto = $('#ReceiptViewModel_VendorName').autocomplete({
        source: function (request, response) {
            $('input#ReceiptViewModel_VendorName').addClass('loading20 vendorLoading');
            $.ajax({
                url: "/receipts/AutoCompleteVendor",
                cache: false,
                dataType: "json",
                data: {
                    searchString: $('#ReceiptViewModel_VendorName').val()
                },
                success: function (data) {
                    response($.map(data.list, function (item) {
                        return {
                            label: item[0],
                            value: item[1]
                        };
                    }));
                }
                , fail: function () {
                    alert("fail");
                }
                , complete: function () {
                    $('input#ReceiptViewModel_VendorName').removeClass('loading20 vendorLoading');
                }
            });
        }
        , select: function (event, ui) {
            $('input#ReceiptViewModel_VendorName').val(ui.item.label);
            return false;
        }
        , open: function (event, ui) {
            var width = $('ul#ui-id-1 li').width() - 20;
            $.each($('ul#ui-id-1 li a.pull-left'), function (ind, val) {
                $(val).css('width', width);
            });

            $('a.delete').click(function () {
                //alert($(this).prev().text());
                $.ajax({
                    url: "/receipts/DeleteVendor",
                    data: {
                        name: $(this).prev().text()
                    }
                  , fail: function () {
                      alert("fail");
                  }
                });
                $(this).parent().remove();
                return false;
            });
        }
        , minLength: 1
    });
    //$('#ReceiptViewModel_VendorName').data('ui-autocomplete')._renderMenu = function (ul, items) {
    //    var that = this;
    //    $.each(items, function (index, item) {
    //        that._renderItemData(ul, item);
    //    });
    //    $(ul).find('li:odd').addClass('odd');
    //};
    $('#ReceiptViewModel_VendorName').data('ui-autocomplete')._renderItem = function (ul, item) {
        var li = $('<li>').prop('data-value', item.value);
        if (item.value == 'Y') {
            li.append($("<a>").addClass('pull-left').text(item.label));
            li.append('<a href="#" class="delete pull-right text-center" style="width: 20px">X</a><div class="clearfix"></div>');
        } else {
            li.append($("<a>").text(item.label));
        }
        li.appendTo(ul);
        return li;
    };

    $('span#vendorDropDown').click(function () {
        if ($('ul#ui-id-1').css('display') == 'none') {
            var temp = $('#ReceiptViewModel_VendorName').val();
            $('#ReceiptViewModel_VendorName').val('');
            auto.autocomplete('option', 'minLength', 0)
                .autocomplete('search', '')
                .autocomplete('option', 'minLength', 1);
            $('#ReceiptViewModel_VendorName').val(temp);
        } else {
            $('ul#ui-id-1').css('display', 'none');
        }
    });
}

