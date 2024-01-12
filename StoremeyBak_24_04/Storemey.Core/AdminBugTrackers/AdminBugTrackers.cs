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


namespace Storemey.AdminBugTrackers
{
    [Table("AdminBugTrackers")]
    public class AdminBugTrackers : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Discription { get; set; }
        public virtual string BugURL { get; set; }
        public virtual DateTime BugDate { get; set; }
        public virtual string BugFromName { get; set; }
        public virtual string BugFromEmail { get; set; }
        public virtual string BugStoreName { get; set; }
        public virtual string AttchedFileName { get; set; }
        public virtual DateTime DeadLineDate { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime BugResolveDate { get; set; }
        public virtual string BugSolvedByName { get; set; }



        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public AdminBugTrackers()
        {
            LastModificationTime = DateTime.UtcNow;
        }
    }
}
