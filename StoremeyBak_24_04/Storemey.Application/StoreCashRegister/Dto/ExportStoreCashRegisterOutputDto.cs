using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCashRegister.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreCashRegisterOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? OutletId { get; set; }
        public long? RegisterId { get; set; }
        public long? StartBy { get; set; }
        public long? CloseBy { get; set; }
        public string ReferenceId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal GiftCardAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual string Date { get; set; }

    }
}