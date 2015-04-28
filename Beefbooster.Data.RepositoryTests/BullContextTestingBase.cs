using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Pattern.Ef6;

namespace Beefbooster.Data.RepositoryTests
{
    [TestClass]
    public class BullContextTestingBase
    {
        protected DataContext _BullContext;
        protected UnitOfWork _UnitOfWork;

        [TestInitialize]
        public void SetupDatabaseContext()
        {
            _BullContext = new BullContext();
            _UnitOfWork = new UnitOfWork(_BullContext);
            Assert.IsTrue(_BullContext.Database.Connection.ConnectionString.Contains("Bull2000"));
        }
    }
}