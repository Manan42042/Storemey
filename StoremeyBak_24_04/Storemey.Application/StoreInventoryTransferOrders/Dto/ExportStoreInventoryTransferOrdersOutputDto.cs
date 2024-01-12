using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreInventoryTransferOrders.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreInventoryTransferOrdersOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? FromOutletId { get; set; }
        public long? ToOutletId { get; set; }
        public long? SupplierId { get; set; }
        public string ReferenceId { get; set; }
        public DateTime? TransferDateTime { get; set; }
        public bool? IsReceived { get; set; }

        public virtual string Date { get; set; }

    }
}