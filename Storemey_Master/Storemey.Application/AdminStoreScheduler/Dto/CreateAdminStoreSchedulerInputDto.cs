using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminStoreScheduler.Dto
{
    public class CreateAdminStoreSchedulerInputDto
    {

        public virtual long Id { get; set; }

        public virtual string StoreName { get; set; }
        public virtual bool SentEmail { get; set; }
        public virtual string FromEmail { get; set; }
        public virtual string ToEmail { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string Status { get; set; }
        public virtual string Frequency { get; set; }
        public virtual string RecurWeekDays { get; set; }
        public virtual string RecurHours { get; set; }

        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }

    }
}