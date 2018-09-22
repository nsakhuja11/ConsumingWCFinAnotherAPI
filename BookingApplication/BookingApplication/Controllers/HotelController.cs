using BookingApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BookingApplication.Controllers
{
    public class HotelController : ApiController
    {
        List<HotelTPSData> hotelTPSList = new List<HotelTPSData>();
        List<HotelJsonData> hotelJsonList = new List<HotelJsonData>();
        public static List<Hotel> hotelList = new List<Hotel>();
        public static List<Room> roomsList = new List<Room>();

        [HttpGet]
        public async Task<List<Hotel>> GetAllHotels()
        {
            Logger.GetInstance.SaveInToLogFile("Get All Hotel");
            hotelTPSList = await GetDataFromTPS();
            hotelJsonList = await GetDataFromJson();

            for (int i = 0; i < hotelTPSList.Count; i++)
            {
                Hotel hotel = new Hotel();
                hotel.HotelId = hotelTPSList[i].HotelId;
                hotel.HotelName = hotelTPSList[i].HotelName;
                hotel.Address = hotelTPSList[i].Address;
                hotel.Rating = hotelTPSList[i].Rating;
                HotelJsonData data = hotelJsonList.Find(x => x.HotelId == hotelTPSList[i].HotelId);
                hotel.Aminities = data.Aminities;
                hotelList.Add(hotel);
            }
            return hotelList;
        }

        private async Task<List<HotelJsonData>> GetDataFromJson()
        {
            Logger.GetInstance.SaveInToLogFile("GetDataFromJson");
            var path = HttpContext.Current.Server.MapPath("~/HotelInfo.json");
            using (StreamReader streamReader = new StreamReader(path))
            {
                var read = streamReader.ReadToEnd();
                hotelJsonList = JsonConvert.DeserializeObject<List<HotelJsonData>>(read);
            }
            return hotelJsonList;
        }

        private async Task<List<HotelTPSData>> GetDataFromTPS()
        {
            Logger.GetInstance.SaveInToLogFile("GetDataFromTPS");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:62688");
            HttpResponseMessage response = client.GetAsync("HotelService.svc/AllHotels").Result;
            if (response.IsSuccessStatusCode)
            {
                hotelTPSList = response.Content.ReadAsAsync<List<HotelTPSData>>().Result;
            }
            return hotelTPSList;
        }

        [HttpGet]
        [Route("api/Hotel/{HotelId}")]
        public List<Room> GetRoomsByHotelId(int HotelId)
        {
            Logger.GetInstance.SaveInToLogFile("GetRoomsByHotelId");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:62688");
            HttpResponseMessage response = client.GetAsync("HotelService.svc/Rooms/" + HotelId + "").Result;
            if (response.IsSuccessStatusCode)
            {
                roomsList = response.Content.ReadAsAsync<List<Room>>().Result;
            }
            return roomsList;
        }


        [HttpPost]
        public void RoomBooking([FromBody] int RoomId)
        {
            Logger.GetInstance.SaveInToLogFile("RoomBooking");
            Room room = roomsList.Find(x => x.RoomId == RoomId);
            Hotel hotel = hotelList.Find(x => x.HotelId == room.HotelId);
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=.;Initial Catalog=BookingDatabase;User ID=Sa;Password=test123!@#";
            connection.Open();
            SqlCommand command = new SqlCommand("insert into BookingDetails values(@RoomId,@HotelName,@HotelAddress,@RoomType,@RoomPrice,@DateTime)", connection);
            command.Parameters.AddWithValue("@RoomId", room.RoomId);
            command.Parameters.AddWithValue("@HotelName", hotel.HotelName);
            command.Parameters.AddWithValue("@HotelAddress", hotel.Address);
            command.Parameters.AddWithValue("@RoomType", room.RoomType);
            command.Parameters.AddWithValue("@RoomPrice", room.Price);
            command.Parameters.AddWithValue("@DateTime", DateTime.Now);
            command.ExecuteNonQuery();
            connection.Close();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:62688");
            client.PutAsJsonAsync("HotelService.svc/Rooms", RoomId);
        }
    }
}
