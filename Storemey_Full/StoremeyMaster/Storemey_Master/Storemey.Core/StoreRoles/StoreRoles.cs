using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreRoles
{
    [Table("StoreRoles")]
    public class StoreRoles : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }

        public string Name { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }

        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public StoreRoles()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
