
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Droid.Activity;
using CricketScoreSheetPro.Droid.Generic.MyAdapter;
using CricketScoreSheetPro.Droid.Generic.MyDialogFragment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Droid
{
    public class NewGameDialogFragment : DialogFragment, IEditedTextListener
    {
        private Driver driver;
        private NewGameViewModel ViewModel;

        private Spinner mHomeTeamName;
        private Spinner mAwayTeamName;
        private Spinner mLocation;
        private Spinner mUmpireOne;
        private Spinner mUmpireTwo;
        private Spinner mOversOrTournaments;
        private Button mCreateMatchBtn;

        private bool isTournament;
        private List<string> Teams;
        private List<string> Locations;
        private List<string> Umpires;
        private List<string> Overs;
        private List<string> Tournaments;

        public NewGameDialogFragment(string gametype)
        {
            isTournament = gametype != "Individual Game";
            driver = new Driver();
            ViewModel = driver.NewGameViewModel();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Teams = new List<string> { "Select Team", "Add New Team" };
            Teams.AddRange(ViewModel.Teams.Select(n => n.Name));
            Locations = new List<string> { "Select Ground/Location", "Add Ground/Location" };
            Locations.AddRange(ViewModel.Locations.Select(n => n.Name));
            Umpires = new List<string> { "Select Umpire", "Add Umpire" };
            Umpires.AddRange(ViewModel.Umpires.Select(n => n.Name));
            Overs = new List<string> { "Ten10", "Twenty20", "ThirtyFive35", "Forty40", "Fifty50", "Custom" };
            if (isTournament)
            {
                Tournaments = 
            }           
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.NewGameView, container, false);            

            mHomeTeamName = view.FindViewById<Spinner>(Resource.Id.homeTeam);
            mAwayTeamName = view.FindViewById<Spinner>(Resource.Id.awayTeam);
            var overs_tournaments = view.FindViewById<TextView>(Resource.Id.overs_tournaments_label);
            mOversOrTournaments = view.FindViewById<Spinner>(Resource.Id.overs_tournaments_values);         
            mLocation = view.FindViewById<Spinner>(Resource.Id.location);          
            mUmpireOne = view.FindViewById<Spinner>(Resource.Id.umpire1);           
            mUmpireTwo = view.FindViewById<Spinner>(Resource.Id.umpire2);

            mCreateMatchBtn = view.FindViewById<Button>(Resource.Id.createMatchButton);
            mCreateMatchBtn.Enabled = false;
            mCreateMatchBtn.Click += (object sender, EventArgs e) =>
            {
                var hometeamname = mHomeTeamName.SelectedItem.ToString();
                var awayteamname = mAwayTeamName.SelectedItem.ToString();
                var oversOrTournaments = mOversOrTournaments.SelectedItem.ToString();
                var location = mLocation.SelectedItem.ToString();
                var primaryumpire = mUmpireOne.SelectedItem.ToString();
                var secondaryumpire = mUmpireTwo.SelectedItem.ToString();

                var match = ViewModel.AddMatch(hometeamname, awayteamname, oversOrTournaments, location, primaryumpire, secondaryumpire);
                var currentMatchActivity = new Intent(this.Activity, typeof(MatchActivity));
                currentMatchActivity.PutExtra("MatchId", match.Id);
                StartActivity(currentMatchActivity);
                Fragment prev = (DialogFragment)FragmentManager.FindFragmentByTag("newgame dialog");
                if (prev != null)
                {
                    DialogFragment df = (DialogFragment)prev;
                    df.Dismiss();
                }
            };

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            //Set teams            
            var adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, Teams.ToArray());            
            mHomeTeamName.Adapter = adapter;
            mHomeTeamName.ItemSelected += SetTeam;
            mAwayTeamName.Adapter = adapter;
            mAwayTeamName.ItemSelected += SetTeam;

            // Set Overs or Tournaments
            if (isTournament)
            {
                mOversOrTournaments.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, Tournaments.ToArray());
                mOversOrTournaments.ItemSelected += SetOvers;
            }
            else
            {
                mOversOrTournaments.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, Overs.ToArray());
                mOversOrTournaments.ItemSelected += SetOvers;
            }
                
            // Set Location  
            mLocation.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, 
               Locations.ToArray());
            mLocation.ItemSelected += SetLocation;

            // Set Umpire      
            mUmpireOne.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow,
                Umpires.ToArray());
            mUmpireOne.ItemSelected += SetUmpires;
        
            mUmpireTwo.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow,
                Umpires.ToArray());
            mUmpireTwo.ItemSelected += SetUmpires;
        }

        private void SetTeam(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != 1) return;
            var ft = ClearPreviousFragments("AddTeam");

            var title = "Add HomeTeam";
            if (Resource.Id.awayTeam == e.Parent.Id)
                title = "Add AwayTeam";
            var addTeam = new EditTextDialogFragment(this, title, "Enter team name");
            addTeam.Show(ft, "AddTeam");
        }

        private void SetTournaments(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != 5) return;
            var ft = ClearPreviousFragments("customovers");
            var title = "Custom Over";
            var customOvers = new EditTextDialogFragment(this, title, "Enter number of over(s)");
            customOvers.Show(ft, "customovers");
        }

        private void SetOvers(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != 5) return;
            var ft = ClearPreviousFragments("customovers");
            var title = "Custom Over";
            var customOvers = new EditTextDialogFragment(this, title, "Enter number of over(s)");
            customOvers.Show(ft, "customovers");
        }

        private void SetLocation(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != 1) return;
            var ft = ClearPreviousFragments("AddLocation");

            var title = "Add Location";
            var addLocation = new EditTextDialogFragment(this, title, "Enter location");
            addLocation.Show(ft, "AddLocation");
        }

        private void SetUmpires(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != 1) return;
            var ft = ClearPreviousFragments("AddUmpire");

            var title = "Add PrimaryUmpire";
            if (Resource.Id.umpire2 == e.Parent.Id)
                title = "Add SecondaryUmpire";
            var addTeam = new EditTextDialogFragment(this, title, "Enter umpire name");
            addTeam.Show(ft, "AddUmpire");
        }

        public void OnEnteredText(string title, string inputText)
        {
            switch(title)
            {
                case "Add HomeTeam":
                    var hometeam = driver.TeamListViewModel().AddTeam(inputText);
                    Teams.Add(inputText);
                    mHomeTeamName.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, Teams.ToArray());
                    mHomeTeamName.SetSelection(Teams.Count - 1);
                    break;
                case "Add AwayTeam":
                    var awayteam = driver.TeamListViewModel().AddTeam(inputText);
                    Teams.Add(inputText);
                    mAwayTeamName.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, Teams.ToArray());
                    mAwayTeamName.SetSelection(Teams.Count - 1);
                    break;
                case "Custom Over":
                    Overs.Add(inputText);
                    var adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, Overs.ToArray());
                    mOversOrTournaments.Adapter = adapter;
                    mOversOrTournaments.SetSelection(Overs.Count - 1);
                    break;
                case "Add Location":
                    var addLocation = ViewModel.AddLocation(inputText);
                    Locations.Add(inputText);
                    mLocation.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, Locations.ToArray());                    
                    mLocation.SetSelection(Locations.Count - 1);
                    break;
                case "Add PrimaryUmpire":
                    var primaryumpire = ViewModel.AddUmpire(inputText);
                    Umpires.Add(inputText);
                    mUmpireOne.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, Umpires.ToArray());
                    mUmpireOne.SetSelection(Umpires.Count - 1);
                    break;
                case "Add SecondaryUmpire":
                    var secondaryumpire = ViewModel.AddUmpire(inputText);
                    Umpires.Add(inputText);
                    mUmpireTwo.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, Umpires.ToArray());
                    mUmpireTwo.SetSelection(Umpires.Count - 1);
                    break;
            }
        }

        protected FragmentTransaction ClearPreviousFragments(string tag)
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