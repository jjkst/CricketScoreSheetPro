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
    public class TournamentFragment : BaseFragment, IEditedTextListener
    {
        protected override int GetLayoutResourceId => Resource.Layout.TournamentView;

        private TournamentListViewModel ViewModel { get; set; }
        private TournamentAdapter TournamentsAdapter { get; set; }
        private RecyclerView TournamentsRecyclerView { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = new Driver().TournamentListViewModel();
            this.Activity.InvalidateOptionsMenu();
            SetHasOptionsMenu(true);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            SearchEditText = view.FindViewById<EditText>(Resource.Id.searchTournament);
            SearchEditText.TextChanged += SearchText_TextChanged;

            FloatingActionButton addTournament = view.FindViewById<FloatingActionButton>(Resource.Id.addtournament);
            addTournament.Click += ShowAddTournamentDialog;

            TournamentsRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.tournamentsrecyclerview);
            var layoutManager = new LinearLayoutManager(this.Activity);
            var onScrollListener = new XamarinRecyclerViewOnScrollListener(layoutManager)
            {
                FloatingButton = addTournament
            };
            TournamentsRecyclerView.AddOnScrollListener(onScrollListener);
            TournamentsRecyclerView.SetLayoutManager(layoutManager);           
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            TournamentsAdapter = new TournamentAdapter(ViewModel.Tournaments);
            TournamentsAdapter.ItemViewClick += OnItemViewClick;
            TournamentsAdapter.ItemDeleteClick += OnItemDeleteClick;
            TournamentsRecyclerView.SetAdapter(TournamentsAdapter);
        }

        protected override void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            IEnumerable<UserTournament> tournaments = ViewModel.Tournaments.Where(t => t.Name.ToLower().Contains(SearchEditText.Text.ToLower()));
            TournamentsAdapter.RefreshTournaments(tournaments);
            TournamentsRecyclerView.SetAdapter(TournamentsAdapter);
        }

        private void ImportTournament()
        {
            var importtournament = ViewModel.ImportTournament("", Driver.UniqueUserId);
            TournamentsAdapter.RefreshTournaments(ViewModel.Tournaments);
            TournamentsRecyclerView.SetAdapter(TournamentsAdapter);
        }

        private void OnItemViewClick(object sender, string tournamentId)
        {
            var detailActivity = new Intent(this.Activity, typeof(TournamentDetailActivity));
            detailActivity.PutExtra("TournamentId", tournamentId);
            StartActivity(detailActivity);
        }

        private void OnItemDeleteClick(object sender, string usertournamentId)
        {
            ViewModel.DeleteTournament(usertournamentId);
            TournamentsAdapter.RefreshTournaments(ViewModel.Tournaments);
            TournamentsRecyclerView.SetAdapter(TournamentsAdapter);
        }

        private void ShowAddTournamentDialog(object sender, EventArgs e)
        {
            var ft = ClearPreviousFragments("AddTournament");
            var addTournament = new EditTextDialogFragment(this, "Add Tournament", "Enter Tournament name");
            addTournament.Show(ft, "AddTournament");
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
            var newtournament = ViewModel.AddTournament(inputText);
            var detailActivity = new Intent(this.Activity, typeof(TournamentDetailActivity));
            detailActivity.PutExtra("TournamentId", newtournament.TournamentId);
            StartActivity(detailActivity);
        }
    }
}