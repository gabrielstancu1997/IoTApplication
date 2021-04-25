using IoTApplication.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTApplication.Models
{
    public class User : BaseEntity
    {
       
        public string Username { get; set; }

        public string Password { get; set; }

        public string Type { get; set; }


    }
}
