using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HotelTPService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HotelService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HotelService.svc or HotelService.svc.cs at the Solution Explorer and start debugging.
    public class HotelService : IHotelService
    {
        public List<Hotel> GetAllHotels()
        {
            HotelDatabase hotel = new HotelDatabase();
            return hotel.GetHotels();
        }

        public List<Room> GetAllRoomById(string HotelId)
        {
            HotelDatabase room = new HotelDatabase();
            return room.GetRooms(Convert.ToInt32(HotelId));
        }

        public void UpdateAvailability(int RoomId)
        {
            HotelDatabase room = new HotelDatabase();
            room.UpdateAvailability(RoomId);
        }
    }
}
