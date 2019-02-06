using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingMicroservices
{
    [TestClass]
    public class MockTest
    {
        [TestMethod]
        public void TestIncorrectLocationName()
        {
            //Act  
            MockSearchService mockobject = new MockSearchService();
            //Inject mock object now  
            SearchAnalyzer analyzer = new SearchAnalyzer(mockobject);
            //Action  
            analyzer.SearchCheck("De$%");

            //Assert  
            Assert.AreEqual(mockobject.ErrorMessage, "Incorrect Location");
        }
    }
}
