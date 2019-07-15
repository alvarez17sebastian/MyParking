using System;
using Android.App;
using Android.Views;
using Android.Widget;

namespace MyParking.Droid
{
    public class CustomDialog
    {

        public void ShowCustomDialogInformationPayment(Activity activity,
                                                       string payment,
                                                       Action confirmAction, 
                                                       Action cancelAction)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(activity);
            View view = activity.LayoutInflater.Inflate(Resource.Layout.custom_dialog_payment, null);
            TextView textViewPayment = view.FindViewById<TextView>(Resource.Id.textView_paymentInformation_customDialogPayment);
            Button buttonConfirmPayment = view.FindViewById<Button>(Resource.Id.button_confirmPayment_customDialogPayment);
            Button buttonCancelPayment = view.FindViewById<Button>(Resource.Id.button_cancelPayment_customDialogPayment);
            builder.SetView(view);

            textViewPayment.Text = payment;

            AlertDialog dialog = builder.Create();
            dialog.SetCancelable(false);
            dialog.Show();

            buttonConfirmPayment.Click += (sender, e) => {
                dialog.Dismiss();
                confirmAction?.Invoke();
            };

            buttonCancelPayment.Click += (sender, e) => {
                dialog.Dismiss(); 
            };
        }
    }
}
