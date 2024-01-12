using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreRegisters.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreRegistersOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public string RegisterName { get; set; }
        public long OutletId { get; set; }
        public long ReceiptTemplateId { get; set; }
        public string ReceiptNumber { get; set; }
        public string ReceiptPrefix { get; set; }
        public string ReceiptSuffix { get; set; }
        public bool SelectUserForNextSale { get; set; }
        public bool EmailReceipt { get; set; }
        public bool PrintReceipt { get; set; }
        public string AskForNote { get; set; }
        public bool PrintNoteOnReceipt { get; set; }
        public bool ShowDiscountOnReceipt { get; set; }

        public virtual string Date { get; set; }

    }
}