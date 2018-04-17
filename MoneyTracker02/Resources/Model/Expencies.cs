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
    [Table("Expencies")]
    public class Expencies
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Expdate { get; set; }
        [ForeignKey(typeof(Groups))]
        public int GroupsId { get; set; }
        [ForeignKey(typeof(Banks))]
        public int BanksId { get; set; }
        public double Expmoney { get; set; }
        public string Expnote { get; set; }
    }
}