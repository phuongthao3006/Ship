package com.example.shipwaymoblieproject.data.remote;


import com.example.shipwaymoblieproject.data.model.Exhibition;
import com.example.shipwaymoblieproject.data.model.Users;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.Field;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;

public interface IUserApi {
    IUserApi iApi = RetrofitService.getRetrofit().create(IUserApi.class);
    @POST("Login/LoginMobile")
    Call<Users> login(@Body Users user);

    @GET("Login/Test")
    Call<String> getData();

    @GET("Login/GetExhibitionById")
    Call<Exhibition> getExhibitionById(@Query("exhibitionId") String exhibitionId);
    @GET("Api/CreateAccount")
    Call<Users> createAccount(
            @Field("email") String email,
            @Field("username") String username,
            @Field("pass") String pass,
            @Field("sdt") String sdt,
            @Field("name") String name
    );

}
