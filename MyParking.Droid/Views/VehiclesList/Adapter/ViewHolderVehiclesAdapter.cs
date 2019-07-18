using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MyParking.Droid;

namespace Parking.Droid.ListVehicles
{
    public class ViewHolderVehiclesAdapter : RecyclerView.ViewHolder
    {
        #region Statement UI

        private readonly VehiclesAdapter vehiclesAdapter;
        private TextView tvLicensePlate;
        private TextView tvType;
        private TextView tvDisplacement;
        private TextView tvDateOfEntry;
        private ImageView imageViewEditVehicle;
        private ImageView imageViewCheckOut;
        private ImageView imageViewDeleteVehicle;

        #endregion

        #region Constructors

        public ViewHolderVehiclesAdapter(View itemView,VehiclesAdapter vehiclesAdapter) : base(itemView)
        {
            this.vehiclesAdapter = vehiclesAdapter;
            InitUserInterface(itemView);
            SetListeners();
        }

        #endregion

        #region Initialization UI

        private void InitUserInterface(View itemView)
        {
            tvLicensePlate = itemView.FindViewById<TextView>(Resource.Id.textView_plate_vehiclesAdapter);
            tvType = itemView.FindViewById<TextView>(Resource.Id.textView_vechicleType_vehiclesAdapter);
            tvDisplacement = itemView.FindViewById<TextView>(Resource.Id.textView_displacement_vehiclesAdapter);
            tvDateOfEntry = itemView.FindViewById<TextView>(Resource.Id.textView_entryDate_vehiclesAdapter);
            imageViewEditVehicle = itemView.FindViewById<ImageView>(Resource.Id.imageView_editVehicle_vehiclesAdapter);
            imageViewCheckOut = itemView.FindViewById<ImageView>(Resource.Id.imageView_registerCheckOut_vehiclesAdapter);
            imageViewDeleteVehicle = itemView.FindViewById<ImageView>(Resource.Id.imageView_deleteVehicle_vehiclesAdapter);

        }

        #endregion

        #region Getters of the UI

        public TextView GettvLicensePlate()
        {
            return this.tvLicensePlate;
        }

        public TextView GettvType()
        {
            return this.tvType;
        }

        public TextView GettvDisplacement()
        {
            return this.tvDisplacement;
        }

        public TextView GettvDateOfEntry()
        {
            return this.tvDateOfEntry;
        }

        public ImageView GetimageViewEditVehicle()
        {
            return this.imageViewEditVehicle;
        }

        public ImageView GetimageViewCheckOut()
        {
            return this.imageViewCheckOut;
        }

        public ImageView GetimageViewDeleteVehicle()
        {
            return this.imageViewDeleteVehicle;
        }

        #endregion

        #region Click events

        private void SetListeners()
        {
            imageViewCheckOut.Click += (sender, e) => vehiclesAdapter.
                                                      ClickItemAdapter((int)ActionCodes.actionRegisterCheckout,
                                                      vehiclesAdapter.Vehicles[LayoutPosition]);

            imageViewEditVehicle.Click += (sender, e) => vehiclesAdapter.
                                                         ClickItemAdapter((int)ActionCodes.actionEditVehicle,
                                                         vehiclesAdapter.Vehicles[LayoutPosition]);

            imageViewDeleteVehicle.Click += (sender, e) => vehiclesAdapter.
                                                           ClickItemAdapter((int)ActionCodes.actionDeleteVehicle,
                                                           vehiclesAdapter.Vehicles[LayoutPosition]);
        }

        #endregion
    }
}
