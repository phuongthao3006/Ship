package com.example.shipwaymoblieproject.data.model;

import com.google.gson.annotations.SerializedName;


public class AssignWork {
    @SerializedName("AssignWorkId")
    private int assignWorkId;

    @SerializedName("ExhibitionId")
    private String exhibitionId;

    @SerializedName("ShipperId")
    private int shipperId;

    @SerializedName("AssignWorkDate")
    private String assignWorkDate;

    @SerializedName("DeliveryDate")
    private String deliveryDate;

    @SerializedName("Note")
    private String note;

    public AssignWork() {
    }

    public int getAssignWorkId() {
        return assignWorkId;
    }

    public void setAssignWorkId(int assignWorkId) {
        this.assignWorkId = assignWorkId;
    }

    public String getExhibitionId() {
        return exhibitionId;
    }

    public void setExhibitionId(String exhibitionId) {
        this.exhibitionId = exhibitionId;
    }

    public int getShipperId() {
        return shipperId;
    }

    public void setShipperId(int shipperId) {
        this.shipperId = shipperId;
    }

    public String getAssignWorkDate() {
        return assignWorkDate;
    }

    public void setAssignWorkDate(String assignWorkDate) {
        this.assignWorkDate = assignWorkDate;
    }

    public String getDeliveryDate() {
        return deliveryDate;
    }

    public void setDeliveryDate(String deliveryDate) {
        this.deliveryDate = deliveryDate;
    }

    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }
}
