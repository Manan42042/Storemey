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
using Storemey.StoreSalePayments.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreSalePayments
{
    [AbpAuthorize]
    public class StoreSalePaymentsAppService : AbpServiceBase, IStoreSalePaymentsAppService
    {
        private readonly IStoreSalePaymentsManager _StoreSalePaymentsManager;
        private readonly IRepository<StoreSalePayments, Guid> _StoreSalePaymentsRepository;

        public StoreSalePaymentsAppService(
            IStoreSalePaymentsManager StoreSalePaymentsManager,
            IRepository<StoreSalePayments, Guid> StoreSalePaymentsRepository)
        {
            _StoreSalePaymentsManager = StoreSalePaymentsManager;
            _StoreSalePaymentsRepository = StoreSalePaymentsRepository;
        }

        public async Task<ListResultDto<GetStoreSalePaymentsOutputDto>> ListAll()
        {
            var events = await _StoreSalePaymentsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreSalePaymentsOutputDto>>();
            return new ListResultDto<GetStoreSalePaymentsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreSalePaymentsInputDto input)
        {
            var mapData = input.MapTo<StoreSalePayments>();
            await _StoreSalePaymentsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreSalePaymentsInputDto input)
        {
            var mapData = input.MapTo<StoreSalePayments>();
            await _StoreSalePaymentsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreSalePaymentsInputDto input)
        {
            var mapData = input.MapTo<StoreSalePayments>();
            await _StoreSalePaymentsManager
                .Delete(mapData);
        }


        public async Task<GetStoreSalePaymentsOutputDto> GetById(GetStoreSalePaymentsInputDto input)
        {
            var registration = await _StoreSalePaymentsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreSalePaymentsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreSalePaymentsOutputDto>> GetAdvanceSearch(StoreSalePaymentsAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreSalePaymentsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreSalePaymentsOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreSalePaymentsManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreSalePaymentsOutputDto>(mapDataquery);


        }


    }
}