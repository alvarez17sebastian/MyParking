using System;
using Android.Content;
using Android.Widget;
using MyParking.Core.Constants;

namespace MyParking.Droid.Views.VehiclesList.PatternCommand
{
    public class EditAction:ICommand
    {
        private readonly Context context;

        public EditAction(Context context)
        {
            this.context = context;
        }

        public void Execute()
        {
            Toast.MakeText(context, MessageConstants.NotAvailableEdit, ToastLength.Short).Show();
        }
    }
}
