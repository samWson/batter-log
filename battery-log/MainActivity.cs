using Android.App;
using Android.Widget;
using Android.OS;
using System;

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

            pickDateButton.Click += (object sender, EventArgs e) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    dateDisplay.Text = time.ToLongDateString();
                });

                frag.Show(FragmentManager, DatePickerFragment.TAG);                
            };
                        
        }
        
    }
}

