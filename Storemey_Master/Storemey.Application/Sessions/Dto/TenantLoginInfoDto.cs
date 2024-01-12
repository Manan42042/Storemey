using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Storemey.MultiTenancy;

namespace Storemey.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}