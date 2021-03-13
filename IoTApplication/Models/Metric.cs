using System.ComponentModel.DataAnnotations;

namespace IoTApplication.Models
{
    public class Metric
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dimensions { get; set; }
    }
}
