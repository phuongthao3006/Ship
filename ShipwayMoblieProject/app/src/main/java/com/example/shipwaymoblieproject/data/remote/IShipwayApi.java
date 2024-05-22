package com.example.shipwaymoblieproject.data.remote;


import com.example.shipwaymoblieproject.data.model.Address;
import com.example.shipwaymoblieproject.data.model.AssignWork;
import com.example.shipwaymoblieproject.data.model.District;
import com.example.shipwaymoblieproject.data.model.Exhibition;
import com.example.shipwaymoblieproject.data.model.ExhibitionList;
import com.example.shipwaymoblieproject.data.model.ExhibitionStatus;
import com.example.shipwaymoblieproject.data.model.HistoryTrip;
import com.example.shipwaymoblieproject.data.model.Province;
import com.example.shipwaymoblieproject.data.model.Ward;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;

public interface IShipwayApi {
    IShipwayApi iApi = RetrofitService.getRetrofit().create(IShipwayApi.class);
    @GET("Api/GetExhibitionById")
    Call<Exhibition> getExhibitionById(@Query("exhibitionId") String exhibitionId);
    @GET("Api/GetAssignworkByShipperId")
    Call<List<AssignWork>> getAssignworkByShipperId(@Query("shipperId") int shipperId);
    @GET("Api/GetExhibitionStatusById")
    Call<ExhibitionStatus> getExhibitionStatusById(@Query("statusId") int statusId);
    @GET("Api/GetAddressByWardId")
    Call<Address> getAddressByWardId(@Query("wardId") String wardId);
    @POST("Api/UpdateStatusHistory")
    Call<String> updateStatusHistory(@Body HistoryTrip history);
    @GET("Api/UpdateStatusHistory")
    Call<String> updateStatusHistory(@Query("ExhibitionId") String exhibitionId,
                                     @Query("ExhibitionStatusId") int exhibitionStatusId,
                                     @Query("CurrentAddress") String currentAddress,
                                     @Query("Note") String note);
    @GET("Api/GetProvince")
    Call<List<Province>> getProvince();
    @GET("Api/GetDistrictByProvinceId")
    Call<List<District>> getDistrictByProvinceId(@Query("provinceId") String provinceId);
    @GET("Api/GetWardByDistrictId")
    Call<List<Ward>> getWardByDistrictId(@Query("districtId") String districtId);
    @GET("Api/GetStatusLast")
    Call<HistoryTrip> getStatusLast(@Query("exhibtionId") String exhibtionId);
    @GET("Api/UpdateUser")
    Call<String> updateUser(@Query("id") int id,
                                     @Query("name") String name,
                                     @Query("wardId") String wardId,
                                     @Query("address") String address,
                                     @Query("email") String email,
                                     @Query("phone") String phone);
    @GET("Api/SearchExhibitionByPhone")
    Call<List<Exhibition>> searchExhibitionByPhone(@Query("shipperId") int shipperId,
                            @Query("key") String key);
    @GET("Api/SearchExhibitionByExhibitionIdOrStatus")
    Call<ExhibitionList> searchExhibitionByExhibitionIdOrStatus(@Query("shipperId") int shipperId,
                                                                @Query("key") String key,
                                                                @Query("statusId") int statusId);
    @GET("Api/GetStatus")
    Call<List<ExhibitionStatus>> getStatus();
    @GET("Api/CheckPassword")
    Call<String> checkPassword(@Query("shipperId") int shipperId,
                                @Query("password") String password);
    @GET("Api/ChangePassword")
    Call<String> changePassword(@Query("shipperId") int shipperId,
                                @Query("password") String password);
    @GET("Api/GetDataByCustomerId")
    Call<List<Exhibition>> getDataByCustomerId(@Query("customerId") int customerId);

}
