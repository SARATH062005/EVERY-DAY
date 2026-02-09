using System;
using EveryDay.Tools;

namespace EveryDay.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            var viewer = new DatabaseViewer();

            if (args.Length == 0)
            {
                ShowMenu(viewer);
                return;
            }

            var command = args[0].ToLower();

            switch (command)
            {
                case "info":
                    viewer.ShowDatabaseInfo();
                    break;

                case "list":
                case "all":
                    viewer.ShowAllBlocks();
                    break;

                case "stats":
                case "statistics":
                    viewer.ShowStatistics();
                    break;

                case "export":
                    var outputPath = args.Length > 1 ? args[1] : "database_export.json";
                    viewer.ExportToJson(outputPath);
                    break;

                case "search":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("❌ Please provide a search term: DbViewer search <term>");
                        return;
                    }
                    viewer.SearchBlocks(args[1]);
                    break;

                case "recovery":
                case "restore":
                    RecoveryTool.RunRecovery(args);
                    break;

                case "dump":
                    RawDump.RunDump(args);
                    break;

                default:
                    ShowMenu(viewer);
                    break;
            }
        }

        static void ShowMenu(DatabaseViewer viewer)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        EVERY-DAY Database Viewer Tool                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            
            viewer.ShowDatabaseInfo();
            
            Console.WriteLine("\n\n📋 Available Commands:");
            Console.WriteLine("  dotnet run --project DbViewer.csproj info       - Show database info");
            Console.WriteLine("  dotnet run --project DbViewer.csproj list       - List all blocks");
            Console.WriteLine("  dotnet run --project DbViewer.csproj stats      - Show statistics");
            Console.WriteLine("  dotnet run --project DbViewer.csproj export     - Export to JSON");
            Console.WriteLine("  dotnet run --project DbViewer.csproj search <term> - Search blocks");
            Console.WriteLine();
            Console.WriteLine("💡 Tip: You can also run without arguments to see this menu.");
        }
    }
}
