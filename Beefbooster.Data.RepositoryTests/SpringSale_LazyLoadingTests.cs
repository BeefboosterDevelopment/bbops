using System.Collections.Generic;
using System.Linq;
using Beefbooster.Bull.Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Pattern.Ef6;

namespace Beefbooster.Data.RepositoryTests
{
    [TestClass]
    public class SpringSale_LazyLoadingTests : BullContextTestingBase
    {
        private Repository<SpringSale> _repository;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            _repository = new Repository<SpringSale>(_BullContext, _UnitOfWork);
        }

        private SpringSale RetrieveSpringSaleEntity(bool lazyEnabled, bool eagerlyLoadDates)
        {
            _BullContext.Configuration.LazyLoadingEnabled = lazyEnabled;
            IEnumerable<SpringSale> retrievedSpringSaleQuery =
                eagerlyLoadDates
                    ? _repository.Query(s => s.CalfBirthYr_Num == 2011).Include(s => s.SpringSaleDates).Select()
                    : _repository.Query(s => s.CalfBirthYr_Num == 2011).Select();
            SpringSale ss = retrievedSpringSaleQuery.FirstOrDefault();
            Assert.IsNotNull(ss);
            return ss;
        }

        [TestMethod]
        public void NotLazyEagerShouldRetreiveDates()
        {
            Assert.AreNotEqual(RetrieveSpringSaleEntity(false, true).SpringSaleDates.Count, 0);
        }

        [TestMethod]
        public void NotLazyNotEagerShouldNotRetreiveDates()
        {
            Assert.AreEqual(RetrieveSpringSaleEntity(false, false).SpringSaleDates.Count, 0);
        }

        [TestMethod]
        public void LazyNotEagerShouldNotRetreiveDates()
        {
            Assert.AreEqual(RetrieveSpringSaleEntity(true, false).SpringSaleDates.Count, 0);
        }

        [TestMethod]
        public void LazyEagerShouldRetreiveDates()
        {
            Assert.AreNotEqual(RetrieveSpringSaleEntity(true, true).SpringSaleDates.Count, 0);
        }
    }
}