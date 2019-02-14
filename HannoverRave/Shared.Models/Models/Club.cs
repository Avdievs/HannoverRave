namespace Shared.Models
{
    public class Club
    {
        public int ClubId { get; set; }
        public int? LocationId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
    }
}
