﻿using FreightControlMaui.Constants;
using SQLite;

namespace FreightControlMaui.Repositories
{
    public class GenericRepository<T> where T : new()
    {
        private readonly SQLiteAsyncConnection _db;

        public GenericRepository()
        {
            _db = new SQLiteAsyncConnection(StringConstants.DbPath);
            _db.CreateTableAsync<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Table<T>().ToListAsync();
        }

        public async Task<int> SaveAsync(T model)
        {
            return await _db.InsertAsync(model);
        }

        public async Task<int> UpdateAsync(T model)
        {
            return await _db.UpdateAsync(model);
        }

        public async Task<int> DeleteAsync(T model)
        {
            return await _db.DeleteAsync(model);
        }

        public async Task<int> DeleteAllAsync()
        {
            return await _db.DeleteAllAsync<T>();
        }
    }
}