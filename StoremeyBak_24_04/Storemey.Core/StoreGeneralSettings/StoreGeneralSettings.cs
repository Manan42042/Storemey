using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreGeneralSettings
{
    [Table("StoreGeneralSettings")]
    public class StoreGeneralSettings : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public virtual int TotalOutlets { get; set; }
        public virtual int TotalResiters { get; set; }
        public virtual int TotalUsers { get; set; }
        public virtual long CurrancyId { get; set; }
        public virtual bool MasterRecordEntry { get; set; }
        public virtual int MaxFileStorage { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public StoreGeneralSettings()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
