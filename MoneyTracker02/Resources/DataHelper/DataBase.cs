using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using MoneyTracker02.Resources.Model;
using SQLite;



namespace MoneyTracker02.Resources.DataHelper
{
    public class DataBase
    {
        string database_name = "moneytracker1.db3";
        string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
       
        public bool CreateDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.CreateTable<Groups>();
                    connection.CreateTable<Banks>();
                    connection.CreateTable<Expencies>();
                    return true;
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        // --------------------------------------------------------------- GROUPS 
        public bool InsertIntoTableGroups(Groups group)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Insert(group);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<Groups> SelectTableGroups()
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    return connection.Table<Groups>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool UpdateTableGroups(Groups group)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Query<Groups>("UPDATE Groups SET Groupname=? WHERE Id=?", group.Groupname, group.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool DeleteTableGroups(Groups group)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Delete(group);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool SelectQueryTableGroups(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Query<Groups>("SELECT * FROM Groups WHERE Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public string GetGroupnameById(int groupId)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    return connection.Table<Groups>().Where(x => x.Id == groupId).FirstOrDefault()?.Groupname.ToString();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }


        // ---------------------------------------------------------------- BANKS 
        public bool InsertIntoTableBanks(Banks bank)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Insert(bank);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<Banks> SelectTableBanks()
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    return connection.Table<Banks>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool UpdateTableBanks(Banks bank)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Query<Banks>("UPDATE Banks SET Bankdate=?, Bankname=?, Bankmoney=? WHERE Id=?", bank.Bankdate, bank.Bankname, bank.Bankmoney, bank.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool DeleteTableBanks(Banks bank)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Delete(bank);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool SelectQueryTableBanks(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Query<Banks>("SELECT * FROM Banks WHERE Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public string GetBanknameById(int bankId)
        {
            using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
            {
                return connection.Table<Banks>().Where(x => x.Id == bankId).FirstOrDefault()?.Bankname.ToString();
            }
        }

        // ---------------------------------------------------------------- EXPENCIES 
        public bool InsertIntoTableExpencies(Expencies exp)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Insert(exp);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<Expencies> SelectTableExpencies()
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    return connection.Table<Expencies>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool UpdateTableExpencies(Expencies exp)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Query<Expencies>("UPDATE Expencies SET Expdate=?, GroupsGroupname=?, BanksBankname=?, Expmoney=?, Expnote=? WHERE Id=?", exp.Expdate, exp.GroupsId, exp.BanksId, exp.Expmoney, exp.Expnote, exp.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool DeleteTableExpencies(Expencies exp)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Delete(exp);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool DeleteTableExpenciesWhereId(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Query<Banks>("DELETE * FROM Expencies WHERE Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool SelectQueryTableExpencies(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(folderPath, database_name)))
                {
                    connection.Query<Banks>("SELECT * FROM Expencies WHERE Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

    }
}