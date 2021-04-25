using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTApplication.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime CreateTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public bool IsDeleted { get; set; }


    }
}
