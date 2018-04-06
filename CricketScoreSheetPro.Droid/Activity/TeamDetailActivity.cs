
using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Droid.Adapter;

namespace CricketScoreSheetPro.Droid.Activity
{
    [Activity(Label = "Team Detail", Theme = "@style/MyTheme")]
    public class TeamDetailActivity : BaseActivity
    {
        protected override int GetLayoutResourceId => Resource.Layout.TeamDetailView;

        private TeamViewModel ViewModel;
        private RecyclerView PlayerRecyclerView;
        private PlayerAdapter PlayerAdapter;

        private TextView Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetTitle(Resource.String.TeamDetailActivity);
            var teamId = Intent.GetStringExtra("TeamId");
            ViewModel = Singleton.Instance.TeamViewModel(teamId);

            Name = (TextView)FindViewById(Resource.Id.NameValue);
            Name.Click += EditTeamDetail;
            var addplayer = (Button)FindViewById(Resource.Id.addplayer);
            addplayer.Click += EditTeamDetail_AddPlayer;

            //Team 
            PlayerRecyclerView = FindViewById<RecyclerView>(Resource.Id.playerrecyclerview);
            PlayerRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
        }

        protected override void OnResume()
        {
            base.OnResume();
            Name.Text = ViewModel.Team.Name;

            PlayerAdapter = new PlayerAdapter(ViewModel.Team.Players.ToList());
            PlayerAdapter.ItemViewClick += OnItemViewClick;
            PlayerAdapter.ItemDeleteClick += OnItemDeleteClick;
            PlayerRecyclerView.SetAdapter(PlayerAdapter);
        }

        private void OnItemViewClick(object sender, string e)
        {
            throw new NotImplementedException();
        }

        private void OnItemDeleteClick(object sender, string userteamId)
        {
            ViewModel.Team.Players.Remove(ViewModel.Team.Players.FirstOrDefault(ut => ut.Id == userteamId));
            PlayerAdapter.Refresh(ViewModel.Team.Players);
            PlayerRecyclerView.SetAdapter(PlayerAdapter);
        }

        private void EditTeamDetail_AddPlayer(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditTeamDetail(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}