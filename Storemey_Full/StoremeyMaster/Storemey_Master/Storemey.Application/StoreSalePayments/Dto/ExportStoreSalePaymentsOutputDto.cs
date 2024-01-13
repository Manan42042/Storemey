using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreSalePayments.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreSalePaymentsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? SaleId { get; set; }
        public string InvoiceId { get; set; }
        public long? PaymentTypeId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime? PaymentDateTime { get; set; }
        public decimal InvoiceTotalAmount { get; set; }

        public virtual string Date { get; set; }

    }
}