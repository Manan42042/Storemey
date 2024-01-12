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


namespace Storemey.AdminBugTrackerComments
{
    [Table("AdminBugTrackerComments")]
    public class AdminBugTrackerComments : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime CommentDate { get; set; }
        public virtual string CommentBy { get; set; }
        public virtual string AttachedFile { get; set; }

        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public AdminBugTrackerComments()
        {
            LastModificationTime = DateTime.UtcNow;
        }
    }
}
