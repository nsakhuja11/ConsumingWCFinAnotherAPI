using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelTPService
{
    interface IHotelDatabase
    {
        List<Hotel> GetHotels();
        List<Room> GetRooms(int hotelId);
        void UpdateAvailability(int RoomId);
    }
}
