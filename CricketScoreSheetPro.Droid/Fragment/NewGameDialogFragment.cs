
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Droid.Generic.MyAdapter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Droid
{
    public class NewGameDialogFragment : DialogFragment
    {
        private NewGameViewModel ViewModel;

        private Spinner mHomeTeamName;
        private Spinner mAwayTeamName;
        private Spinner mLocation;
        private Spinner mUmpireOne;
        private Spinner mUmpireTwo;
        private Spinner mOvers;
        private Button mCreateMatchBtn;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = Singleton.Instance.NewGameViewModel();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.NewGameView, container, false);            

            mHomeTeamName = view.FindViewById<Spinner>(Resource.Id.homeTeam);
            mAwayTeamName = view.FindViewById<Spinner>(Resource.Id.awayTeam);         
            mOvers = view.FindViewById<Spinner>(Resource.Id.overs);         
            mLocation = view.FindViewById<Spinner>(Resource.Id.location);          
            mUmpireOne = view.FindViewById<Spinner>(Resource.Id.umpire1);           
            mUmpireTwo = view.FindViewById<Spinner>(Resource.Id.umpire2);

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            //Set teams
            var teams = new List<string> { "Select Team", "Add New Team" };
            teams.AddRange(ViewModel.Teams.Select(n => n.Name));
            var adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, teams.ToArray());            
            mHomeTeamName.Adapter = adapter;
            mHomeTeamName.ItemSelected += setTeam;
            mAwayTeamName.Adapter = adapter;
            mAwayTeamName.ItemSelected += setTeam;

            // Set Overs            
            var overs = new string[] { "Ten10", "Twenty20", "ThirtyFive35", "Forty40", "Fifty50", "Custom" };
            mOvers.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, overs);
            mOvers.ItemSelected += setOvers;

            // Set Location  
            var locations = new List<string> { "Select Ground/Location", "Add Ground/Location" };
            locations.AddRange(ViewModel.Locations.Select(n => n.Name));
            mLocation.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow, 
               locations.ToArray());
            mLocation.ItemSelected += setLocation;

            // Set Umpire      
            var umpires = new List<string> { "Select Umpire", "Add Umpire" };
            umpires.AddRange(ViewModel.Umpires.Select(n => n.Name));
            mUmpireOne.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow,
                umpires.ToArray());
            mUmpireOne.ItemSelected += setUmpires;
        
            mUmpireTwo.Adapter = new SpinnerAdapter(this.Activity, Resource.Layout.SpinnerTextViewRow,
                umpires.ToArray());
            mUmpireTwo.ItemSelected += setUmpires;
        }

        private void setUmpires(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void setLocation(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void setOvers(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void setTeam(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }
    }
}