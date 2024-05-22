package com.example.shipwaymoblieproject.data.model;

import com.example.shipwaymoblieproject.data.remote.IShipwayApi;
import com.google.gson.annotations.SerializedName;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;


public class HistoryTrip {
    @SerializedName("HistoryTripId")
    private int historyTripId;

    @SerializedName("ExhibitionId")
    private String exhibitionId;

    @SerializedName("DateTrip")
    private String dateTrip;

    @SerializedName("ExhibitionStatusId")
    private int exhibitionStatusId;

    @SerializedName("CurrentAddress")
    private String currentAddress;

    @SerializedName("Note")
    private String note;

    public HistoryTrip() {
    }



    public int getHistoryTripId() {
        return historyTripId;
    }

    public void setHistoryTripId(int historyTripId) {
        this.historyTripId = historyTripId;
    }

    public String getExhibitionId() {
        return exhibitionId;
    }

    public void setExhibitionId(String exhibitionId) {
        this.exhibitionId = exhibitionId;
    }

    public String getDateTrip() {
        return dateTrip;
    }

    public void setDateTrip(String dateTrip) {
        this.dateTrip = dateTrip;
    }

    public int getExhibitionStatusId() {
        return exhibitionStatusId;
    }

    public void setExhibitionStatusId(int exhibitionStatusId) {
        this.exhibitionStatusId = exhibitionStatusId;
    }

    public String getCurrentAddress() {
        return currentAddress;
    }

    public void setCurrentAddress(String currentAddress) {
        this.currentAddress = currentAddress;
    }

    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }
}
