using System;
using System.Collections.Generic;
using System.Linq;
using ChatHistoryLib.Contracts;

namespace ChatHistoryLib.Implementation
{
    public class InMemoryHistoryDataProvider : IHistoryDataProvider
    {
        public void Connect(string connectionString = "")
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {    IsConnected = true;}
            else
            {
                IsConnected = false;
                throw new FailedToConnectException();
            }
        }

        public IQueryable<HistoryItem> RetrieveData()
        {
           return SeedData().AsQueryable();
        }

        private List<HistoryItem> SeedData()
        {

            DateTime startPoint = new DateTime(2021, 7, 12, 2, 0, 0);
            return new List<HistoryItem>()
            {
                new HistoryItem
                    {FromUserName = "John", Type = ActionType.EnterRoom, TimeStamp = startPoint},
                new HistoryItem
                    {FromUserName = "Taher",  Type = ActionType.Comment, TimeStamp = startPoint.Subtract(TimeSpan.FromHours(6))},
                new HistoryItem
                    {FromUserName = "Taher", ToUserName = "John", Type = ActionType.Comment, TimeStamp = startPoint.Subtract(TimeSpan.FromMinutes(5))},
                new HistoryItem
                    {FromUserName = "Mohamed", ToUserName = "John", Type = ActionType.HighFive, TimeStamp = startPoint.Subtract(TimeSpan.FromHours(28))},
                new HistoryItem
                    {FromUserName = "Taher" , ToUserName = "Wesa", Type = ActionType.HighFive, TimeStamp = startPoint.Subtract(TimeSpan.FromMinutes(90))},
                new HistoryItem
                    {FromUserName = "Taher" , ToUserName = "Nada", Type = ActionType.HighFive, TimeStamp = startPoint.Subtract(TimeSpan.FromMinutes(92))},
                new HistoryItem
                    {FromUserName = "Taher", ToUserName = "Taher", Type = ActionType.Comment, TimeStamp = startPoint.Subtract(TimeSpan.FromHours(5))},
                new HistoryItem
                    {FromUserName = "Nada", ToUserName = "John", Type = ActionType.HighFive, TimeStamp = startPoint.Subtract(TimeSpan.FromMinutes(280))},
                new HistoryItem
                    {FromUserName = "Mohamed",  Type = ActionType.Comment, TimeStamp = startPoint.Subtract(TimeSpan.FromHours(12))},
                new HistoryItem
                    {FromUserName = "Mohamed", Type = ActionType.Comment, TimeStamp = startPoint.Subtract(TimeSpan.FromMinutes(22))},
                new HistoryItem
                    {FromUserName = "Mohamed", Type = ActionType.Comment, TimeStamp = startPoint.Subtract(TimeSpan.FromHours(1))},
                new HistoryItem
                    {FromUserName = "John", Type = ActionType.LeaveRoom, TimeStamp = startPoint},
                new HistoryItem
                    {FromUserName = "Nada", ToUserName = "Wesa", Type = ActionType.HighFive, TimeStamp = startPoint.Subtract(TimeSpan.FromMinutes(33))},
                new HistoryItem
                    {FromUserName = "Taher", ToUserName = "Nada", Type = ActionType.HighFive, TimeStamp = startPoint.Subtract(TimeSpan.FromHours(1))},
                new HistoryItem
                    {FromUserName = "Wesa", ToUserName = "John", Type = ActionType.Comment, TimeStamp = startPoint.Subtract(TimeSpan.FromMinutes(300))},

            };
        }

        public bool IsConnected { get; private set; }
    }
}