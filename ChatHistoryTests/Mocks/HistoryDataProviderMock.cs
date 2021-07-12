using System;
using System.Collections.Generic;
using System.Linq;
using ChatHistoryLib;
using ChatHistoryLib.Contracts;
using ChatHistoryLib.Implementation;
using moment.net;
using NUnit.Framework;

namespace ChatHistoryTests.Mocks
{
    public class HistoryDataProviderMock : IHistoryDataProvider
    {
        public void Connect(string connectionString = "")
        {
            if (!string.IsNullOrEmpty(connectionString))
                throw new FailedToConnectException();
        }

        public IQueryable<HistoryItem> RetrieveData()
        {
            var mockData  = new List<HistoryItem>()
            {
                // First Day
                new HistoryItem
                    {FromUserName = "user1", Type = ActionType.EnterRoom, TimeStamp = new DateTime(2021, 7, 5, 5,30,0 )},
                new HistoryItem
                    {FromUserName = "user1", Type = ActionType.LeaveRoom, TimeStamp = new DateTime(2021, 7, 5, 1,31,0 )},
                new HistoryItem
                    {FromUserName = "user2", Type = ActionType.EnterRoom, TimeStamp = new DateTime(2021, 7, 5, 5,30,0 )},
                new HistoryItem
                    {FromUserName = "user2", Type = ActionType.LeaveRoom, TimeStamp = new DateTime(2021, 7, 5, 20,35,0 )},
                
                // Second Day
                new HistoryItem
                    {FromUserName = "user1", Type = ActionType.Comment, Message = "test message", TimeStamp = new DateTime(2021, 7, 6, 5,30,0 )},
                new HistoryItem
                    {FromUserName = "user1", Type = ActionType.LeaveRoom, TimeStamp = new DateTime(2021, 7, 6, 1,30,0 )},
                new HistoryItem
                    {FromUserName = "user2", Type = ActionType.EnterRoom, TimeStamp = new DateTime(2021, 7, 6, 2,30,0 )},
                new HistoryItem
                    {FromUserName = "user2", Type = ActionType.HighFive, ToUserName = "user1", TimeStamp = new DateTime(2021, 7, 6, 20,30,0 )},
                
                // Third Day
                new HistoryItem
                    {FromUserName = "user1", Type = ActionType.HighFive, ToUserName = "user2", TimeStamp = new DateTime(2021, 7, 7, 5,30,0 )},
                new HistoryItem
                    {FromUserName = "user1", Type = ActionType.HighFive, ToUserName = "user2",TimeStamp = new DateTime(2021, 7, 7, 1,30,0 )},
                new HistoryItem
                    {FromUserName = "user2", Type = ActionType.LeaveRoom, TimeStamp = new DateTime(2021, 7, 7, 2,30,0 )},
                new HistoryItem
                    {FromUserName = "user2", Type = ActionType.HighFive, ToUserName = "user3", TimeStamp = new DateTime(2021, 7, 6, 20,30,0 )},
            };

            return mockData.AsQueryable();
        }

        public bool IsConnected => true;
    }
}