using System;
using System.IO;
using System.Linq;
using LiteDB;
using EveryDay.Models;
using EveryDay.Data;

namespace EveryDay.Tools
{
    /// <summary>
    /// Utility to view and inspect the application database
    /// </summary>
    public class DatabaseViewer
    {
        private readonly string _dbPath;

        public DatabaseViewer()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EveryDay");
            _dbPath = Path.Combine(folder, "everyday.db");
        }

        private string GetBlockContent(Block block)
        {
            return block switch
            {
                TextBlock tb => tb.Content,
                CheckboxBlock cb => cb.Content,
                HeaderBlock hb => hb.Content,
                _ => "[Unknown block type]"
            };
        }

        public void ShowDatabaseInfo()
        {
            Console.WriteLine("=== EVERY-DAY Database Information ===\n");
            Console.WriteLine($"Database Location: {_dbPath}");
            
            if (!File.Exists(_dbPath))
            {
                Console.WriteLine("❌ Database file does not exist yet.");
                Console.WriteLine("   The database will be created when you first use the application.");
                return;
            }

            var fileInfo = new FileInfo(_dbPath);
            Console.WriteLine($"Database Size: {fileInfo.Length:N0} bytes ({fileInfo.Length / 1024.0:F2} KB)");
            Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
            Console.WriteLine();

            using (var db = new LiteDatabase(_dbPath))
            {
                var collections = db.GetCollectionNames().ToList();
                Console.WriteLine($"Collections: {collections.Count}");
                
                foreach (var collectionName in collections)
                {
                    var col = db.GetCollection(collectionName);
                    Console.WriteLine($"  - {collectionName}: {col.Count()} documents");
                }
            }
        }

        public void ShowAllBlocks()
        {
            Console.WriteLine("\n=== All Stored Blocks ===\n");

            if (!File.Exists(_dbPath))
            {
                Console.WriteLine("❌ No database found.");
                return;
            }

            using (var context = new LiteDbContext())
            {
                var repository = new BlockRepository(context);
                var blocks = repository.GetAll().OrderBy(b => b.Section).ThenBy(b => b.Order).ToList();

                if (blocks.Count == 0)
                {
                    Console.WriteLine("📭 No data stored yet.");
                    return;
                }

                Console.WriteLine($"Total Blocks: {blocks.Count}\n");

                var groupedBySection = blocks.GroupBy(b => b.Section);
                
                foreach (var section in groupedBySection)
                {
                    Console.WriteLine($"\n📁 Section: {section.Key}");
                    Console.WriteLine(new string('-', 60));
                    
                    foreach (var block in section)
                    {
                        Console.WriteLine($"\n  ID: {block.Id}");
                        Console.WriteLine($"  Type: {block.Type}");
                        Console.WriteLine($"  Order: {block.Order}");
                        Console.WriteLine($"  Content: {GetBlockContent(block)}");
                        
                        if (block is CheckboxBlock checkbox)
                        {
                            Console.WriteLine($"  Checked: {checkbox.IsChecked}");
                        }
                        
                        Console.WriteLine($"  Created: {block.CreatedAt}");
                    }
                }
            }
        }

        public void ShowStatistics()
        {
            Console.WriteLine("\n=== Database Statistics ===\n");

            if (!File.Exists(_dbPath))
            {
                Console.WriteLine("❌ No database found.");
                return;
            }

            using (var context = new LiteDbContext())
            {
                var repository = new BlockRepository(context);
                var blocks = repository.GetAll().ToList();

                if (blocks.Count == 0)
                {
                    Console.WriteLine("📭 No data stored yet.");
                    return;
                }

                Console.WriteLine($"📊 Total Blocks: {blocks.Count}");
                Console.WriteLine();

                // By Type
                Console.WriteLine("By Type:");
                var byType = blocks.GroupBy(b => b.Type);
                foreach (var group in byType)
                {
                    Console.WriteLine($"  {group.Key}: {group.Count()}");
                }
                Console.WriteLine();

                // By Section
                Console.WriteLine("By Section:");
                var bySection = blocks.GroupBy(b => b.Section);
                foreach (var group in bySection)
                {
                    Console.WriteLine($"  {group.Key}: {group.Count()}");
                }
                Console.WriteLine();

                // Checkbox Statistics
                var checkboxes = blocks.OfType<CheckboxBlock>().ToList();
                if (checkboxes.Any())
                {
                    var checkedCount = checkboxes.Count(c => c.IsChecked);
                    var uncheckedCount = checkboxes.Count - checkedCount;
                    Console.WriteLine("Checkbox Status:");
                    Console.WriteLine($"  ✅ Checked: {checkedCount}");
                    Console.WriteLine($"  ⬜ Unchecked: {uncheckedCount}");
                    Console.WriteLine($"  Completion Rate: {(checkedCount * 100.0 / checkboxes.Count):F1}%");
                    Console.WriteLine();
                }

                // Date Statistics
                var oldestBlock = blocks.OrderBy(b => b.CreatedAt).FirstOrDefault();
                var newestBlock = blocks.OrderByDescending(b => b.CreatedAt).FirstOrDefault();

                Console.WriteLine("Date Information:");
                if (oldestBlock != null)
                    Console.WriteLine($"  Oldest Block: {oldestBlock.CreatedAt}");
                if (newestBlock != null)
                    Console.WriteLine($"  Newest Block: {newestBlock.CreatedAt}");
            }
        }

        public void ExportToJson(string outputPath)
        {
            Console.WriteLine($"\n=== Exporting Database to JSON ===\n");

            if (!File.Exists(_dbPath))
            {
                Console.WriteLine("❌ No database found.");
                return;
            }

            using (var context = new LiteDbContext())
            {
                var repository = new BlockRepository(context);
                var blocks = repository.GetAll().OrderBy(b => b.Section).ThenBy(b => b.Order).ToList();

                if (blocks.Count == 0)
                {
                    Console.WriteLine("📭 No data to export.");
                    return;
                }

                // Create a simplified export format
                var exportData = blocks.Select(b => new
                {
                    Id = b.Id,
                    Type = b.Type,
                    Section = b.Section,
                    Order = b.Order,
                    Content = GetBlockContent(b),
                    IsChecked = (b is CheckboxBlock cb) ? cb.IsChecked : (bool?)null,
                    CreatedAt = b.CreatedAt
                }).ToList();

                var json = System.Text.Json.JsonSerializer.Serialize(exportData, new System.Text.Json.JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });

                File.WriteAllText(outputPath, json);
                Console.WriteLine($"✅ Exported {blocks.Count} blocks to: {outputPath}");
                Console.WriteLine($"   File size: {new FileInfo(outputPath).Length:N0} bytes");
            }
        }

        public void SearchBlocks(string searchTerm)
        {
            Console.WriteLine($"\n=== Searching for: '{searchTerm}' ===\n");

            if (!File.Exists(_dbPath))
            {
                Console.WriteLine("❌ No database found.");
                return;
            }

            using (var context = new LiteDbContext())
            {
                var repository = new BlockRepository(context);
                var results = repository.Search(searchTerm).ToList();

                if (results.Count == 0)
                {
                    Console.WriteLine($"❌ No blocks found containing '{searchTerm}'");
                    return;
                }

                Console.WriteLine($"Found {results.Count} matching block(s):\n");

                foreach (var block in results)
                {
                    Console.WriteLine($"  [{block.Section}] {block.Type}: {GetBlockContent(block)}");
                }
            }
        }
    }
}
