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

        private TextView Prize { get; set; }

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

            Prize = (TextView)FindViewById(Resource.Id.PrizeListValue);
            Prize.Click += IncludeExcludeList;

            var facilitylayout  = (Button)FindViewById(Resource.Id.addfacilityitem);
            facilitylayout.Click += AddItemInList;
        }

        private void AddItemInList(object sender, EventArgs e)
        {
            View view = LayoutInflater.From(this).Inflate(Resource.Layout.EditList, null);
            TextView facility = (TextView)view.FindViewById(Resource.Id.facilitylistvalue);
            Button deletefacility = (Button)view.FindViewById(Resource.Id.deletefacilityitem);

            LinearLayout ll = (LinearLayout)FindViewById(Resource.Id.FacilityList);
            ll.AddView(view);
        }

        private void IncludeExcludeList(object sender, EventArgs e)
        {
            AlertDialog.Builder editListDialog = new AlertDialog.Builder(this);
            editListDialog.SetView(Resource.Layout.EditList);

            editListDialog.SetPositiveButton("Save", (senderAlert, args) => {
                Toast.MakeText(this, "Saved.", ToastLength.Short).Show();
            });

            editListDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this, "Canceled.", ToastLength.Short).Show();
            });

            editListDialog.Show();
        }

        private void EditTournamentDetail(object sender, EventArgs e)
        {
            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this);

            TextView editText = (TextView)sender;
            string hint = null;
            switch (editText.Id)
            {
                case Resource.Id.NameValue:
                    hint = "Edit name";
                    break;
                case Resource.Id.SponsorValue:
                    hint = "Edit sponsor";
                    break;
                case Resource.Id.StatusValue:
                    hint = "Edit status";
                    break;
                case Resource.Id.StartDateValue:
                    hint = "Edit start date";
                    break;
                case Resource.Id.EntryFeeValue:
                    hint = "Edit entry fee";
                    break;
            }

            if (editText.Id == Resource.Id.StartDateValue)
            {
                DatePicker dp = new DatePicker(this)
                {
                    Top = 5,
                    Focusable = true,   
                    DateTime = DateTime.Today
                };
                inputDialog.SetView(dp);
                inputDialog.SetPositiveButton("Save", (senderAlert, args) => {
                    editText.Text = dp.DateTime.ToShortDateString();
                    Toast.MakeText(this, "Saved.", ToastLength.Short).Show();
                });
            }
            else
            { 
                EditText userInput = new EditText(this)
                {
                    Hint = hint,
                    Top = 10,
                    Focusable = true,
                    ShowSoftInputOnFocus = true
                };
                inputDialog.SetView(userInput);
                inputDialog.SetPositiveButton("Save", (senderAlert, args) => {
                    editText.Text = userInput.Text;
                    Toast.MakeText(this, "Saved.", ToastLength.Short).Show();
                });
            }

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