using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace CricketScoreSheetPro.Droid.Generic.MyDialogFragment
{
    public class EditTextDialogFragment : DialogFragment
    {
        private IEditedTextListener callback;

        public interface IEditedTextListener
        {
            void OnEnteredText(String inputText);
        }

        static string _title;
        static string _hint;
        public static EditTextDialogFragment NewInstance(Bundle bundle, string title, string hint)
        {
            _title = title;
            _hint = hint;
            EditTextDialogFragment fragment = new EditTextDialogFragment
            {
                Arguments = bundle
            };
            return fragment;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        { 
            EditText userInput = new EditText(this.Activity)
            {
                Hint = _hint,
                Top = 10,
                Focusable = true,
                ShowSoftInputOnFocus = true
            };

            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this.Activity);
            inputDialog.SetTitle(_title);
            inputDialog.SetView(userInput);
            inputDialog.SetPositiveButton("Save", (senderAlert, args) => {
                callback.OnEnteredText(userInput.Text);
                Toast.MakeText(this.Activity, "Saved.", ToastLength.Short).Show();
            });
            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this.Activity, "Canceled.", ToastLength.Short).Show();
            });
            return inputDialog.Show();
        }
    }
}