function GetTotalCost(arrayAddress, arraySize) {
    var array=[];
    for (i = 0; i < arrayAddress.length; i++) {
        array.push(arrayAddress[i]);
    }
    for (i = 0; i < arraySize.length; i++) {
        array.push(arraySize[i]);
    }

    
    for (i = 0; i < array.length; i++) {
        $(array[i]).keyup(function () {
            var isCost = true;
            for (j = 0; j < arrayAddress.length; j++) {
                if ($(arrayAddress[j]).val() == "") {
                    $(".totalmoney").html("0 VND");
                    isCost = false;
                }
            }

            if (isCost == true) {
                $.ajax({
                    url: "/ExhibitionCustomer/TotalMoney",
                    type: "POST",
                    dataType: "json",
                    data: {
                        weigh: parseInt($("#PackageWeigh").val())||0,
                        citysender: ($("#ProvinceSenderCbb").val()).toString(),
                        cityReceiver: ($("#ProvinceReceiveCbb").val()).toString(),
                        kindService: parseInt($("#KindServiceId").val())
                    },
                    success: function (data) {
                        $("#Cost").val(data.TotalMoney);
                        $("#DayReceive").val(new Date(data.DayReceiver));
                        $(".totalmoney").html(data.TotalMoney);
                        var temp = "Dự kiến giao trong ngày : " + data.DayReceiver;
                        $("#dayReceive").html(temp);
                    }
                })
            }
        })

        $(array[i]).change(function () {
            var isCost = true;
            for (j = 0; j < arrayAddress.length; j++) {
                if ($(arrayAddress[j]).val() == "") {
                    $(".totalmoney").html("0 VND");
                    isCost = false;
                }
            }

            if (isCost == true) {
                $.ajax({
                    url: "/ExhibitionCustomer/TotalMoney",
                    type: "POST",
                    dataType: "json",
                    data: {
                        weigh: parseInt($("#PackageWeigh").val()) || 0,
                        packagelong: parseInt($("#PackageLong").val()) || 0,
                        wide: parseInt($("#PackageWide").val()) || 0,
                        high: parseInt($("#PackageHigh").val()) || 0,
                        citysender: ($("#ProvinceSenderCbb").val()).toString(),
                        cityReceiver: ($("#ProvinceReceiveCbb").val()).toString(),
                        kindService: parseInt($("#KindServiceId").val())
                    },
                    success: function (data) {
                        $("#Cost").val(data.TotalMoney);
                        $("#DayReceive").val(data.DayReceiver);
                        $(".totalmoney").html(data.TotalMoney);
                        var temp = "Dự kiến giao trong ngày : " + data.DayReceiver;
                        $("#dayReceive").html(temp);
                    }
                })
            }
        })

        $(array[i]).keydown(function () {
            var isCost = true;
            for (j = 0; j < arrayAddress.length; j++) {
                if ($(arrayAddress[j]).val() == "") {
                    $(".totalmoney").html("0 VND");
                    isCost = false;
                }
            }

            if (isCost == true) {
                $.ajax({
                    url: "/ExhibitionCustomer/TotalMoney",
                    type: "POST",
                    dataType: "json",
                    data: {
                        weigh: parseInt($("#PackageWeigh").val()) || 0,
                        packagelong: parseInt($("#PackageLong").val()) || 0,
                        wide: parseInt($("#PackageWide").val()) || 0,
                        high: parseInt($("#PackageHigh").val()) || 0,
                        citysender: ($("#ProvinceSenderCbb").val()).toString(),
                        cityReceiver: ($("#ProvinceReceiveCbb").val()).toString(),
                        kindService: parseInt($("#KindServiceId").val())
                    },
                    success: function (data) {
                        $("#Cost").val(data.TotalMoney);
                        $("#DayReceive").val(data.DayReceiver);
                        $(".totalmoney").html(data.TotalMoney);
                        var temp = "Dự kiến giao trong ngày : " + data.DayReceiver;
                        $("#dayReceive").html(temp);
                    }
                })
            }
        })
    }
}

GetTotalCost(["#SenderAddress", "#ReceiverAddress"], ["#PackageWeigh", "#KindServiceId"]);

$(".itemExhibition").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $(".partial-container").html(data);
        $("#modal_exhibition").modal("show");
    })
});

$(".editmodal").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $('.partial-container').html(data);
        $("#modal_exhibition").modal("show");
    })
})

$(".deletemodal").click(function () {
    var url = $(this).data("url");
    $.get(url, function (data) {
        $('.partial-container').html(data);
        $("#modal_exhibition").modal("show");
    })
})

$("#ProvinceSenderCbb").change(function () {
    if($(this).val() != ""){
        idProvinceSelected = $(this).val();
        $.ajax({
            url: "/District/GetByIdProvice",
            type: "POST",
            dataType: "json",
            data: {
                idProvice: idProvinceSelected
            },
            success: function (data) {
                $('#WardSenderCbb').empty();
                $('#WardSenderCbb').append($(`<option value="">-- Chọn Phường/Xã --</option>`));
                var citiesSelect = $('#DistrictSenderCbb');
                citiesSelect.empty();

                citiesSelect.append($(`<option value="">-- Chọn Quận/Huyện --</option>`));

                for (var key in data) {
                    if (data.hasOwnProperty(key)) {
                        $(citiesSelect).append($(`<option value="${data[key]['DistrictId']}">${data[key]['Name']}</option>`));
                    }
                }
            }
        })
    }
}) 

$("#DistrictSenderCbb").change(function () {
    if ($(this).val() != "") {
        idDistrictSelected = $(this).val();
        $.ajax({
            url: "/Ward/GetByIdDistrict",
            type: "POST",
            dataType: "json",
            data: {
                idDistrict: idDistrictSelected
            },
            success: function (data) {
                var citiesSelect = $('#WardSenderCbb');
                citiesSelect.empty();

                citiesSelect.append($(`<option value="">-- Chọn Phường/Xã --</option>`));

                for (var key in data) {
                    if (data.hasOwnProperty(key)) {
                        $(citiesSelect).append($(`<option value="${data[key]['WardId']}">${data[key]['Name']}</option>`));
                    }
                }
            }
        })
    }
})

$("#ProvinceReceiveCbb").change(function () {
    if($(this).val() != ""){
        idProvinceSelected = $(this).val();
        $.ajax({
            url: "/District/GetByIdProvice",
            type: "POST",
            dataType: "json",
            data: {
                idProvice: idProvinceSelected
            },
            success: function (data) {
                $('#WardReceiveCbb').empty();
                $('#WardReceiveCbb').append($(`<option value="">-- Chọn Phường/Xã --</option>`));
                var citiesSelect = $('#DistrictReceiveCbb');
                citiesSelect.empty();

                citiesSelect.append($(`<option value="">-- Chọn Quận/Huyện --</option>`));

                for (var key in data) {
                    if (data.hasOwnProperty(key)) {
                        $(citiesSelect).append($(`<option value="${data[key]['DistrictId']}">${data[key]['Name']}</option>`));
                    }
                }
            }
        })
    }
}) 

$("#DistrictReceiveCbb").change(function () {
    if ($(this).val() != "") {
        idDistrictSelected = $(this).val();
        $.ajax({
            url: "/Ward/GetByIdDistrict",
            type: "POST",
            dataType: "json",
            data: {
                idDistrict: idDistrictSelected
            },
            success: function (data) {
                var citiesSelect = $('#WardReceiveCbb');
                citiesSelect.empty();

                citiesSelect.append($(`<option value="">-- Chọn Phường/Xã --</option>`));

                for (var key in data) {
                    if (data.hasOwnProperty(key)) {
                        $(citiesSelect).append($(`<option value="${data[key]['WardId']}">${data[key]['Name']}</option>`));
                    }
                }
            }
        })
    }
})


