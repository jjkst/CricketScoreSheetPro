using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Droid.Generic.MyAdapter;
using CricketScoreSheetPro.Droid.Generic.MyDialogFragment;
using System;
using System.Collections.Generic;
using System.Linq;
using static CricketScoreSheetPro.Droid.Generic.MyDialogFragment.DatePickerDialogFragment;
using static CricketScoreSheetPro.Droid.Generic.MyDialogFragment.EditTextDialogFragment;
using static CricketScoreSheetPro.Droid.Generic.MyDialogFragment.SpinnerDialogFragment;

namespace CricketScoreSheetPro.Droid.Activity
{
    [Activity(Label = "Tournament Detail", Theme = "@style/MyTheme")]
    public class TournamentDetailActivity : BaseActivity , ISelectedSpinnerItemListener, IEditedTextListener, ISelectedDateListener
    {
        protected override int GetLayoutResourceId => Resource.Layout.TournamentDetailView;

        private Tournament _tournament;
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
            includeteam.Click += EditTournamentDetail_InclueTeam;

            //Set Prize
            PrizeRecyclerView = FindViewById<RecyclerView>(Resource.Id.prizerecyclerview);
            PrizeRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            //Set Facility
            FacilityRecyclerView = FindViewById<RecyclerView>(Resource.Id.facilityrecyclerview);
            FacilityRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            //Set Venue
            VenueRecyclerView = FindViewById<RecyclerView>(Resource.Id.venuerecyclerview);
            VenueRecyclerView.SetLayoutManager(new LinearLayoutManager(this));

        }

        protected override void OnResume()
        {
            base.OnResume();
            _tournament = new Tournament();
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
        }

        private void DeletePrize(object sender, string value)
        {
            _tournament.Prizes.Remove(value);
            PrizeAdapter.Refresh(_tournament.Prizes);
            PrizeRecyclerView.SetAdapter(PrizeAdapter);
        }

        private void DeleteFacility(object sender, string value)
        {
            _tournament.Facilities.Remove(value);
            FacilityAdapter.Refresh(_tournament.Facilities);
            PrizeRecyclerView.SetAdapter(FacilityAdapter);
        }

        private void DeleteVenue(object sender, string value)
        {
            _tournament.Prizes.Remove(value);
            VenueAdapter.Refresh(_tournament.Venues);
            VenueRecyclerView.SetAdapter(VenueAdapter);
        }

        private void EditTournamentDetail(object sender, EventArgs e)
        {
            var ft = ClearPreviousFragments("EditTournamentDetail");
            string title = "Edit Tournament Detail";

            SelectedTextView = (TextView)sender;
            if (SelectedTextView.Id == Resource.Id.StartDateValue)
            {
                DatePickerDialogFragment startdate = DatePickerDialogFragment.NewInstance(null);
                startdate.Show(ft, "EditTournamentDetail");
            }
            else
            {
                EditTextDialogFragment tournamentDetailEdit = null;
                switch (SelectedTextView.Id)
                {
                    case Resource.Id.NameValue:
                        tournamentDetailEdit = EditTextDialogFragment.NewInstance(null, title, "Edit name");
                        break;
                    case Resource.Id.SponsorValue:
                        tournamentDetailEdit = EditTextDialogFragment.NewInstance(null, title, "Edit sponsor");
                        break;
                    case Resource.Id.StatusValue:
                        tournamentDetailEdit = EditTextDialogFragment.NewInstance(null, title, "Edit status");
                        break;
                    case Resource.Id.EntryFeeValue:
                        tournamentDetailEdit = EditTextDialogFragment.NewInstance(null, title, "Edit entry fee");
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
                    tournamentDetailEdit_list = EditTextDialogFragment.NewInstance(null, title, "Enter prize");
                    break;
                case Resource.Id.addfacilityitem:
                    tournamentDetailEdit_list = EditTextDialogFragment.NewInstance(null, title, "Enter facility");
                    break;
                case Resource.Id.addvenueitem:
                    tournamentDetailEdit_list = EditTextDialogFragment.NewInstance(null, title, "Enter venue");
                    break;
            }
            tournamentDetailEdit_list.Show(ft, "EditTournamentDetail");
            AddAction = true;
        }

        private void EditTournamentDetail_InclueTeam(object sender, EventArgs e)
        {
            SpinnerDialogFragment includeTeamFragment = SpinnerDialogFragment.NewInstance(null, new List<string>() { { "JK" } });
            includeTeamFragment.Show(ClearPreviousFragments("InclueTeam"), "InclueTeam");
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
            _tournament.StartDate = selectedDate;
        }

        public void OnEnteredText(string inputText)
        {
            if (AddAction)
            {
                switch (AddItemButton.Id)
                {
                    case Resource.Id.addprizeitem:
                        _tournament.Prizes.Add(inputText);
                        PrizeAdapter.Refresh(_tournament.Prizes);
                        PrizeRecyclerView.SetAdapter(PrizeAdapter);
                        break;
                    case Resource.Id.addfacilityitem:
                        _tournament.Facilities.Add(inputText);
                        FacilityAdapter.Refresh(_tournament.Facilities);
                        FacilityRecyclerView.SetAdapter(FacilityAdapter);
                        break;
                    case Resource.Id.addvenueitem:
                        _tournament.Venues.Add(inputText);
                        VenueAdapter.Refresh(_tournament.Venues);
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
                        _tournament.Name = inputText;
                        break;
                    case Resource.Id.SponsorValue:
                        _tournament.Sponsor = inputText;
                        break;
                    case Resource.Id.StatusValue:
                        _tournament.Status = inputText;
                        break;
                    case Resource.Id.EntryFeeValue:
                        _tournament.EntryFee = Convert.ToDecimal(inputText);
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
    }
}