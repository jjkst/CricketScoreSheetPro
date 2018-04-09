
using System;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using CricketScoreSheetPro.Droid.Adapter;

namespace CricketScoreSheetPro.Droid.Activity
{
    [Activity(Label = "MatchListActivity")]
    public class MatchListActivity : BaseActivity
    {
        protected override int GetLayoutResourceId => Resource.Layout.MatchView;

        private RecyclerView MatchRecyclerView;
        private MatchAdapter MatchAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var isMatchComplete = Intent.GetBooleanExtra("CompletedMatches", false);
            var title = isMatchComplete ? "Completed Matches" : "Saved Matches";
            SupportActionBar.Title = title;

            //Matches 
            MatchRecyclerView = FindViewById<RecyclerView>(Resource.Id.matchList);
            MatchRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
        }

        protected override void OnResume()
        {
            base.OnResume();

            MatchAdapter = new MatchAdapter(null);
            MatchAdapter.ItemViewClick += OnItemViewClick;
            MatchRecyclerView.SetAdapter(MatchAdapter);
        }

        private void OnItemViewClick(object sender, string e)
        {
            throw new NotImplementedException();
        }
    }
}