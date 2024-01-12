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
using Storemey.StoreGiftCards.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreGiftCards
{
    [AbpAuthorize]
    public class StoreGiftCardsAppService : AbpServiceBase, IStoreGiftCardsAppService
    {
        private readonly IStoreGiftCardsManager _StoreGiftCardsManager;
        private readonly IRepository<StoreGiftCards, Guid> _StoreGiftCardsRepository;

        public StoreGiftCardsAppService(
            IStoreGiftCardsManager StoreGiftCardsManager,
            IRepository<StoreGiftCards, Guid> StoreGiftCardsRepository)
        {
            _StoreGiftCardsManager = StoreGiftCardsManager;
            _StoreGiftCardsRepository = StoreGiftCardsRepository;
        }

        public async Task<ListResultDto<GetStoreGiftCardsOutputDto>> ListAll()
        {
            var events = await _StoreGiftCardsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreGiftCardsOutputDto>>();
            return new ListResultDto<GetStoreGiftCardsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreGiftCardsInputDto input)
        {
            var mapData = input.MapTo<StoreGiftCards>();
            await _StoreGiftCardsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreGiftCardsInputDto input)
        {
            var mapData = input.MapTo<StoreGiftCards>();
            await _StoreGiftCardsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreGiftCardsInputDto input)
        {
            var mapData = input.MapTo<StoreGiftCards>();
            await _StoreGiftCardsManager
                .Delete(mapData);
        }


        public async Task<GetStoreGiftCardsOutputDto> GetById(GetStoreGiftCardsInputDto input)
        {
            var registration = await _StoreGiftCardsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreGiftCardsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreGiftCardsOutputDto>> GetAdvanceSearch(StoreGiftCardsAdvanceSearchInputDto input)
        {

            try
            {
                var filtereddatatquery = await _StoreGiftCardsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

                var mapDataquery = filtereddatatquery.MapTo<List<GetStoreGiftCardsOutputDto>>();
                //mapDataquery.ForEach(x => x.recordsTotal = _StoreGiftCardsManager.GetRecordCount().Result);
                return new ListResultDto<GetStoreGiftCardsOutputDto>(mapDataquery);

            }
            catch (Exception ec)
            {

            }
            return null;

        }


    }
}