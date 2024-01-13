using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace Storemey.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant)), AutoMapFrom(typeof(Tenant))]
    public class TenantDto : EntityDto
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string ConnectionString { get; set; }

        public DateTime StoreExpireDate { get; set; }

        public bool PaidUser { get; set; }

        public long PlanID { get; set; }

        public long PlanAmount { get; set; }

        public DateTime LastPaidDate { get; set; }


    }
}
