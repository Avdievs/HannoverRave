using System.Collections;
using System.Collections.Generic;

namespace Shared.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Address { get; set; }
        public Club Club { get; set; }
        //TODO: GeoCoordinate
    }
}
