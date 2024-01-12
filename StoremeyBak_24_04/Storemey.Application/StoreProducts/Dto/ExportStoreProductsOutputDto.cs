using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProducts.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public string ProductName { get; set; }
        public bool? IsVariant { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string SupplierCode { get; set; }
        public string Description { get; set; }
        public bool? IsInventoryOn { get; set; }
        public bool? IsPOSProduct { get; set; }
        public bool? IsEcommarceProduct { get; set; }
        public long? SupplierId { get; set; }
        public decimal SupplierPrice { get; set; }
        public decimal Price { get; set; }
        public decimal MarkUp { get; set; }
        public decimal PriceExcludingTax { get; set; }
        public long? TaxId { get; set; }
        public decimal PriceIncludingTax { get; set; }
        public decimal TotalPrice { get; set; }
        public bool? IsStoremeyProduct { get; set; }
        public bool? IsAllowtoSellOutofStock { get; set; }
        public long? MainId { get; set; }
        public bool? IsEnableSEO { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }

        public virtual string Date { get; set; }

    }
}