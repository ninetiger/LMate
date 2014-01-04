function docReady() {

    $('button.addMore').click(function () {
        var section = '<div class="well well-sm"><label class="pull-left">User email</label><input class="email pull-left form-control input-sm" style="width:300px" /><button class="add btn btn-default btn-sm">Add</button><button class="remove btn btn-default btn-sm">X</button></div>';
        $(section).insertBefore($(this));
    });

    $("div.section").on("click", "button.remove", function () {
        $(this).parent().remove();
    });

    addPermission();

}

function addPermission() {
    $("div.section").on("click", "button.add", function () {
        var addBtn = $('button.add');
        $.ajax({
            url: "/Permission/GivePermission",
            cache: false,
            data: {
                email: addBtn.prev().val(),
                roleName: addBtn.parent().parent().children(':first').text()
            }
            , success: function (isGranted) {
                alert(isGranted);
                //if (isGranted == 'False') {
                //}
            }
            , error: function () {
                alert("an error has happened");
            }
            , complete: function () {
            }
        });
    });
}

