using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreProducts
{
    [Table("StoreProducts")]
    public class StoreProducts : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }


        public string ProductName { get; set; }
        public bool? IsVariant { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string SupplierCode { get; set; }
        public string Description { get; set; }
        public bool? IsInventoryOn { get; set; }
        public bool? IsPOSProduct { get; set; }
        public bool? IsEcommarceProduct { get; set; }
        public long? SupplierId { get; set; }
        public decimal SupplierPrice { get; set; }

        public decimal VariantId1 { get; set; }
        public decimal VariantId2 { get; set; }
        public decimal VariantId3 { get; set; }

        public decimal VariantValueId1 { get; set; }
        public decimal VariantValueId2 { get; set; }
        public decimal VariantValueId3 { get; set; }

        //public decimal Price { get; set; }
        //public decimal MarkUp { get; set; }
        //public decimal PriceExcludingTax { get; set; }
        //public long? TaxId { get; set; }
        //public decimal PriceIncludingTax { get; set; }
        //public decimal TotalPrice { get; set; }
        public bool? IsStoremeyProduct { get; set; }
        public bool? IsAllowtoSellOutofStock { get; set; }
        public long? MainId { get; set; }
        public bool? IsEnableSEO { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }

        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public StoreProducts()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
