using LiteDB;
using EveryDay.Models;
using System;
using System.Collections.Generic;

namespace EveryDay.Data
{
    public class BlockRepository
    {
        private readonly LiteDatabase _db;
        private readonly ILiteCollection<Block> _collection;

        public BlockRepository(LiteDbContext context)
        {
            _db = context.Database;
            _collection = _db.GetCollection<Block>("blocks");
        }

        public IEnumerable<Block> GetAll()
        {
            return _collection.FindAll();
        }
        
        public IEnumerable<Block> Search(string text)
        {
             return _collection.Find(Query.Contains("Content", text));
        }

        public Block GetById(Guid id) => _collection.FindById(id);
        public void Insert(Block entity) => _collection.Insert(entity);
        public void Update(Block entity) => _collection.Update(entity);
        public void Delete(Guid id) => _collection.Delete(id);
    }
}
