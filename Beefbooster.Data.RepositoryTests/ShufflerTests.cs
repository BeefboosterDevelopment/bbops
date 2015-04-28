using Beefbooster.Bull.Entities.Models;
using Beefbooster.Operations.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Pattern.Ef6;

namespace Beefbooster.Data.RepositoryTests
{
    [TestClass]
    public class ShufflerTests : BullContextTestingBase
    {
        private Repository<VWPOD> _poRep;
        private ShufflerService _service;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            _poRep = new Repository<VWPOD>(_BullContext, _UnitOfWork);
            _service = new ShufflerService(_poRep);
        }


        [TestMethod]
        public void ShuffleTest1()
        {
            var randomizedDraw = _service.BingoDraw(196);
        }
    }
}