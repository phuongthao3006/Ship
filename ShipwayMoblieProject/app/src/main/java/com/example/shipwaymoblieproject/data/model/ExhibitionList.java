package com.example.shipwaymoblieproject.data.model;

import com.google.gson.annotations.SerializedName;

import java.util.List;

public class ExhibitionList {
    @SerializedName("data")
    private List<Exhibition> listExhibition;

    public List<Exhibition> getListExhibition() {
        return listExhibition;
    }

    public void setListExhibition(List<Exhibition> listExhibition) {
        this.listExhibition = listExhibition;
    }
}
