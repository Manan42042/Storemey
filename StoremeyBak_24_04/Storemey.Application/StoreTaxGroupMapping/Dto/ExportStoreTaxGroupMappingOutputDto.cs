using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreTaxGroupLinks.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreTaxGroupLinksOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual int TaxId { get; set; }
        public virtual int TaxGroupId { get; set; }
        public virtual string Note { get; set; }
        public virtual string Date { get; set; }

    }
}