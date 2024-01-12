using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreWarehouses.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreWarehousesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public string WarehouseName { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Country { get; set; }
        public long CountryId { get; set; }
        public string State { get; set; }
        public long StateId { get; set; }
        public string City { get; set; }
        public long CityId { get; set; }
        public string ZipCode { get; set; }
        public string ContactNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public virtual string Date { get; set; }

    }
}