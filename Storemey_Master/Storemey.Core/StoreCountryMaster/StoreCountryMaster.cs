using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreCountryMaster
{
    [Table("StoreCountryMaster")]
    public class StoreCountryMaster : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }


        public virtual string CountryName { get; set; }
        public virtual string CountryCode { get; set; }

               
        /// <summary>
        /// Default Records
        /// </summary>
        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public StoreCountryMaster()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
