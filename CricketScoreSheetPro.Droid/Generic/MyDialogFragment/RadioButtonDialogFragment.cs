using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CricketScoreSheetPro.Droid.Generic.MyDialogFragment
{
    public interface ISelectedRadioButtonListener
    {
        void OnSelectedRadioButton(string title, string inputText);
    }

    public class RadioButtonDialogFragment : DialogFragment 
    {
        private ISelectedRadioButtonListener _callback;
        private RadioButton Individual;
        private RadioButton Tournament;
        private string _title;

        public RadioButtonDialogFragment(ISelectedRadioButtonListener callback, string title)
        {
            _callback = callback;
            _title = title;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            LinearLayout container = new LinearLayout(this.Activity);
            LinearLayout.LayoutParams layoutparameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            layoutparameters.SetMargins(15, 20, 15, 5);

            Individual = new RadioButton(this.Activity) { Text = "Individual Game", LayoutParameters = layoutparameters };
            Tournament = new RadioButton(this.Activity) { Text = "Tournament", LayoutParameters = layoutparameters };
            RadioGroup rg = new RadioGroup(this.Activity) { LayoutParameters = layoutparameters };
            rg.AddView(Individual);
            rg.AddView(Tournament);
            rg.CheckedChange += Rg_CheckedChange;

            container.AddView(rg);

            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this.Activity);
            inputDialog.SetTitle(_title);
            inputDialog.SetView(container);
            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this.Activity, "Canceled.", ToastLength.Short).Show();
            });
            return inputDialog.Show();
        }

        private void Rg_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            _callback.OnSelectedRadioButton(_title, Individual.Id == e.CheckedId ? "Individual Game" : "Tournament");
        }
    }
}