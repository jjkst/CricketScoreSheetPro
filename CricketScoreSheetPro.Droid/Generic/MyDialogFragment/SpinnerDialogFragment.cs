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

        private Dictionary<string, string> _dictionary;
        private Spinner spinner;

        public SpinnerDialogFragment(ISelectedSpinnerItemListener callback, Dictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
            _callback = callback;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.SpinnerDialogFragmentLayout, container, false);

            List<string> teamnames = new List<string>();
            for(int i=0; i < _dictionary.Count; i++)
                teamnames.Add(_dictionary.Values.ElementAt(i));

            var adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, teamnames.ToArray());
            spinner = view.FindViewById<Spinner>(Resource.Id.spinnerlist);
            spinner.Adapter = adapter;

            Button save = view.FindViewById<Button>(Resource.Id.save);
            save.Click += delegate {
                _callback.OnSelectSpinnerItem(spinner.SelectedItem.ToString());
                Dismiss();
                Toast.MakeText(Activity, "Saved", ToastLength.Short).Show();
            };

            Button cancel = view.FindViewById<Button>(Resource.Id.cancel);
            cancel.Click += delegate {
                Dismiss();
                Toast.MakeText(Activity, "Canceled", ToastLength.Short).Show();
            };

            return view;
        }

    }
}