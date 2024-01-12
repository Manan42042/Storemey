using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCurrencies.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreCurrenciesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string Currency { get; set; }
        public virtual string Symbol { get; set; }
        public virtual string Digital_code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Country { get; set; }

        public virtual string Date { get; set; }

    }
}