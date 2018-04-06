using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Core.Model;

namespace CricketScoreSheetPro.Droid.Adapter
{
    public class PlayerAdapter : RecyclerView.Adapter
    {
        public event EventHandler<string> ItemViewClick;
        public event EventHandler<string> ItemDeleteClick;
        private List<Player> _players;

        public PlayerAdapter(List<Player> players)
        {
            _players = players.OrderByDescending(d => d.AddDate).ToList();
        }

        public override int ItemCount => _players.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PlayerViewHolder vh = holder as PlayerViewHolder;

            vh?.ItemView.SetBackgroundResource(position % 2 == 1
                            ? Resource.Drawable.listview_selector_even
                            : Resource.Drawable.listview_selector_odd);

            vh.Name.Text = _players[position].Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.TextViewWithDeleteActionRow, parent, false);
            return new PlayerViewHolder(itemView, OnViewClick, OnDeleteClick);
        }

        public void Refresh(IEnumerable<Player> players)
        {
            _players = players.ToList();
        }

        private void OnViewClick(int position)
        {
            ItemViewClick?.Invoke(this, _players[position].Id);
        }

        private void OnDeleteClick(int position)
        {
            ItemDeleteClick?.Invoke(this, _players[position].Id);
        }
    }

    public class PlayerViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public Button Delete { get; private set; }

        public PlayerViewHolder(View itemView, Action<int> viewlistener, Action<int> deletelistener)
            : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.itemvalue);
            Name.Click += (sender, e) => viewlistener(AdapterPosition);
            Delete = itemView.FindViewById<Button>(Resource.Id.deleteitem);
            Delete.Click += (sender, e) => deletelistener(AdapterPosition);
        }

    }
}