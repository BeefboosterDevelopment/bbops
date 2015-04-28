using Beefbooster.Bull.Entities.Models;
using Beefbooster.Operations.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Pattern.Ef6;

namespace Beefbooster.Data.RepositoryTests
{
    [TestClass]
    public class SpringSaleServiceTests : BullContextTestingBase
    {
        private Repository<SpringSale> _ssRep;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            _ssRep = new Repository<SpringSale>(_BullContext, _UnitOfWork);
        }

        [TestMethod]
        public void SpringSaleIsForPreviousYearsCalves()
        {
            var springSaleService = new SpringSaleService(_ssRep);
            SpringSale ss = springSaleService.GetForSaleYear(2012);
            Assert.AreEqual(ss.CalfBirthYr_Num, 2011);
        }

/*      [TestMethod]
        public void POD()
        {
            var springSaleService = new SpringSaleService(_ssRep);
            IEnumerable<VWPOD> pods = springSaleService.PurchaseOrdersForSaleDate(196);
            Assert.AreNotEqual(0, pods.Count());
        }
 */
    }
}