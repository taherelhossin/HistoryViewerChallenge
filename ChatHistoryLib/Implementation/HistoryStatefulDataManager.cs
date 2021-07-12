using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ChatHistoryLib.Contracts;

namespace ChatHistoryLib.Implementation
{
    public class HistoryDataStatefulManager : IHistoryDataManager
    {
        private readonly IHistoryDataProvider _dataProvider;

        public HistoryDataStatefulManager(IHistoryDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            try
            {
                dataProvider.Connect();
            }
            catch (FailedToConnectException)
            {
                Console.WriteLine("Failed To connect to Data Provider");
                throw;
            }
        }

        public IQueryable<HistoryItem> DataSet => _dataProvider.RetrieveData();

        public IQueryable<(String TimePoint, List<string>)> GetActionsSummeryHourlyPerDay(DateTime day)
        {
            return this.DataSet
                .Where(x => x.TimeStamp.ToShortDateString() == day.ToShortDateString())
                .GroupBy(x => x.TimeStamp.ToString("hh a"))
                .OrderBy(x => x.Key)
                .Select(x => BeautifyActions(x, false));
        }

        public IQueryable<(string TimePoint, List<string>)> GetActionsSummeryMinuteByMinutePerDay(DateTime day)
        {
            return this.DataSet
                .Where(x => x.TimeStamp.ToShortDateString() == day.ToShortDateString())
                .GroupBy(x => x.TimeStamp.ToShortTimeString())
                .OrderBy(x => x.Key)
                .Select(x => BeautifyActions(x, true));
        }

        private (String TimePoint, List<string>) BeautifyActions(IGrouping<string, HistoryItem> historyItems,
            bool expandActions)
        {
            var resultsList = new List<string>();

            if (expandActions)
            {
                foreach (var item in historyItems)
                {
                    switch (item.Type)
                    {
                        case ActionType.EnterRoom:
                            resultsList.Add($"{item.FromUserName} enters the room");
                            break;
                        case ActionType.LeaveRoom:
                            resultsList.Add($"{item.FromUserName} leaves");
                            break;
                        case ActionType.Comment:
                            resultsList.Add($"{item.FromUserName} comments {item.Message}");
                            break;
                        case ActionType.HighFive:
                            resultsList.Add($"{item.FromUserName} high-fives to {item.ToUserName}");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(
                                $"{nameof(historyItems)}Contains undefind action type");
                    }
                }

                return new(historyItems.Key, resultsList);
            }
            else
            {
                var groupedActionsList = historyItems
                    .GroupBy(x => x.Type)
                    .Select(x => new
                    {
                        action = x.Key, items = x.GroupBy(p => p.FromUserName)
                            .ToList()
                    });
                groupedActionsList.ToList().ForEach(z =>
                {
                    switch (z.action)
                    {
                        case ActionType.EnterRoom:
                            // It's meant here to treat the same person with multiple actions as one action for convince 
                            resultsList.Add($"{z.items.Count} entered");
                            break;
                        case ActionType.LeaveRoom:
                            // It's meant here to treat the same person with multiple actions as one action for convince 
                            resultsList.Add($"{z.items.Count} left");
                            break;
                        case ActionType.Comment:
                            resultsList.Add($"{z.items.Sum(i => i.Count())} comments");
                            break;
                        case ActionType.HighFive:
                            resultsList
                                .Add($"{z.items.Count} high-five {z.items.Sum(i => i.GroupBy(t=>t.ToUserName).ToList().Count)}");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(
                                $"{nameof(historyItems)}Contains undefind action type");
                    }
                });
            }

            return new(historyItems.Key, resultsList);
        }
    }
}