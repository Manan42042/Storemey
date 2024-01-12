using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using Abp.UI;
using Storemey.StoreBrands.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;
using Storemey.StoreProducts;
using Storemey.ProductService.Dto;
using Storemey.StoreCategories;
using Storemey.StoreTags;
using Storemey.StoreBrands;
using Storemey.StoreSeasons;
using Storemey.StoreSuppliers;
using Storemey.StoreTax;
using Storemey.StoreProductVariants;
using Storemey.StoreProductVariantValues;
using Storemey.StoreOutlets;
using Storemey.StoreProductImages;
using AutoMapper;
using Storemey.StoreProductImages.Dto;
using Storemey.StoreProductPricing;
using Storemey.StoreInventory;
using Storemey.StoreProductQuentity;
using Storemey.StoreProductQuentityHistory;
using Storemey.Application;
using System.Web.Hosting;
using Storemey.StoreProductCategoryLinks;
using Storemey.StoreProductBrandLinks;
using Storemey.StoreProductSeasonLinks;
using Storemey.StoreProductTagLinks;
using Storemey.StoreProductVariantsLinks;
using Storemey.StoreOutlets.Dto;
using System.Drawing;

namespace Storemey.ProductService
{
    [AbpAuthorize]
    public class ProductService : AbpServiceBase, IProductService
    {
        private readonly IStoreProductsAppService _StoreProductsAppService;
        private readonly IStoreCategoriesAppService _StoreCategoriesAppService;
        private readonly IStoreTagsAppService _StoreTagsAppService;
        private readonly IStoreBrandsAppService _StoreBrandsAppService;
        private readonly IStoreSeasonsAppService _StoreSeasonsAppService;

        private readonly IStoreSuppliersAppService _StoreSuppliersAppService;
        private readonly IStoreTaxAppService _StoreTaxAppService;
        private readonly IStoreProductVariantsAppService _StoreProductVariantsAppService;
        private readonly IStoreProductVariantValuesAppService _StoreProductVariantValuesAppService;
        private readonly IStoreOutletsAppService _StoreOutletsAppService;
        private readonly IStoreProductImagesAppService _StoreProductImagesAppService;
        private readonly IStoreProductPricingAppService _StoreProductPricingAppService;
        private readonly IStoreInventoryAppService _StoreInventoryAppService;

        private readonly IStoreProductQuentityAppService _StoreProductQuentityAppService;
        private readonly IStoreProductQuentityHistoryAppService _StoreProductQuentityHistoryAppService;

        private readonly IStoreProductCategoryLinksAppService _StoreProductCategoryLinksAppService;
        private readonly IStoreProductBrandLinksAppService _StoreProductBrandLinksAppService;
        private readonly IStoreProductSeasonLinksAppService _StoreProductSeasonLinksAppService;
        private readonly IStoreProductTagLinksAppService _StoreProductTagLinksAppService;
        private readonly IStoreProductVariantsLinksAppService _StoreProductVariantsLinksAppService;



        string folderImage = "images", folderupload = "upload", folderexport = "export", folderbackup = "backup", folderROOT = "storeDocuments", backslace = "/", productImage = "productImage", userImages = "userImages", CatergoryImage = "CatergoryImage", storeImage = "storeImage";




        public ProductService(
            IStoreProductsAppService StoreProductsAppService,
            IStoreCategoriesAppService StoreCategoriesAppService,
            IStoreTagsAppService StoreTagsAppService,
            IStoreBrandsAppService StoreBrandsAppService,
            IStoreSeasonsAppService StoreSeasonsAppService,
            IStoreProductImagesAppService StoreProductImagesAppService,

            IStoreSuppliersAppService StoreSuppliersAppService,
            IStoreTaxAppService StoreTaxAppService,
            IStoreProductVariantsAppService StoreProductVariantsAppService,
            IStoreProductVariantValuesAppService StoreProductVariantValuesAppService,
            IStoreOutletsAppService StoreOutletsAppService,
            IStoreProductPricingAppService StoreProductPricingAppService,
            IStoreInventoryAppService StoreInventoryAppService,
            IStoreProductQuentityAppService StoreProductQuentityAppService,
            IStoreProductQuentityHistoryAppService StoreProductQuentityHistoryAppService,


            IStoreProductCategoryLinksAppService StoreProductCategoryLinksAppService,
            IStoreProductBrandLinksAppService StoreProductBrandLinksAppService,
            IStoreProductSeasonLinksAppService StoreProductSeasonLinksAppService,
            IStoreProductTagLinksAppService StoreProductTagLinksAppService,
            IStoreProductVariantsLinksAppService StoreProductVariantsLinksAppService


            )
        {
            _StoreProductsAppService = StoreProductsAppService;
            _StoreCategoriesAppService = StoreCategoriesAppService;
            _StoreTagsAppService = StoreTagsAppService;
            _StoreBrandsAppService = StoreBrandsAppService;
            _StoreSeasonsAppService = StoreSeasonsAppService;
            _StoreSuppliersAppService = StoreSuppliersAppService;
            _StoreTaxAppService = StoreTaxAppService;
            _StoreProductVariantsAppService = StoreProductVariantsAppService;
            _StoreProductVariantValuesAppService = StoreProductVariantValuesAppService;
            _StoreOutletsAppService = StoreOutletsAppService;
            _StoreProductImagesAppService = StoreProductImagesAppService;
            _StoreProductPricingAppService = StoreProductPricingAppService;
            _StoreInventoryAppService = StoreInventoryAppService;
            _StoreProductQuentityAppService = StoreProductQuentityAppService;
            _StoreProductQuentityHistoryAppService = StoreProductQuentityHistoryAppService;


            _StoreProductCategoryLinksAppService = StoreProductCategoryLinksAppService;
            _StoreProductBrandLinksAppService = StoreProductBrandLinksAppService;
            _StoreProductSeasonLinksAppService = StoreProductSeasonLinksAppService;
            _StoreProductTagLinksAppService = StoreProductTagLinksAppService;
            _StoreProductVariantsLinksAppService = StoreProductVariantsLinksAppService;


        }

        public static StoreProductVariantsDto GetDefaultVariant(List<GetStoreOutletsOutputDto> outlet)
        {
            StoreProductVariantsDto result = new StoreProductVariantsDto();

            List<StoreProductImagesDto> l1 = new List<StoreProductImagesDto>();
            List<StoreProductInventoryDto> l2 = new List<StoreProductInventoryDto>();
            List<StoreProductPricingDto> l3 = new List<StoreProductPricingDto>();
            List<StoreProductQuentityDto> l4 = new List<StoreProductQuentityDto>();
            List<StoreProductQuentityHistoryDto> l5 = new List<StoreProductQuentityHistoryDto>();

            foreach (var item in outlet)
            {
                StoreProductImagesDto O1 = new StoreProductImagesDto();
                StoreProductInventoryDto O2 = new StoreProductInventoryDto();
                StoreProductPricingDto O3 = new StoreProductPricingDto();
                StoreProductQuentityDto O4 = new StoreProductQuentityDto();
                StoreProductQuentityHistoryDto O5 = new StoreProductQuentityHistoryDto();
                l1.Add(O1);

                O2.OutletId = item.Id;
                O2.OutletName = item.OutletName;
                l2.Add(O2);

                O3.OutletId = item.Id;
                O3.OutletName = item.OutletName;
                l3.Add(O3);

                O4.OutletId = item.Id;
                O4.OutletName = item.OutletName;
                l4.Add(O4);

                O5.OutletId = item.Id;
                O5.OutletName = item.OutletName;
                l5.Add(O5);


            }

            result.ListProductImages = l1;
            result.ListProductInventory = l2;
            result.ListProductPricing = l3;
            result.ListProductQuentity = l4;
            result.ListProductQuentityHistory = l5;


            return result;
        }

        public async Task<GetStoreProductsDto> GetProductById(GetStoreProductsDto input)
        {
            try
            {


                long productId = input.Id;
                var registration = await _StoreProductsAppService.GetProductById(input.Id);
                var mapData = registration.MapTo<GetStoreProductsDto>();
                var outlets = await _StoreOutletsAppService.ListAlldata();


                var productImages = await _StoreProductImagesAppService.GetByProduct(productId);
                mapData.ListProductImages = productImages.MapTo<List<StoreProductImagesDto>>();

                mapData.DefaultProductVariants = GetDefaultVariant(outlets);

                mapData.ListProductPricing = new List<StoreProductPricingDto>();
                foreach (var item in outlets)
                {
                    var data = await _StoreProductPricingAppService.GetByProductAndOutlet(productId, item.Id);
                    foreach (var item2 in data)
                    {
                        var result = item2.MapTo<StoreProductPricingDto>();
                        result.OutletId = item.Id;
                        result.OutletName = item.OutletName;

                        mapData.ListProductPricing.Add(result);
                    }
                }

                mapData.ListProductInventory = new List<StoreProductInventoryDto>();
                foreach (var item in outlets)
                {
                    var data = await _StoreInventoryAppService.GetByProductAndOutlet(productId, item.Id);
                    foreach (var item2 in data)
                    {
                        var result = item2.MapTo<StoreProductInventoryDto>();
                        result.OutletId = item.Id;
                        result.OutletName = item.OutletName;

                        mapData.ListProductInventory.Add(result);
                    }
                }


                mapData.ListProductQuentity = new List<StoreProductQuentityDto>();
                foreach (var item in outlets)
                {

                    var data = await _StoreProductQuentityAppService.GetByProductAndOutlet(productId, item.Id);
                    foreach (var item2 in data)
                    {
                        var result = item2.MapTo<StoreProductQuentityDto>();
                        result.OutletId = item.Id;
                        result.OutletName = item.OutletName;

                        mapData.ListProductQuentity.Add(result);
                    }
                }

                mapData.ListProductQuentityHistory = new List<StoreProductQuentityHistoryDto>();
                foreach (var item in outlets)
                {

                    var data = await _StoreProductQuentityHistoryAppService.GetByProductAndOutlet(productId, item.Id);
                    foreach (var item2 in data)
                    {
                        var result = item2.MapTo<StoreProductQuentityHistoryDto>();
                        result.OutletId = item.Id;
                        result.OutletName = item.OutletName;

                        mapData.ListProductQuentityHistory.Add(result);
                    }
                }


                mapData.SelectedVariant = new List<StoreProductVariantsLinksDto>();
                var data113 = await _StoreProductVariantsLinksAppService.GetByProductId(productId);
                if (data113 != null && data113.Count > 0)
                {
                    var result = data113.MapTo<List<StoreProductVariantsLinksDto>>();
                    mapData.SelectedVariant = result;
                }
                else
                {
                    List<StoreProductVariantsLinksDto> mo1 = new List<StoreProductVariantsLinksDto>();
                    StoreProductVariantsLinksDto o1 = new StoreProductVariantsLinksDto();
                    mo1.Add(o1);
                    mo1.Add(o1);
                    mo1.Add(o1);
                    mapData.SelectedVariant = mo1;
                }



                mapData.ListCategory = new List<StoreProductCategoryLinksDto>();
                var data11 = await _StoreProductCategoryLinksAppService.GetByProductId(productId);
                if (data11 != null)
                {
                    var result = data11.MapTo<List<StoreProductCategoryLinksDto>>();
                    mapData.ListCategory = result;
                }


                mapData.ListBrands = new List<StoreProductBrandLinksDto>();
                var data22 = await _StoreProductBrandLinksAppService.GetByProductId(productId);
                if (data22 != null)
                {
                    var result = data22.MapTo<List<StoreProductBrandLinksDto>>();
                    mapData.ListBrands = result;
                }


                mapData.ListSeason = new List<StoreProductSeasonLinksDto>();
                var data33 = await _StoreProductSeasonLinksAppService.GetByProductId(productId);
                if (data33 != null)
                {
                    var result = data33.MapTo<List<StoreProductSeasonLinksDto>>();
                    mapData.ListSeason = result;
                }

                mapData.ListTags = new List<StoreProductTagLinksDto>();
                var data44 = await _StoreProductTagLinksAppService.GetByProductId(productId);
                if (data44 != null)
                {
                    var result = data44.MapTo<List<StoreProductTagLinksDto>>();
                    mapData.ListTags = result;
                }


                /*
                 
                 */

                #region Product Variant



                mapData.ListProductVariants = new List<StoreProductVariantsDto>();


                var variantdata = await _StoreProductsAppService.GetVariantProductById(productId);
                var mapData1 = variantdata.MapTo<List<StoreProductVariantsDto>>();

                foreach (var item2 in mapData1)
                {
                    mapData.ListProductVariants.Add(item2);
                }


                foreach (var variantobject in mapData.ListProductVariants)
                {
                    // ListProductImages
                    var productImages2 = await _StoreProductImagesAppService.GetByProduct(variantobject.Id);
                    variantobject.ListProductImages = productImages2.MapTo<List<StoreProductImagesDto>>();

                    long variantId = variantobject.Id;

                    // ListProductInventory
                    variantobject.ListProductInventory = new List<StoreProductInventoryDto>();
                    foreach (var item4 in outlets)
                    {
                        var data1 = await _StoreInventoryAppService.GetByProductAndOutlet(variantId, item4.Id);
                        foreach (var item2 in data1)
                        {
                            var result = item2.MapTo<StoreProductInventoryDto>();
                            result.OutletId = item4.Id;
                            result.OutletName = item4.OutletName;

                            variantobject.ListProductInventory.Add(result);
                        }
                    }


                    // ListProductPricing
                    variantobject.ListProductPricing = new List<StoreProductPricingDto>();
                    foreach (var item5 in outlets)
                    {
                        var data1 = await _StoreProductPricingAppService.GetByProductAndOutlet(variantId, item5.Id);
                        foreach (var item2 in data1)
                        {
                            var result = item2.MapTo<StoreProductPricingDto>();
                            result.OutletId = item5.Id;
                            result.OutletName = item5.OutletName;

                            variantobject.ListProductPricing.Add(result);
                        }
                    }


                    // ListProductQuentity
                    variantobject.ListProductQuentity = new List<StoreProductQuentityDto>();
                    foreach (var item6 in outlets)
                    {

                        var data1 = await _StoreProductQuentityAppService.GetByProductAndOutlet(variantId, item6.Id);
                        foreach (var item2 in data1)
                        {
                            var result = item2.MapTo<StoreProductQuentityDto>();
                            result.OutletId = item6.Id;
                            result.OutletName = item6.OutletName;

                            variantobject.ListProductQuentity.Add(result);
                        }
                    }

                    // ListProductQuentityHistory

                    variantobject.ListProductQuentityHistory = new List<StoreProductQuentityHistoryDto>();
                    foreach (var item7 in outlets)
                    {

                        var data1 = await _StoreProductQuentityHistoryAppService.GetByProductAndOutlet(variantId, item7.Id);
                        foreach (var item2 in data1)
                        {
                            var result = item2.MapTo<StoreProductQuentityHistoryDto>();
                            result.OutletId = item7.Id;
                            result.OutletName = item7.OutletName;

                            variantobject.ListProductQuentityHistory.Add(result);
                        }
                    }

                }

                #endregion
                return mapData;
            }
            catch (Exception ex)
            {

            }
            return null;
        }



        public async Task SaveOrUpdateProduct(CreateUpdateStoreProductsDto input)
        {
            try
            {

                var mapData = input.MapTo<Storemey.StoreProducts.Dto.CreateUpdateStoreProductsDto>();
                var productId = await _StoreProductsAppService
                    .CreateOrUpdate(mapData);

                foreach (var item in input.ListProductPricing)
                {
                    item.ProductId = productId;
                    var mapData1 = item.MapTo<Storemey.StoreProductPricing.Dto.CreateStoreProductPricingInputDto>();
                    var mapData2 = item.MapTo<Storemey.StoreProductPricing.Dto.UpdateStoreProductPricingInputDto>();
                    if (item.Id > 0)
                    {
                        await _StoreProductPricingAppService.Update(mapData2);
                    }
                    else
                    {
                        await _StoreProductPricingAppService.Create(mapData1);
                    }
                }

                foreach (var item in input.ListProductInventory)
                {
                    item.ProductId = productId;

                    var mapData1 = item.MapTo<Storemey.StoreInventory.Dto.CreateStoreInventoryInputDto>();
                    var mapData2 = item.MapTo<Storemey.StoreInventory.Dto.UpdateStoreInventoryInputDto>();
                    if (item.Id > 0)
                    {
                        await _StoreInventoryAppService.Update(mapData2);
                    }
                    else
                    {
                        await _StoreInventoryAppService.Create(mapData1);
                    }
                }



                foreach (var item in input.ListProductImages) /*StoreProductImagesDto*/
                {
                    try
                    {

                        Bitmap bitmapimage = GetCopyImage(HttpContext.Current.Server.MapPath("/") + item.Image);


                        string ext = Path.GetExtension(item.Image);
                        string newfilename = Guid.NewGuid().ToString() + ext;
                  
                        bitmapimage.Save(HttpContext.Current.Server.MapPath("/TempImage/") + Path.GetFileName(newfilename));

                        //var sourcePath = HttpContext.Current.Server.MapPath("/") + item.Image;
                        //var destinationPath = HttpContext.Current.Server.MapPath("/TempImage/") + item.Image;
                        //if (File.Exists(sourcePath))
                        //{
                        //    if (File.Exists(destinationPath))
                        //        File.Delete(destinationPath);
                        //    File.Move(sourcePath, destinationPath);
                        //}
                        item.Image = "/TempImage/" + Path.GetFileName(newfilename);
                    }
                    catch (Exception)
                    {

                    }

                }
            



                await _StoreProductImagesAppService.DeleteByProductId(productId);

                for (int i = 1; i <= 10; i++)
                {
                    CommonEntityHelper.DeleteOldProducts(HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + productImage + backslace, "p_" + productId.ToString() + "_" + i);
                }

                int count = 1;
                foreach (var item in input.ListProductImages) /*StoreProductImagesDto*/
                {
                    item.ProductId = productId;

                    var mapData1 = item.MapTo<Storemey.StoreProductImages.Dto.CreateStoreProductImagesInputDto>();

                    await _StoreProductImagesAppService.Create(mapData1, count, "p_");
                    count++;
                }

                await _StoreProductCategoryLinksAppService.DeleteByProductId(productId);

                foreach (var item in input.ListCategory)
                {
                    await _StoreProductCategoryLinksAppService.CreateProductLinks(productId, item.Id);
                }


                await _StoreProductVariantsLinksAppService.DeleteByProductId(productId);

                if (input.IsVariant == true && (input.SelectedVariant.Count() > 0))
                {
                    foreach (var item in input.SelectedVariant)
                    {
                        if (item.SelectedVariantValue != null && item.SelectedVariantValue.Count() > 0)
                        {
                            foreach (var item2 in item.SelectedVariantValue)
                            {
                                await _StoreProductVariantsLinksAppService.CreateValriantLink(productId, item.Id, item2.Id);
                            }
                        }
                    }
                }

                await _StoreProductBrandLinksAppService.DeleteByProductId(productId);

                foreach (var item in input.ListBrands)
                {
                    await _StoreProductBrandLinksAppService.CreateProductLinks(productId, item.Id);
                }


                await _StoreProductSeasonLinksAppService.DeleteByProductId(productId);

                foreach (var item in input.ListSeason)
                {
                    await _StoreProductSeasonLinksAppService.CreateProductLinks(productId, item.Id);
                }

                await _StoreProductTagLinksAppService.DeleteByProductId(productId);

                foreach (var item in input.ListTags)
                {
                    await _StoreProductTagLinksAppService.CreateProductLinks(productId, item.Id);
                }









                var variantdata = await _StoreProductsAppService.GetVariantProductById(productId);
                var mapDataForImage = variantdata.MapTo<List<StoreProductVariantsDto>>();

                //foreach (var item in mapDataForImage)
                //{
                //    for (int i = 1; i <= 10; i++)
                //    {
                //        CommonEntityHelper.DeleteOldProducts(HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + productImage + backslace, "pv_" + item.Id.ToString() + "_" + i);
                //    }
                //}


                await _StoreProductsAppService.DeleteByProductId(productId);


                if (input.IsVariant == true)
                {
                    foreach (var variantobject in input.ListProductVariants)
                    {
                        long variantId = variantobject.Id;


                        var mapData1 = variantobject.MapTo<Storemey.StoreProducts.Dto.CreateUpdateStoreProductsDto>();
                        mapData1.MainId = productId;
                        var VariantproductId = await _StoreProductsAppService
                            .Create2(mapData1);



                        await _StoreProductPricingAppService.DeleteByProductId(productId);
                        if (variantobject.ListProductPricing != null)
                        {
                            // Product variant  Pricing
                            foreach (var item in variantobject.ListProductPricing)
                            {
                                item.ProductId = VariantproductId;
                                var mapData111 = item.MapTo<Storemey.StoreProductPricing.Dto.CreateStoreProductPricingInputDto>();
                                await _StoreProductPricingAppService.Create(mapData111);
                            }
                        }

                        await _StoreInventoryAppService.DeleteByProductId(productId);

                        //Product variant inventory

                        if (variantobject.ListProductInventory != null)
                        {
                            foreach (var item in variantobject.ListProductInventory)
                            {
                                item.ProductId = VariantproductId;
                                var mapData111 = item.MapTo<Storemey.StoreInventory.Dto.CreateStoreInventoryInputDto>();
                                await _StoreInventoryAppService.Create(mapData111);

                            }
                        }

                        if (variantobject.ListProductImages != null)
                        {
                            await _StoreProductImagesAppService.DeleteByProductId(VariantproductId);






                            foreach (var item in variantobject.ListProductImages) /*StoreProductImagesDto*/
                            {
                                try
                                {

                                    Bitmap bitmapimage = GetCopyImage(HttpContext.Current.Server.MapPath("/") + item.Image);

                                    string ext = Path.GetExtension(item.Image);
                                    string newfilename = Guid.NewGuid().ToString() + ext;

                                    bitmapimage.Save(HttpContext.Current.Server.MapPath("/TempImage/") + Path.GetFileName(newfilename));

                                    //var sourcePath = HttpContext.Current.Server.MapPath("/") + item.Image;
                                    //var destinationPath = HttpContext.Current.Server.MapPath("/TempImage/") + item.Image;
                                    //if (File.Exists(sourcePath))
                                    //{
                                    //    if (File.Exists(destinationPath))
                                    //        File.Delete(destinationPath);
                                    //    File.Move(sourcePath, destinationPath);
                                    //}
                                    item.Image = "/TempImage/" + Path.GetFileName(newfilename);
                                }
                                catch (Exception)
                                {

                                }

                            }



                            for (int i = 1; i <= 10; i++)
                            {
                                CommonEntityHelper.DeleteOldProducts(HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + productImage + backslace, "pv_" + variantId.ToString() + "_" + i);
                            }


                            int count1 = 1;
                            foreach (var item in variantobject.ListProductImages) /*StoreProductImagesDto*/
                            {
                                item.ProductId = VariantproductId;

                                var mapData1111 = item.MapTo<Storemey.StoreProductImages.Dto.CreateStoreProductImagesInputDto>();

                                await _StoreProductImagesAppService.Create(mapData1111, count1, "pv_");
                                count1++;
                            }

                        }


                    }
                }

            }
            catch (Exception ex)
            {
            }


        }
        public static Bitmap GetCopyImage(string path)
        {
            using (Bitmap im = (Bitmap)Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }
        public async Task<GetStoreMastersDto> GetMastersdata()
        {
            GetStoreMastersDto result = new GetStoreMastersDto();

            result.ListCategories = await _StoreCategoriesAppService.ListAll();

            result.ListBrands = await _StoreBrandsAppService.ListAll();

            result.ListTags = await _StoreTagsAppService.ListAll();

            result.ListSeasons = await _StoreSeasonsAppService.ListAll();

            result.ListSuppliers = await _StoreSuppliersAppService.ListAll();

            result.ListTax = await _StoreTaxAppService.ListAll();

            result.ListProductVariants = await _StoreProductVariantsAppService.ListAll();

            result.ListProductVariantValues = await _StoreProductVariantValuesAppService.ListAll();

            //result.ListOutlets = await _StoreOutletsAppService.ListAll();

            return result;
        }


    }
}