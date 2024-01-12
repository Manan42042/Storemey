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
using Storemey.StoreProductImages.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;
using Storemey.Application;
using System.Web.Hosting;

namespace Storemey.StoreProductImages
{
    [AbpAuthorize]
    public class StoreProductImagesAppService : AbpServiceBase, IStoreProductImagesAppService
    {
        private readonly IStoreProductImagesManager _StoreProductImagesManager;
        private readonly IRepository<StoreProductImages, Guid> _StoreProductImagesRepository;
        string folderImage = "images", folderupload = "upload", folderexport = "export", folderbackup = "backup", folderROOT = "storeDocuments", backslace = "/", productImage = "productImage", userImages = "userImages", CatergoryImage = "CatergoryImage", storeImage = "storeImage";


        public StoreProductImagesAppService(
            IStoreProductImagesManager StoreProductImagesManager,
            IRepository<StoreProductImages, Guid> StoreProductImagesRepository)
        {
            _StoreProductImagesManager = StoreProductImagesManager;
            _StoreProductImagesRepository = StoreProductImagesRepository;
        }

        public async Task<ListResultDto<GetStoreProductImagesOutputDto>> ListAll()
        {
            var events = await _StoreProductImagesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductImagesOutputDto>>();
            return new ListResultDto<GetStoreProductImagesOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductImagesInputDto input, int count, string prifix)
        {
            var mapData = input.MapTo<StoreProductImages>();

            mapData.Size1 = prifix + mapData.ProductId.ToString() + "_" + count;
            mapData.Size2 = prifix + mapData.ProductId.ToString() + "_" + count;
            mapData.Size3 = prifix + mapData.ProductId.ToString() + "_" + count;


            if (!string.IsNullOrEmpty(input.Image))
            {
                var RESULT = CommonEntityHelper.Savenewimage(input.Image, HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + productImage + backslace, prifix + mapData.ProductId.ToString() + "_" + count, prifix + mapData.ProductId.ToString() + "_" + count);
                mapData.Image = RESULT;
            }

            await _StoreProductImagesManager
                .Create(mapData);

        }

        public async Task Update(UpdateStoreProductImagesInputDto input)
        {
            var mapData = input.MapTo<StoreProductImages>();
            await _StoreProductImagesManager
                .Update(mapData);
        }

        public async Task DeleteByProductId(long ProductId)
        {
            await _StoreProductImagesManager
               .DeleteByProductId(ProductId);
        }

        public async Task Delete(DeleteStoreProductImagesInputDto input)
        {
            var mapData = input.MapTo<StoreProductImages>();
            await _StoreProductImagesManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductImagesOutputDto> GetById(GetStoreProductImagesInputDto input)
        {
            var registration = await _StoreProductImagesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductImagesOutputDto>();

            return mapData;
        }


        public async Task<List<GetStoreProductImagesOutputDto>> GetByProduct(long ProductId)
        {
            var registration = await _StoreProductImagesManager.ListAllByProduct(ProductId);

            if (registration == null)
            {
                return new List<GetStoreProductImagesOutputDto>();
            }
            var mapData = registration.MapTo<List<GetStoreProductImagesOutputDto>>();

            return mapData;
        }


        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductImagesOutputDto>> GetAdvanceSearch(StoreProductImagesAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductImagesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductImagesOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductImagesOutputDto>(mapDataquery);


        }


    }
}