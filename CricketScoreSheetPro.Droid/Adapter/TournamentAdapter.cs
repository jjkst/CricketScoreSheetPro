using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Droid.Adapter
{
    public class TournamentAdapter : RecyclerView.Adapter
    {
        public event EventHandler<string> ItemViewClick;
        public event EventHandler<string> ItemDeleteClick;
        private List<UserTournament> _tournaments;

        public TournamentAdapter(List<UserTournament> tournaments)
        {
            _tournaments = tournaments.OrderByDescending(d => d.AddDate).ToList();
        }

        public override int ItemCount => _tournaments.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TournamentViewHolder vh = holder as TournamentViewHolder;

            vh?.ItemView.SetBackgroundResource(position % 2 == 1
                            ? Resource.Drawable.listview_selector_even
                            : Resource.Drawable.listview_selector_odd);

            vh.Name.Text = _tournaments[position].Name;
            vh.Status.Text = _tournaments[position].Status;

            if (_tournaments[position].AccessType != AccessType.Moderator)
                vh.Delete.Visibility = ViewStates.Gone;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.TournamentRow, parent, false);
            return new TournamentViewHolder(itemView, OnViewClick, OnDeleteClick);
        }

        public void RefreshTournaments(IEnumerable<UserTournament> tournaments)
        {
            _tournaments = tournaments.ToList();
        }

        private void OnViewClick(int position)
        {
            ItemViewClick?.Invoke(this, _tournaments[position].Id);
        }

        private void OnDeleteClick(int position)
        {
            ItemDeleteClick?.Invoke(this, _tournaments[position].Id);
        }
    }

    public class TournamentViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public TextView Status { get; private set; }
        public Button Delete { get; private set; }

        public TournamentViewHolder(View itemView, Action<int> viewlistener, Action<int> deletelistener)
            : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.tournamentname);
            Status = itemView.FindViewById<TextView>(Resource.Id.tournamentstatus);
            Delete = itemView.FindViewById<Button>(Resource.Id.deletetournament);

            Name.Click += (sender, e) => viewlistener(AdapterPosition);
            Delete.Click += (sender, e) => deletelistener(AdapterPosition);            
        }

    }
}