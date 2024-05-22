package com.example.shipwaymoblieproject.data.model;

import androidx.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

public class Province {
    @SerializedName("ProvinceId")
    private String provinceId;

    @SerializedName("Name")
    private String name;

    @SerializedName("Type")
    private String type;

    @SerializedName("Region")
    private int region;

    public Province() {
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

    public int getRegion() {
        return region;
    }

    public void setRegion(int region) {
        this.region = region;
    }

    @NonNull
    @Override
    public String toString() {
        return name;
    }
}
