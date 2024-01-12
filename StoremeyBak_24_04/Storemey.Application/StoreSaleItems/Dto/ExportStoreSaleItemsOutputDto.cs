using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreSaleItems.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreSaleItemsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }


        public long? SaleId { get; set; }
        public string InvoiceId { get; set; }
        public long? ProductId { get; set; }
        public long? GiftcardId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemAmount { get; set; }


        public virtual string Date { get; set; }

    }
}