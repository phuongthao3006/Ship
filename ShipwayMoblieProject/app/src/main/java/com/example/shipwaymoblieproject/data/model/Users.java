package com.example.shipwaymoblieproject.data.model;

import static android.content.Context.MODE_PRIVATE;

import android.content.Context;
import android.content.SharedPreferences;

import com.example.shipwaymoblieproject.constant.UsersConstant;
import com.google.gson.Gson;
import com.google.gson.annotations.SerializedName;

public class Users {
    @SerializedName("Id")
    private int id;
    @SerializedName("Username")
    private String username;
    @SerializedName("Password")
    private String password;
    @SerializedName("Email")
    private String email;
    @SerializedName("PhoneNumber")
    private String phoneNumber;
    @SerializedName("Name")
    private String name;
    @SerializedName("Address")
    private String address;
    @SerializedName("WardId")
    private String wardId;
    @SerializedName("TypeId")
    private int typeId;
    @SerializedName("AgencyId")
    private int agencyId;
    public Users() {
    }

    public Users(int id, String email, String phoneNumber, String name, String address, String wardId) {
        this.id = id;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.name = name;
        this.address = address;
        this.wardId = wardId;
    }

    public static void saveUserPrefer(Context context, Users user) {
        SharedPreferences userPrefer = context.getSharedPreferences(UsersConstant.USER_SHAREPREFERENCES, MODE_PRIVATE);
        SharedPreferences.Editor preferEdit = userPrefer.edit();
        Gson gson = new Gson();
        String json = gson.toJson(user);
        preferEdit.putString(UsersConstant.USER_DATA, json);
        preferEdit.apply();
    }

    public static Users getUserPrefer(Context context) {
        SharedPreferences userPrefer = context.getSharedPreferences(UsersConstant.USER_SHAREPREFERENCES, MODE_PRIVATE);
        String json = userPrefer.getString(UsersConstant.USER_DATA, "");
        Gson gson = new Gson();
        Users user = gson.fromJson(json, Users.class);
        return user;
    }
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getWardId() {
        return wardId;
    }

    public void setWardId(String wardId) {
        this.wardId = wardId;
    }

    public int getTypeId() {
        return typeId;
    }

    public void setTypeId(int typeId) {
        this.typeId = typeId;
    }

    public int getAgencyId() {
        return agencyId;
    }

    public void setAgencyId(int agencyId) {
        this.agencyId = agencyId;
    }
}
