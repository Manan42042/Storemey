﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductCategoryLinks.Dto;

namespace Storemey.StoreProductCategoryLinks
{

    public interface IStoreProductCategoryLinksAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductCategoryLinksOutputDto>> ListAll();

        Task Create(CreateStoreProductCategoryLinksInputDto input);


        Task Update(UpdateStoreProductCategoryLinksInputDto input);

        Task Delete(DeleteStoreProductCategoryLinksInputDto input);


        Task<GetStoreProductCategoryLinksOutputDto> GetById(GetStoreProductCategoryLinksInputDto input);


        Task<ListResultDto<GetStoreProductCategoryLinksOutputDto>> GetAdvanceSearch(StoreProductCategoryLinksAdvanceSearchInputDto input);

        Task CreateProductLinks(long ProductId, long Id);

        Task DeleteByProductId(long ProductId);

        Task<List<GetStoreProductCategoryLinksOutputDto>> GetByProductId(long ProductId);


    }
}