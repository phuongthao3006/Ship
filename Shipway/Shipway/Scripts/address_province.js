$(document).ready(function () {
    $(".ProvinceCbb").on('change', function () {
        console.log(1);
        if ($(this).val() != "") {
            idProvinceSelected = $(this).val();
            $.ajax({
                url: "/Address/GetDistrictByIdProvice",
                type: "POST",
                dataType: "json",
                data: {
                    idProvince: idProvinceSelected
                },
                success: function (data) {
                    console.log(data);
                    $('.WardCbb').empty();
                    $('.WardCbb').append($(`<option value="">-- Chọn Phường/Xã --</option>`));
                    var citiesSelect = $('.DistrictCbb');
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
    });

    $(".DistrictCbb").on('change', function () {
        if ($(this).val() != "") {
            idDistrictSelected = $(this).val();
            $.ajax({
                url: "/Address/GetWardByIdDistrict",
                type: "POST",
                dataType: "json",
                data: {
                    idDistrict: idDistrictSelected
                },
                success: function (data) {
                    var citiesSelect = $('.WardCbb');
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
    });
});