using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.MasterPlanServices.Dto
{
    public class GetMasterPlanServicesOutputDto
    {
        public virtual long Id { get; set; }

        public virtual int PlanID { get; set; }

        public virtual string PlanName { get; set; }
        public virtual string ServiceName { get; set; }

        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }

        public virtual int? recordsTotal { get; set; }
    }
}