package com.example.shipwaymoblieproject.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.shipwaymoblieproject.R;
import com.example.shipwaymoblieproject.data.model.Users;
import com.example.shipwaymoblieproject.data.remote.IUserApi;
import com.example.shipwaymoblieproject.data.remote.RetrofitService;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class DangKyActivity extends AppCompatActivity {

    EditText email, pass, repass, sdt, username, name;
    Button button;
    IUserApi apiShipway;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_dang_ky);
        initView();
        initControll();
        apiShipway = RetrofitService.getRetrofit().create(IUserApi.class);
    }

    private void initControll() {
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dangKi();
            }
        });
    }

    private void dangKi() {
        String str_username = username.getText().toString().trim();
        String str_email = email.getText().toString().trim();
        String str_pass = pass.getText().toString().trim();
        String str_repass = repass.getText().toString().trim();
        String str_name = name.getText().toString().trim();
        String str_sdt = sdt.getText().toString().trim();

        if (TextUtils.isEmpty(str_username)) {
            showMessage("Vui lòng nhập tên đăng nhập");
        } else if (TextUtils.isEmpty(str_email)) {
            showMessage("Vui lòng nhập Email");
        } else if (TextUtils.isEmpty(str_pass)) {
            showMessage("Vui lòng nhập mật khẩu");
        } else if (TextUtils.isEmpty(str_repass)) {
            showMessage("Vui lòng nhập xác nhận mật khẩu");
        } else if (TextUtils.isEmpty(str_sdt)) {
            showMessage("Vui lòng nhập số điện thoại");
        } else if (TextUtils.isEmpty(str_name)) {
            showMessage("Vui lòng nhập họ tên");
        } else {
            if (str_pass.equals(str_repass)) {
                Call<Users> call = apiShipway.createAccount(str_email, str_username, str_pass, str_sdt, str_name);
                call.enqueue(new Callback<Users>() {
                    @Override
                    public void onResponse(Call<Users> call, Response<Users> response) {
                        if (response.isSuccessful()) {
                            showMessage("Đăng ký thành công");
                            Intent intent = new Intent(DangKyActivity.this, DangNhapActivity.class);
                            startActivity(intent);
                        } else {
                            showMessage("Đăng ký thất bại");
                        }
                    }

                    @Override
                    public void onFailure(Call<Users> call, Throwable t) {
                        showMessage("Lỗi: " + t.getMessage());
                    }
                });
            } else {
                showMessage("Mật khẩu không khớp");
            }
        }
    }

    private void initView() {

        username = findViewById(R.id.edtUser);
        name = findViewById(R.id.edtHoTen);
        email = findViewById(R.id.edtEmail);
        pass = findViewById(R.id.edtMatKhau);
        repass = findViewById(R.id.edtXacNhan);
        button = findViewById(R.id.btnDangKy);
        sdt = findViewById(R.id.edtSDT);
        initControll();
    }

    private void showMessage(String content) {
        Toast.makeText(DangKyActivity.this, content, Toast.LENGTH_SHORT).show();
    }
}