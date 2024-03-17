$(document).ready(function () {

    $(".listShipper").DataTable({
        "bFilter": false,
        "bInfo": false,
        "bPageLength": 10,
        "bProcessing": true,
        "bDestroy": true,
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
    });
})

$(".editmodal").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $(".partial-container").html(data);
        $("#modal_shipper").modal("show");
    })
})

$(".deletemodal").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $(".partial-container").html(data);
        $("#modal_shipper").modal("show");
    })
})

$(".deletemodal").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $(".partial-container").html(data);
        $("#modal_serviceCharge").modal("show");
    })
})

$("#clearFilter").click(function () {
    $("#agencySearch").val("");
    $("#fullTextSearch").val("");
    $("#tableContent tr").filter(function () {
        if ($(this).css("display") == "none") {
            $(this).toggle();
        }
    })
})

$("#agencySearch").change(search)

$("#fullTextSearch").on("keyup", search)

function search() {
    var idSelectedSearchs = new Array();
    var fullTextSearch = "";
    if ($("#agencySearch").val() != "") {
        idSelectedSearchs.push("#agency_" + $("#agencySearch").val());
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