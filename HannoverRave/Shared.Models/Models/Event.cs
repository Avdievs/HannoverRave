using System;
using System.Collections.Generic;

namespace Shared.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int? LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Location Location { get; set; }
        public EventType EventType { get; set; }
        public List<EventDJ> EventDJs { get; set; }
        //TODO: Logo or a video
    }
}
