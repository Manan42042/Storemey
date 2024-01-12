using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreInventoryPurchaseOrders
{
    [Table("StoreInventoryPurchaseOrders")]
    public class StoreInventoryPurchaseOrders : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }

        public long? SupplierId { get; set; }
        public long? OutletId { get; set; }
        public string ReferenceId { get; set; }
        public DateTime? ExpectedDateTime { get; set; }
        public bool? IsReceived { get; set; }
        public decimal TotalCost { get; set; }

        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public StoreInventoryPurchaseOrders()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
