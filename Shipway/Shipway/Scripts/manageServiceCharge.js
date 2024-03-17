$(document).ready(function () {

    $(".listServiceCharge").DataTable({
        "bPageLength": 10,
        "bProcessing": true,
        "bDestroy": true,
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bFilter": false,
        "bInfo": false,
    });
});

$(".editmodal").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $(".partial-container").html(data);
        $("#modal_serviceCharge").modal("show");
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
    $("#routerSearch").val("");
    $("#kindServiceSearch").val("");
    $("#kindTimeReceiveSearch").val("");
    $("#fullTextSearch").val("");
    $("#tableContent tr").filter(function () {
        if ($(this).css("display") == "none") {
            $(this).toggle();
        }
    })
})

$("#routerSearch").change(search)

$("#kindServiceSearch").change(search)

$("#kindTimeReceiveSearch").change(search)

$("#fullTextSearch").on("keyup", search)

function search() {
    var idSelectedSearchs = new Array();
    var fullTextSearch = "";
	if ($("#routerSearch").val() != "") {
        idSelectedSearchs.push("#router_" + $("#routerSearch").val());
    }
    if ($("#kindServiceSearch").val() != "") {
        idSelectedSearchs.push("#kindService_" + $("#kindServiceSearch").val());
    }
    if ($("#kindTimeReceiveSearch").val() != "") {
        idSelectedSearchs.push("#kindTimeReceive_" + $("#kindTimeReceiveSearch").val());
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