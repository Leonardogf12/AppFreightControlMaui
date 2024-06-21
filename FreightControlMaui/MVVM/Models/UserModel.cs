using SQLite;

namespace FreightControlMaui.MVVM.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string FirebaseLocalId { get; set; }
    }
}

