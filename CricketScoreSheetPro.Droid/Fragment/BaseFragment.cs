using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Text;

namespace CricketScoreSheetPro.Droid
{
    public abstract class BaseFragment : Fragment
    {
        protected abstract int GetLayoutResourceId { get; }
        protected abstract void SearchText_TextChanged(object sender, TextChangedEventArgs e);
        protected EditText SearchEditText { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(GetLayoutResourceId, container, false);
            SearchEditText = view.FindViewById<EditText>(Resource.Id.searchTournament);
            SearchEditText.TextChanged += SearchText_TextChanged;

            return view;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.searchText)
            {
                if (SearchEditText.Visibility == ViewStates.Gone)
                    SearchEditText.Visibility = ViewStates.Visible;
                else SearchEditText.Visibility = ViewStates.Gone;
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        

    }
}