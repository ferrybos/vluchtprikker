//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vluchtprikker.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Availability
    {
        public System.Guid ParticipantID { get; set; }
        public long FlightID { get; set; }
        public Nullable<short> Value { get; set; }
        public string Comment { get; set; }
    
        public virtual Flight Flight { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
