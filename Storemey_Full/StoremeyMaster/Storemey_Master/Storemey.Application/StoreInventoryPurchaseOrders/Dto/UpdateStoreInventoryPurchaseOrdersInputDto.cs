using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreInventoryPurchaseOrders.Dto
{
    public class UpdateStoreInventoryPurchaseOrdersInputDto
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
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }

    }
}