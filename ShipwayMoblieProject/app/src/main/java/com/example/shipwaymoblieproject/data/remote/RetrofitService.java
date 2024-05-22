package com.example.shipwaymoblieproject.data.remote;

import static com.example.shipwaymoblieproject.configip.IpAddress.IP_ADDRESS1;
import static com.example.shipwaymoblieproject.configip.IpAddress.IP_ADDRESS2;
import static com.example.shipwaymoblieproject.configip.IpAddress.IP_ADDRESS3;
import static com.example.shipwaymoblieproject.configip.IpAddress.IP_ADDRESS4;
import static com.example.shipwaymoblieproject.configip.IpAddress.IP_ADDRESS5;
import static com.example.shipwaymoblieproject.configip.IpAddress.IP_ADDRESS6;
import static com.example.shipwaymoblieproject.configip.IpAddress.IP_ADDRESS7;

import okhttp3.OkHttpClient;
import okhttp3.logging.HttpLoggingInterceptor;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class RetrofitService {


    public static Retrofit getRetrofit() {
        HttpLoggingInterceptor httpLoggingInterceptor = new HttpLoggingInterceptor();
        httpLoggingInterceptor.setLevel(HttpLoggingInterceptor.Level.BODY);
        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .addInterceptor(httpLoggingInterceptor)
                .build();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("http://" + IP_ADDRESS7 + ":8089/")
                .client(okHttpClient)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        return retrofit;
    }

//    public static Retrofit getClient() {
//        HttpLoggingInterceptor interceptor = new HttpLoggingInterceptor();
//        interceptor.setLevel(HttpLoggingInterceptor.Level.BODY);
//        OkHttpClient client = new OkHttpClient.Builder().addInterceptor(interceptor).build();
//        Retrofit retrofit = new Retrofit.Builder()
//                .baseUrl("http://" + IP_ADDRESS1 + ":8089/")
//                .addConverterFactory(GsonConverterFactory.create())
//                .client(client)
//                .build();
//        return retrofit;
//    }
}
