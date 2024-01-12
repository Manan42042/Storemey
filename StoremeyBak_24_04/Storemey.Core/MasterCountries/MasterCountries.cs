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


namespace Storemey.MasterCountries
{
    [Table("MasterCountries")]
    public class MasterCountries : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Dail_Code { get; set; }
        public virtual string Currency_Name { get; set; }
        public virtual string Curreny_Symbol { get; set; }
        public virtual string Current_Code { get;set; }
        public virtual string Flagimage { get; set; }
        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public MasterCountries()
        {
            LastModificationTime = DateTime.UtcNow;
        }
    }
}
