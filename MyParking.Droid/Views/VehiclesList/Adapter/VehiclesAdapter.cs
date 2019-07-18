using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using MyParking.core.Dto;
using MyParking.Droid;


namespace Parking.Droid.ListVehicles
{
    public class VehiclesAdapter : RecyclerView.Adapter
    {
        #region Statement of list

        public List<VehicleDto> Vehicles { get; internal set; }

        #endregion

        #region Actions

        public Action<int,VehicleDto> ClickItemAdapter { get; set; }

        #endregion

        #region Constructors

        public VehiclesAdapter()
        {
            Vehicles = new List<VehicleDto>();
        }

        public VehiclesAdapter(Action<int, VehicleDto> clickItemAdapter)
        {
            Vehicles = new List<VehicleDto>();
            this.ClickItemAdapter = clickItemAdapter;
        }

        #endregion

        #region Override adapter methods

        public override int ItemCount => Vehicles.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolderVehiclesAdapter viewHolder = holder as ViewHolderVehiclesAdapter;
            VehicleDto vehicleDto = Vehicles[position];

            viewHolder.GettvLicensePlate().Text = vehicleDto.Plate;
            viewHolder.GettvType().Text = vehicleDto.Type;
            viewHolder.GettvDisplacement().Text = vehicleDto.Displacement.ToString();
            viewHolder.GettvDateOfEntry().Text = vehicleDto.DateOfEntry.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_custom_adapter_vehicles, parent, false);
            ViewHolderVehiclesAdapter viewHolder = new ViewHolderVehiclesAdapter(view, this);
            return viewHolder;
        }

        #endregion

        #region Add and Delete methods on list

        public void Add(VehicleDto vehicle)
        {
            Vehicles.Add(vehicle);
            NotifyDataSetChanged();
        }
        public void AddList(List<VehicleDto> listVehicles)
        {
            this.Vehicles = listVehicles;
            NotifyDataSetChanged();
        }
        public void Delete(VehicleDto vehicle)
        {
            Vehicles.Remove(vehicle);
            NotifyDataSetChanged();
        }

        #endregion
    }
}
