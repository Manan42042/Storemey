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
using Storemey.MasterPlanPrices.Dto;

namespace Storemey.MasterPlanPrices
{
    [AbpAuthorize]
    public class MasterPlanPricesAppService : IMasterPlanPricesAppService
    {
        private readonly IMasterPlanPricesManager _MasterPlanPricesManager;
        private readonly IRepository<MasterPlanPrices, Guid> _MasterPlanPricesRepository;

        public MasterPlanPricesAppService(
            IMasterPlanPricesManager MasterPlanPricesManager,
            IRepository<MasterPlanPrices, Guid> MasterPlanPricesRepository)
        {
            _MasterPlanPricesManager = MasterPlanPricesManager;
            _MasterPlanPricesRepository = MasterPlanPricesRepository;
        }

        public async Task<ListResultDto<GetMasterPlanPricesOutputDto>> ListAll()
        {
            var events = await _MasterPlanPricesManager.ListAll();
            return new ListResultDto<GetMasterPlanPricesOutputDto>(events.MapTo<List<GetMasterPlanPricesOutputDto>>());
        }

        public async Task Create(CreateMasterPlanPricesInputDto input)
        {
            try
            {
                input.LastModificationTime = DateTime.UtcNow;
                input.CreationTime = DateTime.UtcNow;
                input.DeletionTime = DateTime.UtcNow;
                var mapData = input.MapTo<MasterPlanPrices>();
                await _MasterPlanPricesManager
                    .Create(mapData);

            }
            catch (Exception exxx)
            {

            }
        }

        public async Task Update(UpdateMasterPlanPricesInputDto input)
        {
            try
            {
                input.LastModificationTime = DateTime.UtcNow;
                input.CreationTime = DateTime.UtcNow;
                input.DeletionTime = DateTime.UtcNow;
                var mapData = input.MapTo<MasterPlanPrices>();
                await _MasterPlanPricesManager
                    .Update(mapData);

            }
            catch (Exception exxx)
            {

            }
        }

        public async Task Delete(DeleteMasterPlanPricesInputDto input)
        {
            try
            {

                var mapData = input.MapTo<MasterPlanPrices>();
                await _MasterPlanPricesManager
                    .Delete(mapData);
            }
            catch (Exception ex)
            {

            }
        }


        public async Task<GetMasterPlanPricesOutputDto> GetById(GetMasterPlanPricesInputDto input)
        {
            var registration = await _MasterPlanPricesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetMasterPlanPricesOutputDto>();

            return mapData;
        }

        public async Task<ListResultDto<GetMasterPlanPricesOutputDto>> GetAdvanceSearch(MasterPlanPricesAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _MasterPlanPricesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.sortColumn, input.sortDirection);

            var mapDataquery = filtereddatatquery.MapTo<List<GetMasterPlanPricesOutputDto>>();
            return new ListResultDto<GetMasterPlanPricesOutputDto>(mapDataquery);

        }
    }
}