using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;

namespace CricketScoreSheetPro.Droid.Activity
{
    [Activity(Label = "BaseActivity")]
    public abstract class BaseActivity : AppCompatActivity
    {
        protected abstract int GetLayoutResourceId { get; }
        protected Toolbar Toolbar { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(GetLayoutResourceId);

            // Initialize toolbar
            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(Toolbar);
            SupportActionBar.SetTitle(Resource.String.ApplicationName);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.home:
                    OnBackPressed();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed()
        {
            if (FragmentManager.BackStackEntryCount != 0)
                FragmentManager.PopBackStack();
            else
                base.OnBackPressed();
        }

        protected FragmentTransaction ClearPreviousFragments(string tag)
        {
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            Fragment prev = FragmentManager.FindFragmentByTag(tag);
            if (prev != null)
                ft.Remove(prev);
            ft.AddToBackStack(null);
            return ft;
        }
    }
}