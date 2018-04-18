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

using MoneyTracker02.Resources;
using MoneyTracker02.Resources.DataHelper;
using MoneyTracker02.Resources.Model;
using Android.Util;

namespace MoneyTracker02
{
    [Activity(Label = "Expencies")]
    public class ExpenciesActivity : Activity
    {
        ListView lstexpencies;
        List<Expencies> lstExpencies = new List<Expencies>();
        Button btnexpadd;
        DataBase db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Expencies);

            //Create DataBase
            db = new DataBase();
            db.CreateDataBase();

            btnexpadd = FindViewById<Button>(Resource.Id.btnExpadd);
            lstexpencies = FindViewById<ListView>(Resource.Id.listExpencies);

            // Load Data
            LoadData();

            btnexpadd.Click += delegate {
                StartActivity(typeof(ExpenciesAddActivity));
            };

            lstexpencies.ItemClick += Lstexpencies_ItemClick;

        }

        private void Lstexpencies_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            for (int i = 0; i < lstexpencies.Count; i++)
            {
                if (e.Position == i)
                {
                    lstexpencies.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkCyan);
                    //Console.WriteLine("Clicked item id: " + e.View.FindViewById<TextView>(Resource.Id.txtExpid).Text);

                    var activity = new Intent(this, typeof(ExpenciesEditActivity));
                    activity.PutExtra("ExpId", e.View.FindViewById<TextView>(Resource.Id.txtExpid).Text);
                    StartActivity(activity);

                }
                else
                    lstexpencies.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }

        private void LoadData()
        {
            lstExpencies = db.SelectTableExpencies();
            var adapter = new ExpenciesListViewAdapter(this, lstExpencies);
            lstexpencies.Adapter = adapter;
        }
    }
}