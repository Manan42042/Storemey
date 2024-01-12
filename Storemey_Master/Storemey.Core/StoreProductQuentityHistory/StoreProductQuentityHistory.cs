using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreProductQuentityHistory
{
    [Table("StoreProductQuentityHistory")]
    public class StoreProductQuentityHistory : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }


        public long? ProductId { get; set; }
        public long? OutletId { get; set; }
        public long? OldValue { get; set; }
        public long? NewValue { get; set; }
        public long? Margin { get; set; }
        public long? FinalQuentity { get; set; }
        public string ActionDetail { get; set; }
        public long? ActionById { get; set; }
        public string ActionByName { get; set; }
        public DateTime? ActionDateTime { get; set; }


        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public StoreProductQuentityHistory()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
