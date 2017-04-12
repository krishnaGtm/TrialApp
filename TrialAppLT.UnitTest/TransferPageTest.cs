using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialApp.ViewModels;
using TrialApp.ViewModels.Inetrfaces;
using System;

namespace TrialAppLT.UnitTest
{
    [TestClass]
    public class TransferPageTest
    {
        private IDependencyService _dependency;
        private TestContext testContextInstance;

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
        [TestMethod]
        public void SearchMethod()
        {
            _dependency = new DependencyServiceStub();
            TransferPageViewModel vm = new TransferPageViewModel(_dependency);
            if(vm?.TrialList == null)
                vm.TrialList = new List<TrialData>();
            if(vm?.FilteredDownloadedTrialList == null)
                vm.FilteredDownloadedTrialList = new List<TrialData>();
            vm.FilteredDownloadedTrialList.AddRange(new []
            {
                new TrialData {EZID = 1,CountryCode = "NL",CropCode = "TO",TrialName = "test",TrialTypeID = 1},
                new TrialData {EZID = 2,CountryCode = "NL",CropCode = "TO",TrialName = "test1",TrialTypeID = 1} 
            });
            Console.WriteLine("Two Trials with name 'test' and 'test1' are added to list.");
            Console.WriteLine("Search applied to list with parameter 'test1' with expecetd output of list items count to be one.");
            int expectedValue = 1;
            vm.FilterData("test1");
            Assert.AreEqual(expectedValue,vm.TrialList.Count, "search feature on transfer page failed.");
            Console.WriteLine("Search feature for Transfer unit test passed with item count to one.");
        }
    }
}
