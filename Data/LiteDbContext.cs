using LiteDB;
using System;
using System.IO;
using EveryDay.Models;

namespace EveryDay.Data
{
    public class LiteDbContext : IDisposable
    {
        public LiteDatabase Database { get; }

        public LiteDbContext()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EveryDay");
            Directory.CreateDirectory(folder);
            var path = Path.Combine(folder, "everyday.db");
            
            // Open database with optimized settings for faster startup
            var connectionString = new ConnectionString
            {
                Filename = path,
                Connection = ConnectionType.Shared // Allow multiple connections
            };
            
            Database = new LiteDatabase(connectionString);
            
            // Skip index creation on startup for faster launch
            // Indexes will be created automatically by LiteDB as needed
        }

        public void Dispose()
        {
            Database?.Dispose();
        }
    }
}
