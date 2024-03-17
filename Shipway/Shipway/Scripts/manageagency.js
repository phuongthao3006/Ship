$(document).ready(function () {
    $(".listAgency").DataTable({
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
        $('.partial-container').html(data);
        $("#modal_agency").modal("show");
    })
})

$(".deletemodal").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $('.partial-container').html(data);
        $("#modal_agency").modal("show");
    })
})

$("#clearFilter").click(function () {
    $("#fullTextSearch").val("");
    $("#proviceSearch").val("");
    $("#districtSearch").val("");
    $("#wardSearch").val("");
    $("#tableContent tr").filter(function () {
        if ($(this).css("display") == "none") {
            $(this).toggle();
        }
    })
})


$("#fullTextSearch").on("keyup", search)
$("#proviceSearch").change(search);
$("#districtSearch").change(search);
$("#wardSearch").change(search);

function search() {
    var idSelectedSearchs = new Array();
    var fullTextSearch = "";
    if ($("#proviceSearch").val() != "") {
        idSelectedSearchs.push("#province_" + $("#proviceSearch").val());
    } 
    if ($("#districtSearch").val() != "") {
        idSelectedSearchs.push("#district_" + $("#districtSearch").val());
    }
    if ($("#wardSearch").val() != "") {
        idSelectedSearchs.push("#ward_" + $("#wardSearch").val());
    }
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
        if (idSelectedSearchs.length > 0 || fullTextSearch != "") {
            $(this).toggle((idSelectedSearchs.length == 0
                || $(this).children(idSelectedSearchs.join(',')).length == idSelectedSearchs.length)
                && (fullTextSearch == ""
                    || $(this).text().toLowerCase().indexOf(fullTextSearch) > -1));
        }
    });
}