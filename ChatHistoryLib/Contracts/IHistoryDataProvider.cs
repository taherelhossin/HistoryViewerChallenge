using System.Linq;
using ChatHistoryLib.Implementation;

namespace ChatHistoryLib.Contracts
{
    public interface IHistoryDataProvider
    {
        /// <summary>
        /// Connect to HistoryItems data source & return IQueryable 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <exception cref="FailedToConnectException">If connection failed </exception>
        void Connect(string connectionString = "");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<HistoryItem> RetrieveData();

        /// <summary>
        /// gets if this provider is connected to data-source or not
        /// </summary>
        bool IsConnected { get;}
    }
}