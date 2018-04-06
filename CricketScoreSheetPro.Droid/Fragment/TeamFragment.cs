using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Droid.Activity;
using CricketScoreSheetPro.Droid.Adapter;

namespace CricketScoreSheetPro.Droid
{
    public class TeamFragment : BaseFragment
    {
        protected override int GetLayoutResourceId => Resource.Layout.TeamView;

        private TeamListViewModel ViewModel { get; set; }
        private TeamAdapter TeamAdapter { get; set; }
        private RecyclerView TeamsRecyclerView { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = Singleton.Instance.TeamListViewModel();
            this.Activity.InvalidateOptionsMenu();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            SearchEditText = view.FindViewById<EditText>(Resource.Id.searchTeam);
            SearchEditText.TextChanged += SearchText_TextChanged;

            FloatingActionButton addTeam = view.FindViewById<FloatingActionButton>(Resource.Id.addteam);
            addTeam.Click += ShowAddTeamDialog;

            TeamsRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.teamsrecyclerview);
            var layoutManager = new LinearLayoutManager(this.Activity);
            var onScrollListener = new XamarinRecyclerViewOnScrollListener(layoutManager)
            {
                FloatingButton = addTeam
            };
            TeamsRecyclerView.AddOnScrollListener(onScrollListener);
            TeamsRecyclerView.SetLayoutManager(layoutManager);
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            TeamAdapter = new TeamAdapter(ViewModel.Teams);
            TeamAdapter.ItemViewClick += OnItemViewClick;
            TeamAdapter.ItemDeleteClick += OnItemDeleteClick;
            TeamsRecyclerView.SetAdapter(TeamAdapter);
        }

        protected override void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            IEnumerable<UserTeam> teams = ViewModel.Teams.Where(t => t.Name.ToLower().Contains(SearchEditText.Text.ToLower()));
            TeamAdapter.Refresh(teams);
            TeamsRecyclerView.SetAdapter(TeamAdapter);
        }

        private void OnItemViewClick(object sender, string teamId)
        {
            var detailActivity = new Intent(this.Activity, typeof(TeamDetailActivity));
            detailActivity.PutExtra("TeamId", teamId);
            StartActivity(detailActivity);
        }

        private void OnItemDeleteClick(object sender, string userteamId)
        {
            ViewModel.DeleteTeam(userteamId);
            TeamAdapter.Refresh(ViewModel.Teams);
            TeamsRecyclerView.SetAdapter(TeamAdapter);
        }

        private void ShowAddTeamDialog(object sender, EventArgs e)
        {
            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this.Activity);
            inputDialog.SetTitle("Add Team");

            EditText userInput = new EditText(Activity)
            {
                Hint = "Enter Team Name",
                Top = 5,
                Focusable = true,
                ShowSoftInputOnFocus = true
            };
            inputDialog.SetView(userInput);

            inputDialog.SetPositiveButton("Add", (senderAlert, args) => {
                var newteam = ViewModel.AddTeam(userInput.Text);
                var detailActivity = new Intent(this.Activity, typeof(TeamDetailActivity));
                detailActivity.PutExtra("TeamId", newteam.TeamId);
                StartActivity(detailActivity);
            });
            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this.Activity, "Canceled.", ToastLength.Short).Show();
            });
            inputDialog.Show();
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