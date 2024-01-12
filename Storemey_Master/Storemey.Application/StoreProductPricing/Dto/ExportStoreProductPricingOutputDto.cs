using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductPricing.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductPricingOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public long? OutletId { get; set; }
        public decimal Price { get; set; }
        public decimal MarkUp { get; set; }
        public decimal PriceExcludingTax { get; set; }
        public long? TaxId { get; set; }
        public decimal PriceIncludingTax { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual string Date { get; set; }

    }
}