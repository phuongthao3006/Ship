package com.example.shipwaymoblieproject.data.model;

import androidx.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

public class ExhibitionStatus {
    @SerializedName("ExhibitionStatusId")
    private int exhibitionStatusId;

    @SerializedName("Name")
    private String name;

    public ExhibitionStatus() {
    }

    public ExhibitionStatus(int exhibitionStatusId, String name) {
        this.exhibitionStatusId = exhibitionStatusId;
        this.name = name;
    }

    public int getExhibitionStatusId() {
        return exhibitionStatusId;
    }

    public void setExhibitionStatusId(int exhibitionStatusId) {
        this.exhibitionStatusId = exhibitionStatusId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @NonNull
    @Override
    public String toString() {
        return name;
    }
}
