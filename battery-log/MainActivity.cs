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
            RadioButton left = FindViewById<RadioButton>(Resource.Id.left);
            RadioButton right = FindViewById<RadioButton>(Resource.Id.right);            
            Button addRecord = FindViewById<Button>(Resource.Id.addRecord);
            Button showHistory = FindViewById<Button>(Resource.Id.BtnShowHistory);

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
                // Add a new record from the picked date and selected radio buttons
                records.Add(dateDisplay.Text, selection);
            };

            // Start a history activity showing previous entries
            showHistory.Click += (object sender, EventArgs e) =>
            {
                // Store the records Dictionary in the bundle
                bundle.PutSerializable("records", (Java.IO.ISerializable)records);

                var intent = new Intent(this, typeof(History));
                StartActivity(intent);
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

