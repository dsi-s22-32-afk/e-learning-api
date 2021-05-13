using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Data.Entities
{
    public class Address : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public int? ApartmentNumber { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public override string ToString()
        {
            return ApartmentNumber != null
                ? string.Format("{0} {1}/{2}, {3} {4}", Street, BuildingNumber, ApartmentNumber, PostalCode, City)
                : string.Format("{0} {1}, {2} {3}", Street, BuildingNumber, PostalCode, City);
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Address && obj.ToString() == ToString();
        }
    }
}
