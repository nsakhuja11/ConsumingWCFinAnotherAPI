using System;
using System.Collections.Generic;
using Cassandra;
using System.Linq;
using System.Web;

namespace HotelTPService
{
    public class HotelDatabase : IHotelDatabase
    {
        List<Hotel> hotelList = new List<Hotel>();
        List<Room> roomList = new List<Room>();
        public List<Hotel> GetHotels()
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("HotelBookingDataBase");
            RowSet rows = session.Execute("SELECT * FROM \"HotelBookingDataBase\".\"HotelDatabase\"");
            foreach(var row in rows)
            {
                Hotel hotel = new Hotel();
                hotel.HotelId = row.GetValue<int>("HotelId");
                hotel.Address = row.GetValue<string>("Address");
                hotel.HotelName = row.GetValue<string>("HotelName");
                hotel.Rating = row.GetValue<int>("Rating");
                hotelList.Add(hotel);
            }
            return hotelList;
        }

        public List<Room> GetRooms(int hotelId)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("HotelBookingDataBase");
            RowSet rows = session.Execute("SELECT * FROM \"HotelBookingDataBase\".\"RoomsDatabase\" WHERE \"HotelId\" = " + hotelId +" ALLOW FILTERING");
            foreach(var row in rows)
            {
                Room room = new Room();
                room.RoomId = row.GetValue<int>("RoomId");
                room.HotelId = row.GetValue<int>("HotelId");
                room.Availability = row.GetValue<int>("Availability");
                room.Price = row.GetValue<int>("Price");
                room.RoomType = row.GetValue<string>("RoomType");
                roomList.Add(room);
            }
            return roomList;
        }

        public void UpdateAvailability(int RoomId)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("HotelBookingDataBase");
            RowSet rows = session.Execute("SELECT \"Availability\" FROM \"HotelBookingDataBase\".\"RoomsDatabase\" WHERE \"RoomId\" = " + RoomId + " ALLOW FILTERING");
            int availibility = 0;
            foreach (var row in rows)
            {
                availibility = row.GetValue<int>("Availability");
            }
            availibility = availibility - 1;
            session.Execute("UPDATE \"HotelBookingDataBase\".\"RoomsDatabase\" SET \"Availability\" = " + availibility + " WHERE \"RoomId\" = " + RoomId + "");
        }
    }
}