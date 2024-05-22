package com.example.shipwaymoblieproject.data.model;

import com.google.gson.annotations.SerializedName;


public class Exhibition {
    @SerializedName("ExhibitionId")
    private String exhibitionId;

    @SerializedName("SenderId")
    private int senderId;

    @SerializedName("ReceiverName")
    private String receiverName;

    @SerializedName("ReceiverPhoneNumber")
    private String receiverPhoneNumber;

    @SerializedName("ReceiverAddress")
    private String receiverAddress;

    @SerializedName("WardReceiveId")
    private String wardReceiveId;

    @SerializedName("PackageWeigh")
    private int packageWeight;

    @SerializedName("PackageLong")
    private int packageLength;

    @SerializedName("PackageWide")
    private int packageWidth;

    @SerializedName("PackageHigh")
    private int packageHeight;

    @SerializedName("Note")
    private String note;

    @SerializedName("CreatedDate")
    private String createdDate;

    @SerializedName("CreatedUserId")
    private int createdUserId;

    @SerializedName("LastModifiedDate")
    private String lastModifiedDate;

    @SerializedName("LastModifiedUserId")
    private int lastModifiedUserId;

    @SerializedName("KindServiceId")
    private int kindServiceId;

    @SerializedName("Cost")
    private int cost;

    @SerializedName("ExhibitionStatusId")
    private int exhibitionStatusId;

    @SerializedName("DayReceive")
    private String dayReceive;

    @SerializedName("LocationSender")
    private String locationSender;

    @SerializedName("LocationReceive")
    private String locationReceive;

    @SerializedName("IsSendToAgency")
    private boolean isSendToAgency;

    @SerializedName("AssignToAgencyId")
    private int assignToAgencyId;

    @SerializedName("GroupLump")
    private int groupLump;

    @SerializedName("NumberOfFailedDeliveries")
    private int numberOfFailedDeliveries;

    @SerializedName("AgencyId")
    private int agencyId;

    @SerializedName("KindTimeReceivedId")
    private int kindTimeReceivedId;

    @SerializedName("RouterId")
    private int routerId;

    @SerializedName("MoveShipper")
    private boolean moveShipper;

    public Exhibition() {
    }

    public String getExhibitionId() {
        return exhibitionId;
    }

    public void setExhibitionId(String exhibitionId) {
        this.exhibitionId = exhibitionId;
    }

    public int getSenderId() {
        return senderId;
    }

    public void setSenderId(int senderId) {
        this.senderId = senderId;
    }

    public String getReceiverName() {
        return receiverName;
    }

    public void setReceiverName(String receiverName) {
        this.receiverName = receiverName;
    }

    public String getReceiverPhoneNumber() {
        return receiverPhoneNumber;
    }

    public void setReceiverPhoneNumber(String receiverPhoneNumber) {
        this.receiverPhoneNumber = receiverPhoneNumber;
    }

    public String getReceiverAddress() {
        return receiverAddress;
    }

    public void setReceiverAddress(String receiverAddress) {
        this.receiverAddress = receiverAddress;
    }

    public String getWardReceiveId() {
        return wardReceiveId;
    }

    public void setWardReceiveId(String wardReceiveId) {
        this.wardReceiveId = wardReceiveId;
    }

    public int getPackageWeight() {
        return packageWeight;
    }

    public void setPackageWeight(int packageWeight) {
        this.packageWeight = packageWeight;
    }

    public int getPackageLength() {
        return packageLength;
    }

    public void setPackageLength(int packageLength) {
        this.packageLength = packageLength;
    }

    public int getPackageWidth() {
        return packageWidth;
    }

    public void setPackageWidth(int packageWidth) {
        this.packageWidth = packageWidth;
    }

    public int getPackageHeight() {
        return packageHeight;
    }

    public void setPackageHeight(int packageHeight) {
        this.packageHeight = packageHeight;
    }

    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }
    public int getCreatedUserId() {
        return createdUserId;
    }

    public void setCreatedUserId(int createdUserId) {
        this.createdUserId = createdUserId;
    }

    public int getLastModifiedUserId() {
        return lastModifiedUserId;
    }

    public String getCreatedDate() {
        return createdDate;
    }

    public void setCreatedDate(String createdDate) {
        this.createdDate = createdDate;
    }

    public String getLastModifiedDate() {
        return lastModifiedDate;
    }

    public void setLastModifiedDate(String lastModifiedDate) {
        this.lastModifiedDate = lastModifiedDate;
    }

    public boolean isSendToAgency() {
        return isSendToAgency;
    }

    public boolean isMoveShipper() {
        return moveShipper;
    }

    public void setLastModifiedUserId(int lastModifiedUserId) {
        this.lastModifiedUserId = lastModifiedUserId;
    }

    public int getKindServiceId() {
        return kindServiceId;
    }

    public void setKindServiceId(int kindServiceId) {
        this.kindServiceId = kindServiceId;
    }

    public int getCost() {
        return cost;
    }

    public void setCost(int cost) {
        this.cost = cost;
    }

    public int getExhibitionStatusId() {
        return exhibitionStatusId;
    }

    public void setExhibitionStatusId(int exhibitionStatusId) {
        this.exhibitionStatusId = exhibitionStatusId;
    }

    public String getDayReceive() {
        return dayReceive;
    }

    public void setDayReceive(String dayReceive) {
        this.dayReceive = dayReceive;
    }

    public String getLocationSender() {
        return locationSender;
    }

    public void setLocationSender(String locationSender) {
        this.locationSender = locationSender;
    }

    public String getLocationReceive() {
        return locationReceive;
    }

    public void setLocationReceive(String locationReceive) {
        this.locationReceive = locationReceive;
    }

    public boolean getSendToAgency() {
        return isSendToAgency;
    }

    public void setSendToAgency(boolean sendToAgency) {
        isSendToAgency = sendToAgency;
    }

    public int getAssignToAgencyId() {
        return assignToAgencyId;
    }

    public void setAssignToAgencyId(int assignToAgencyId) {
        this.assignToAgencyId = assignToAgencyId;
    }

    public int getGroupLump() {
        return groupLump;
    }

    public void setGroupLump(int groupLump) {
        this.groupLump = groupLump;
    }

    public int getNumberOfFailedDeliveries() {
        return numberOfFailedDeliveries;
    }

    public void setNumberOfFailedDeliveries(int numberOfFailedDeliveries) {
        this.numberOfFailedDeliveries = numberOfFailedDeliveries;
    }

    public int getAgencyId() {
        return agencyId;
    }

    public void setAgencyId(int agencyId) {
        this.agencyId = agencyId;
    }

    public int getKindTimeReceivedId() {
        return kindTimeReceivedId;
    }

    public void setKindTimeReceivedId(int kindTimeReceivedId) {
        this.kindTimeReceivedId = kindTimeReceivedId;
    }

    public int getRouterId() {
        return routerId;
    }

    public void setRouterId(int routerId) {
        this.routerId = routerId;
    }

    public boolean getMoveShipper() {
        return moveShipper;
    }

    public void setMoveShipper(boolean moveShipper) {
        this.moveShipper = moveShipper;
    }
}
