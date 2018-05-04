using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Droid.Generic.MyDialogFragment;
using System;

namespace CricketScoreSheetPro.Droid
{
    public class HomeFragment : Fragment, ISelectedRadioButtonListener
    {
        private Button mNewGameButton;
        private Button mSavedGameButton;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.HomeView, container, false);

            mNewGameButton = view.FindViewById<Button>(Resource.Id.newgameButton);
            mSavedGameButton = view.FindViewById<Button>(Resource.Id.savedgamesButton);

            mNewGameButton.Click += (object sender, EventArgs args) =>
            {
                var ft = ClearPreviousFragments("GameType");
                var gametype = new RadioButtonDialogFragment(this, "Select Game Type");
                gametype.Show(ft, "GameType");
            };

            //mSavedGameButton.Click += (object sender, EventArgs args) =>
            //{
            //    var matchActivity = new Intent(this.Activity, typeof(MatchesActivity));
            //    StartActivity(matchActivity);
            //};

            return view;
        }

        public void OnSelectedRadioButton(string title, string inputText)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            NewGameDialogFragment newGameDialog = new NewGameDialogFragment();
            newGameDialog.SetStyle(DialogFragmentStyle.NoTitle, 0);
            newGameDialog.Show(transaction, "newgame dialog");
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