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
using AutoMapper;
using System.Drawing;
using Storemey.StoreProducts;

namespace Storemey.POSService
{
    [AbpAuthorize]
    public class POSService : AbpServiceBase, IPOSService
    {
        private readonly IStoreProductsAppService _StoreProductsAppService;



        public POSService(
            IStoreProductsAppService StoreProductsAppService
            )
        {
            _StoreProductsAppService = StoreProductsAppService;
        }



        public Task<string> GetsaleById() {

            return null;
        }

        public Task<string> SaveOrUpdatesale()
        {
            return null;
        }

        public Task<string> GetMastersdata() {
            return null;
        }
    }
}