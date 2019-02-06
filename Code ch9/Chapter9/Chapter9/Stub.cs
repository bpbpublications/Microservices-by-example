using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chapter9
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    

    namespace TestingMicroservices
    {
        public class HotelInfo
        {
            public int HotelID { get; set; }
            public string HotelName { get; set;  }
        }
        public interface IHotelManager
        {
            List<HotelInfo> GetHotelListForLocation(string location);
        }

        public class HotelManager : IHotelManager
        {
            public List<HotelInfo> GetHotelListForLocation(string location)
            {
                //Some complex business logic might goes here. May be DB operation  
                return new List<HotelInfo>(); 
            }
        }

        //Stub implementation to bypass actual Extension manager class.  
        public class StubExtensionManager : IHotelManager
        {
            public List<HotelInfo> GetHotelListForLocation(string location)
            {
                return new List<HotelInfo>() {
                    new HotelInfo(){ HotelID = 100, HotelName = "Ma Sharda Business Suites"},
                     new HotelInfo(){ HotelID = 101, HotelName = "Royal Getaway"}
                };
            }
        }

        public class SearchHotel
        {
            IHotelManager objmanager = null;
            //Default constructor  
            public SearchHotel()
            {
                objmanager = new HotelManager();
            }
            //parameterized constructor  
            public SearchHotel(IHotelManager tmpManager)
            {
                objmanager = tmpManager;
            }

            public List<HotelInfo> GetHotelListForLocation(String location)
            {
                return objmanager.GetHotelListForLocation(location);
            }
        }
    }
}
