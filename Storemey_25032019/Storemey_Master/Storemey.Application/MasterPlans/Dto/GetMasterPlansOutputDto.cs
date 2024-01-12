using Abp.Application.Services.Dto;
using Storemey.MasterPlanPrices.Dto;
using Storemey.MasterPlanServices.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Storemey.MasterPlans.Dto
{
    public class GetMasterPlansOutputDto
    {
        public virtual long Id { get; set; }

        public virtual string PlanName { get; set; }

        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual GetMasterPlanPricesOutputDto Price { get; set; }

        public virtual List<GetMasterPlanServicesOutputDto> PlanServices { get; set; }

        public virtual int? recordsTotal { get; set; }
    }
}