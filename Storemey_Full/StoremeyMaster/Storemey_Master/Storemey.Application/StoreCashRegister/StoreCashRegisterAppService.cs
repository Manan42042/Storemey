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
using Storemey.StoreCashRegister.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreCashRegister
{
    [AbpAuthorize]
    public class StoreCashRegisterAppService : AbpServiceBase, IStoreCashRegisterAppService
    {
        private readonly IStoreCashRegisterManager _StoreCashRegisterManager;
        private readonly IRepository<StoreCashRegister, Guid> _StoreCashRegisterRepository;

        public StoreCashRegisterAppService(
            IStoreCashRegisterManager StoreCashRegisterManager,
            IRepository<StoreCashRegister, Guid> StoreCashRegisterRepository)
        {
            _StoreCashRegisterManager = StoreCashRegisterManager;
            _StoreCashRegisterRepository = StoreCashRegisterRepository;
        }

        public async Task<ListResultDto<GetStoreCashRegisterOutputDto>> ListAll()
        {
            var events = await _StoreCashRegisterManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCashRegisterOutputDto>>();
            return new ListResultDto<GetStoreCashRegisterOutputDto>(returnData);
        }


        public async Task Create(CreateStoreCashRegisterInputDto input)
        {
            var mapData = input.MapTo<StoreCashRegister>();
            await _StoreCashRegisterManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreCashRegisterInputDto input)
        {
            var mapData = input.MapTo<StoreCashRegister>();
            await _StoreCashRegisterManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreCashRegisterInputDto input)
        {
            var mapData = input.MapTo<StoreCashRegister>();
            await _StoreCashRegisterManager
                .Delete(mapData);
        }


        public async Task<GetStoreCashRegisterOutputDto> GetById(GetStoreCashRegisterInputDto input)
        {
            var registration = await _StoreCashRegisterManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreCashRegisterOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreCashRegisterOutputDto>> GetAdvanceSearch(StoreCashRegisterAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreCashRegisterManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreCashRegisterOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreCashRegisterOutputDto>(mapDataquery);


        }


      
    }
}