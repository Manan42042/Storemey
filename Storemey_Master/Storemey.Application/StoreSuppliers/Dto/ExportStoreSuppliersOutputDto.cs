using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreSuppliers.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreSuppliersOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public virtual string SupplierFullName { get; set; }
        public virtual string Description { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Website { get; set; }
        public virtual string Address { get; set; }
        public virtual string StateName { get; set; }
        public virtual string PostCode { get; set; }
        public string Country { get; set; }
        public long CountryId { get; set; }
        public string State { get; set; }
        public long StateId { get; set; }

        public string City { get; set; }
        public long CityId { get; set; }
        public virtual string Date { get; set; }
    }
}