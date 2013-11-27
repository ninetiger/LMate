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