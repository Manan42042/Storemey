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

namespace Storemey.StoreBrands
{
    [AbpAuthorize]
    public class StoreBrandsAppService : AbpServiceBase, IStoreBrandsAppService
    {
        private readonly IStoreBrandsManager _StoreBrandsManager;
        private readonly IRepository<StoreBrands, Guid> _StoreBrandsRepository;

        public StoreBrandsAppService(
            IStoreBrandsManager StoreBrandsManager,
            IRepository<StoreBrands, Guid> StoreBrandsRepository)
        {
            _StoreBrandsManager = StoreBrandsManager;
            _StoreBrandsRepository = StoreBrandsRepository;
        }

        public async Task<ListResultDto<GetStoreBrandsOutputDto>> ListAll()
        {
            var events = await _StoreBrandsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreBrandsOutputDto>>();
            return new ListResultDto<GetStoreBrandsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreBrandsInputDto input)
        {
            var mapData = input.MapTo<StoreBrands>();
            await _StoreBrandsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreBrandsInputDto input)
        {
            var mapData = input.MapTo<StoreBrands>();
            await _StoreBrandsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreBrandsInputDto input)
        {
            var mapData = input.MapTo<StoreBrands>();
            await _StoreBrandsManager
                .Delete(mapData);
        }


        public async Task<GetStoreBrandsOutputDto> GetById(GetStoreBrandsInputDto input)
        {
            var registration = await _StoreBrandsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreBrandsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreBrandsOutputDto>> GetAdvanceSearch(StoreBrandsAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreBrandsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreBrandsOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreBrandsOutputDto>(mapDataquery);


        }




     
    }
}