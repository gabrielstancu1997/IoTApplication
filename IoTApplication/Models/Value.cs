using System;
using System.ComponentModel.DataAnnotations;


namespace IoTApplication.Models
{
    public class Value
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Timestamp { get; set; }
        public double? Value1 { get; set; }
        public int? MetricId { get; set; }
        public string ValueMeta { get; set; }
    }
}
