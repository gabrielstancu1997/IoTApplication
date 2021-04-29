
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTApplication.Models
{
    public class ValueSummarize
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? MetricId { get; set; }
        public double? TheSum { get; set; }
        public double? TheCount { get; set; }


    }
}
