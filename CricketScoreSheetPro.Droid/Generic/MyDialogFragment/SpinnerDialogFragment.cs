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
using CricketScoreSheetPro.Droid.Generic.MyAdapter;

namespace CricketScoreSheetPro.Droid.Generic.MyDialogFragment
{
    public class SpinnerDialogFragment : DialogFragment
    {
        public interface ISelectedSpinnerItemListener
        {
            void OnSelectSpinnerItem(String inputText);
        }

        static string[] _itemlist;

        private Spinner spinner;

        public static SpinnerDialogFragment NewInstance(Bundle bundle, List<string> list)
        {
            _itemlist = list.ToArray();
            SpinnerDialogFragment fragment = new SpinnerDialogFragment
            {
                Arguments = bundle
            };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.SpinnerDialogFragmentLayout, container, false);
            var adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, _itemlist);

            //Set list
            spinner = view.FindViewById<Spinner>(Resource.Id.spinnerlist);
            spinner.Adapter = adapter;

            Button save = view.FindViewById<Button>(Resource.Id.save);
            save.Click += delegate {
                ISelectedSpinnerItemListener parent = (ISelectedSpinnerItemListener)this.Activity;
                parent.OnSelectSpinnerItem(spinner.SelectedItem.ToString());
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