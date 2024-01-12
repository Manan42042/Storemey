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
using Storemey.StoreSaleTransactions.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreSaleTransactions
{
    [AbpAuthorize]
    public class StoreSaleTransactionsAppService : AbpServiceBase, IStoreSaleTransactionsAppService
    {
        private readonly IStoreSaleTransactionsManager _StoreSaleTransactionsManager;
        private readonly IRepository<StoreSaleTransactions, Guid> _StoreSaleTransactionsRepository;

        public StoreSaleTransactionsAppService(
            IStoreSaleTransactionsManager StoreSaleTransactionsManager,
            IRepository<StoreSaleTransactions, Guid> StoreSaleTransactionsRepository)
        {
            _StoreSaleTransactionsManager = StoreSaleTransactionsManager;
            _StoreSaleTransactionsRepository = StoreSaleTransactionsRepository;
        }

        public async Task<ListResultDto<GetStoreSaleTransactionsOutputDto>> ListAll()
        {
            var events = await _StoreSaleTransactionsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreSaleTransactionsOutputDto>>();
            return new ListResultDto<GetStoreSaleTransactionsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreSaleTransactionsInputDto input)
        {
            var mapData = input.MapTo<StoreSaleTransactions>();
            await _StoreSaleTransactionsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreSaleTransactionsInputDto input)
        {
            var mapData = input.MapTo<StoreSaleTransactions>();
            await _StoreSaleTransactionsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreSaleTransactionsInputDto input)
        {
            var mapData = input.MapTo<StoreSaleTransactions>();
            await _StoreSaleTransactionsManager
                .Delete(mapData);
        }


        public async Task<GetStoreSaleTransactionsOutputDto> GetById(GetStoreSaleTransactionsInputDto input)
        {
            var registration = await _StoreSaleTransactionsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreSaleTransactionsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreSaleTransactionsOutputDto>> GetAdvanceSearch(StoreSaleTransactionsAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreSaleTransactionsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreSaleTransactionsOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreSaleTransactionsOutputDto>(mapDataquery);


        }


    }
}