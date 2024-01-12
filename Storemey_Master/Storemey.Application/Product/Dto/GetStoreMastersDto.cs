using Abp.Application.Services.Dto;
using FileHelpers;
using Storemey.StoreBrands.Dto;
using Storemey.StoreCategories.Dto;
using Storemey.StoreOutlets.Dto;
using Storemey.StoreProductVariants.Dto;
using Storemey.StoreProductVariantValues.Dto;
using Storemey.StoreSeasons.Dto;
using Storemey.StoreSuppliers.Dto;
using Storemey.StoreTags.Dto;
using Storemey.StoreTax.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.ProductService.Dto
{
    public class GetStoreMastersDto
    {
        public ListResultDto<GetStoreCategoriesOutputDto> ListCategories { get; set; }
        public ListResultDto<GetStoreBrandsOutputDto> ListBrands { get; set; }
        public ListResultDto<GetStoreTagsOutputDto> ListTags { get; set; }
        public ListResultDto<GetStoreSeasonsOutputDto> ListSeasons { get; set; }
        public ListResultDto<GetStoreSuppliersOutputDto> ListSuppliers { get; set; }
        public ListResultDto<GetStoreTaxOutputDto> ListTax { get; set; }
        public ListResultDto<GetStoreProductVariantsOutputDto> ListProductVariants { get; set; }
        public ListResultDto<GetStoreProductVariantValuesOutputDto> ListProductVariantValues { get; set; }
        //public ListResultDto<GetStoreOutletsOutputDto> ListOutlets { get; set; }

    }
}