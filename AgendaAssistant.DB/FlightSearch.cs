//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgendaAssistant.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class FlightSearch
    {
        public FlightSearch()
        {
            this.Events = new HashSet<Event>();
            this.Events1 = new HashSet<Event>();
            this.Flights = new HashSet<Flight>();
        }
    
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public long ID { get; set; }
        public int DaysOfWeek { get; set; }
        public Nullable<int> MaxPrice { get; set; }
        public Nullable<long> SelectedFlightID { get; set; }
    
        public virtual ICollection<Event> Events { internal get; set; }
        public virtual ICollection<Event> Events1 { internal get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
        public virtual Flight SelectedFlight { get; set; }
    }
}
