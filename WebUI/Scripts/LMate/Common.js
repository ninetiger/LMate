function UserPermissionChange() {
    var actAsUser = $('select#PermissionId');
    actAsUser.change(function () {
        $.ajax({
            url: "/UserDelegate/SetCurrentUser",
            cache: false,
            data: {
                permissionId: actAsUser.val(),
                //actAsUserRoleId: $('#CurrentActAsRoleId').val(),
                returnUrl: window.location.pathname
            }
            , fail: function () {
                alert("fail");
            }
            , error: function () {
                alert("an error has happened");
            }
        });
    });
}