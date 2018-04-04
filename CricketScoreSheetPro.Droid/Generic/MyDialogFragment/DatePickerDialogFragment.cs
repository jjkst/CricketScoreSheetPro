using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CricketScoreSheetPro.Droid.Generic.MyDialogFragment
{
    public class DatePickerDialogFragment : DialogFragment
    {
        private ISelectedDateListener callback;

        public interface ISelectedDateListener
        {
            void OnSelectedDate(DateTime selectedDate);
        }

        public static DatePickerDialogFragment NewInstance(Bundle bundle)
        {
            DatePickerDialogFragment fragment = new DatePickerDialogFragment
            {
                Arguments = bundle
            };
            return fragment;
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
                callback.OnSelectedDate(datepicker.DateTime);
                Toast.MakeText(this.Activity, "Saved.", ToastLength.Short).Show();
            });
            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this.Activity, "Canceled.", ToastLength.Short).Show();
            });
            return inputDialog.Show();
        }
    }
}