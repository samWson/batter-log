using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections.Generic;
using Android.Content;

namespace battery_log
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Declaring widget variables
            Button pickDateButton = FindViewById<Button>(Resource.Id.pickDate);
            TextView dateDisplay = FindViewById<TextView>(Resource.Id.dateDisplay);
            TextView historyArea = FindViewById<TextView>(Resource.Id.historyArea);
            RadioButton left = FindViewById<RadioButton>(Resource.Id.left);
            RadioButton right = FindViewById<RadioButton>(Resource.Id.right);            
            Button addRecord = FindViewById<Button>(Resource.Id.addRecord);            

            // A Dictionary to store created records with key=date, value=left/right
            Dictionary<string, string> records = new Dictionary<string, string>();

            pickDateButton.Click += (object sender, EventArgs e) =>
            {
                // Get a new DatePicker from the factory method
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    // Show the picked date
                    dateDisplay.Text = time.ToLongDateString();
                });

                // Show the DatePicker
                frag.Show(FragmentManager, DatePickerFragment.TAG);                
            };

            addRecord.Click += (object sender, EventArgs e) =>
            {
                string selection = GetSelection(left, right);
                string date = dateDisplay.Text;

                // Add a new record from the picked date and selected radio buttons
                records.Add(date, selection);

                // Show the record in historyArea
                historyArea.Text += date + "\t" + selection + "\n";

            };
            
        }

        // Returns the radio button selection
        private string GetSelection(RadioButton left, RadioButton right)
        {
            string selection = string.Empty;
            if (left.Checked)
            {
                selection = "left";
            }
            else if (right.Checked)
            {
                selection = "right";
            }

            return selection;
        }
    }
}

