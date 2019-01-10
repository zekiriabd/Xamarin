using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SqlitDatabase
{


    public class TUser
    {
        [PrimaryKey]
        public int userId { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }


    }


    public partial class MainPage : ContentPage
    {

        private string GetDbPath()
        {
            string strPath = "";
            if (Device.RuntimePlatform == Device.Android)
            {
                strPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            return strPath + "/MyDb.db";
        }

        private void GetUserList() {
            string strPath = GetDbPath();
            List<TUser> Users = new List<TUser>();
            using (var con = new SQLite.SQLiteConnection(strPath))
            {
                Users = con.Table<TUser>().ToList() as List<TUser>;
                UserList.ItemsSource = Users;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            string strPath = GetDbPath();
            File.Delete(strPath);
            if (!File.Exists(strPath))
            {
                using (var con = new SQLite.SQLiteConnection(strPath))
                {
                    con.CreateTable<TUser>();
                }               
            }
            GetUserList();
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
           
            TUser oUser = new TUser();
            oUser.userId =int.Parse(txId.Text);
            oUser.firstName = txFirstName.Text;
            oUser.lastName = txLastName.Text;
            string strPath = GetDbPath();
            using (var con = new SQLite.SQLiteConnection(strPath))
            {
                con.Insert(oUser);
            }
            GetUserList();

        }

        private void BtnDel_Clicked(object sender, EventArgs e)
        {
            var id = int.Parse(txId.Text);           
            string strPath = GetDbPath();
            using (var con = new SQLite.SQLiteConnection(strPath))
            {
                con.Delete<TUser>(id);
            }
            GetUserList();
        }

        private void BtnSerch_Clicked(object sender, EventArgs e)
        {
            List<TUser> Users = new List<TUser>();
            var id = int.Parse(txId.Text);
            string strPath = GetDbPath();
            using (var con = new SQLite.SQLiteConnection(strPath))
            {
                Users.Add(con.Find<TUser>(id));
                UserList.ItemsSource = Users;
            }
        }
            
    }
}
