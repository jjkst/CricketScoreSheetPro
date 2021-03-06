﻿using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Droid.Generic.MyAdapter
{
    public class TextViewWithDeleteActionAdapter : RecyclerView.Adapter
    {
        public event EventHandler<string> ItemViewClick;
        public event EventHandler<string> ItemDeleteClick;
        private List<string> _items;

        public TextViewWithDeleteActionAdapter(List<string> itemlist)
        {
            _items = itemlist;
        }

        public override int ItemCount => _items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TextViewWithDeleteActionViewHolder vh = holder as TextViewWithDeleteActionViewHolder;
            vh.Name.Text = _items[position];
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.TextViewWithDeleteActionRow, parent, false);
            return new TextViewWithDeleteActionViewHolder(itemView, OnViewClick, OnDeleteClick);
        }

        public void Refresh(IEnumerable<string> items)
        {
            _items = items.ToList();
        }

        private void OnViewClick(int position)
        {
            ItemViewClick?.Invoke(this, _items[position]);
        }

        private void OnDeleteClick(int position)
        {
            ItemDeleteClick?.Invoke(this, _items[position]);
        }
    }

    public class TextViewWithDeleteActionViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public Button Delete { get; private set; }

        public TextViewWithDeleteActionViewHolder(View itemView, Action<int> viewlistener, Action<int> deletelistener)
            : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.itemvalue);
            Name.Click += (sender, e) => viewlistener(AdapterPosition);
            Delete = itemView.FindViewById<Button>(Resource.Id.deleteitem);
            Delete.Click += (sender, e) => deletelistener(AdapterPosition);
        }

    }
}