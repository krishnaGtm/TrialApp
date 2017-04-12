using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialApp.ViewModels;
using System.Collections.Generic;
using TrialApp.ViewModels.Inetrfaces;
using TrialApp.Services;
using System;

namespace TrialAppLT.UnitTest
{
    [TestClass]
    public class MainPagePropertyChangeTest
    {
        private List<string> receivedEvents;
        IDependencyService _dependencyService;
        private TestContext testContextInstance;
        MainPageViewModel sut;
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
        public MainPagePropertyChangeTest()
        {
            _dependencyService = new DependencyServiceStub();
            // Use the testable stub for unit tests
           // _dependencyService.Register<IFileHelper>(new Mockings.FileHelper());
        }
        /// <summary>
        /// Number of Property changed events fired passed for dependant properties
        /// </summary>
        [TestMethod]
        public void TestAllPropertyChangeForMainPage()
        {
            // Fixture setup
            receivedEvents = new List<string>();
            if (sut == null) sut = new MainPageViewModel(_dependencyService);
            sut.PropertyChanged += (sender, e) =>
            {
                receivedEvents.Add(e.PropertyName);
            };
            // Exercise system
            sut.SubmitVisible = false;
            sut.SubmitText = "Testing";
            sut.DisplayConfirmation = false;
            // Verify outcome
            Assert.AreEqual(3, receivedEvents.Count);
            Assert.AreEqual("SubmitVisible", receivedEvents[0]);
            Assert.AreEqual("SubmitText", receivedEvents[1]);
            Assert.AreEqual("DisplayConfirmation", receivedEvents[2]);
            Console.WriteLine("Number of propertyChanged events fire : Passed.\n"
                + "Events fire order: Passed.");
        }
        [TestMethod]
        public void TestTokenExpiryDate()
        {

            //Fixture setup
            var ExpiryDateTrue = DateTime.Now + new TimeSpan(1, 0, 0);
            var ExpiryDateFalse= DateTime.Now + new TimeSpan(0, 2, 0);

            // Exercise system
           WebserviceTasks.TokenExpiryDate = ExpiryDateFalse;

            // Verify outcome
            Assert.IsFalse(WebserviceTasks.CheckTokenValidDate());

            // Exercise system
           WebserviceTasks.TokenExpiryDate = ExpiryDateTrue;


            // Verify outcome
            Assert.IsTrue(WebserviceTasks.CheckTokenValidDate());
            Console.WriteLine("CheckTokenValidDate returns :\n"+
                "False if Expiry date is "+ ExpiryDateFalse.ToString() + "\n returns true if Expiry date is " + ExpiryDateTrue.ToString());
        }
        [TestMethod]
        public void TestIfSignOutSucceeded()
        {
            //Fixure SetUp
            if (sut == null) sut = new MainPageViewModel(_dependencyService);

            //Excersise system
            sut.ClearUserForSingOut();

            //Verify output
            Assert.IsTrue(string.IsNullOrEmpty(sut.UserName));
            Assert.IsTrue(string.IsNullOrEmpty(WebserviceTasks.passwordWS));
            Assert.IsTrue(string.IsNullOrEmpty(WebserviceTasks.token));
            Console.WriteLine("After SignOutFunction call:\nToken get flused.\nUserName get flused.\nPassword get flused.");
        }

    }
    
}
