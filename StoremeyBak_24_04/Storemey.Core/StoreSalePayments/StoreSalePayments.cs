﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreSalePayments
{
    [Table("StoreSalePayments")]
    public class StoreSalePayments : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }

        public long? SaleId { get; set; }
        public string InvoiceId { get; set; }
        public long? PaymentTypeId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime? PaymentDateTime { get; set; }
        public decimal InvoiceTotalAmount { get; set; }

        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public StoreSalePayments()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}