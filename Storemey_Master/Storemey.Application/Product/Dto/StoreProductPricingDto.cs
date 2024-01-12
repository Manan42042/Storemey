using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.ProductService.Dto
{
    public class StoreProductPricingDto
    {
        public virtual long Id { get; set; }
        public long? ProductId { get; set; }
        public long? OutletId { get; set; }
        public string OutletName { get; set; }
        public decimal Price { get; set; }
        public decimal MarkUp { get; set; }
        public decimal PriceExcludingTax { get; set; }
        public long? TaxId { get; set; }
        public decimal PriceIncludingTax { get; set; }
        public decimal TotalPrice { get; set; }
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