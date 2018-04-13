using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace CricketScoreSheetPro.Droid
{
    public abstract class BaseFragment : Fragment
    {
        protected abstract int GetLayoutResourceId { get; }
        protected abstract void SearchText_TextChanged(object sender, TextChangedEventArgs e);
        protected EditText SearchEditText { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(GetLayoutResourceId, container, false);
            return view;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
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