using FreightControlMaui.Constants;
using FreightControlMaui.MVVM.Models;
using SQLite;

namespace FreightControlMaui.Repositories
{
    public class UserRepository : GenericRepository<UserModel>
    {
        private readonly SQLiteAsyncConnection _db;
        
        public UserRepository()
        {
            _db = new SQLiteAsyncConnection(StringConstants.DbPath);
        }
       
        public async Task<UserModel> GetUserByFirebaseLocalId(string localId)
        {
            return await _db.Table<UserModel>().Where(x => x.FirebaseLocalId == localId).FirstOrDefaultAsync();
        }
    }
}

