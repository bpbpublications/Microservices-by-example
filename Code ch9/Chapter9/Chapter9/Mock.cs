using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingMicroservices
{
    public interface IServiceProvider
    {
        void searchService(String location);
    }
    //Mock extenison service provider  
    public class MockSearchService : IServiceProvider
    {
        public string ErrorMessage = null;
        public void searchService(string location)
        {
            if (location.Contains("!") || location.Contains("@") || location.Contains("#")
                || location.Contains("$") || location.Contains("%") | location.Contains("^")
                || location.Contains("*") || location.Contains("(") || location.Contains(")"))
            {
                ErrorMessage = "Incorrect Location";
            }
        }
    }

    //Actual incomplete ExtensionManager functionality  
    public class SearchManager : IServiceProvider
    {
        public void searchService(string location)
        {
            throw new NotImplementedException();
        }
    }

    public class SearchAnalyzer
    {
        public IServiceProvider provider = null;
        public SearchAnalyzer(IServiceProvider tmpProvider)
        {
            provider = tmpProvider;
        }

        public void SearchCheck(string location)
        {
            provider.searchService(location);
        }
    }
}
