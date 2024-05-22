package com.example.shipwaymoblieproject.data.model;

import androidx.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

public class Ward {
    @SerializedName("WardId")
    private String wardId;

    @SerializedName("DistrictId")
    private String districtId;

    @SerializedName("Name")
    private String name;

    @SerializedName("Type")
    private String type;

    @SerializedName("Region")
    private Integer region;

    public Ward() {
    }

    public String getWardId() {
        return wardId;
    }

    public void setWardId(String wardId) {
        this.wardId = wardId;
    }

    public String getDistrictId() {
        return districtId;
    }

    public void setDistrictId(String districtId) {
        this.districtId = districtId;
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

    public Integer getRegion() {
        return region;
    }

    public void setRegion(Integer region) {
        this.region = region;
    }

    @NonNull
    @Override
    public String toString() {
        return name;
    }
}
