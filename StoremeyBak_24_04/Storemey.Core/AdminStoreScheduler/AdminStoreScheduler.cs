using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;
using System;


namespace Storemey.AdminStoreScheduler
{
    [Table("AdminStoreScheduler")]
    public class AdminStoreScheduler : FullAuditedEntity<Guid>
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
        
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public AdminStoreScheduler()
        {
            LastModificationTime = DateTime.UtcNow;
        }
    }
}
