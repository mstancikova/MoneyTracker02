using Android.App;
using Android.Widget;
using Android.OS;

namespace MoneyTracker02
{
    [Activity(Label = "MoneyTracker02", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button btnexpencies;
        Button btngroups;
        Button btnbanks;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btnexpencies = FindViewById<Button>(Resource.Id.btnExpencies);
            btngroups = FindViewById<Button>(Resource.Id.btnGroups);
            btnbanks = FindViewById<Button>(Resource.Id.btnBanks);

            btnexpencies.Click += delegate {
                StartActivity(typeof(ExpenciesActivity));
            };
            btngroups.Click += delegate {
                StartActivity(typeof(GroupsActivity));
            };

            btnbanks.Click += delegate {
                StartActivity(typeof(BanksActivity));
            };
        }
    }
}

