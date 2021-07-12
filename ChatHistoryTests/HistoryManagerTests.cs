using System;
using System.Linq;
using ChatHistoryLib.Contracts;
using ChatHistoryLib.Implementation;
using ChatHistoryTests.Mocks;
using NUnit.Framework;

namespace ChatHistoryTests
{
    [TestFixture]
    public class HistoryStatefulManagerTests
    {
        private IHistoryDataManager _historyDataManager;
        
        [SetUp]
        public void Setup()
        {
            _historyDataManager = new HistoryDataStatefulManager(new HistoryDataProviderMock());
        }


        [Test]
        public void DataRetrieveTest()
        {
            Assert.AreEqual(_historyDataManager.DataSet.Count(), 12);
            
        }
        
        [Test]
        public void OutOfRangeDataRetrieveTest()
        {
            Assert.IsEmpty(_historyDataManager.GetActionsSummeryMinuteByMinutePerDay(new DateTime(2023, 7, 5)).ToList());

        }

        [Test]
        public void DataRetrieveMinuteByMinute_CombineSameMinutes()
        {
            Assert.AreNotEqual(_historyDataManager.GetActionsSummeryMinuteByMinutePerDay(new DateTime(2021, 7, 5)).Count(), 4);
            Assert.AreEqual(_historyDataManager.GetActionsSummeryMinuteByMinutePerDay(new DateTime(2021, 7, 5)).Count(), 3);

        }
        
        [Test]
        public void DataRetrieveMinuteByMinute_PointInTime()
        {
            Assert.IsNull(_historyDataManager.GetActionsSummeryMinuteByMinutePerDay(new DateTime(2021, 7, 8))
                .Select(i=>i.TimePoint)
                .FirstOrDefault(i=> i == "3:30"));
            
            Assert.AreEqual("02:30",
                _historyDataManager.GetActionsSummeryMinuteByMinutePerDay(new DateTime(2021, 7, 6))
                .Select(i=>i.TimePoint)
                .FirstOrDefault(i=> i == "02:30"));
            
            // we could do a lot of other test

        }
        
        // Also we could test combine by hours in the same way
        // I think the main target is to be sure that the code is testable 


    }
}