using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelTPService
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public int Rating { get; set; }
    }
}