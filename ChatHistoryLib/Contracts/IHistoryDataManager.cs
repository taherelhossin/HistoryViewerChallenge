using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatHistoryLib.Contracts
{
    public interface IHistoryDataManager
    {
        public IQueryable<HistoryItem> DataSet { get; }

        public IQueryable<(string TimePoint, List<string>)> GetActionsSummeryHourlyPerDay(DateTime day);
        
        public IQueryable<(string TimePoint, List<string>)> GetActionsSummeryMinuteByMinutePerDay(DateTime day);

    }
}