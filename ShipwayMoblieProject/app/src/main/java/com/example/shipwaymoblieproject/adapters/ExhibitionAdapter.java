package com.example.shipwaymoblieproject.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.shipwaymoblieproject.R;
import com.example.shipwaymoblieproject.activities.TrangChuActivity;
import com.example.shipwaymoblieproject.constant.ExhibitionStatusConstant;
import com.example.shipwaymoblieproject.data.model.Address;
import com.example.shipwaymoblieproject.data.model.Exhibition;
import com.example.shipwaymoblieproject.data.model.ExhibitionStatus;
import com.example.shipwaymoblieproject.data.model.HistoryTrip;
import com.example.shipwaymoblieproject.data.remote.IShipwayApi;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ExhibitionAdapter extends RecyclerView.Adapter<ExhibitionAdapter.ExhibitionViewHolder>{
    private List<Exhibition> mList;
    private TrangChuActivity mContext;
    private View mView;
    public ExhibitionAdapter(TrangChuActivity context, List<Exhibition> list) {
        mContext = context;
        mList = list;
    }
    @NonNull
    @Override
    public ExhibitionViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        mView = LayoutInflater.from(mContext).inflate(R.layout.item_exhibition, parent, false);

        return new ExhibitionViewHolder(mView);
    }

    @Override
    public void onBindViewHolder(@NonNull ExhibitionViewHolder holder, int position) {
        Exhibition exhi = mList.get(position);
        if(exhi == null)
            return;

        setData(holder, exhi);
        events(holder, position);
    }

    private void setData(ExhibitionViewHolder holder, Exhibition exhi) {
        holder.tvId.setText(exhi.getExhibitionId());
        holder.tvName.setText(exhi.getReceiverName() + " - " + exhi.getReceiverPhoneNumber());

        // status
        IShipwayApi.iApi.getStatusLast(exhi.getExhibitionId()).enqueue(new Callback<HistoryTrip>() {
            @Override
            public void onResponse(Call<HistoryTrip> call, Response<HistoryTrip> response) {
                HistoryTrip his = response.body();
                if(his != null) {
                    IShipwayApi.iApi.getExhibitionStatusById(his.getExhibitionStatusId()).enqueue(new Callback<ExhibitionStatus>() {
                        @Override
                        public void onResponse(Call<ExhibitionStatus> call, Response<ExhibitionStatus> response) {
                            ExhibitionStatus status = response.body();
                            if(status != null) {
                                holder.tvStatus.setText(status.getName());
                                if(status.getExhibitionStatusId() == ExhibitionStatusConstant.STATUS_SUCCESS) {
                                    holder.btnUpdate.setVisibility(View.GONE);
                                    holder.btnSuccess.setVisibility(View.GONE);
                                    holder.tvStatusBtn.setVisibility(View.VISIBLE);
                                } else if(status.getExhibitionStatusId() == ExhibitionStatusConstant.STATUS_FAIL){
                                    holder.btnUpdate.setVisibility(View.VISIBLE);
                                    holder.btnSuccess.setVisibility(View.VISIBLE);
                                    holder.tvStatusBtn.setVisibility(View.GONE);
                                }
                            }
                        }

                        @Override
                        public void onFailure(Call<ExhibitionStatus> call, Throwable t) {

                        }
                    });
                }
            }

            @Override
            public void onFailure(Call<HistoryTrip> call, Throwable t) {

            }
        });


        // address
        IShipwayApi.iApi.getAddressByWardId(exhi.getWardReceiveId()).enqueue(new Callback<Address>() {
            @Override
            public void onResponse(Call<Address> call, Response<Address> response) {
                Address address = response.body();
                if(address != null) {
                    holder.tvAddress.setText(exhi.getReceiverAddress() + ", " + address.getWardName() + ", " + address.getDistrictName() + ", " + address.getProvinceName());
                }
            }

            @Override
            public void onFailure(Call<Address> call, Throwable t) {

            }
        });

        holder.tvPrice.setText(exhi.getCost() * 1000 + " vnđ");
    }

    private void events(ExhibitionViewHolder holder, int position) {
        holder.btnSuccess.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                mContext.updateStatus(position, ExhibitionStatusConstant.STATUS_SUCCESS);
                holder.btnUpdate.setVisibility(View.GONE);
                holder.btnSuccess.setVisibility(View.GONE);
                holder.tvStatusBtn.setVisibility(View.VISIBLE);
            }
        });

        holder.btnUpdate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                mContext.updateStatus(position, -1);
            }
        });
    }

    @Override
    public int getItemCount() {
        return mList.size();
    }

    public class ExhibitionViewHolder extends RecyclerView.ViewHolder {
        private TextView tvId, tvStatus, tvName, tvAddress, tvPrice;
        private Button btnSuccess, btnUpdate;
        private TextView tvStatusBtn;
        public ExhibitionViewHolder(@NonNull View itemView) {
            super(itemView);

            tvId = itemView.findViewById(R.id.tv_id);
            tvStatus = itemView.findViewById(R.id.tv_status);
            tvName = itemView.findViewById(R.id.tv_name);
            tvAddress = itemView.findViewById(R.id.tv_address);
            tvPrice = itemView.findViewById(R.id.tv_total_price);
            btnSuccess = itemView.findViewById(R.id.btn_success);
            btnUpdate = itemView.findViewById(R.id.btn_update);
            tvStatusBtn = itemView.findViewById(R.id.tv_status_btn);
        }
    }
}
