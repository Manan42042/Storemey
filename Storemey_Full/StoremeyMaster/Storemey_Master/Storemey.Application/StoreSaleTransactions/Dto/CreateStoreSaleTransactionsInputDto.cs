using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreSaleTransactions.Dto
{
    public class CreateStoreSaleTransactionsInputDto
    {

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

        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }

    }
}