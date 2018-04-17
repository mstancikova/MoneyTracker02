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
    [Activity(Label = "Add new expence")]
    public class ExpenciesAddActivity : Activity
    {
        EditText edtexpdate, edtexpmoney, edtexpnote;
        Spinner spnnrexpgroup, spnnrexpbank;
        Button btnexpadd;
        DataBase db;
        List<Groups> spinnerGroups = new List<Groups>();
        List<Banks> spinnerBanks = new List<Banks>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here

            SetContentView(Resource.Layout.ExpenciesAdd);

            //Create DataBase
            db = new DataBase();
            db.CreateDataBase();

            edtexpdate = FindViewById<EditText>(Resource.Id.edtExpdate);
            edtexpdate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            spnnrexpgroup = FindViewById<Spinner>(Resource.Id.spinnerExpgroup);
            spnnrexpbank = FindViewById<Spinner>(Resource.Id.spinnerExpbanks);
            edtexpmoney = FindViewById<EditText>(Resource.Id.edtExpmoney);
            edtexpnote = FindViewById<EditText>(Resource.Id.edtExpnote);
            btnexpadd = FindViewById<Button>(Resource.Id.btnExpadd);

            // spinner data
            LoadSpinnerGroups();
            LoadSpinnerBanks();

            // Events
            edtexpdate.Click += EdtExpdate_Click;

            btnexpadd.Click += delegate
            {
                double edt_expmoney = double.Parse(edtexpmoney.Text, System.Globalization.CultureInfo.InvariantCulture);

                Expencies exp = new Expencies()
                {
                    Expdate = edtexpdate.Text,
                    GroupsId = (int)spnnrexpgroup.SelectedItemId,
                    BanksId = (int)spnnrexpbank.SelectedItemId,
                    Expmoney = edt_expmoney,
                    Expnote = edtexpnote.Text
                };
                db.InsertIntoTableExpencies(exp);
                StartActivity(typeof(ExpenciesActivity));
            };
        }

        private void LoadSpinnerGroups()
        {
            spinnerGroups = db.SelectTableGroups();
            var adapter = new GroupListViewAdapter(this, spinnerGroups);
            spnnrexpgroup.Adapter = adapter;
        }

        private void LoadSpinnerBanks()
        {
            spinnerBanks = db.SelectTableBanks();
            var adapter = new BanksListViewAdapter(this, spinnerBanks);
            spnnrexpbank.Adapter = adapter;
        }

        private void EdtExpdate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                edtexpdate.Text = time.ToString("dd/MM/yyyy");
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
                DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month - 1, currently.Day);
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