using FreightControlMaui.MVVM.Models;
using SQLite;

namespace FreightControlMaui.Data
{
    public class DbApp
    {
        private SQLiteAsyncConnection _dbApp;

        public DbApp(string path)
        {
            _dbApp = new SQLiteAsyncConnection(path);
            
            _dbApp.CreateTableAsync<FreightModel>();
            _dbApp.CreateTableAsync<ToFuelModel>();
            _dbApp.CreateTableAsync<UserModel>();

        }
    }
}

