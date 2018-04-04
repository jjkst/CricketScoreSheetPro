﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Droid.Generic.MyDialogFragment;
using static CricketScoreSheetPro.Droid.Generic.MyDialogFragment.SpinnerDialogFragment;

namespace CricketScoreSheetPro.Droid.Activity
{
    [Activity(Label = "Tournament Detail", Theme = "@style/MyTheme")]
    public class TournamentDetailActivity : BaseActivity , ISelectedSpinnerItemListener
    {
        protected override int GetLayoutResourceId => Resource.Layout.TournamentDetailView;

        private Tournament _tournament;
        private TournamentViewModel ViewModel;

        private TextView Name;
        private TextView Sponsor;
        private TextView Status;
        private TextView StartDate;
        private TextView EntryFee;

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
            addPrize.Click += AddItemInList;
            var addFacility  = (Button)FindViewById(Resource.Id.addfacilityitem);
            addFacility.Click += AddItemInList;
            var addVenue = (Button)FindViewById(Resource.Id.addvenueitem);
            addVenue.Click += AddItemInList;
            var includeteam = (Button)FindViewById(Resource.Id.includeteam);
            includeteam.Click += InclueTeam;
        }

        private void EditTournamentDetail(object sender, EventArgs e)
        {
            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this);

            TextView editText = (TextView)sender;
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
                    _tournament.StartDate = dp.DateTime;
                    Toast.MakeText(this, "Saved.", ToastLength.Short).Show();
                });
            }
            else
            {
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
                    case Resource.Id.EntryFeeValue:
                        hint = "Edit entry fee";
                        break;
                }
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
                    switch (editText.Id)
                    {
                        case Resource.Id.NameValue:
                            _tournament.Name = userInput.Text;
                            break;
                        case Resource.Id.SponsorValue:
                            _tournament.Sponsor = userInput.Text;
                            break;
                        case Resource.Id.StatusValue:
                            _tournament.Status = userInput.Text;
                            break;
                        case Resource.Id.EntryFeeValue:
                            _tournament.EntryFee = Convert.ToDecimal(userInput.Text);
                            break;
                    }
                    Toast.MakeText(this, "Saved.", ToastLength.Short).Show();
                });
            }

            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this, "Canceled.", ToastLength.Short).Show();
            });
            inputDialog.Show();
        }

        private void AddItemInList(object sender, EventArgs e)
        {
            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this);
            Button addItem = (Button)sender;
            string hint = null;
            switch (addItem.Id)
            {
                case Resource.Id.addprizeitem:
                    hint = "Enter prize";
                    break;
                case Resource.Id.addfacilityitem:
                    hint = "Enter facility";
                    break;
                case Resource.Id.addvenueitem:
                    hint = "Enter venue";
                    break;
            }
            EditText userInput = new EditText(this)
            {
                Hint = hint,
                Top = 10,
                Focusable = true,
                ShowSoftInputOnFocus = true
            };
            inputDialog.SetView(userInput);
            inputDialog.SetPositiveButton("Save", (senderAlert, args) => {
                View view = LayoutInflater.From(this).Inflate(Resource.Layout.EditList, null);
                view.Tag = userInput.Text;
                TextView item = (TextView)view.FindViewById(Resource.Id.itemvalue);
                Button deleteitem = (Button)view.FindViewById(Resource.Id.deleteitem);
                switch (addItem.Id)
                {
                    case Resource.Id.addprizeitem:
                        _tournament.Prizes.Add(userInput.Text);
                        item.Text = userInput.Text;
                        deleteitem.Click += (b, r) => DeleteItem(b, r, "prize", userInput.Text);
                        LinearLayout prizelist = (LinearLayout)FindViewById(Resource.Id.prizelist);
                        prizelist.AddView(view);
                        break;
                    case Resource.Id.addfacilityitem:
                        _tournament.Facilities.Add(userInput.Text);
                        item.Text = userInput.Text;
                        deleteitem.Click += (b, r) => DeleteItem(b, r, "facility", userInput.Text);
                        LinearLayout facilitylist = (LinearLayout)FindViewById(Resource.Id.facilitylist);
                        facilitylist.AddView(view);
                        break;
                    case Resource.Id.addvenueitem:
                        _tournament.Venues.Add(userInput.Text);
                        item.Text = userInput.Text;
                        deleteitem.Click += (b, r) => DeleteItem(b, r, "venue", userInput.Text);
                        LinearLayout venuelist = (LinearLayout)FindViewById(Resource.Id.venuelist);
                        venuelist.AddView(view);
                        break;
                }
                Toast.MakeText(this, "Saved.", ToastLength.Short).Show();
            });


            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this, "Canceled.", ToastLength.Short).Show();
            });
            inputDialog.Show();
        }

        private void DeleteItem(object sender, EventArgs e, string list, string value)
        {
            LinearLayout listlayout = null;
            switch (list)
            {
                case "prize":
                    listlayout = (LinearLayout)FindViewById(Resource.Id.prizelist);
                    _tournament.Prizes.Remove(value);
                    listlayout.RemoveView(listlayout.FindViewWithTag(value));
                    break;
                case "facility":
                    listlayout = (LinearLayout)FindViewById(Resource.Id.facilitylist);
                    _tournament.Facilities.Remove(value);
                    listlayout.RemoveView(listlayout.FindViewWithTag(value));
                    break;
                case "venue":
                    listlayout = (LinearLayout)FindViewById(Resource.Id.venuelist);
                    _tournament.Venues.Remove(value);
                    listlayout.RemoveView(listlayout.FindViewWithTag(value));
                    break;
            }
        }

        private void InclueTeam(object sender, EventArgs e)
        {
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            Fragment prev = FragmentManager.FindFragmentByTag("dialog");
            if (prev != null)
                ft.Remove(prev);
            ft.AddToBackStack(null);

            // Create and show the dialog.
            SpinnerDialogFragment includeTeamFragment = SpinnerDialogFragment.NewInstance(null, new List<string>() { { "JK" } });
            includeTeamFragment.Show(ft, "dialog");
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
        }

        public void OnSelectSpinnerItem(string inputText)
        {
            View view = LayoutInflater.From(this).Inflate(Resource.Layout.EditList, null);
            view.Tag = inputText;
            TextView item = (TextView)view.FindViewById(Resource.Id.itemvalue);
            item.Text = inputText;

            Button deleteitem = (Button)view.FindViewById(Resource.Id.deleteitem);            
            deleteitem.Click += (b, r) => DeleteItem(b, r, "prize", inputText);
            LinearLayout teamlist = (LinearLayout)FindViewById(Resource.Id.teamlist);
            teamlist.AddView(view);
        }
    }
}