using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreInventoryPurchaseOrders.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreInventoryPurchaseOrdersOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? SupplierId { get; set; }
        public long? OutletId { get; set; }
        public string ReferenceId { get; set; }
        public DateTime? ExpectedDateTime { get; set; }
        public bool? IsReceived { get; set; }
        public decimal TotalCost { get; set; }

        public virtual string Date { get; set; }

    }
}