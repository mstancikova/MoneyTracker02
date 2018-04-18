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
using MoneyTracker02.Resources.DataHelper;

namespace MoneyTracker02.Resources
{
    public class ViewHolderExpencies : Java.Lang.Object
    {
        public TextView TxtExpid { get; set; }
        public TextView TxtExpdata { get; set; }
        public TextView TxtExpgroup { get; set; }
        public TextView TxtExpbank { get; set; }
        public TextView TxtExpmoney { get; set; }
    }

    class ExpenciesListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Expencies> lstExpencies;

        public ExpenciesListViewAdapter(Activity activity, List<Expencies> lstExpencies)
        {
            this.activity = activity;
            this.lstExpencies = lstExpencies;
        }

        public override int Count
        {
            get
            {
                return lstExpencies.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstExpencies[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view_Expencies, parent, false);

            var txtExpid = view.FindViewById<TextView>(Resource.Id.txtExpid);
            var txtExpdate = view.FindViewById<TextView>(Resource.Id.txtExpdate);
            var txtExpgroup = view.FindViewById<TextView>(Resource.Id.txtExpgroup);
            var txtExpbank = view.FindViewById<TextView>(Resource.Id.txtExpbank);
            var txtExpmoney = view.FindViewById<TextView>(Resource.Id.txtExpmoney);

            DataBase db = new DataBase();
            db.CreateDataBase();

            txtExpid.Text = Convert.ToString(lstExpencies[position].Id);
            txtExpdate.Text = lstExpencies[position].Expdate;
            txtExpgroup.Text = db.GetGroupnameById(lstExpencies[position].GroupsId);
            txtExpbank.Text = db.GetBanknameById(lstExpencies[position].BanksId);
            txtExpmoney.Text = Convert.ToString(lstExpencies[position].Expmoney);

            return view;
        }
    }
}