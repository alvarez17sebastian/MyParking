using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MyParking.Droid;

namespace Parking.Droid.ListVehicles
{
    public class ViewHolderVehiclesAdapter : RecyclerView.ViewHolder
    {

        private VehiclesAdapter vehiclesAdapter;
        public TextView tvLicensePlate;
        public TextView tvType;
        public TextView tvDisplacement;
        public TextView tvDateOfEntry;
        public ImageView imageViewEditVehicle;
        private ImageView imageViewCheckOut;

        public ViewHolderVehiclesAdapter(View itemView,VehiclesAdapter vehiclesAdapter) : base(itemView)
        {
            this.vehiclesAdapter = vehiclesAdapter;
            InitUserInterface(itemView);
            SetListeners();
        }
        private void InitUserInterface(View itemView)
        {
            tvLicensePlate = itemView.FindViewById<TextView>(Resource.Id.textView_plate_vehiclesAdapter);
            tvType = itemView.FindViewById<TextView>(Resource.Id.textView_vechicleType_vehiclesAdapter);
            tvDisplacement = itemView.FindViewById<TextView>(Resource.Id.textView_displacement_vehiclesAdapter);
            tvDateOfEntry = itemView.FindViewById<TextView>(Resource.Id.textView_entryDate_vehiclesAdapter);
            imageViewEditVehicle = itemView.FindViewById<ImageView>(Resource.Id.imageView_editVehicle_vehiclesAdapter);
            imageViewCheckOut = itemView.FindViewById<ImageView>(Resource.Id.imageView_registerCheckOut_vehiclesAdapter);

        }

        private void SetListeners()
        {
            imageViewCheckOut.Click += (sender, e) => vehiclesAdapter.ClickItemAdapter(ActionsCode.ACTION_REGISTER_CHECKOUT,vehiclesAdapter.Vehicles[LayoutPosition]);
        }
    }
}
