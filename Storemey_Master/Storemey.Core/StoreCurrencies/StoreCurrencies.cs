using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreCurrencies
{
    [Table("StoreCurrencies")]
    public class StoreCurrencies : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public virtual string Currency { get; set; }
        public virtual string Symbol { get; set; }
        public virtual string Digital_code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Country { get; set; }

        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public StoreCurrencies()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
