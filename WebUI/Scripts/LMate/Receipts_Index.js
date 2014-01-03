function docReady() {
    ReceiptsDataTable();
}

function ReceiptsDataTable() {
    $('#dataTable').dataTable({
        "sServerMethod": "POST",
        "sAjaxSource": "/Receipts/DataTableAjaxHandler"
        //, 'bStateSave': true
        //, "iCookieDuration": 1800 //30 mins
        ,
        'bPaginate': true,
        'bLengthChange': true,
        'iDisplayLength': 10
        //, 'bHeader': false
        //, "aaSorting": [[1, "asc"]] //"desc"]]
        ,
        'bDeferRender': true,
        "bFilter": true,
        "bServerSide": false //set to true to do everything at server site inc. sorting
        ,
        "bProcessing": true,
        "sPaginationType": 'full_numbers',
        'bSortCellsTop': true,
        'bAutoWidth': true,
        //'aoColumns': [
        //       { 'sName': 'ID', 'bVisible': true, 'bSortable': false, 'bSearchable': false },
        //       { 'sName': 'Description' },
        //       { 'sName': 'PurchaseDate' },
        //       { 'sName': 'Price' },
        //       { 'sName': 'Vendor' },
        //       { 'sName': 'AccountType' },
        //       { 'sName': 'IsBulk' },
        //       { 'sName': 'HasImage' },
        //       { 'sName': 'Actions', 'bSortable': false, 'bSearchable': false }
        //],
        'aoColumnDefs': [
            { 'bSortable': false, 'bSearchable': false, 'aTargets': [0] },
            { 'bSortable': false, 'bSearchable': false, 'aTargets': [8] }
        ],
        'oLanguage': { "sSearch": "Search all columns:" },
        'fnCreatedRow': function(nRow, aData) {
            var path = location.pathname.split('/');
            //var appRoot = location.protocol + '//' + location.host + '/' + path[1];

            //todo shouldnt need jquery a css with class should do for the 2 function below
            $(nRow).css('cursor', 'pointer');
            $(nRow).hover(
                function() {
                    $(this).css("background-color", "lightcyan");
                },
                function() {
                    $(this).css("background", "");
                }
            );
            $(nRow).click(function() {
                //nRow.setAttribute("id", aData[0]);
                document.location.href = '/receipts/Edit?ID=' + aData[0];
            });

            $('td:eq(0)', nRow)
                .html('<input class="receiptId" type="checkbox" value="' + aData[0] + '"/>');

            $('td:eq(8)', nRow)
                .html('<a href="Receipts/Delete/?Id=' + aData[0] + '&Description=' +
                    aData[1] + '"><span class="glyphicon glyphicon-trash" style="font-size:16px"></span></a>'
                    + '<a href="Receipts/Edit/?Id=' + aData[0] + '"><span class="glyphicon glyphicon-edit" style="font-size:16px"></span></a>');
            return nRow;
        }
    });
}

//function AutoComplete() {
//    $("#Receipts_Desc").autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "/receipts/AutoCompleteReceiptSearch",
//                dataType: "json",
//                data: {
//                    id: "Receipts_Desc",
//                    searchString: $('#Receipts_Desc').val()
//                },
//                success: function (data) {
//                    response($.map(data.list, function (item) {
//                        return {
//                            label: item,
//                            value: item
//                        };
//                    }));
//                }
//            });
//        },
//        minLength: 2
//        //select: function (event, ui) {
//        //    log(ui.item ?
//        //      "Selected: " + ui.item.label :
//        //      "Nothing selected, input was " + this.value);
//        //},
//        //open: function () {
//        //    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
//        //},
//        //close: function () {
//        //    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
//        //}
//    });
//}