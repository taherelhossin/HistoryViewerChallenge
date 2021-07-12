using System;
using System.Collections.Generic;
using System.Linq;
using ChatHistoryLib.Contracts;
using ChatHistoryLib.Implementation;
using ConsoleTools;

namespace ChatHistoryViewer
{
    static class Program
    {
        static void Main(string[] args)
        {
            // program will act as DI container
            Console.WriteLine(Figgle.FiggleFonts.Slant.Render("\nHistory Viewer"));

            IHistoryDataProvider dataProvider = new InMemoryHistoryDataProvider();
            IHistoryDataManager dataManager = new HistoryDataStatefulManager(dataProvider);
            ConsoleMenu menu = null;

            try
            {
                Console.WriteLine("\n\n\nNote: this Demo show three days back History only, just to be human testable\n\n\n");
                Console.WriteLine("Please press any key to open the menu, use arrow keys up/down to navigate");
                Console.ReadKey();
                        
                int selectedBackDaysCount = 0;
                
                
                var subMenu = new ConsoleMenu(args, level: 1)
                    .Add("Minute By Minute", (c) =>
                    {
                        c.CloseMenu();
                        menu.CloseMenu();
                        ShowResults(dataManager
                                .GetActionsSummeryMinuteByMinutePerDay(
                                    DateTime.Now.Subtract(TimeSpan.FromDays(selectedBackDaysCount))).ToList());
                    })
                    .Add("Hourly", (c) =>
                    {
                        c.CloseMenu();
                        menu.CloseMenu();
                        ShowResults(dataManager
                                .GetActionsSummeryHourlyPerDay(
                                    DateTime.Now.Subtract(TimeSpan.FromDays(selectedBackDaysCount))).ToList());
                    })
                    .Add("Close", ConsoleMenu.Close)
                    .Configure(config =>
                    {
                        config.SelectedItemBackgroundColor = ConsoleColor.DarkCyan;
                        config.ClearConsole = true;
                        config.Title = "Choose View Method";
                        config.EnableWriteTitle = true;
                        config.EnableBreadcrumb = false;
                        config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                    });
                
                 menu = new ConsoleMenu(args, level: 0)
                    .Add("Today", () =>
                    {
                        selectedBackDaysCount = 0;
                        subMenu.Show();
                    })
                    .Add("Yesterday", () => {
                        selectedBackDaysCount = 1;
                        subMenu.Show();
                    })
                    .Add("3 days ago", () => {
                        selectedBackDaysCount = 2;
                        subMenu.Show();
                    })
                    .Add("Exit", () => Environment.Exit(0))
                    .Configure(config =>
                    {
                        config.Selector = "--> ";
                        config.Title = "Choose the day";
                        config.SelectedItemBackgroundColor = ConsoleColor.DarkCyan;
                        config.EnableWriteTitle = true;
                        config.EnableBreadcrumb = false;
                    });
                
                     menu.Show();
                     Console.WriteLine("\n\n\n-----Thank you for using the app-----");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ShowResults(List<(string TimePoint, List<string>)> dataMinutByMinute)
        {
            if (dataMinutByMinute.Count == 0)
            {
                Console.WriteLine("No History data for this date");
            }
            else
            {
                dataMinutByMinute.ForEach(x =>
                {
                    Console.WriteLine($"{x.TimePoint}:");
                    x.Item2.ForEach(str => Console.WriteLine($"\t{str}"));
                });
            }
        }
    }
}
