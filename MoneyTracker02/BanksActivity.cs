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
using Java.Util;
using System.Globalization;

namespace MoneyTracker02
{
    [Activity(Label = "Banks")]
    public class BanksActivity : Activity
    {
        ListView listbanks;
        EditText edtbankdate, edtbankname, edtbankmoney;
        List<Banks> lstBanks = new List<Banks>();
        Button btnaddbank, btneditbank, btndeletebank;
        DataBase db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Banks);

            //Create DataBase
            db = new DataBase();
            db.CreateDataBase();

            edtbankdate = FindViewById<EditText>(Resource.Id.edtBankdate);
            edtbankdate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            edtbankname = FindViewById<EditText>(Resource.Id.edtBankname);
            edtbankmoney = FindViewById<EditText>(Resource.Id.edtBankmoney);

            btnaddbank = FindViewById<Button>(Resource.Id.btnAddBank);
            btneditbank = FindViewById<Button>(Resource.Id.btnEditBank);
            btndeletebank = FindViewById<Button>(Resource.Id.btnDeleteBank);

            listbanks = FindViewById<ListView>(Resource.Id.listBanks);

            // Load Data
            LoadData();

            // Events
            edtbankdate.Click += Edtbankdate_Click;

            btnaddbank.Click += delegate
            {
                double edt_bankmoney = double.Parse(edtbankmoney.Text, System.Globalization.CultureInfo.InvariantCulture);
                Banks banks = new Banks()
                {
                    Bankdate = edtbankdate.Text,
                    Bankname = edtbankname.Text,
                    Bankmoney = edt_bankmoney
                };
                db.InsertIntoTableBanks(banks);
                //Clear();
                LoadData();
            };

            btneditbank.Click += delegate
            {
                double edt_bankmoney = double.Parse(edtbankmoney.Text, System.Globalization.CultureInfo.InvariantCulture);
                Banks banks = new Banks()
                {
                    Id = int.Parse(edtbankname.Tag.ToString()),
                    Bankdate = edtbankdate.Text,
                    Bankname = edtbankname.Text,
                    Bankmoney = edt_bankmoney
                };
                db.UpdateTableBanks(banks);
                //Clear();
                LoadData();

            };

            btndeletebank.Click += delegate
            {
                double edt_bankmoney = double.Parse(edtbankmoney.Text, System.Globalization.CultureInfo.InvariantCulture);
                Banks banks = new Banks()
                {
                    Id = int.Parse(edtbankname.Tag.ToString()),
                    Bankdate = edtbankdate.Text,
                    Bankname = edtbankname.Text,
                    Bankmoney = edt_bankmoney
                };
                db.DeleteTableBanks(banks);
                Clear();
                LoadData();

            };

            // empty inputs
            void Clear()
            {
                edtbankdate.Text = "";
                edtbankname.Text = "";
                edtbankmoney.Text = "";
            }

            listbanks.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {

                //Set background for selected item
                for (int i = 0; i < listbanks.Count; i++)
                {
                    if (e.Position == i) { 
                        listbanks.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkCyan);
                    }
                    else
                        listbanks.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }

                //binding data
                var txtBankdate = e.View.FindViewById<TextView>(Resource.Id.txtBankdate);
                var txtBankname = e.View.FindViewById<TextView>(Resource.Id.txtBankname);
                var txtBankmoney = e.View.FindViewById<TextView>(Resource.Id.txtBankmoney);

                edtbankname.Text = txtBankname.Text;
                edtbankname.Tag = e.Id;
                edtbankdate.Text = txtBankdate.Text;
                edtbankmoney.Text = txtBankmoney.Text;
            };
        }

        private void LoadData()
        {
            lstBanks = db.SelectTableBanks();
            var adapter = new BanksListViewAdapter(this, lstBanks);
            listbanks.Adapter = adapter;
        }

        private void Edtbankdate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                edtbankdate.Text = time.ToString("dd/MM/yyyy");
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        // Create a class DatePickerFragment  
        public class DatePickerFragment : DialogFragment,
            DatePickerDialog.IOnDateSetListener
        {
            // TAG can be any string of your choice.  
            public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();
            // Initialize this value to prevent NullReferenceExceptions.  
            Action<DateTime> _dateSelectedHandler = delegate { };
            public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
            {
                DatePickerFragment frag = new DatePickerFragment
                {
                    _dateSelectedHandler = onDateSelected
                };
                return frag;
            }
            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                DateTime currently = DateTime.Now;
                DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month-1, currently.Day);
                return dialog;
            }
            public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
            {
                // Note: monthOfYear is a value between 0 and 11, not 1 and 12!  
                DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
                Android.Util.Log.Debug(TAG, selectedDate.ToLongDateString());
                _dateSelectedHandler(selectedDate);
            }
        }
    }
}