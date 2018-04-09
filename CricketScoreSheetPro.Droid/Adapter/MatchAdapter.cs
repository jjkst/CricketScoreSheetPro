using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Droid.Adapter
{
    public class MatchAdapter : RecyclerView.Adapter
    {
        public event EventHandler<string> ItemViewClick;
        private List<Match> _matches;

        public MatchAdapter(List<Match> matches)
        {
            _matches = matches.OrderByDescending(d => d.AddDate).ToList();
        }

        public override int ItemCount => _matches.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MatchViewHolder vh = holder as MatchViewHolder;

            vh?.ItemView.SetBackgroundResource(position % 2 == 1
                            ? Resource.Drawable.listview_selector_even
                            : Resource.Drawable.listview_selector_odd);

            var match = _matches[position];

            vh.MatchDateLocation.Text = $"{match.AddDate}{(string.IsNullOrEmpty(match.Location) ? "" : $", at {match.Location}")}";

            var umpires = match.PrimaryUmpire + (string.IsNullOrEmpty(match.SecondaryUmpire) ? "" : $", {match.SecondaryUmpire}");
            if (string.IsNullOrEmpty(umpires.Trim()))
                vh.Umpires.Visibility = ViewStates.Gone;
            else
                vh.Umpires.Text = umpires;

            string hometeamovers = Function.BallsToOversValueConverter(match.HomeTeam.Balls);
            string awayteamovers = Function.BallsToOversValueConverter(match.AwayTeam.Balls);

            vh.HomeTeamDetail.Text = $"{match.HomeTeam.TeamName} {match.HomeTeam.Runs}/{match.HomeTeam.Wickets} ({hometeamovers}/{match.TotalOvers}) " +
                              $"Extras (nb {match.HomeTeam.NoBalls}, w {match.HomeTeam.Wides}, b {match.HomeTeam.Byes},lb {match.HomeTeam.LegByes})";
            vh.AwayTeamDetail.Text = $"{match.AwayTeam.TeamName} {match.AwayTeam.Runs}/{match.AwayTeam.Wickets} ({awayteamovers}/{match.TotalOvers}) " +
                                $"Extras(nb {match.AwayTeam.NoBalls}, w {match.AwayTeam.Wides}, b {match.AwayTeam.Byes},lb {match.AwayTeam.LegByes})";

            vh.MatchComments.Text = match.MatchComplete ? match.Comments : "In Progress";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.MatchRow, parent, false);
            return new MatchViewHolder(itemView, OnViewClick);
        }

        public void Refresh(IEnumerable<Match> matches)
        {
            _matches = matches.ToList();
        }

        private void OnViewClick(int position)
        {
            ItemViewClick?.Invoke(this, _matches[position].Id);
        }

    }
    public class MatchViewHolder : RecyclerView.ViewHolder
    {
        public TextView MatchDateLocation { get; private set; }
        public TextView Umpires { get; private set; }
        public TextView HomeTeamDetail { get; private set; }
        public TextView AwayTeamDetail { get; private set; }
        public TextView MatchComments { get; private set; }

        public MatchViewHolder(View itemView, Action<int> viewlistener)
            : base(itemView)
        {
            MatchDateLocation = itemView.FindViewById<TextView>(Resource.Id.matchdatelocation);
            Umpires = itemView.FindViewById<TextView>(Resource.Id.umpires);
            HomeTeamDetail = itemView.FindViewById<TextView>(Resource.Id.hometeamdetail);
            AwayTeamDetail = itemView.FindViewById<TextView>(Resource.Id.awayteamdetail);
            MatchComments = itemView.FindViewById<TextView>(Resource.Id.matchcomments);
            itemView.Click += (sender, e) => viewlistener(AdapterPosition);
        }
    }
}