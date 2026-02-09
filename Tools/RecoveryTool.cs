using System;
using System.IO;
using System.Linq;
using LiteDB;
using EveryDay.Models;
using EveryDay.Data;
using System.Collections.Generic;

namespace EveryDay.Tools
{
    public class RecoveryTool
    {
        public static void RunRecovery(string[] args)
        {
            var appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EveryDay");
            var backupPath = Path.Combine(appData, "everyday_corrupted_backup.db");
            var livePath = Path.Combine(appData, "everyday.db");

            if (!File.Exists(backupPath))
            {
                Console.WriteLine("❌ Backup file not found at: " + backupPath);
                return;
            }

            Console.WriteLine("🔄 Starting Recovery Process...");
            Console.WriteLine("From: " + backupPath);
            Console.WriteLine("To: " + livePath);

            int recoveredBlocks = 0;
            int failedBlocks = 0;

            try
            {
                using (var backupDb = new LiteDatabase($"Filename={backupPath};ReadOnly=true"))
                using (var liveDb = new LiteDatabase(livePath))
                {
                    var backupBlocks = backupDb.GetCollection("blocks");
                    var liveBlocks = liveDb.GetCollection("blocks");

                    Console.WriteLine($"Found {backupBlocks.Count()} blocks in backup.");

                    foreach (var doc in backupBlocks.FindAll())
                    {
                        try
                        {
                            // Try to insert the BsonDocument directly to avoid mapping errors
                            if (!liveBlocks.Exists(Query.EQ("_id", doc["_id"])))
                            {
                                liveBlocks.Insert(doc);
                                recoveredBlocks++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"⚠️ Could not recover block {doc["_id"]}: {ex.Message}");
                            failedBlocks++;
                        }
                    }

                    // Also try to recover UserStats if possible
                    try
                    {
                        var backupStats = backupDb.GetCollection("userstats");
                        var liveStats = liveDb.GetCollection<UserStats>("userstats");
                        if (backupStats.Count() > 0)
                        {
                            var statsDoc = backupStats.FindAll().FirstOrDefault();
                            if (statsDoc != null && liveStats.Count() == 0)
                            {
                                var stats = BsonMapper.Global.ToObject<UserStats>(statsDoc);
                                liveStats.Insert(stats);
                                Console.WriteLine("✅ User statistics recovered.");
                            }
                        }
                    }
                    catch { /* Stats might be what corrupted it, so ignore if fails */ }
                }

                Console.WriteLine("\n✨ Recovery Summary:");
                Console.WriteLine($"✅ Successfully recovered: {recoveredBlocks} blocks");
                Console.WriteLine($"❌ Failed to recover: {failedBlocks} blocks");
                Console.WriteLine("\nYou can now open the application to see your restored data.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Critical error during recovery: " + ex.Message);
            }
        }
    }
}
