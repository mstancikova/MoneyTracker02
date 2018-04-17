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

using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MoneyTracker02.Resources.Model
{
    [Table("Banks")]
    public class Banks
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Bankdate { get; set; }
        public string Bankname { get; set; }
        public double Bankmoney { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Expencies> Expencies { get; set; }
    }
}