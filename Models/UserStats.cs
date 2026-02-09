using LiteDB;
using System;

namespace EveryDay.Models
{
    public class UserStats
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public DateTime? LastActivityDate { get; set; }
    }
}
