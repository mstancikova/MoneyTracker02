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
    public class ViewHolderBanks : Java.Lang.Object
    {
        public TextView TxtBankname { get; set; }
        public TextView TxtBankdate { get; set; }
        public TextView TxtBankmoney { get; set; }
    }

    class BanksListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Banks> lstBanks;

        public BanksListViewAdapter(Activity activity, List<Banks> lstBanks)
        {
            this.activity = activity;
            this.lstBanks = lstBanks;
        }

        public override int Count
        {
            get
            {
                return lstBanks.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstBanks[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view_Banks, parent, false);

            var txtBankname = view.FindViewById<TextView>(Resource.Id.txtBankname);
            var txtBankdate = view.FindViewById<TextView>(Resource.Id.txtBankdate);
            var txtBankmoney = view.FindViewById<TextView>(Resource.Id.txtBankmoney);

            txtBankname.Text = lstBanks[position].Bankname;
            txtBankdate.Text = lstBanks[position].Bankdate;
            txtBankmoney.Text = Convert.ToString(lstBanks[position].Bankmoney);

            return view;
        }
    }
}