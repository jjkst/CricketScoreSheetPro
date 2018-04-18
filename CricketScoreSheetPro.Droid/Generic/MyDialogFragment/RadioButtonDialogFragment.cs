using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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

            RadioButton individual = new RadioButton(this.Activity)
            {
                Clickable = true,
                Checked = true,
                Text = "Individual Game"
            };
            RadioButton tournament = new RadioButton(this.Activity)
            {
                Checked = true,
                Text = "Tournament"
            };

            RadioGroup rg = new RadioGroup(this.Activity);
            rg.AddView(individual);
            rg.AddView(tournament);
            container.AddView(rg);

            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this.Activity);
            inputDialog.SetTitle(_title);
            inputDialog.SetView(container);
            inputDialog.SetPositiveButton("Save", (senderAlert, args) => {
                var selected = individual.Id == rg.CheckedRadioButtonId ? "Individual Game" : "Tournament";
                _callback.OnSelectedRadioButton(_title, selected);
                Toast.MakeText(this.Activity, "Saved.", ToastLength.Short).Show();
            });
            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this.Activity, "Canceled.", ToastLength.Short).Show();
            });
            return inputDialog.Show();
        }
    }
}