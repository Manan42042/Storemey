using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.ProductService.Dto
{
    public class StoreProductInventoryDto
    {

        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public int? TotalStock { get; set; }
        public int? CurrentStock { get; set; }
        public long? OutletId { get; set; }
        public string OutletName { get; set; }


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