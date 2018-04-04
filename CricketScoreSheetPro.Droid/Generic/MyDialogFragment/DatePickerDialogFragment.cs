using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace CricketScoreSheetPro.Droid.Generic.MyDialogFragment
{
    public interface ISelectedDateListener
    {
        void OnSelectedDate(DateTime selectedDate);
    }

    public class DatePickerDialogFragment : DialogFragment
    {
        private ISelectedDateListener _callback;

        public DatePickerDialogFragment (ISelectedDateListener callback)
        {
            _callback = callback;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DatePicker datepicker = new DatePicker(this.Activity)
            {
                Top = 5,
                Focusable = true,
                DateTime = DateTime.Today
            };

            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this.Activity);
            inputDialog.SetView(datepicker);
            inputDialog.SetPositiveButton("Save", (senderAlert, args) => {
                _callback.OnSelectedDate(datepicker.DateTime);
                Toast.MakeText(this.Activity, "Saved.", ToastLength.Short).Show();
            });
            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this.Activity, "Canceled.", ToastLength.Short).Show();
            });
            return inputDialog.Show();
        }
    }
}