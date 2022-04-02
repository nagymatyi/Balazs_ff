using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PKMXEN.Models
{
    [Table("Carriers")]  // Database Table
    public class Carrier
    {
        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // Creates an ID for the carrier
        public int CarrierID { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 99)]
        public int Age { get; set; }    
        public double Salary { get; set; }
        public int TotalNumberOfParcels { get; set; }

        public Carrier()
        {
            Orders = new HashSet<Order>();
        }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            return $"CarrierID: {this.CarrierID}, \tName: {this.Name}, \tAge: {this.Age}, \tSalary: {this.Salary}, \tTotalNumberOfParcels: {this.TotalNumberOfParcels}";
        }
    }
}
