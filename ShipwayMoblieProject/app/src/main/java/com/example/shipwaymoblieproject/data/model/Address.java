package com.example.shipwaymoblieproject.data.model;

import com.google.gson.annotations.SerializedName;

public class Address {
    @SerializedName("ProvinceId")
    private String provinceId;

    @SerializedName("ProvinceName")
    private String provinceName;

    @SerializedName("DistrictId")
    private String districtId;

    @SerializedName("DistrictName")
    private String districtName;

    @SerializedName("WardId")
    private String wardId;

    @SerializedName("WardName")
    private String wardName;

    public Address() {
    }

    public String getProvinceId() {
        return provinceId;
    }

    public void setProvinceId(String provinceId) {
        this.provinceId = provinceId;
    }

    public String getProvinceName() {
        return provinceName;
    }

    public void setProvinceName(String provinceName) {
        this.provinceName = provinceName;
    }

    public String getDistrictId() {
        return districtId;
    }

    public void setDistrictId(String districtId) {
        this.districtId = districtId;
    }

    public String getDistrictName() {
        return districtName;
    }

    public void setDistrictName(String districtName) {
        this.districtName = districtName;
    }

    public String getWardId() {
        return wardId;
    }

    public void setWardId(String wardId) {
        this.wardId = wardId;
    }

    public String getWardName() {
        return wardName;
    }

    public void setWardName(String wardName) {
        this.wardName = wardName;
    }
}
