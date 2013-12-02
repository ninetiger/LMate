function docReady() {
    $("#ReceiptViewModel_PurchaseDate").datepicker({
        changeMonth: true,
        changeYear: true
    });

    $('#ReceiptViewModel_AccountTypeId').select2({
        placeholder: "Select a Type"
        ,allowClear: true
    });
    
    $('#ReceiptViewModel_CurrencyId').select2({
        placeholder: "Select a Currency"
        ,allowClear: true
    });
    
    $('#ReceiptViewModel_ReceiptCategoryId').select2({
        placeholder: "Select a Category"
    , allowClear: true
    });
}

function GetImages() {
    $.ajax({
        url: "/receipts/GetImage",
        dataType: "json",
        cache: false,
        data: {
            receiptId: 15//$('#Receipts_Desc').val()
        },
        success: function (data) {
            alert('in: ');
            //var image = '<img width="100%" height="100%" class="img-rounded" src = "@imageFile" /  >';
            //$('#receiptImages').html(image);
            response($.map(data.list, function (item) {
                alert('inn');
                return {
                    label: item,
                    value: item
                };
            }));
        }
    });
}