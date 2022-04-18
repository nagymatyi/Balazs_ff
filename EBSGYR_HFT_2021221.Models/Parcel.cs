using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PKMXEN.Models
{
    [Table("Parcels")]
    public class Parcel
    {
        [Key]   // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // Generates IDs automatically
        public int TrackingID { get; set; }
        public double Weight { get; set; }
        public bool COD { get; set; }   // CashOnDelivery
        public DateTime ShippingDate { get; set; }  // Date of dispatch from warehouse
        public string CustomerName { get; set; }
        public string Country { get; set; } // Country Abbreviations
        [Required]
        public string Address { get; set; } // In Address, City, ZIP Format

        [ForeignKey(nameof(Order))]
        public int? OrderID { get; set; }

        [NotMapped]
        public virtual Order Orders { get; set; }

        public override string ToString()
        {
            return $"TrackingID: {this.TrackingID}, \tOrderID: {this.OrderID}, \tWeight: {this.Weight}, \tCOD: {this.COD}, \tShipping Date: {this.ShippingDate}, \tCustomer Name: {this.CustomerName}, Country: {this.Country}, Address: {this.Address}";
        }
    }
}
