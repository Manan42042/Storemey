using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreTimeZones
{
    [Table("StoreTimeZones")]
    public class StoreTimeZones : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public string name { get; set; }
        public string current_utc_offset { get; set; }
        public bool is_currently_dst { get; set; }

        public Boolean IsSelected { get; set; }

        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public StoreTimeZones()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
