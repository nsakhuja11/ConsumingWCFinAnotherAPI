using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HotelTPService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHotelService" in both code and config file together.
    [ServiceContract]
    public interface IHotelService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/AllHotels", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Hotel> GetAllHotels();

        [OperationContract]
        [WebGet(UriTemplate = "/Rooms/{HotelId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Room> GetAllRoomById(string HotelId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Rooms", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateAvailability (int RoomId);
    }
}
