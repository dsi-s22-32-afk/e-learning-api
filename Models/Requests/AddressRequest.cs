using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Requests
{
    public class AddressRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.104")]
        public string Street { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.105")]
        public string BuildingNumber { get; set; }

        public int? ApartmentNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.106")]
        public string City { get; set; }

        [RegularExpression("^[0-9]{2}-[0-9]{3}$", ErrorMessage = "VALID.107")]
        public string PostalCode { get; set; }
    }
}
