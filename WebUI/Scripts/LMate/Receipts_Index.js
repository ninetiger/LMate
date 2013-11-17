function ReceiptsDataTable() {
    $('#dataTable').dataTable({
        //"bJQueryUI": true,
        //'iDisplayLength': 100,
        //'bRetrieve': true,
        //'bHeader': true,
        //"bSearchable": false

        "aaSorting": [[0, "asc"]], //"desc"]]
        "bFilter": false
        //, "bSortable": true
        //, "bInfo": false
        , "bServerSide": true
        , "sAjaxSource": "/Receipts/DataTableAjaxHandler"
        //,"sServerMethod": "POST"
        , "bProcessing": true
        , "sPaginationType": "full_numbers"
        , 'aoColumns': [
                { 'sName': 'ID', 'bVisible': true, 'bSortable': false },
                { 'sName': 'Description' },
                { 'sName': 'PurchaseDate' },
                { 'sName': 'Price' },
                { 'sName': 'Vendor' },
                { 'sName': 'ReceiptType' },
                { 'sName': 'IsBulk' },
                { 'sName': 'HasImage' },
                { 'sName': 'Actions', 'bSortable': false }
        ]
        , 'fnRowCallback': function (nRow, aData) {
            var path = location.pathname.split('/');
            //var appRoot = location.protocol + '//' + location.host + '/' + path[1];


            $('td:eq(0)', nRow)
                .html('<input id="selectAllReceipts" type="checkbox" value="' + aData[0] + '"/>');

            $('td:eq(8)', nRow)
                .html('<a href="Receipts/Delete/?Id=' + aData[0] + '&Description=' +
                    aData[1] + '"><span class="glyphicon glyphicon-trash" style="font-size:16px"></span></a>'
            + '<a href="Receipts/Edit/?Id=' + aData[0] + '"><span class="glyphicon glyphicon-edit" style="font-size:16px"></span></a>');
            return nRow;
        }
        //, "fnDrawCallback": function () {
        //    if (addControlInDataTableHtml.length > 0) {
        //        $(addControlInDataTableHtml).prependTo(".reviewRuleTBody");
        //    }
        //}
        //, "aaData": data
        //, "fnServerData": function (sSource, aoData, fnCallback) {
        //    $.getJSON(sSource, aoData, function (json) {
        //        map = {};
        //        map["aaData"] = json;
        //        fnCallback(map);
        //    });
        //}

        //, "fnServerData": function (sSource, aoData, fnCallback) {
        //    $.getJSON(sSource, aoData, function (json) {
        //        /* --- Here is where we massage the data --- */
        //        /* if the variable "json" is just a string (I forgot) then evaluate it first into an object (download a json library)*/
        //        var aaData = [];
        //        $.each(json, function (index, object) {
        //            alert('index:' + index + '\r\n' + 'obj:'+ object);
        //            var aData = [];
        //            $.each(object, function (key, value) {
        //                aData.push(value); //be careful here, you might put things in the wrong column
        //            });
        //            aaData.push(aData);
        //        });
        //        /* --- And after we're done, we give the correctly formatted data to datatables --- */  /* --- if "json" was a string and not an object, then stringify aaData first (object to json notation, download a library) --- */
        //        fnCallback(aaData);
        //});
        //}
    });
}

function AutoComplete() {
    $("#Receipts_Desc").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/receipts/AutoCompleteSearch",
                dataType: "json",
                data: {
                    id: "Receipts_Desc",
                    searchString: $('#Receipts_Desc').val()
                },
                success: function (data) {
                    response($.map(data.list, function (item) {
                        return {
                            label: item,
                            value: item
                        };
                    }));
                }
            });
        },
        minLength: 2
        //select: function (event, ui) {
        //    log(ui.item ?
        //      "Selected: " + ui.item.label :
        //      "Nothing selected, input was " + this.value);
        //},
        //open: function () {
        //    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        //},
        //close: function () {
        //    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        //}
    });
}