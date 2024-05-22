package com.example.shipwaymoblieproject.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.shipwaymoblieproject.R;
import com.example.shipwaymoblieproject.activities.TrangChuKHActivity;
import com.example.shipwaymoblieproject.data.model.Address;
import com.example.shipwaymoblieproject.data.model.Exhibition;
import com.example.shipwaymoblieproject.data.model.ExhibitionStatus;
import com.example.shipwaymoblieproject.data.model.HistoryTrip;
import com.example.shipwaymoblieproject.data.remote.IShipwayApi;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ExAdapter extends RecyclerView.Adapter<ExAdapter.ExhibitionViewHolder1>{
    private List<Exhibition> mList;
    private TrangChuKHActivity mContext;
    private View mView;

    public ExAdapter(TrangChuKHActivity context, List<Exhibition> list) {
        mContext = context;
        mList = list;
    }
    @NonNull
    @Override
    public ExAdapter.ExhibitionViewHolder1 onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {

        mView = LayoutInflater.from(mContext).inflate(R.layout.item_exhibition_1, parent, false);

        return new ExAdapter.ExhibitionViewHolder1(mView);
    }



    //gán dữ liệu cho ViewHolder
    @Override
    public void onBindViewHolder(@NonNull ExhibitionViewHolder1 holder, int position) {
        Exhibition exhi = mList.get(position);
        if(exhi == null)
            return;

        setData1(holder, exhi);

    }

    private void setData1(ExAdapter.ExhibitionViewHolder1 holder, Exhibition exhi) {
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
                                // Handle status display logic if needed
                            }
                        }

                        @Override
                        public void onFailure(Call<ExhibitionStatus> call, Throwable t) {
                            // Handle failure if needed
                        }
                    });
                }
            }

            @Override
            public void onFailure(Call<HistoryTrip> call, Throwable t) {
                // Handle failure if needed
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
                // Handle failure if needed
            }
        });

        holder.tvPrice.setText(exhi.getCost() * 1000 + " vnđ");
    }
    @Override
    public int getItemCount() {
        return mList.size();
    }
    public class ExhibitionViewHolder1 extends RecyclerView.ViewHolder {
        private TextView tvId, tvStatus, tvName, tvAddress, tvPrice;


        public ExhibitionViewHolder1(@NonNull View itemView) {
            super(itemView);

            tvId = itemView.findViewById(R.id.tv_id);
            tvStatus = itemView.findViewById(R.id.tv_status);
            tvName = itemView.findViewById(R.id.tv_name);
            tvAddress = itemView.findViewById(R.id.tv_address);
            tvPrice = itemView.findViewById(R.id.tv_total_price);


        }
    }
}
