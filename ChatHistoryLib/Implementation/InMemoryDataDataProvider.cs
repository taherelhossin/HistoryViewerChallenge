using System;
using System.Collections.Generic;
using System.Linq;
using ChatHistoryLib.Contracts;

namespace ChatHistoryLib.Implementation
{
    public class InMemoryDataDataProvider : IHistoryDataProvider
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
            return new List<HistoryItem>()
            {
                new HistoryItem
                    {FromUserName = "Joun", Type = ActionType.Comment, TimeStamp = DateTime.Now},
                new HistoryItem
                    {FromUserName = "Taher",  Type = ActionType.Comment, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromHours(1))},
                new HistoryItem
                    {FromUserName = "Taher", ToUserName = "John", Type = ActionType.Comment, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(5))},
                new HistoryItem
                    {FromUserName = "Mohamed", Type = ActionType.HighFive, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromHours(2))},
                new HistoryItem
                    {FromUserName = "Taher", Type = ActionType.HighFive, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(90))},
                new HistoryItem
                    {FromUserName = "Taher", ToUserName = "Taher", Type = ActionType.Comment, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromHours(1))},
                new HistoryItem
                    {FromUserName = "Nada", ToUserName = "John", Type = ActionType.HighFive, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(280))},
                new HistoryItem
                    {FromUserName = "Mohamed",  Type = ActionType.Comment, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromHours(3))},
                new HistoryItem
                    {FromUserName = "Mohamed", Type = ActionType.Comment, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(22))},
                new HistoryItem
                    {FromUserName = "Mohamed", Type = ActionType.Comment, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromHours(2))},
                new HistoryItem
                    {FromUserName = "Nada", Type = ActionType.HighFive, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(33))},
                new HistoryItem
                    {FromUserName = "Taher", ToUserName = "Taher", Type = ActionType.Comment, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromHours(1))},
                new HistoryItem
                    {FromUserName = "Wesa", ToUserName = "John", Type = ActionType.Comment, TimeStamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(300))},

            };
        }

        public bool IsConnected { get; private set; }
    }
}