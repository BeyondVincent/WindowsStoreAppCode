using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace SQLiteUsage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        SQLiteAsyncConnection db;
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            await connectDB();
            await insetData();
            await selectData();
            await upDateData();
            await deleteData();
        }

        async Task<int> connectDB()
        {
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\beyondvincent.db";

            db = new SQLiteAsyncConnection(path);
            await db.DropTableAsync<User>();
            await db.CreateTableAsync<User>();
            return 0;
        }

        async Task<int> insetData()
        {
            var tom = new User
            {
                Name = "破船",
                Age = 30
            };

            await db.InsertAsync(tom);

            var isUser = new List<User>()
            {
                new User
                {
                   Name = "小王",
                    Age = 18
                 },
                 new User
                 {
                     Name = "小张",
                     Age = 29
                 },
                 new User
                 {
                     Name = "小李",
                     Age = 30
                 },
            };

            await db.InsertAllAsync(isUser);
            return 0;
        }

        async Task<int> selectData()
        {
            //var users = await db.QueryAsync<User>("SELECT * FROM Users");
            var users = await db.QueryAsync<User>("SELECT * FROM Users WHERE Name == ?", new object[] { "破船" });

            var count = users.Any() ? users.Count : 0;

            if (count > 0)
            {
                foreach (User user in users)
                {
                    Debug.WriteLine("id:" + user.Id + "name:" + user.Name + "age:" + user.Age);
                }
            }
            return 0;
        }

        async Task<int> upDateData()
        {
            var someUsers = await db.QueryAsync<User>("SELECT * FROM Users WHERE Name == ?", new object[] { "破船" });
            var firstUser = someUsers.First();
            firstUser.Age = 100;
            await db.UpdateAsync(firstUser);

            await selectData();

            return 0;
        }

        async Task<int> deleteData()
        {
            var someUsers = await db.QueryAsync<User>("SELECT * FROM Users WHERE Name == ?", new object[] { "破船" });
            var firstUser = someUsers.First();
            await db.DeleteAsync(firstUser);

            await selectData();

            return 0;

        }



    }

    [Table("Users")]
    public class User
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
