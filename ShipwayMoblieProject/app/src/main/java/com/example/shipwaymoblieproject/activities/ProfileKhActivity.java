package com.example.shipwaymoblieproject.activities;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Dialog;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.shipwaymoblieproject.R;
import com.example.shipwaymoblieproject.adapters.ExAdapter;
import com.example.shipwaymoblieproject.data.model.Address;
import com.example.shipwaymoblieproject.data.model.District;
import com.example.shipwaymoblieproject.data.model.Exhibition;
import com.example.shipwaymoblieproject.data.model.Province;
import com.example.shipwaymoblieproject.data.model.Users;
import com.example.shipwaymoblieproject.data.model.Ward;
import com.example.shipwaymoblieproject.data.remote.IShipwayApi;
import com.example.shipwaymoblieproject.data.remote.IUserApi;

import java.util.ArrayList;
import java.util.List;

import okhttp3.OkHttpClient;
import okhttp3.logging.HttpLoggingInterceptor;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;

public class ProfileKhActivity extends AppCompatActivity {
    private ImageView imvIcon;
    private TextView tvName, tvAddress, tvEmail, tvPhone;
    private Button btnEdit;
    private Users user;

    private EditText edtName, edtAddress, edtEmail, edtPhone;
    private Spinner spinProvince, spinDistrict, spinWard;
    private Button btnEditDia, btnCancelDia;

    private List<Province> provinceList;
    private List<District> districtList;
    private List<Ward> wardList;
    private ArrayAdapter<Province> adapterProvince;
    private ArrayAdapter<District> adapterDistrict;
    private ArrayAdapter<Ward> adapterWard;
    private int provinceCurrentSelected = -1;
    private int districtCurrentSelected = -1;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);
        user = Users.getUserPrefer(this);
        onInit();
        events();
    }

    private void onInit() {
        imvIcon = findViewById(R.id.imv_icon);
        tvName = findViewById(R.id.tv_name);
        tvAddress = findViewById(R.id.tv_address);
        tvEmail = findViewById(R.id.tv_email);
        tvPhone = findViewById(R.id.tv_phone);
        btnEdit = findViewById(R.id.btn_edit);

        tvName.setText(user.getName());
        IShipwayApi.iApi.getAddressByWardId(user.getWardId()).enqueue(new Callback<Address>() {
            @Override
            public void onResponse(Call<Address> call, Response<Address> response) {
                Address address = response.body();
                if(address != null) {
                    tvAddress.setText(user.getAddress() + ", " + address.getWardName() + ", " + address.getDistrictName() + ", " + address.getProvinceName());
                }
            }

            @Override
            public void onFailure(Call<Address> call, Throwable t) {

            }
        });

        tvEmail.setText(user.getEmail());
        tvPhone.setText(user.getPhoneNumber());
    }
    private void events() {
        imvIcon.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                  startActivity(new Intent(ProfileKhActivity.this, TrangChuKHActivity.class));
            }
        });
        btnEdit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                showDialog();
            }
        });
    }

    private void showDialog() {
        Dialog dialog = new Dialog(ProfileKhActivity.this);
        dialog.requestWindowFeature(Window.FEATURE_NO_TITLE);
        dialog.setContentView(R.layout.dialog_update_info);

        Window window = dialog.getWindow();
        if(window == null) {
            return;
        }
        window.setLayout(WindowManager.LayoutParams.MATCH_PARENT, WindowManager.LayoutParams.WRAP_CONTENT);
        WindowManager.LayoutParams params = window.getAttributes();
        params.gravity = Gravity.CENTER;
        window.setAttributes(params);
        dialog.setCancelable(true);
        onInitDiaLog(dialog);
        eventsDialog(dialog);
        dialog.show();
    }

    private void onInitDiaLog(Dialog dialog) {
        edtName = dialog.findViewById(R.id.edt_name);
        spinProvince = dialog.findViewById(R.id.spin_province);
        spinDistrict = dialog.findViewById(R.id.spin_district);
        spinWard = dialog.findViewById(R.id.spin_ward);
        edtAddress = dialog.findViewById(R.id.edt_address);
        edtEmail = dialog.findViewById(R.id.edt_email);
        edtPhone = dialog.findViewById(R.id.edt_phone);
        btnCancelDia = dialog.findViewById(R.id.btn_cancel);
        btnEditDia = dialog.findViewById(R.id.btn_edit);

        provinceList = new ArrayList<>();
        districtList = new ArrayList<>();
        wardList = new ArrayList<>();
        // setdata
        edtName.setText(user.getName());
        //address
        edtAddress.setText(user.getAddress());
        edtEmail.setText(user.getEmail());
        edtPhone.setText(user.getPhoneNumber());

//      province
        IShipwayApi.iApi.getProvince().enqueue(new Callback<List<Province>>() {
            @Override
            public void onResponse(Call<List<Province>> call, Response<List<Province>> response) {
                List<Province> list = response.body();
                if(list != null && list.size() > 0) {
                    provinceList = list;
                    adapterProvince = new ArrayAdapter<>(ProfileKhActivity.this, android.R.layout.simple_list_item_1, provinceList);
                    spinProvince.setAdapter(adapterProvince);
                }
            }

            @Override
            public void onFailure(Call<List<Province>> call, Throwable t) {

            }
        });
        loadFirstAddress();
        adapterDistrict = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1, districtList);
        spinDistrict.setAdapter(adapterDistrict);
        adapterWard = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1, wardList);
        spinWard.setAdapter(adapterWard);
    }

    private void eventsDialog(Dialog dialog) {
        spinProvince.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                if(provinceCurrentSelected != i && provinceCurrentSelected != -1)
                    loadDistrictByProvinceId(provinceList.get(i).getProvinceId());
                provinceCurrentSelected = i;
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
            }
        });
        spinDistrict.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int iD, long l) {
                loadWardByDistrictId(districtList.get(iD).getDistrictId());
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
            }
        });



        btnCancelDia.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dialog.dismiss();
            }
        });

        btnEditDia.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String name = edtName.getText().toString();
                String address = edtAddress.getText().toString();
                String email = edtEmail.getText().toString();
                String phone = edtPhone.getText().toString();
                String wardId = ((Ward)spinWard.getSelectedItem()).getWardId();
                IShipwayApi.iApi.updateUser(user.getId(), name, wardId, address, email, phone).enqueue(new Callback<String>() {
                    @Override
                    public void onResponse(Call<String> call, Response<String> response) {
                        showMessage("Cập nhật thông tin thành công");
                        dialog.dismiss();
                        tvName.setText(name);
                        //set address
                        IShipwayApi.iApi.getAddressByWardId(wardId).enqueue(new Callback<Address>() {
                            @Override
                            public void onResponse(Call<Address> call, Response<Address> response) {
                                Address addressCall = response.body();
                                if(addressCall != null) {
                                    tvAddress.setText(address + ", " + addressCall.getWardName() + ", " + addressCall.getDistrictName() + ", " + addressCall.getProvinceName());
                                }
                            }

                            @Override
                            public void onFailure(Call<Address> call, Throwable t) {

                            }
                        });
                        tvEmail.setText(email);
                        tvPhone.setText(phone);

                        // update local
                        user.setName(name);
                        user.setAddress(address);
                        user.setEmail(email);
                        user.setPhoneNumber(phone);
                        user.setWardId(wardId);
                        Users.saveUserPrefer(ProfileKhActivity.this, user);




                    }

                    @Override
                    public void onFailure(Call<String> call, Throwable t) {

                    }
                });
            }
        });
    }

    private void loadFirstAddress() {
        IShipwayApi.iApi.getAddressByWardId(user.getWardId()).enqueue(new Callback<Address>() {
            @Override
            public void onResponse(Call<Address> call, Response<Address> response) {
                Address address = response.body();
                if(address != null) {
                    IShipwayApi.iApi.getProvince().enqueue(new Callback<List<Province>>() {
                        @Override
                        public void onResponse(Call<List<Province>> call, Response<List<Province>> response) {
                            List<Province> list = response.body();
                            int indexPro = -1;
                            if(list != null && list.size() > 0) {
                                for (int i = 0; i < provinceList.size(); i++) {
                                    if(provinceList.get(i).getProvinceId().equals(address.getProvinceId())) {
                                        indexPro = i;
                                        break;
                                    }
                                }
                                spinProvince.setSelection(indexPro);
                                provinceCurrentSelected = indexPro;
                            }
                        }

                        @Override
                        public void onFailure(Call<List<Province>> call, Throwable t) {

                        }
                    });

                    IShipwayApi.iApi.getDistrictByProvinceId(address.getProvinceId()).enqueue(new Callback<List<District>>() {
                        @Override
                        public void onResponse(Call<List<District>> call, Response<List<District>> response) {
                            List<District> list = response.body();
                            if(list != null && list.size() > 0) {
                                districtList.clear();
                                spinDistrict.setAdapter(null);
                                districtList = list;
                                adapterDistrict = new ArrayAdapter<>(ProfileKhActivity.this, android.R.layout.simple_list_item_1, districtList);
                                spinDistrict.setAdapter(adapterDistrict);


                                int indexDis = -1;
                                for (int i = 0; i < districtList.size(); i++) {
                                    if(districtList.get(i).getDistrictId().equals(address.getDistrictId())) {
                                        indexDis = i;
                                        break;
                                    }
                                }
                                spinDistrict.setSelection(indexDis);
                                districtCurrentSelected = indexDis;
                            }
                        }

                        @Override
                        public void onFailure(Call<List<District>> call, Throwable t) {

                        }
                    });

                    IShipwayApi.iApi.getWardByDistrictId(address.getDistrictId()).enqueue(new Callback<List<Ward>>() {
                        @Override
                        public void onResponse(Call<List<Ward>> call, Response<List<Ward>> response) {
                            List<Ward> list = response.body();
                            if(list != null && list.size() > 0) {
                                wardList.clear();
                                spinWard.setAdapter(null);
                                wardList = list;
                                adapterWard = new ArrayAdapter<>(ProfileKhActivity.this, android.R.layout.simple_list_item_1, wardList);
                                spinWard.setAdapter(adapterWard);


                                int indexWard = -1;
                                for (int i = 0; i < wardList.size(); i++) {
                                    if(wardList.get(i).getWardId().equals(address.getWardId())) {
                                        indexWard = i;
                                    }
                                }
                                spinWard.setSelection(indexWard);
                            }
                        }
                        @Override
                        public void onFailure(Call<List<Ward>> call, Throwable t) {

                        }
                    });
                }
            }
            @Override
            public void onFailure(Call<Address> call, Throwable t) {
            }
        });
    }

    private void loadDistrictByProvinceId(String provinceId) {
        IShipwayApi.iApi.getDistrictByProvinceId(provinceId).enqueue(new Callback<List<District>>() {
            @Override
            public void onResponse(Call<List<District>> call, Response<List<District>> response) {
                List<District> list = response.body();
                if(list != null && list.size() > 0) {
                    districtList.clear();
                    spinDistrict.setAdapter(null);
                    districtList = list;
                    adapterDistrict = new ArrayAdapter<>(ProfileKhActivity.this, android.R.layout.simple_list_item_1, districtList);
                    spinDistrict.setAdapter(adapterDistrict);
                }
            }

            @Override
            public void onFailure(Call<List<District>> call, Throwable t) {
            }
        });
    }

    private void loadWardByDistrictId(String districtId) {
        IShipwayApi.iApi.getWardByDistrictId(districtId).enqueue(new Callback<List<Ward>>() {
            @Override
            public void onResponse(Call<List<Ward>> call, Response<List<Ward>> response) {
                List<Ward> list = response.body();
                if(list != null && list.size() > 0) {
                    wardList.clear();
                    spinWard.setAdapter(null);
                    wardList = list;
                    adapterWard = new ArrayAdapter<>(ProfileKhActivity.this, android.R.layout.simple_list_item_1, wardList);
                    spinWard.setAdapter(adapterWard);
                }
            }

            @Override
            public void onFailure(Call<List<Ward>> call, Throwable t) {

            }
        });
    }
    private void showMessage(String content) {
        Toast.makeText(ProfileKhActivity.this, content, Toast.LENGTH_SHORT).show();
    }
    }
