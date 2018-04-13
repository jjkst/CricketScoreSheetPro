using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Droid.Adapter;
using CricketScoreSheetPro.Droid.Generic.MyAdapter;
using CricketScoreSheetPro.Droid.Generic.MyDialogFragment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Droid.Activity
{
    [Activity(Label = "Tournament Detail", Theme = "@style/MyTheme")]
    public class TournamentDetailActivity : BaseActivity , ISelectedSpinnerItemListener, IEditedTextListener, ISelectedDateListener
    {
        protected override int GetLayoutResourceId => Resource.Layout.TournamentDetailView;

        private TournamentViewModel ViewModel;

        private TextView Name;
        private TextView Sponsor;
        private TextView Status;
        private TextView StartDate;
        private TextView EntryFee;
        private RecyclerView PrizeRecyclerView;
        private TextViewWithDeleteActionAdapter PrizeAdapter;
        private RecyclerView FacilityRecyclerView;
        private TextViewWithDeleteActionAdapter FacilityAdapter;
        private RecyclerView VenueRecyclerView;
        private TextViewWithDeleteActionAdapter VenueAdapter;
        private RecyclerView TeamRecyclerView;
        private TeamAdapter TeamAdapter;

        private TextView SelectedTextView;
        private Button AddItemButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportActionBar.SetTitle(Resource.String.TournamentDetailActivity);
            var tournamentId = Intent.GetStringExtra("TournamentId");            
            ViewModel = new Driver().TournamentViewModel(tournamentId);

            Name = (TextView)FindViewById(Resource.Id.NameValue);            
            Sponsor = (TextView)FindViewById(Resource.Id.SponsorValue);
            Status = (TextView)FindViewById(Resource.Id.StatusValue);
            StartDate = (TextView)FindViewById(Resource.Id.StartDateValue);
            EntryFee = (TextView)FindViewById(Resource.Id.EntryFeeValue);

            Name.Click += EditTournamentDetail;
            Sponsor.Click += EditTournamentDetail;
            Status.Click += EditTournamentDetail;
            StartDate.Click += EditTournamentDetail;
            EntryFee.Click += EditTournamentDetail;

            var addPrize = (TextView)FindViewById(Resource.Id.addprizeitem);
            addPrize.Click += EditTournamentDetail_AddItem;
            var addFacility  = (Button)FindViewById(Resource.Id.addfacilityitem);
            addFacility.Click += EditTournamentDetail_AddItem;
            var addVenue = (Button)FindViewById(Resource.Id.addvenueitem);
            addVenue.Click += EditTournamentDetail_AddItem;
            var includeteam = (Button)FindViewById(Resource.Id.includeteam);
            includeteam.Click += EditTournamentDetail_IncludeTeam;

            //Set Prize
            PrizeRecyclerView = FindViewById<RecyclerView>(Resource.Id.prizerecyclerview);
            PrizeRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            //Set Facility
            FacilityRecyclerView = FindViewById<RecyclerView>(Resource.Id.facilityrecyclerview);
            FacilityRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            //Set Venue
            VenueRecyclerView = FindViewById<RecyclerView>(Resource.Id.venuerecyclerview);
            VenueRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            //Team 
            TeamRecyclerView = FindViewById<RecyclerView>(Resource.Id.teamrecyclerview);
            TeamRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
        }

        protected override void OnResume()
        {
            base.OnResume();
            Name.Text = ViewModel.Tournament.Name;
            Sponsor.Text = ViewModel.Tournament.Sponsor;
            Status.Text = ViewModel.Tournament.Status;
            StartDate.Text = ViewModel.Tournament.StartDate.ToShortDateString();
            EntryFee.Text = "$" + ViewModel.Tournament.EntryFee;

            PrizeAdapter = new TextViewWithDeleteActionAdapter(ViewModel.Tournament.Prizes.ToList());
            PrizeAdapter.ItemDeleteClick += DeletePrize;
            PrizeRecyclerView.SetAdapter(PrizeAdapter);

            FacilityAdapter = new TextViewWithDeleteActionAdapter(ViewModel.Tournament.Facilities.ToList());
            FacilityAdapter.ItemDeleteClick += DeleteFacility;
            FacilityRecyclerView.SetAdapter(FacilityAdapter);

            VenueAdapter = new TextViewWithDeleteActionAdapter(ViewModel.Tournament.Venues.ToList());
            VenueAdapter.ItemDeleteClick += DeleteVenue;
            VenueRecyclerView.SetAdapter(VenueAdapter);

            TeamAdapter = new TeamAdapter(ViewModel.Tournament.Teams.ToList());
            TeamAdapter.ItemDeleteClick += OnItemDeleteClick;
            TeamRecyclerView.SetAdapter(TeamAdapter);
        }

        private void DeletePrize(object sender, string value)
        {
            ViewModel.Tournament.Prizes.Remove(value);
            PrizeAdapter.Refresh(ViewModel.Tournament.Prizes);
            PrizeRecyclerView.SetAdapter(PrizeAdapter);
        }

        private void DeleteFacility(object sender, string value)
        {
            ViewModel.Tournament.Facilities.Remove(value);
            FacilityAdapter.Refresh(ViewModel.Tournament.Facilities);
            FacilityRecyclerView.SetAdapter(FacilityAdapter);
        }

        private void DeleteVenue(object sender, string value)
        {
            ViewModel.Tournament.Venues.Remove(value);
            VenueAdapter.Refresh(ViewModel.Tournament.Venues);
            VenueRecyclerView.SetAdapter(VenueAdapter);
        }

        private void EditTournamentDetail(object sender, EventArgs e)
        {
            var ft = ClearPreviousFragments("EditTournamentDetail");

            SelectedTextView = (TextView)sender;
            if (SelectedTextView.Id == Resource.Id.StartDateValue)
            {
                DatePickerDialogFragment startdate = new DatePickerDialogFragment(this);
                startdate.Show(ft, "EditTournamentDetail");
            }
            else
            {
                EditTextDialogFragment tournamentDetailEdit = null;
                switch (SelectedTextView.Id)
                {
                    case Resource.Id.NameValue:
                        tournamentDetailEdit = new EditTextDialogFragment(this, "Edit Name", "Enter name");
                        break;
                    case Resource.Id.SponsorValue:
                        tournamentDetailEdit = new EditTextDialogFragment(this, "Edit Sponsor", "Enter sponsor");
                        break;
                    case Resource.Id.StatusValue:
                        tournamentDetailEdit = new EditTextDialogFragment(this, "Edit Status", "Enter status");
                        break;
                    case Resource.Id.EntryFeeValue:
                        tournamentDetailEdit = new EditTextDialogFragment(this, "Edit Entry Fee", "Enter entry fee");
                        break;
                }
                tournamentDetailEdit.Show(ft, "EditTournamentDetail");
            }
        }

        private void EditTournamentDetail_AddItem(object sender, EventArgs e)
        {            
            var ft = ClearPreviousFragments("EditTournamentDetail");
            
            AddItemButton = (Button)sender;
            EditTextDialogFragment tournamentDetailEdit_list = null;
            switch (AddItemButton.Id)
            {
                case Resource.Id.addprizeitem:
                    tournamentDetailEdit_list = new EditTextDialogFragment(this, "Add Prize", "Enter prize");
                    break;
                case Resource.Id.addfacilityitem:
                    tournamentDetailEdit_list = new EditTextDialogFragment(this, "Add Facility", "Enter facility");
                    break;
                case Resource.Id.addvenueitem:
                    tournamentDetailEdit_list = new EditTextDialogFragment(this, "Add Venue", "Enter venue");
                    break;
            }
            tournamentDetailEdit_list.Show(ft, "EditTournamentDetail");
        }

        private void EditTournamentDetail_IncludeTeam(object sender, EventArgs e)
        {
            var teams = new List<UserTeam>
            {
                new UserTeam { Id = "1", Name = "JK" },
                new UserTeam { Id = "2", Name = "RK" }
            }; // Should be from TeamViewModel

            var teamnames = new List<string>();
            foreach(var team in teams)
                teamnames.Add(team.Name);
            SpinnerDialogFragment includeTeamFragment = new SpinnerDialogFragment(this, "Include Team", teamnames);
            includeTeamFragment.Show(ClearPreviousFragments("IncludeTeam"), "IncludeTeam");
        }

        private void OnItemDeleteClick(object sender, string userteamId)
        {
            ViewModel.Tournament.Teams.Remove(ViewModel.Tournament.Teams.FirstOrDefault(ut=>ut.Id == userteamId));
            TeamAdapter.Refresh(ViewModel.Tournament.Teams);
            TeamRecyclerView.SetAdapter(TeamAdapter);
        }

        public void OnSelectSpinnerItem(string inputText)
        {
            var teams = new List<UserTeam>
            {
                new UserTeam { Id = "1", Name = "JK" },
                new UserTeam { Id = "2", Name = "RK" }
            }; // Should be from TeamViewModel

            var team = teams.FirstOrDefault(t => t.Name == inputText);
            ViewModel.Tournament.Teams.Add(team);
            TeamAdapter.Refresh(ViewModel.Tournament.Teams);
            TeamRecyclerView.SetAdapter(TeamAdapter);
        }

        public void OnSelectedDate(DateTime selectedDate)
        {
            SelectedTextView.Text = selectedDate.ToShortDateString();
            ViewModel.Tournament.StartDate = selectedDate;
        }

        public void OnEnteredText(string title, string inputText)
        {
            switch (title)
            {
                case "Add Prize":
                    ViewModel.Tournament.Prizes.Add(inputText);
                    PrizeAdapter.Refresh(ViewModel.Tournament.Prizes);
                    PrizeRecyclerView.SetAdapter(PrizeAdapter);
                    break;
                case "Add Facility":
                    ViewModel.Tournament.Facilities.Add(inputText);
                    FacilityAdapter.Refresh(ViewModel.Tournament.Facilities);
                    FacilityRecyclerView.SetAdapter(FacilityAdapter);
                    break;
                case "Add Venue":
                    ViewModel.Tournament.Venues.Add(inputText);
                    VenueAdapter.Refresh(ViewModel.Tournament.Venues);
                    VenueRecyclerView.SetAdapter(VenueAdapter);
                    break;
                case "Edit Name":
                    ViewModel.Tournament.Name = inputText;
                    SelectedTextView.Text = inputText;
                    break;
                case "Edit Sponsor":
                    ViewModel.Tournament.Sponsor = inputText;
                    SelectedTextView.Text = inputText;
                    break;
                case "Edit Status":
                    ViewModel.Tournament.Status = inputText;
                    SelectedTextView.Text = inputText;
                    break;
                case "Edit Entry Fee":
                    ViewModel.Tournament.EntryFee = Convert.ToDecimal(inputText);
                    SelectedTextView.Text = inputText;
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
            ViewModel.UpdateTournament();
            base.OnBackPressed();
        }
    }
}