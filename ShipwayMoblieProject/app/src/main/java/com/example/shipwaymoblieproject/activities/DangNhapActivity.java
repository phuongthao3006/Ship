package com.example.shipwaymoblieproject.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.shipwaymoblieproject.R;
import com.example.shipwaymoblieproject.constant.TypeUserConstant;
import com.example.shipwaymoblieproject.constant.UsersConstant;
import com.example.shipwaymoblieproject.data.model.Exhibition;
import com.example.shipwaymoblieproject.data.model.Users;
import com.example.shipwaymoblieproject.data.remote.IShipwayApi;
import com.example.shipwaymoblieproject.data.remote.IUserApi;
import com.example.shipwaymoblieproject.data.remote.RetrofitService;
import com.google.gson.Gson;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class DangNhapActivity extends AppCompatActivity {
    EditText edtEmail, edtPassword;
    Button btnLogin, btnSignUp;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_dang_nhap);

        onInit();
        events();
    }

    private void onInit() {
        edtEmail = findViewById(R.id.edt_email);
        edtPassword = findViewById(R.id.edt_password);
        btnLogin = findViewById(R.id.btn_login);
        btnSignUp = findViewById(R.id.btn_signup);
    }


    private void checkLogin(Users user) {
        IUserApi.iApi.login(user).enqueue(new Callback<Users>() {
            @Override
            public void onResponse(Call<Users> call, Response<Users> response) {
                Users user = response.body();
                if(user != null && (user.getTypeId() == TypeUserConstant.TYPEUSER_SHIPPER)) {
                    Users.saveUserPrefer(DangNhapActivity.this, user);

                    Intent intent = new Intent(DangNhapActivity.this, TrangChuActivity.class);
                    startActivity(intent);
                } else if (user != null && user.getTypeId() == TypeUserConstant.TYPEUSER_CUSTOMER) {
                    Users.saveUserPrefer(DangNhapActivity.this, user);

                    Intent intent = new Intent(DangNhapActivity.this, TrangChuKHActivity.class);
                    startActivity(intent);
                } else {
                    showMessage("Sai tên đăng nhập hoặc mật khẩu");
                }
            }

            @Override
            public void onFailure(Call<Users> call, Throwable t) {
                showMessage("Có lỗi xảy ra. Vui lòng thử lại sau.");
            }
        });
    }
private void events() {
    btnLogin.setOnClickListener(new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Users user = new Users();
            user.setUsername(edtEmail.getText().toString());
            user.setPassword(edtPassword.getText().toString());
            checkLogin(user);
        }
    });
    btnSignUp.setOnClickListener(new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            openSignUpActivity();
        }
    });
}


    private void openSignUpActivity() {
        Intent intent = new Intent(DangNhapActivity.this, DangKyActivity.class);
        startActivity(intent);
    }
    private void showMessage(String message) {
        Toast.makeText(DangNhapActivity.this, message, Toast.LENGTH_SHORT).show();
    }

}