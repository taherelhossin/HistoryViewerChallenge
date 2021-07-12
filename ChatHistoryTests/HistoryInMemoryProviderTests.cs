using ChatHistoryLib.Contracts;
using ChatHistoryLib.Implementation;
using NUnit.Framework;

namespace ChatHistoryTests
{
    public class HistoryInMemoryProviderTests
    {
        private IHistoryDataProvider _dataProvider;
        [SetUp]
        public void Setup()
        {
            _dataProvider = new InMemoryHistoryDataProvider();
        }

        [Test]
        public void TestProviderConnectionFailed()
        {
            Assert.Throws<FailedToConnectException>(() => _dataProvider.Connect("wrong connection"));
            Assert.IsFalse( _dataProvider.IsConnected);

        }
        
        [Test]
        public void TestProviderConnectionSuccess()
        {
            Assert.DoesNotThrow(() => _dataProvider.Connect());
            Assert.IsTrue( _dataProvider.IsConnected);

            
        }
    }
}