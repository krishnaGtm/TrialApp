using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialApp.Entities.Master;
using TrialApp.ViewModels;
using TrialApp.ViewModels.Inetrfaces;
using TrialApp.Views;
using Trial = TrialApp.Entities.Transaction.Trial;
using TrialApp.Services;
using System.Collections.ObjectModel;
using Model;

namespace TrialAppLT.UnitTest
{
    /// <summary>
    /// Summary description for FilterPageUnitTest
    /// </summary>
    [TestClass]
    public class FilterPageUnitTest
    {
        IDependencyService _dependencyService;
        public FilterPageUnitTest()
        {
            _dependencyService = new DependencyServiceStub();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void FilterPage_FilterListChangeTest()
        {
            //Arrange
            const int cropcount = 2;
            const int trialtypecount = 2;
            const int cropsegmentcount = 2;
            const int trialregioncount = 2;

            //Act
            var vm = new FilterPageViewModel(_dependencyService);
            vm.TrialList = new List<Trial>();
                       
            vm.TrialList.AddRange(new[]
            {
                new Trial {CountryCode="BE",CropCode ="LT",CropSegmentCode="01G",TrialRegionID=1,TrialTypeID=2 },
                new Trial {CountryCode="BE",CropCode ="TO",CropSegmentCode="15T",TrialRegionID=1017,TrialTypeID=3 },
                new Trial {CountryCode="AD",CropCode ="LT",CropSegmentCode="01A",TrialRegionID=1,TrialTypeID=3 },
                new Trial {CountryCode="AD",CropCode ="LT",CropSegmentCode="01A",TrialRegionID=1,TrialTypeID=2 },
                new Trial {CountryCode="AD",CropCode ="LT",CropSegmentCode="",TrialRegionID=1,TrialTypeID=2 },
                new Trial {CountryCode="AD",CropCode ="LT",CropSegmentCode="",TrialRegionID=1,TrialTypeID=1 },
                new Trial {CountryCode="NL",CropCode ="LT",CropSegmentCode="",TrialRegionID=4,TrialTypeID=1 },


            });
            var cropsegment = new List<CropSegment>();
            var trialregion = new List<TrialRegion>();
            var crop = new List<CropRD>();
            var trialtype = new List<TrialType>();
            var country = new List<Country>();
            country.AddRange(new[]
            {
                new Country {CountryCode="BE",CountryName="Belgium" },
                new Country { CountryCode="AD", CountryName="Ade"},
                new Country { CountryCode="NL", CountryName="The Netherlands"},
            });
            vm.CountryListFromDb = new ObservableCollection<Country>(country);
            trialtype.AddRange(new[]
            {
                new TrialType { TrialTypeID=1,CropGroupID=1, TrialTypeName="one"},
                new TrialType { TrialTypeID=2, CropGroupID=2, TrialTypeName="Two"},
                new TrialType { TrialTypeID=3, CropGroupID=3, TrialTypeName="Three"},
            });
            vm.TrialTypeListFromDb = new ObservableCollection<TrialType>(trialtype);
            crop.AddRange(new[]
            {
                new CropRD { CropCode = "TO",CropGroupID=1,CropName="Tomato"},
                new CropRD { CropCode="LT", CropGroupID=2,CropName="Lettuce" }
            });
            vm.CropListFromDb = new ObservableCollection<CropRD>(crop);
            trialregion.AddRange(new[]
            {
                new TrialRegion {TrialRegionID = 1, TrialRegionCode = "NWE",TrialRegionName = "Northwest Europe", CropCode = "LT"},
                new TrialRegion {TrialRegionID = 4, TrialRegionCode = "SWE",TrialRegionName = "South west Europe", CropCode = "LT"},
                new TrialRegion {TrialRegionID = 1017, TrialRegionCode = "R4",TrialRegionName = "Region4", CropCode = "TO"},
                
            });
            vm.TrialRegionListFromDb = new ObservableCollection<TrialRegion>(trialregion);
            cropsegment.AddRange(new[]
            {
                new CropSegment {CropCode = "LT", CropSegmentCode = "01A", CropSegmentName = "Batavia Greenhouse"},
                new CropSegment {CropCode = "TO", CropSegmentCode = "15T", CropSegmentName = "Leaf Lettuce Greenhouse"},
                new CropSegment {CropCode = "LT", CropSegmentCode = "01G", CropSegmentName = "Ananas"}
            });
            vm.CropSegmentListFromDb = new ObservableCollection<CropSegment>(cropsegment);
            vm.ReloadFilter("Country", "", "", "", "", "BE");

            //Assert
            Console.WriteLine(@"7 Trial items added for filter with " + System.Environment.NewLine +
                        "CountryCode =BE,CropCode =LT,CropSegmentCode=01G,TrialRegionID=1,TrialTypeID=2 " + System.Environment.NewLine +
                        "CountryCode =BE,CropCode =TO,CropSegmentCode=15T,TrialRegionID=1017,TrialTypeID=3" + System.Environment.NewLine +
                        "CountryCode=AD,CropCode =LT,CropSegmentCode=01A,TrialRegionID=1,TrialTypeID=3" + System.Environment.NewLine +
                        "CountryCode =AD,CropCode =LT,CropSegmentCode=01A,TrialRegionID=1,TrialTypeID=2 " + System.Environment.NewLine +
                        "CountryCode =AD,CropCode =LT,CropSegmentCode=,TrialRegionID=1,TrialTypeID=2 " + System.Environment.NewLine +
                        "CountryCode =AD,CropCode =LT,CropSegmentCode=,TrialRegionID=1,TrialTypeID=1 " + System.Environment.NewLine +
                        "CountryCode =NL,CropCode =LT,CropSegmentCode=,TrialRegionID=4,TrialTypeID=1 " + System.Environment.NewLine);
            Console.WriteLine(System.Environment.NewLine +
                "Filter applied with country with value 'BE'");

            Assert.AreEqual(trialtypecount, vm.TrialTypeList.Count, "Unit test failed.");
            Console.WriteLine(System.Environment.NewLine +
                "initially Trialtype list item contains 3 but now contains 2.");
            Assert.AreEqual(cropcount, vm.CropList.Count, "Unit test failed.");
            Console.WriteLine(System.Environment.NewLine +
                "initially Crop list item contains 2 and after applying filtere also contains 2 items.");
            Assert.AreEqual(cropsegmentcount, vm.CropSegmentList.Count, "Unit test failed.");
            Console.WriteLine(System.Environment.NewLine +
                "initially CropSegment list item contains 3 but now contains 2");
            Assert.AreEqual(trialregioncount, vm.TrialRegionList.Count, "Unit test failed.");
            Console.WriteLine(System.Environment.NewLine +
                "initially Trialregion list item contains 3 but now contains 2. Filter individual item working perfectly.");

        }
    }
}
