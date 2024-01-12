using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreCashRegister
{
    [Table("StoreCashRegister")]
    public class StoreCashRegister : FullAuditedEntity<Guid>
    {
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

        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public StoreCashRegister()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
