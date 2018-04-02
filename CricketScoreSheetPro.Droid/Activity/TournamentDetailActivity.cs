using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.ViewModel;

namespace CricketScoreSheetPro.Droid.Activity
{
    [Activity(Label = "Tournament Detail", Theme = "@style/MyTheme")]
    public class TournamentDetailActivity : BaseActivity
    {
        protected override int GetLayoutResourceId => Resource.Layout.TournamentDetailView;

        private TournamentViewModel ViewModel { get; set; }
        private TextView Name { get; set; }
        private TextView Sponsor { get; set; }
        private TextView Status { get; set; }
        private TextView StartDate { get; set; }
        private TextView EntryFee { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var tournamentId = Intent.GetStringExtra("TournamentId");
            ViewModel = Singleton.Instance.TournamentViewModel(tournamentId);

            Name = (TextView)FindViewById(Resource.Id.NameValue);
            Name.Click += EditTournamentDetail;
            Sponsor = (TextView)FindViewById(Resource.Id.SponsorValue);
            Status = (TextView)FindViewById(Resource.Id.StatusValue);
            StartDate = (TextView)FindViewById(Resource.Id.StartDateValue);
            EntryFee = (TextView)FindViewById(Resource.Id.EntryFeeValue);
        }

        private void EditTournamentDetail(object sender, EventArgs e)
        {
            //if(sender.GetType().GetProperty("Id").GetValue() == Resource.Id.NameValue)
            //{
            //
            //}
            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this);
            inputDialog.SetTitle("Add Tournament");

            EditText userInput = new EditText(this)
            {
                Hint = "Enter Tournament Name",
                Top = 5,
                Focusable = true,
                ShowSoftInputOnFocus = true
            };
            inputDialog.SetView(userInput);

            inputDialog.SetPositiveButton("Add", (senderAlert, args) => {
                var detailActivity = new Intent(this, typeof(TournamentDetailActivity));
               // detailActivity.PutExtra("TournamentId", newtournament.Id);
                StartActivity(detailActivity);
            });
            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this, "Canceled.", ToastLength.Short).Show();
            });
            inputDialog.Show();
        }

        protected override void OnResume()
        {
            base.OnResume();
            Name.Text = ViewModel.Tournament.Name;
            Sponsor.Text = ViewModel.Tournament.Sponsor;
            Status.Text = ViewModel.Tournament.Status;
            StartDate.Text = ViewModel.Tournament.StartDate.ToShortDateString();
            EntryFee.Text = ViewModel.Tournament.EntryFee.ToString();
        }

    }
}