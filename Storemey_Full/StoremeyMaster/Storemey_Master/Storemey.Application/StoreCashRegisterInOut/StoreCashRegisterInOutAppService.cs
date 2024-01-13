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
using Storemey.StoreCashRegisterInOut.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreCashRegisterInOut
{
    [AbpAuthorize]
    public class StoreCashRegisterInOutAppService : AbpServiceBase, IStoreCashRegisterInOutAppService
    {
        private readonly IStoreCashRegisterInOutManager _StoreCashRegisterInOutManager;
        private readonly IRepository<StoreCashRegisterInOut, Guid> _StoreCashRegisterInOutRepository;

        public StoreCashRegisterInOutAppService(
            IStoreCashRegisterInOutManager StoreCashRegisterInOutManager,
            IRepository<StoreCashRegisterInOut, Guid> StoreCashRegisterInOutRepository)
        {
            _StoreCashRegisterInOutManager = StoreCashRegisterInOutManager;
            _StoreCashRegisterInOutRepository = StoreCashRegisterInOutRepository;
        }

        public async Task<ListResultDto<GetStoreCashRegisterInOutOutputDto>> ListAll()
        {
            var events = await _StoreCashRegisterInOutManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCashRegisterInOutOutputDto>>();
            return new ListResultDto<GetStoreCashRegisterInOutOutputDto>(returnData);
        }


        public async Task Create(CreateStoreCashRegisterInOutInputDto input)
        {
            var mapData = input.MapTo<StoreCashRegisterInOut>();
            await _StoreCashRegisterInOutManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreCashRegisterInOutInputDto input)
        {
            var mapData = input.MapTo<StoreCashRegisterInOut>();
            await _StoreCashRegisterInOutManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreCashRegisterInOutInputDto input)
        {
            var mapData = input.MapTo<StoreCashRegisterInOut>();
            await _StoreCashRegisterInOutManager
                .Delete(mapData);
        }


        public async Task<GetStoreCashRegisterInOutOutputDto> GetById(GetStoreCashRegisterInOutInputDto input)
        {
            var registration = await _StoreCashRegisterInOutManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreCashRegisterInOutOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreCashRegisterInOutOutputDto>> GetAdvanceSearch(StoreCashRegisterInOutAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreCashRegisterInOutManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreCashRegisterInOutOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreCashRegisterInOutOutputDto>(mapDataquery);


        }


    }
}