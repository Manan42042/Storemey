using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCustomers.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreCustomersOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Date { get; set; }

    }
}