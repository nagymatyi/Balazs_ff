using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PKMXEN.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // Generates the OrderID
        public int OrderID { get; set; }
        public string OrderDescription { get; set; }
        [Required]
        public double OrderValue { get; set; }
        public DateTime OrderDate { get; set; }

        [ForeignKey(nameof(Carrier))]
        public int? CarrierID { get; set; }

        public Order()
        {
            Parcel = new HashSet<Parcel>();
        }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Parcel> Parcel { get; set; }

        [NotMapped]
        public virtual Carrier Carriers { get; set; }

        public override string ToString()
        {
            return $"OrderID: {this.OrderID}, \tOrderDescription: {this.OrderDescription}, \tOrderValue: {this.OrderValue}, \tOrderDate: {this.OrderDate}";
        }
    }
}
