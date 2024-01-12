using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreInventory.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreInventoryOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public int? TotalStock { get; set; }
        public int? CurrentStock { get; set; }
        public long? OutletId { get; set; }

        public virtual string Date { get; set; }

    }
}