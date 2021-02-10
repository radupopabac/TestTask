using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Core.Entities
{
    public class AddressEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Country { get; set; }

        public string State { get; set; }

        [Required]
        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string FlatNumber { get; set; }

        public string PostalCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}