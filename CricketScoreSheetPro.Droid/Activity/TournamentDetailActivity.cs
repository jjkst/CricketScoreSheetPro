﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
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

        private TextView SelectedTextView;
        private Button AddItemButton;
        private bool AddAction;

        private RecyclerView PrizeRecyclerView;
        private TextViewWithDeleteActionAdapter PrizeAdapter;
        private RecyclerView FacilityRecyclerView;
        private TextViewWithDeleteActionAdapter FacilityAdapter;
        private RecyclerView VenueRecyclerView;
        private TextViewWithDeleteActionAdapter VenueAdapter;
        private RecyclerView TeamRecyclerView;
        private TeamAdapter TeamAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var tournamentId = Intent.GetStringExtra("TournamentId");            
            ViewModel = Singleton.Instance.TournamentViewModel(tournamentId);

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
            string title = "Edit Tournament Detail";

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
                        tournamentDetailEdit = new EditTextDialogFragment(this, title, "Edit name");
                        break;
                    case Resource.Id.SponsorValue:
                        tournamentDetailEdit = new EditTextDialogFragment(this, title, "Edit sponsor");
                        break;
                    case Resource.Id.StatusValue:
                        tournamentDetailEdit = new EditTextDialogFragment(this, title, "Edit status");
                        break;
                    case Resource.Id.EntryFeeValue:
                        tournamentDetailEdit = new EditTextDialogFragment(this, title, "Edit entry fee");
                        break;
                }
                tournamentDetailEdit.Show(ft, "EditTournamentDetail");
            }
        }

        private void EditTournamentDetail_AddItem(object sender, EventArgs e)
        {            
            var ft = ClearPreviousFragments("EditTournamentDetail");
            string title = "Edit Tournament Detail";
            
            AddItemButton = (Button)sender;
            EditTextDialogFragment tournamentDetailEdit_list = null;
            switch (AddItemButton.Id)
            {
                case Resource.Id.addprizeitem:
                    tournamentDetailEdit_list = new EditTextDialogFragment(this, title, "Enter prize");
                    break;
                case Resource.Id.addfacilityitem:
                    tournamentDetailEdit_list = new EditTextDialogFragment(this, title, "Enter facility");
                    break;
                case Resource.Id.addvenueitem:
                    tournamentDetailEdit_list = new EditTextDialogFragment(this, title, "Enter venue");
                    break;
            }
            tournamentDetailEdit_list.Show(ft, "EditTournamentDetail");
            AddAction = true;
        }

        private void EditTournamentDetail_IncludeTeam(object sender, EventArgs e)
        {
            SpinnerDialogFragment includeTeamFragment = new SpinnerDialogFragment(this, 
                new Dictionary<string, string>() { { "1", "JK" } });
            includeTeamFragment.Show(ClearPreviousFragments("InclueTeam"), "InclueTeam");
        }

        private void OnItemDeleteClick(object sender, string userteamId)
        {
            ViewModel.Tournament.Teams.Remove(ViewModel.Tournament.Teams.FirstOrDefault(ut=>ut.Id == userteamId));
            TeamAdapter.Refresh(ViewModel.Tournament.Teams);
            TeamRecyclerView.SetAdapter(TeamAdapter);
        }

        public void OnSelectSpinnerItem(string inputText)
        {
            View view = LayoutInflater.From(this).Inflate(Resource.Layout.TextViewWithDeleteActionRow, null);
            view.Tag = inputText;
            TextView item = (TextView)view.FindViewById(Resource.Id.itemvalue);
            item.Text = inputText;

            Button deleteitem = (Button)view.FindViewById(Resource.Id.deleteitem);            
            LinearLayout teamlist = (LinearLayout)FindViewById(Resource.Id.teamlist);
            teamlist.AddView(view);
        }

        public void OnSelectedDate(DateTime selectedDate)
        {
            SelectedTextView.Text = selectedDate.ToShortDateString();
            ViewModel.Tournament.StartDate = selectedDate;
        }

        public void OnEnteredText(string inputText)
        {
            if (AddAction)
            {
                switch (AddItemButton.Id)
                {
                    case Resource.Id.addprizeitem:
                        ViewModel.Tournament.Prizes.Add(inputText);
                        PrizeAdapter.Refresh(ViewModel.Tournament.Prizes);
                        PrizeRecyclerView.SetAdapter(PrizeAdapter);
                        break;
                    case Resource.Id.addfacilityitem:
                        ViewModel.Tournament.Facilities.Add(inputText);
                        FacilityAdapter.Refresh(ViewModel.Tournament.Facilities);
                        FacilityRecyclerView.SetAdapter(FacilityAdapter);
                        break;
                    case Resource.Id.addvenueitem:
                        ViewModel.Tournament.Venues.Add(inputText);
                        VenueAdapter.Refresh(ViewModel.Tournament.Venues);
                        VenueRecyclerView.SetAdapter(VenueAdapter);
                        break;
                }
            }
            else
            {          
                SelectedTextView.Text = inputText;
                switch (SelectedTextView.Id)
                {
                    case Resource.Id.NameValue:
                        ViewModel.Tournament.Name = inputText;
                        break;
                    case Resource.Id.SponsorValue:
                        ViewModel.Tournament.Sponsor = inputText;
                        break;
                    case Resource.Id.StatusValue:
                        ViewModel.Tournament.Status = inputText;
                        break;
                    case Resource.Id.EntryFeeValue:
                        ViewModel.Tournament.EntryFee = Convert.ToDecimal(inputText);
                        break;
                }
            }
        }

        private FragmentTransaction ClearPreviousFragments(string tag)
        {
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            Fragment prev = FragmentManager.FindFragmentByTag(tag);
            if (prev != null)
                ft.Remove(prev);
            ft.AddToBackStack(null);
            return ft;
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