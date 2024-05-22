package com.example.shipwaymoblieproject.activities;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.annotation.SuppressLint;
import android.app.Dialog;
import android.content.Intent;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.Gravity;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.shipwaymoblieproject.R;
import com.example.shipwaymoblieproject.adapters.ExhibitionAdapter;
import com.example.shipwaymoblieproject.constant.ExhibitionStatusConstant;
import com.example.shipwaymoblieproject.constant.TypeUserConstant;
import com.example.shipwaymoblieproject.data.model.Address;
import com.example.shipwaymoblieproject.data.model.AssignWork;
import com.example.shipwaymoblieproject.data.model.Exhibition;
import com.example.shipwaymoblieproject.data.model.ExhibitionList;
import com.example.shipwaymoblieproject.data.model.ExhibitionStatus;
import com.example.shipwaymoblieproject.data.model.HistoryTrip;
import com.example.shipwaymoblieproject.data.model.Province;
import com.example.shipwaymoblieproject.data.model.Users;
import com.example.shipwaymoblieproject.data.remote.IShipwayApi;
import com.example.shipwaymoblieproject.data.remote.IUserApi;
import com.google.android.material.navigation.NavigationView;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.reflect.TypeToken;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TrangChuActivity extends AppCompatActivity {
    DrawerLayout drawerLayout;
    ImageView imgSidebar;
    EditText edtSearch;
    RecyclerView recyclerView;
    Spinner spStatus;
    Button btnFilter;

    NavigationView navigationView;
    TextView navName;

    EditText edtPasswordOld, edtPasswordNew, edtRetype;
    Button btnBackDia, btnChangeDia;

    private List<Exhibition> mListExhibition;
    private List<ExhibitionStatus> mListStatus;
    private ExhibitionAdapter adapter;
    private ArrayAdapter<ExhibitionStatus> adapterStatus;

    private Spinner spintStatus;
    private EditText noteDia;
    private Button btnCancelDia, btnUpdateDia;
    private List<ExhibitionStatus> listStatus;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_trang_chu);


        onInit();
        loadData();
        events();
    }

    private void onInit() {
        drawerLayout = findViewById(R.id.drl_main);
        imgSidebar = findViewById(R.id.img_sidebar);
        edtSearch = findViewById(R.id.edt_search);
        recyclerView = findViewById(R.id.rcv_exhibis);
//        spStatus = findViewById(R.id.spin_status);
        btnFilter = findViewById(R.id.btn_filter);
        navigationView = findViewById(R.id.navView);
        navigationView.bringToFront();
        navName = navigationView.getHeaderView(0).findViewById(R.id.tv_name);

        LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
        recyclerView.setLayoutManager(linearLayoutManager);
        mListExhibition = new ArrayList<>();
        adapter = new ExhibitionAdapter(this, mListExhibition);
        recyclerView.setAdapter(adapter);
    }

    private void loadData() {
//        IShipwayApi.iApi.getStatus().enqueue(new Callback<List<ExhibitionStatus>>() {
//            @Override
//            public void onResponse(Call<List<ExhibitionStatus>> call, Response<List<ExhibitionStatus>> response) {
//                List<ExhibitionStatus> list = response.body();
//                mListStatus = list;
//                mListStatus.remove(0);
//                mListStatus.remove(1);
//
//                mListStatus.add(0, new ExhibitionStatus(-1, "Tất cả trạng thái"));
//                adapterStatus = new ArrayAdapter<>(TrangChuActivity.this, android.R.layout.simple_list_item_1, mListStatus);
//                spStatus.setAdapter(adapterStatus);
//            }
//
//            @Override
//            public void onFailure(Call<List<ExhibitionStatus>> call, Throwable t) {
//
//            }
//        });

        IShipwayApi.iApi.getAssignworkByShipperId(Users.getUserPrefer(this).getId()).enqueue(new Callback<List<AssignWork>>() {
            @Override
            public void onResponse(Call<List<AssignWork>> call, Response<List<AssignWork>> response) {
                List<AssignWork> listAssign = response.body();
                if(listAssign != null && listAssign.size() > 0) {
                    for(AssignWork assign : response.body()) {
                        if(assign != null) {
                            IShipwayApi.iApi.getExhibitionById(assign.getExhibitionId()).enqueue(new Callback<Exhibition>() {
                                @Override
                                public void onResponse(Call<Exhibition> call, Response<Exhibition> response) {
                                    Exhibition exhi = response.body();
                                    if(exhi != null) {
                                        mListExhibition.add(exhi);
                                        adapter.notifyDataSetChanged();
                                    }
                                }

                                @Override
                                public void onFailure(Call<Exhibition> call, Throwable t) {
                                    Log.e("Exhibition", t.getMessage().toString());
                                }
                            });
                        }
                    }
                }
            }

            @Override
            public void onFailure(Call<List<AssignWork>> call, Throwable t) {
                Log.e("Assignwork", t.getMessage().toString());
            }
        });
    }

    private void events() {
        imgSidebar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Users user = Users.getUserPrefer(TrangChuActivity.this);
                navName.setText(user.getName());
                if (drawerLayout.isDrawerOpen(GravityCompat.END)) {
                    drawerLayout.closeDrawer(GravityCompat.END);
                } else {
                    drawerLayout.openDrawer(GravityCompat.END);
                }
            }
        });

        navigationView.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                int id = item.getItemId();
                if(id == R.id.nav_manager_exhibi) {
//            startActivities(new Intent(TrangChuActivity.this, TrangChuActivity.class));
                } else if(id == R.id.nav_profile) {
                    startActivity(new Intent(TrangChuActivity.this, ProfileActivity.class));
                    finish();
                } else if(id == R.id.nav_change_password) {
                    showDialogChangePass();
                } else if(id == R.id.nav_logout) {
                    logOut();
                }
                return true;
            }
        });

        btnFilter.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String exhiId = edtSearch.getText().toString();
//                int statusId = ((ExhibitionStatus)spStatus.getSelectedItem()).getExhibitionStatusId();

                searchExhibitionByExhibitionIdOrStatus(exhiId);
            }
        });
    }

    private void showDialogChangePass() {
        Dialog dialog = new Dialog(TrangChuActivity.this);
        dialog.requestWindowFeature(Window.FEATURE_NO_TITLE);
        dialog.setContentView(R.layout.dialog_change_pass);

        Window window = dialog.getWindow();
        if(window == null) {
            return;
        }
        window.setLayout(WindowManager.LayoutParams.MATCH_PARENT, WindowManager.LayoutParams.WRAP_CONTENT);

        WindowManager.LayoutParams params = window.getAttributes();
        params.gravity = Gravity.CENTER;
        window.setAttributes(params);

        dialog.setCancelable(true);

        onInitDiaLogChangePass(dialog);
        eventsDialogChangePass(dialog);


        dialog.show();
    }
    private void onInitDiaLogChangePass(Dialog dialog) {
        edtPasswordOld = dialog.findViewById(R.id.passwordOld);
        edtPasswordNew = dialog.findViewById(R.id.passwordChange);
        edtRetype = dialog.findViewById(R.id.repassword);
        btnBackDia = dialog.findViewById(R.id.backbtn);
        btnChangeDia = dialog.findViewById(R.id.changePasswordBtn);
    }
    private void eventsDialogChangePass(Dialog dialog) {
        btnBackDia.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dialog.dismiss();
            }
        });

        btnChangeDia.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String old = edtPasswordOld.getText().toString();
                String passNew = edtPasswordNew.getText().toString();
                String rePass = edtRetype.getText().toString();

                IShipwayApi.iApi.checkPassword(Users.getUserPrefer(TrangChuActivity.this).getId(), old).enqueue(new Callback<String>() {
                    @Override
                    public void onResponse(Call<String> call, Response<String> response) {
                        String password = response.body();
                        if(password == null || password.equals("")) {
                            showMessage("Mật khẩu cũ không đúng. Vui lòng nhập lại");
                            return;
                        }
                        if(passNew.equals("") || rePass.equals("") ) {
                            showMessage("Vui lòng nhập đầy đủ thông tin!");
                            return;
                        }
                        if(!passNew.equals(rePass)) {
                            showMessage("Nhập lại mật khẩu không đúng");
                        } else {
                            IShipwayApi.iApi.changePassword(Users.getUserPrefer(TrangChuActivity.this).getId(), passNew).enqueue(new Callback<String>() {
                                @Override
                                public void onResponse(Call<String> call, Response<String> response) {
                                    String passwordNew = response.body();
                                    if(passwordNew == null || passwordNew == "") {
                                        showMessage("Lỗi! Vui lòng liên hệ với trung tâm tư vấn");
                                    } else {
                                        showMessage("Cập nhật mật khẩu thành công");
                                        dialog.dismiss();
                                    }
                                }

                                @Override
                                public void onFailure(Call<String> call, Throwable t) {

                                }
                            });
                        }
                    }

                    @Override
                    public void onFailure(Call<String> call, Throwable t) {

                    }
                });
            }
        });
    }

    private void searchExhibitionByExhibitionIdOrStatus(String exhiId) {
        mListExhibition.clear();
        adapter.notifyDataSetChanged();
        IShipwayApi.iApi.getAssignworkByShipperId(Users.getUserPrefer(this).getId()).enqueue(new Callback<List<AssignWork>>() {
            @Override
            public void onResponse(Call<List<AssignWork>> call, Response<List<AssignWork>> response) {
                List<AssignWork> listAssign = response.body();
                if(listAssign != null && listAssign.size() > 0) {
                    for(AssignWork assign : response.body()) {
                        if(assign != null) {
                            IShipwayApi.iApi.getExhibitionById(assign.getExhibitionId()).enqueue(new Callback<Exhibition>() {
                                @Override
                                public void onResponse(Call<Exhibition> call, Response<Exhibition> response) {
                                    Exhibition exhi = response.body();
                                    if(exhi != null) {
                                        if(exhi.getExhibitionId().contains(exhiId))
                                        {
                                            mListExhibition.add(exhi);
                                            adapter.notifyDataSetChanged();
                                        }
                                    }
                                }

                                @Override
                                public void onFailure(Call<Exhibition> call, Throwable t) {
                                    Log.e("Exhibition", t.getMessage().toString());
                                }
                            });
                        }
                    }
                }
            }

            @Override
            public void onFailure(Call<List<AssignWork>> call, Throwable t) {
                Log.e("Assignwork", t.getMessage().toString());
            }
        });
    }

    public void updateStatus(int position, int status) {
        Exhibition exhibition = mListExhibition.get(position);
        if(status == -1) {
            showDialogUpdate(position, status);
        } else {
            IShipwayApi.iApi.getAddressByWardId(exhibition.getWardReceiveId()).enqueue(new Callback<Address>() {
                @Override
                public void onResponse(Call<Address> call, Response<Address> response) {
                    Address address = response.body();
                    if(address != null) {
                        HistoryTrip historyTrip = new HistoryTrip();
                        historyTrip.setExhibitionId(exhibition.getExhibitionId());
                        historyTrip.setCurrentAddress(exhibition.getReceiverAddress() + ", " + address.getWardName() + ", " + address.getDistrictName() + ", " + address.getProvinceName());
                        if(status == ExhibitionStatusConstant.STATUS_SUCCESS)
                            historyTrip.setExhibitionStatusId(ExhibitionStatusConstant.STATUS_SUCCESS);
                        updateHistory(historyTrip);
                    }
                }

                @Override
                public void onFailure(Call<Address> call, Throwable t) {

                }
            });
        }
    }

    private void showDialogUpdate(int position, int status) {
        Dialog dialog = new Dialog(TrangChuActivity.this);
        dialog.requestWindowFeature(Window.FEATURE_NO_TITLE);
        dialog.setContentView(R.layout.dialog_update_status);

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
        eventsDialog(dialog, position);


        dialog.show();
    }

    private void eventsDialog(Dialog dialog, int position) {
        btnCancelDia.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dialog.dismiss();
            }
        });
        btnUpdateDia.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExhibitionStatus status = listStatus.get(spintStatus.getSelectedItemPosition());
                String note = noteDia.getText().toString();
                HistoryTrip historyTrip = new HistoryTrip();
                historyTrip.setExhibitionId(mListExhibition.get(position).getExhibitionId());
                historyTrip.setExhibitionStatusId(status.getExhibitionStatusId());
                historyTrip.setNote(note);
                updateHistory(historyTrip);
                dialog.dismiss();
            }
        });
    }

    private void onInitDiaLog(Dialog dialog) {
        spintStatus = dialog.findViewById(R.id.spin_status);
        noteDia = dialog.findViewById(R.id.edit_text);
        btnCancelDia = dialog.findViewById(R.id.btn_cancel);
        btnUpdateDia = dialog.findViewById(R.id.btn_edit);
        listStatus = new ArrayList<>();

        IShipwayApi.iApi.getStatus().enqueue(new Callback<List<ExhibitionStatus>>() {
            @Override
            public void onResponse(Call<List<ExhibitionStatus>> call, Response<List<ExhibitionStatus>> response) {
                List<ExhibitionStatus> list = response.body();
                listStatus = list;
                ArrayAdapter<ExhibitionStatus> arrAdapter = new ArrayAdapter<>(TrangChuActivity.this, android.R.layout.simple_list_item_1, listStatus);
                spintStatus.setAdapter(arrAdapter);
            }

            @Override
            public void onFailure(Call<List<ExhibitionStatus>> call, Throwable t) {

            }
        });

    }

    public void updateHistory(HistoryTrip his) {
        IShipwayApi.iApi.updateStatusHistory(his.getExhibitionId(),
                                            his.getExhibitionStatusId(),
                                            his.getCurrentAddress(),
                                            his.getNote()).enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                String result = response.body();
                if(result != null && !result.equals("")) {
                    showMessage("Cập nhật trạng thái đơn hàng thành công!");
                    clear();
                    loadData();
                }
            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {
                Log.e("DatetimeError", t.getMessage().toString());
            }
        });
    }
    private void logOut() {
        Users.saveUserPrefer(this, null);
        startActivity(new Intent(this, DangNhapActivity.class));
    }
    private void showMessage(String content) {
        Toast.makeText(this, content, Toast.LENGTH_SHORT).show();
    }

    public void clear(){
        int size = mListExhibition.size();
        mListExhibition.clear();
        adapter.notifyItemRangeRemoved(0, size);
    }

}