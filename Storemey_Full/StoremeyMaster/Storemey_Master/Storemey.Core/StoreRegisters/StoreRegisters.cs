using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreRegisters
{
    [Table("StoreRegisters")]
    public class StoreRegisters : FullAuditedEntity<Guid>
    {
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

        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public StoreRegisters()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
