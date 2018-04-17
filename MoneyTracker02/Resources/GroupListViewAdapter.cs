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

using Java.Lang;
using MoneyTracker02.Resources.Model;

namespace MoneyTracker02.Resources
{
    public class ViewHolderGroups : Java.Lang.Object
    {
        public TextView TxtGroupname { get; set; }
    }
    public class GroupListViewAdapter:BaseAdapter
    {
        private Activity activity;
        private List<Groups> lstGroups;

        public GroupListViewAdapter(Activity activity, List<Groups> lstGroups)
        {
            this.activity = activity;
            this.lstGroups = lstGroups;
        }

        public override int Count
        {
            get
            {
                return lstGroups.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstGroups[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view_Groups, parent, false);
            var txtGroupname = view.FindViewById<TextView>(Resource.Id.txtGroupname);
            txtGroupname.Text = lstGroups[position].Groupname;
            return view;
        }
    }
}