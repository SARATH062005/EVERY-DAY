using EveryDay.Models;
using LiteDB;
using System;
using System.Linq;

namespace EveryDay.Data
{
    public class UserStatsRepository
    {
        private readonly ILiteCollection<UserStats> _collection;

        public UserStatsRepository(LiteDbContext context)
        {
            _collection = context.Database.GetCollection<UserStats>("userstats");
        }

        public UserStats GetStats()
        {
            var stats = _collection.FindAll().FirstOrDefault();
            if (stats == null)
            {
                stats = new UserStats();
                _collection.Insert(stats);
            }
            return stats;
        }

        public void Update(UserStats stats)
        {
            _collection.Update(stats);
        }
    }
}
