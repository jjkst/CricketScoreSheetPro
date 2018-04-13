
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Droid.Generic.MyAdapter;
using CricketScoreSheetPro.Droid.Generic.MyDialogFragment;
using System;
using System.Linq;

namespace CricketScoreSheetPro.Droid.Activity
{
    [Activity(Label = "Team Detail", Theme = "@style/MyTheme")]
    public class TeamDetailActivity : BaseActivity, IEditedTextListener
    {
        protected override int GetLayoutResourceId => Resource.Layout.TeamDetailView;

        private TeamViewModel ViewModel;
        private TextView Name;
        private RecyclerView PlayerRecyclerView;
        private TextViewWithDeleteActionAdapter PlayerAdapter;

        private int SelectedPlayerNameIndex;
        private TextView SelectedTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportActionBar.SetTitle(Resource.String.TeamDetailActivity);
            var teamId = Intent.GetStringExtra("TeamId");
            ViewModel = new Driver().TeamViewModel(teamId);

            Name = (TextView)FindViewById(Resource.Id.NameValue);
            Name.Click += EditTeamName;
            var addplayer = (Button)FindViewById(Resource.Id.addplayer);
            addplayer.Click += AddPlayer;

            //Players 
            PlayerRecyclerView = FindViewById<RecyclerView>(Resource.Id.playerrecyclerview);
            PlayerRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
        }

        protected override void OnResume()
        {
            base.OnResume();
            Name.Text = ViewModel.Team.Name;

            PlayerAdapter = new TextViewWithDeleteActionAdapter(ViewModel.Team.Players.ToList());
            PlayerAdapter.ItemViewClick += OnItemViewClick;
            PlayerAdapter.ItemDeleteClick += OnItemDeleteClick;
            PlayerRecyclerView.SetAdapter(PlayerAdapter);
        }

        private void OnItemViewClick(object sender, string playername)
        {
            var ft = ClearPreviousFragments("EditPlayerName");
            SelectedPlayerNameIndex = ViewModel.Team.Players.IndexOf(playername); 
            var editPlayerName = new EditTextDialogFragment(this, "Edit Player Name", playername);
            editPlayerName.Show(ft, "EditPlayerName");
        }

        private void OnItemDeleteClick(object sender, string playername)
        {
            ViewModel.Team.Players.Remove(ViewModel.Team.Players.FirstOrDefault(ut => ut == playername));
            PlayerAdapter.Refresh(ViewModel.Team.Players);
            PlayerRecyclerView.SetAdapter(PlayerAdapter);
        }

        private void AddPlayer(object sender, EventArgs e)
        {
            var ft = ClearPreviousFragments("AddPlayer");
            var editPlayerName = new EditTextDialogFragment(this, "Add Player", "Enter player name");
            editPlayerName.Show(ft, "AddPlayer");
        }

        private void EditTeamName(object sender, EventArgs e)
        {
            var ft = ClearPreviousFragments("EditTeamName");
            SelectedTextView = (TextView)sender;
            var editTeamName = new EditTextDialogFragment(this, "Edit Team Name", "Enter team name");
            editTeamName.Show(ft, "EditTeamName");
        }

        public void OnEnteredText(string title, string inputText)
        {
            switch (title)
            {
                case "Add Player":
                    ViewModel.Team.Players.Add(inputText);
                    PlayerAdapter.Refresh(ViewModel.Team.Players);
                    PlayerRecyclerView.SetAdapter(PlayerAdapter);
                    break;
                case "Edit Team Name":
                    ViewModel.Team.Name = inputText;
                    SelectedTextView.Text = inputText;
                    break;
                case "Edit Player Name": 
                    ViewModel.Team.Players[SelectedPlayerNameIndex] = inputText;
                    PlayerAdapter.Refresh(ViewModel.Team.Players);
                    PlayerRecyclerView.SetAdapter(PlayerAdapter);
                    break;
            }            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                OnBackPressed();
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            ViewModel.UpdateTeam();
            base.OnBackPressed();
        }
    }
}