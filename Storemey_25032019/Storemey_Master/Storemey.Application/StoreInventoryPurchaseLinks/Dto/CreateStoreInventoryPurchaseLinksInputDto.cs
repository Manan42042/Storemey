using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreInventoryPurchaseLinks.Dto
{
    public class CreateStoreInventoryPurchaseLinksInputDto
    {

        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public long? InventoryPurchaseId { get; set; }
        public int? OrderQuantity { get; set; }
        public decimal Cost { get; set; }
        public int? TaxId { get; set; }


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