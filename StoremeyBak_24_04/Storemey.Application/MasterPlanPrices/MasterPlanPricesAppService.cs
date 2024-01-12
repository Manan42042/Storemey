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
            var mapData = input.MapTo<MasterPlanPrices>();
            await _MasterPlanPricesManager
                .Create(mapData);
        }

        public async Task Update(UpdateMasterPlanPricesInputDto input)
        {
            var mapData = input.MapTo<MasterPlanPrices>();
            await _MasterPlanPricesManager
                .Create(mapData);
        }
        
        public async Task Delete(DeleteMasterPlanPricesInputDto input)
        {
            var mapData = input.MapTo<MasterPlanPrices>();
            await _MasterPlanPricesManager
                .Create(mapData);
        }
        
        
        public async Task<GetMasterPlanPricesOutputDto> GetById(GetMasterPlanPricesInputDto input)
        {
            var registration = await _MasterPlanPricesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetMasterPlanPricesOutputDto>();

            return mapData;
        }

        public async Task<ListResultDto<GetMasterPlanPricesOutputDto>> GetAdvanceSearch(MasterPlanPricesAdvanceSearchInputDto input)
        {
            var registration = await _MasterPlanPricesManager.ListAll();

            var filtereddatat = registration.ToList().Skip(input.CurrentPage * input.MaxRecords).Take(input.CurrentPage).ToList();
            //.Result.Skip(input.CurrentPage * input.MaxRecords).Take(input.CurrentPage).ToList()
            var mapData = filtereddatat.MapTo<ListResultDto<GetMasterPlanPricesOutputDto>>();

            return mapData;
        }
    }
}