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
    [Activity(Label = "Groups")]
    public class GroupsActivity : Activity
    {
        ListView listgroups;
        List<Groups> lstSource = new List<Groups>();
        EditText edtgroupname;
        Button btnaddgroup, btneditgroup, btndeletegroup;
        DataBase db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Groups);

            //Create DataBase
            db = new DataBase();
            db.CreateDataBase();

            listgroups = FindViewById<ListView>(Resource.Id.listGroups);
            edtgroupname = FindViewById<EditText>(Resource.Id.edtGroupname);

            btnaddgroup = FindViewById<Button>(Resource.Id.btnAddGroup);
            btneditgroup = FindViewById<Button>(Resource.Id.btnEditGroup);
            btndeletegroup = FindViewById<Button>(Resource.Id.btnDeleteGroup);

            // Load Data
            LoadData();

            // Events
            btnaddgroup.Click += delegate
            {
                Groups groups = new Groups()
                {
                    Groupname = edtgroupname.Text
                };
                db.InsertIntoTableGroups(groups);
                //Clear();
                LoadData();

            };

            btneditgroup.Click += delegate
            {
                Groups groups = new Groups()
                {
                    Id = int.Parse(edtgroupname.Tag.ToString()),
                    Groupname = edtgroupname.Text
                };
                db.UpdateTableGroups(groups);
                //Clear();
                LoadData();

            };

            btndeletegroup.Click += delegate
            {
                Groups groups = new Groups()
                {
                    Id = int.Parse(edtgroupname.Tag.ToString()),
                    Groupname = edtgroupname.Text
                };
                db.DeleteTableGroups(groups);
                Clear();
                LoadData();

            };

            listgroups.ItemClick += (s, e) => {

                //Set background for selected item
                for(int i = 0; i < listgroups.Count; i++)
                {
                    if (e.Position == i)
                        listgroups.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkCyan);
                    else
                        listgroups.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }

                //binding data
                var txtGroupname = e.View.FindViewById<TextView>(Resource.Id.txtGroupname);

                edtgroupname.Text = txtGroupname.Text;
                edtgroupname.Tag = e.Id;
            };

        }

        // empty inputs
        void Clear()
        {
            edtgroupname.Text = "";
        }

        private void LoadData()
        {
            lstSource = db.SelectTableGroups();
            var adapter = new GroupListViewAdapter(this, lstSource);
            listgroups.Adapter = adapter;
            //listviewgroups.ItemClick += ListViewGroups_ItemClick;
        }
    }
}