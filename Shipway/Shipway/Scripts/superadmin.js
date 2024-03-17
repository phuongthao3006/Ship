
$(document).ready(function () {
    
    var valInput = parseInt($('#PermissionId').val());
    if (valInput == 2 || valInput == 3 || valInput == 4) {
        $('.adminAgency').css('display', 'block');
    } else {
        $('.adminAgency').css('display', 'none');
    }


    $(document).on('click', '.btn-reset-pass', function () {
        if (confirm("Xác nhận đặt lại mật khẩu?")) {
            var id = $(this).attr("value");
            resetPassword(id);
        }
    });

    function resetPassword(id) {
        $.ajax({
            url: '/SuperAdmin/ResetPassword',
            type: 'POST',
            dataType: 'json',
            data: {
                userId: id
            },
            success: function (response) {
                if (response) {
                    location.reload();
                    console.log("Reset pass successfuly");
                }
            },
            error: function (err) {
                console.log("reset pass", err);
            }
        });
    }
});

$('.editmodal').click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $('.partial-container').html(data);
        $("#modal_account").modal("show");
    })
});

$('.deletemodal').click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $('.partial-container').html(data);
        $("#modal_account").modal("show");
    })
})

$('#PermissionId').change(function () {
    var valInput = parseInt($('#PermissionId').val());
    if (valInput == 2 || valInput == 3 || valInput == 4) {
        $('.adminAgency').css('display', 'block');
    } else{
        $('.adminAgency').css('display', 'none');
    }
})



$("#clearFilter").click(function () {
    $("#permissionSearch").val("");
    $("#fullTextSearch").val("");
    $("#tableContent tr").filter(function () {
        if ($(this).css("display") == "none") {
            $(this).toggle();
        }
    })
})

$("#permissionSearch").change(search)

$("#fullTextSearch").on("keyup", search)

function search() {
    var idSelectedSearchs = new Array();
    var fullTextSearch = "";
    if ($("#permissionSearch").val() != "") {
        idSelectedSearchs.push("#permission_" + $("#permissionSearch").val());
    }
    if ($("#fullTextSearch").val() != "") {
        fullTextSearch = $("#fullTextSearch").val().toLowerCase().trim();
    }

    $("#tableContent tr").filter(function () {
        if ($(this).css("display") == "none") {
            $(this).toggle();
        }
        if (idSelectedSearchs.length > 0 || fullTextSearch != "") {
            $(this).toggle((idSelectedSearchs.length == 0
                || $(this).children(idSelectedSearchs.join(',')).length == idSelectedSearchs.length)
                && (fullTextSearch == ""
                    || $(this).text().toLowerCase().indexOf(fullTextSearch) > -1));
        }
    });
}

$(document).on('ready', function () {
    $(document).on('change', '.types-user', function() {
        console.log(1123);
        var agency = $(this).closest('.modal-body').find('.adminAgency');
        switch (parseInt($(this).val())) {
            case 1:
                agency.css('display', 'none');
                break;
            case 2:
            case 3:
            case 4:
                agency.css('display', 'block');
                break;
            }
    });
});

