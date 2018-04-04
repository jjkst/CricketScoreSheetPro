using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Droid.Generic.MyAdapter;
using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Droid.Generic.MyDialogFragment
{
    public class SpinnerDialogFragment : DialogFragment
    {
        private ISelectedSpinnerItemListener callback;

        public interface ISelectedSpinnerItemListener
        {
            void OnSelectSpinnerItem(String inputText);
        }

        private static string[] _arrarylist;
        private Spinner spinner;

        public static SpinnerDialogFragment NewInstance(Bundle bundle, List<string> list)
        {
            _arrarylist = list.ToArray();
            SpinnerDialogFragment fragment = new SpinnerDialogFragment
            {
                Arguments = bundle
            };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.SpinnerDialogFragmentLayout, container, false);

            var adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, _arrarylist);
            spinner = view.FindViewById<Spinner>(Resource.Id.spinnerlist);
            spinner.Adapter = adapter;

            Button save = view.FindViewById<Button>(Resource.Id.save);
            save.Click += delegate {
                callback.OnSelectSpinnerItem(spinner.SelectedItem.ToString());
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