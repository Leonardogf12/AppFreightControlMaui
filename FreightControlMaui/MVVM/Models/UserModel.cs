﻿using SQLite;

namespace FreightControlMaui.MVVM.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string FirebaseLocalId { get; set; }
    }
}

