using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Droid.Activity;
using CricketScoreSheetPro.Droid.Adapter;
using CricketScoreSheetPro.Droid.Generic.MyDialogFragment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Droid
{
    public class TeamFragment : BaseFragment, IEditedTextListener
    {
        protected override int GetLayoutResourceId => Resource.Layout.TeamView;

        private TeamListViewModel ViewModel { get; set; }
        private TeamAdapter TeamAdapter { get; set; }
        private RecyclerView TeamsRecyclerView { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = new Driver().TeamListViewModel();
            this.Activity.InvalidateOptionsMenu();
            SetHasOptionsMenu(true);
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
            IEnumerable<Team> teams = ViewModel.Teams.Where(t => t.Name.ToLower().Contains(SearchEditText.Text.ToLower()));
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
            var ft = ClearPreviousFragments("AddTeam");
            var addTournament = new EditTextDialogFragment(this, "Add Team", "Enter Tournament name");
            addTournament.Show(ft, "AddTeam");
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

        public void OnEnteredText(string title, string inputText)
        {
            var newteam = ViewModel.AddTeam(inputText);
            var detailActivity = new Intent(this.Activity, typeof(TeamDetailActivity));
            detailActivity.PutExtra("TeamId", newteam);
            StartActivity(detailActivity);
        }
    }
}