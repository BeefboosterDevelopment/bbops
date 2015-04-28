using System.Collections.Generic;
using System.Linq;
using Beefbooster.Bull.Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Pattern.Ef6;

namespace Beefbooster.Data.RepositoryTests
{
    [TestClass]
    public class POTests : BullContextTestingBase
    {
        /*
       private Repository<SpringSale> _springSaleRepository;
       private Repository<SpringSaleDate> _springSaleDateRepository;

       private IEnumerable<SpringSale> retrievedSpringSaleQuery;

       [TestInitialize]
       public void RunBeforeEachTest()
       {
           _BullContext.Configuration.LazyLoadingEnabled = false;
           _springSaleRepository = new Repository<SpringSale>(_BullContext, _UnitOfWork);
           _springSaleDateRepository = new Repository<SpringSaleDate>(_BullContext, _UnitOfWork);
       }

               [TestMethod]
               public void Get_Sale_Dates_For_A_Strain_And_Year()
               {
                   const string strain = "M3";
                   const int calvesBornIn = 2011;
                   const int breederDay = 0;
            
                   IEnumerable<SpringSaleDate> springSaleDateQuery =
                       _springSaleDateRepository
                       .Query(s => s.SpringSale.CalfBirthYr_Num == calvesBornIn && s.StrainCode == strain && s.BreederDay == breederDay)
                       .Include(s => s.PODetails)
                       .Select();
                   var ssd = springSaleDateQuery.ToList();
                   Assert.IsNotNull(ssd);
                   Assert.AreNotEqual(ssd[0].PODetails.Count, 0);
               }



               [TestMethod]
               public void Eagerly_Load_PO_Details_From_SpringSaleDate()
               {
                   IEnumerable<SpringSale> springSaleQuery =
                       _springSaleRepository
                       .Query(s => s.CalfBirthYr_Num == 2011)
                       .Include(s => s.POs)
                       .Include(s => s.SpringSaleDates)
                       .Select();
                   SpringSale ss = springSaleQuery.FirstOrDefault();
                   Assert.IsNotNull(ss);

                   Assert.AreNotEqual(ss.SpringSaleDates, 0);


                   var poDetailRepository = new Repository<VWPOD>(_BullContext, _UnitOfWork);
                   var theSQL = poDetailRepository
                          .Query(p => p.SpringSaleDateSN == 196)
                          .Select()
                          .ToString();


                   foreach (var retrievedPODetails in ss.SpringSaleDates.ToList()
                       .Select(sd => poDetailRepository
                           .Query(p => p.SpringSaleDateSN == sd.SpringSaleDateSN)
                           .Select()
                           .ToList()))
                   {
                       Assert.AreNotEqual(retrievedPODetails.Count, 0);
                   }
               }

               [TestMethod]
               public void Eagerly_Load_POs_From_SpringSale()
               {
                   IEnumerable<SpringSale> retrievedSpringSaleQuery =
                       _springSaleRepository
                       .Query(s => s.CalfBirthYr_Num == 2011)
                       .Include(s => s.POs)
                       .Include(s => s.SpringSaleDates)
                       .Select();
                   SpringSale ss = retrievedSpringSaleQuery.FirstOrDefault();
                   Assert.IsNotNull(ss);

                   Assert.AreNotEqual(ss.POs.Count, 0);
               }

               [TestMethod]
               public void Retrieve_POs_After_SpringSale()
               {
                   retrievedSpringSaleQuery =
                       _springSaleRepository
                       .Query(s => s.CalfBirthYr_Num == 2011)
                       .Select();

                   SpringSale ss = retrievedSpringSaleQuery.FirstOrDefault();
                   Assert.IsNotNull(ss);

                   var poRepository = new Repository<PO>(_BullContext, _UnitOfWork);

                   var retrievedPOs = poRepository.Query(p => p.SpringSaleSN == ss.SpringSaleSN).Select().ToList();
                   Assert.AreNotEqual(retrievedPOs.Count, 0);
               }
       */
    }
}