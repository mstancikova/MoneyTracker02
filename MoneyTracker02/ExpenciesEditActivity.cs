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
    [Activity(Label = "Edit Expence")]
    public class ExpenciesEditActivity : Activity
    {
        EditText /*edteexpid,*/ edteexpdate, edteexpmoney, edteexpnote;
        Spinner spnnreexpgroup, spnnreexpbank;
        Button btneexpedit, btneexpdelete;
        DataBase db;
        List<Groups> spinnerGroups = new List<Groups>();
        List<Banks> spinnerBanks = new List<Banks>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ExpenciesEdit);

            string eexpid = Intent.GetStringExtra("ExpId") ?? string.Empty;

            //Create DataBase
            db = new DataBase();
            db.CreateDataBase();

            LoadData();

            //edteexpid = FindViewById<EditText>(Resource.Id.edtEExpid);
            edteexpdate = FindViewById<EditText>(Resource.Id.edtEExpdate);
            spnnreexpgroup = FindViewById<Spinner>(Resource.Id.spinnerEExpgroup);
            spnnreexpbank = FindViewById<Spinner>(Resource.Id.spinnerEExpbanks);
            edteexpmoney = FindViewById<EditText>(Resource.Id.edtEExpmoney);
            edteexpnote = FindViewById<EditText>(Resource.Id.edtEExpnote);
            btneexpedit = FindViewById<Button>(Resource.Id.btnEExpedit);
            btneexpdelete = FindViewById<Button>(Resource.Id.btnEExpdelete);

            // spinner data
            LoadSpinnerGroups();
            LoadSpinnerBanks();

            // Events
            edteexpdate.Click += EdtExpdate_Click;

            btneexpedit.Click += delegate
            {
                /*double edt_expmoney = double.Parse(edteexpmoney.Text, System.Globalization.CultureInfo.InvariantCulture);
                int expid = Convert.ToInt32(eexpid);
                Expencies exp = new Expencies()
                {
                    Id = expid,
                    Expdate = edteexpdate.Text,
                    GroupsId = (int)spnnreexpgroup.SelectedItemId,
                    BanksId = (int)spnnreexpbank.SelectedItemId,
                    Expmoney = edt_expmoney,
                    Expnote = edteexpnote.Text
                };
                db.UpdateTableExpencies(exp);
                StartActivity(typeof(ExpenciesActivity));*/
            };

            btneexpdelete.Click += delegate
            {
                int expid = Convert.ToInt32(eexpid);
                db.DeleteTableExpenciesWhereId(expid);
            };

        }

        private void LoadData()
        {
            string eexpid = Intent.GetStringExtra("ExpId") ?? string.Empty;
            int expid = Convert.ToInt32(eexpid);
            db.SelectQueryTableExpencies(expid);

        }


        private void LoadSpinnerGroups()
        {
            spinnerGroups = db.SelectTableGroups();
            var adapter = new GroupListViewAdapter(this, spinnerGroups);
            spnnreexpgroup.Adapter = adapter;
        }

        private void LoadSpinnerBanks()
        {
            spinnerBanks = db.SelectTableBanks();
            var adapter = new BanksListViewAdapter(this, spinnerBanks);
            spnnreexpbank.Adapter = adapter;
        }

        private void EdtExpdate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                edteexpdate.Text = time.ToString("dd/MM/yyyy");
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