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
using Storemey.StoreProducts.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;
using Storemey.Application;
using System.Web.Hosting;
using Storemey.StoreProductImages;

namespace Storemey.StoreProducts
{
    [AbpAuthorize]
    public class StoreProductsAppService : AbpServiceBase, IStoreProductsAppService
    {
        private readonly IStoreProductsManager _StoreProductsManager;
        private readonly IRepository<StoreProducts, Guid> _StoreProductsRepository;

        private readonly IStoreProductImagesAppService _StoreProductImagesAppService;


        string folderImage = "images", folderupload = "upload", folderexport = "export", folderbackup = "backup", folderROOT = "storeDocuments", backslace = "/", productImage = "productImage", userImages = "userImages", CatergoryImage = "CatergoryImage", storeImage = "storeImage";




        public StoreProductsAppService(
            IStoreProductsManager StoreProductsManager,
            IRepository<StoreProducts, Guid> StoreProductsRepository,
              IStoreProductImagesAppService StoreProductImagesAppService)
        {
            _StoreProductsManager = StoreProductsManager;
            _StoreProductsRepository = StoreProductsRepository;

            _StoreProductImagesAppService = StoreProductImagesAppService;
        }

        public async Task<ListResultDto<GetStoreProductsOutputDto>> ListAll()
        {
            var events = await _StoreProductsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductsOutputDto>>();
            return new ListResultDto<GetStoreProductsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductsInputDto input)
        {
            var mapData = input.MapTo<StoreProducts>();
            await _StoreProductsManager
                .Create(mapData);
        }

        public async Task<long> CreateOrUpdate(CreateUpdateStoreProductsDto input)
        {
            if (input.Id > 0)
            {
                var mapData = input.MapTo<StoreProducts>();
                var result = await _StoreProductsManager
                    .Update(mapData);

                return result;
            }
            else
            {
                var mapData = input.MapTo<StoreProducts>();
                var result = await _StoreProductsManager
                    .Create(mapData);

                return result;
            }
            return 0;
        }

        public async Task<long> Create2(CreateUpdateStoreProductsDto input)
        {
            var mapData = input.MapTo<StoreProducts>();
            var result = await _StoreProductsManager
                .Create(mapData);

            return result;
        }

        public async Task Update(UpdateStoreProductsInputDto input)
        {
            var mapData = input.MapTo<StoreProducts>();
            await _StoreProductsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductsInputDto input)
        {
            var mapData = input.MapTo<StoreProducts>();
            await _StoreProductsManager
                .Delete(mapData);




            await _StoreProductImagesAppService.DeleteByProductId(input.Id);

            for (int i = 1; i <= 10; i++)
            {
                CommonEntityHelper.DeleteOldProducts(HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + productImage + backslace, "p_" + input.Id.ToString() + "_" + i);
            }

            var variantdata = await GetVariantProductById(input.Id);

            foreach (var item in variantdata)
            {
                for (int i = 1; i <= 10; i++)
                {
                    CommonEntityHelper.DeleteOldProducts(HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + productImage + backslace, "pv_" + item.Id.ToString() + "_" + i);
                }
            }
        }

        public async Task DeleteByProductId(long ProductId)
        {
            await _StoreProductsManager.DeleteByProductId(ProductId);
        }

        public async Task<GetStoreProductsOutputDto> GetById(GetStoreProductsInputDto input)
        {
            var registration = await _StoreProductsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductsOutputDto>();

            return mapData;
        }

        public async Task<GetStoreProductsOutputDto> GetProductById(long Id)
        {
            var registration = await _StoreProductsManager.GetByID(Id);

            var mapData = registration.MapTo<GetStoreProductsOutputDto>();

            return mapData;
        }

        public async Task<List<GetStoreProductsOutputDto>> GetVariantsByProductId(long Id)
        {
            var registration = await _StoreProductsManager.GetVariantsByProductId(Id);

            var mapData = registration.MapTo<List<GetStoreProductsOutputDto>>();

            return mapData;
        }
        public async Task<List<GetStoreProductsOutputDto>> GetVariantProductById(long Id)
        {
            var registration = await _StoreProductsManager.GetVariantsByID(Id);

            var mapData = registration.MapTo<List<GetStoreProductsOutputDto>>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductsOutputDto>> GetAdvanceSearch(StoreProductsAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.MapTo<List<GetStoreProductsOutputDto>>();
            //mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductsOutputDto>(mapDataquery);

        }

    }
}