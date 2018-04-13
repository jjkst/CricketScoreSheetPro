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

namespace CricketScoreSheetPro.Droid.Activity
{
    [Activity(Label = "MatchActivity")]
    public class MatchActivity : BaseActivity
    {
        protected override int GetLayoutResourceId => Resource.Layout.MatchView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
    }
}