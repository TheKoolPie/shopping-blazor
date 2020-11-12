using Shopping.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Client.Models
{
    public class StoreCreateViewModel
    {
        public string Name { get; set; }
        public string StoreChainId { get; set; }
        public PriceCategory Category { get; set; }
        [Required(ErrorMessage = "Streetname is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Housenumber is required")]
        public string HouseNumber { get; set; }
        [Required(ErrorMessage = "Postalcode is required")]
        [RegularExpression("[0-9]{5}", ErrorMessage = "Given input does not match valid format")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "City name is needed")]
        public string City { get; set; }
    }
}
