using BookingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApplication
{
    sealed class Logger
    {

        private static Logger instance = null;

        public static Logger GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Logger();
                return instance;
            }
        }

        private Logger()
        {

        }

        public void SaveInToLogFile(string message)
        {
            LoggerOperation Log = new LoggerOperation();
            Log.AddToCassendra(message);
        }
    }
}