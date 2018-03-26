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

namespace CricketScoreSheetPro.Droid
{
    public class HomeFragment : Fragment
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

            //mNewGameButton.Click += (object sender, EventArgs args) =>
            //{
            //    FragmentTransaction transaction = FragmentManager.BeginTransaction();
            //    NewGameDialogFragment newGameDialog = new NewGameDialogFragment();
            //    newGameDialog.SetStyle(DialogFragmentStyle.NoTitle, 0);
            //    newGameDialog.Show(transaction, "newgame dialog");
            //};

            //mSavedGameButton.Click += (object sender, EventArgs args) =>
            //{
            //    var matchActivity = new Intent(this.Activity, typeof(MatchesActivity));
            //    StartActivity(matchActivity);
            //};

            return view;
        }
    }
}