using System;

namespace TheControlTower
{

    /// <summary>
    /// Info about the flight event 
    /// </summary>
    public class FlightEventInfo : EventArgs
    {
        private string flightCode;
        private string flightEvent;
        private string eventTime;

        /// <summary>
        /// Constructor for flight event 
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="flightEvent">A string with the event</param>
        public FlightEventInfo(string flightCode, string flightEvent)
        {
            FlightCode = flightCode;
            FlightEvent = flightEvent;
            EventTime = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}";
        }

        public string FlightCode
        {
            get { return flightCode; }
            set { flightCode = value; }
        }

        public string FlightEvent
        {
            get { return flightEvent; }
            set { flightEvent = value; }
        }

        public string EventTime
        {
            get { return eventTime; }
            set { eventTime = value; }
        }
    }
}
