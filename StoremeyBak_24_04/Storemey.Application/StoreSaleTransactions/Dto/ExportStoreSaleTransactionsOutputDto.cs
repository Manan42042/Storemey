using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreSaleTransactions.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreSaleTransactionsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public string InvoiceId { get; set; }
        public long? CustomerId { get; set; }
        public string Status { get; set; }
        public bool IsPaid { get; set; }
        public long? UserId { get; set; }
        public DateTime? SaleDatetime { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ProductTotalAmount { get; set; }
        public bool IsPOSSale { get; set; }
        public bool IsEcommareceSale { get; set; }

        public virtual string Date { get; set; }

    }
}