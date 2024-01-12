using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreInventoryPurchaseLinks.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreInventoryPurchaseLinksOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public long? InventoryPurchaseId { get; set; }
        public int? OrderQuantity { get; set; }
        public decimal Cost { get; set; }
        public int? TaxId { get; set; }

        public virtual string Date { get; set; }

    }
}