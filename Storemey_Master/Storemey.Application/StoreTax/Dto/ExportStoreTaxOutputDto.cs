using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreTax.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreTaxOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string TaxName { get; set; }
        public virtual int Rate { get; set; }
        public virtual bool IsDefault { get; set; }
        public virtual string Date { get; set; }

    }
}