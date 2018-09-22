using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApplication.Models
{
    public class LoggerOperation
    {
        public void AddToCassendra(string message)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("HotelBookingDataBase");
            var ps = session.Prepare("INSERT INTO \"HotelBookingDataBase\".\"LogDatabase\" (\"LogId\",\"DateTime\",\"Message\") VALUES (?,?,?)");
            var statement = ps.Bind(Guid.NewGuid(), DateTime.Now, message);
            session.Execute(statement);
        }
    }
}