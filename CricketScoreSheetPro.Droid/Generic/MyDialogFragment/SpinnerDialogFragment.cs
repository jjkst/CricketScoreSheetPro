using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Droid.Generic.MyAdapter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Droid.Generic.MyDialogFragment
{
    public interface ISelectedSpinnerItemListener
    {
        void OnSelectSpinnerItem(String inputText);
    }

    public class SpinnerDialogFragment : DialogFragment
    {
        private ISelectedSpinnerItemListener _callback;

        private string _title;
        private List<string> _items;

        public SpinnerDialogFragment(ISelectedSpinnerItemListener callback, string title, List<string> list)
        {
            _title = title;
            _items = list;
            _callback = callback;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, _items.ToArray());

            LinearLayout container = new LinearLayout(this.Activity);
            LinearLayout.LayoutParams layoutparameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            layoutparameters.SetMargins(15, 20, 15, 5);
            Spinner userInput = new Spinner(this.Activity, SpinnerMode.Dialog)
            {                
                Focusable = true,
                LayoutParameters = layoutparameters,
                Adapter = adapter
            };
            container.AddView(userInput);

            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this.Activity);
            inputDialog.SetTitle(_title);
            inputDialog.SetView(container);
            inputDialog.SetPositiveButton("Save", (senderAlert, args) => {
                _callback.OnSelectSpinnerItem(userInput.SelectedItem.ToString());
                Dismiss();
                Toast.MakeText(this.Activity, "Saved.", ToastLength.Short).Show();
            });
            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Dismiss();
                Toast.MakeText(this.Activity, "Canceled.", ToastLength.Short).Show();
            });
            return inputDialog.Show();
        }
    }
}