package com.example.shipwaymoblieproject.data.model;

import androidx.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

public class District {
    @SerializedName("DistrictId")
    private String districtId;

    @SerializedName("ProvinceId")
    private String provinceId;

    @SerializedName("Name")
    private String name;

    @SerializedName("Type")
    private String type;

    @SerializedName("Location")
    private String location;

    public District() {
    }

    public String getDistrictId() {
        return districtId;
    }

    public void setDistrictId(String districtId) {
        this.districtId = districtId;
    }

    public String getProvinceId() {
        return provinceId;
    }

    public void setProvinceId(String provinceId) {
        this.provinceId = provinceId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    @NonNull
    @Override
    public String toString() {
        return name;
    }
}
