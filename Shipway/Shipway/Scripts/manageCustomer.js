$(document).ready(function () {
    $(".listCustomer").DataTable({
        "bPageLength": 10,
        "bProcessing": true,
        "bDestroy": true,
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bFilter": false,
        "bInfo": false,
    });
})

$(".editmodal").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $(".partial-container").html(data);
        $("#modal_customer").modal("show");
    })
})

$(".deletemodal").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $(".partial-container").html(data);
        $("#modal_customer").modal("show");
    })
})

$("#clearFilter").click(function () {
    $("#fullTextSearch").val("");
    $("#tableContent tr").filter(function () {
        if ($(this).css("display") == "none") {
            $(this).toggle();
        }
    })
})


$("#fullTextSearch").on("keyup", search)

function search() {
    var fullTextSearch = "";
    if ($("#fullTextSearch").val() != "") {
        fullTextSearch = $("#fullTextSearch").val().toLowerCase().trim();
    }

    $("#tableContent tr").filter(function () {
        if ($(this).css("display") == "none") {
            $(this).toggle();
        }
        if (fullTextSearch != "") {
            $(this).toggle(fullTextSearch == "" || $(this).text().toLowerCase().indexOf(fullTextSearch) > -1);
        }
    });
}