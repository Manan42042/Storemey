using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreSaleTransactions
{
    [Table("StoreSaleTransactions")]
    public class StoreSaleTransactions : FullAuditedEntity<Guid>
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
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public StoreSaleTransactions()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
