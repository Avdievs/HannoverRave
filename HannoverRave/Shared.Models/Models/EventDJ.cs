namespace Shared.Models
{
    public class EventDJ
    {
        public int EventDJId { get; set; }
        public int? EventId { get; set; }
        public int? DJId { get; set; }
        public Event Event { get; set; }
        public DJ DJ { get; set; }
    }
}
