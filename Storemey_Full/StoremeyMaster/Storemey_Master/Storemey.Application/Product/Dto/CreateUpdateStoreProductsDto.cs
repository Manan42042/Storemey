using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Storemey.ProductService.Dto
{
    public class CreateUpdateStoreProductsDto
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

        public decimal Price { get; set; }
        public decimal MarkUp { get; set; }
        public decimal PriceExcludingTax { get; set; }
        public long? TaxId { get; set; }
        public decimal PriceIncludingTax { get; set; }
        public decimal TotalPrice { get; set; }
        public bool? IsStoremeyProduct { get; set; }
        public bool? IsAllowtoSellOutofStock { get; set; }
        public long? MainId { get; set; }
        public bool? IsEnableSEO { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }

        public virtual List<StoreProductImagesDto> ListProductImages { get; set; }
        public virtual List<StoreProductInventoryDto> ListProductInventory { get; set; }
        public virtual List<StoreProductVariantsDto> ListProductVariants { get; set; }
        public virtual List<StoreProductPricingDto> ListProductPricing { get; set; }
        public virtual List<StoreProductQuentityDto> ListProductQuentity { get; set; }
        public virtual List<StoreProductQuentityHistoryDto> ListProductQuentityHistory { get; set; }

        public virtual List<StoreProductCategoriesDto> ListCategory { get; set; }
        public virtual List<StoreProductBrandsDto> ListBrands { get; set; }
        public virtual List<StoreProductTagsDto> ListTags { get; set; }
        public virtual List<StoreProductSeasonsDto> ListSeason { get; set; }

        public virtual List<StoreMasterVariantsDto> SelectedVariant { get; set; }

        public virtual string imageName { get; set; }
        public virtual string imageX { get; set; }
        public virtual string imageY { get; set; }

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