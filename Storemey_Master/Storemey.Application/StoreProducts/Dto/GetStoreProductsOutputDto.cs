using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProducts.Dto
{
    public class GetStoreProductsOutputDto
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


        public string Supplier { get; set; }
        public string Variants { get; set; }
        public string Inventory { get; set; }
        public string Date { get; set; }
        public string DefaultImage { get; set; }


        public decimal VariantId1 { get; set; }
        public decimal VariantId2 { get; set; }
        public decimal VariantId3 { get; set; }
        public decimal VariantValueId1 { get; set; }
        public decimal VariantValueId2 { get; set; }
        public decimal VariantValueId3 { get; set; }

        public bool? IsStoremeyProduct { get; set; }
        public bool? IsAllowtoSellOutofStock { get; set; }
        public long? MainId { get; set; }
        public bool? IsEnableSEO { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }

        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual int? recordsTotal { get; set; }
    }
}