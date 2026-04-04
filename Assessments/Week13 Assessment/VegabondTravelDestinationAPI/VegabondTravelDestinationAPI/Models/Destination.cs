using System.ComponentModel.DataAnnotations;

namespace VegabondTravelDestinationAPI.Models
{
    public class Destination
    {
        public int DestinationId { get; set; }
        //[Required]
        public string CityName { get; set; }
        //[Required]
        public string Country { get; set; }
        public string Description { get; set; }
        //[Range(1,5)]
        public int Rating { get; set; }
        public DateTime LastVisited { get; set; }
    }
}
