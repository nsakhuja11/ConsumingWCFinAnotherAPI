using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelTPService
{
    public class Room
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public int Availability { get; set; }
        public int Price { get; set; }
    }
}