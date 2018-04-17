using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Droid.Adapter
{
    public class TeamAdapter : RecyclerView.Adapter
    {
        public event EventHandler<string> ItemViewClick;
        public event EventHandler<string> ItemDeleteClick;
        private List<Team> _teams;

        public TeamAdapter(List<Team> teams)
        {
            _teams = teams.OrderByDescending(d => d.AddDate).ToList();
        }

        public override int ItemCount => _teams.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TeamViewHolder vh = holder as TeamViewHolder;

            vh?.ItemView.SetBackgroundResource(position % 2 == 1
                            ? Resource.Drawable.listview_selector_even
                            : Resource.Drawable.listview_selector_odd);

            vh.Name.Text = _teams[position].Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.TextViewWithDeleteActionRow, parent, false);
            return new TeamViewHolder(itemView, OnViewClick, OnDeleteClick);
        }

        public void Refresh(IEnumerable<Team> teams)
        {
            _teams = teams.ToList();
        }

        private void OnViewClick(int position)
        {
            ItemViewClick?.Invoke(this, _teams[position].Id);
        }

        private void OnDeleteClick(int position)
        {
            ItemDeleteClick?.Invoke(this, _teams[position].Id);
        }
    }
    public class TeamViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public Button Delete { get; private set; }

        public TeamViewHolder(View itemView, Action<int> viewlistener, Action<int> deletelistener)
            : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.itemvalue);
            Name.Click += (sender, e) => viewlistener(AdapterPosition);
            Delete = itemView.FindViewById<Button>(Resource.Id.deleteitem);
            Delete.Click += (sender, e) => deletelistener(AdapterPosition);
        }

    }
}