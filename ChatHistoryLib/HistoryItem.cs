using System;
using Microsoft.VisualBasic;

namespace ChatHistoryLib
{
    public class HistoryItem
    {
        public string FromUserName { get; set; }
        
        public string ToUserName { get; set; }
        
        public DateTime TimeStamp { get; set; }
        
        public ActionType Type { get; set; }
        
        public string Message { get; set; }
    }
}