using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StorePaymentTypes
{
    [Table("StorePaymentTypes")]
    public class StorePaymentTypes : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int MerchantId { get; set; }
        public virtual int TerminalId { get; set; }

        public virtual bool IsPOSPaymentType { get; set; }
        public virtual bool IsEcommarcePaymentType { get; set; }


        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public StorePaymentTypes()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
