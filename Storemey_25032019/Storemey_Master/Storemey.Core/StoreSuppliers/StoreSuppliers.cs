using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreSuppliers
{
    [Table("StoreSuppliers")]
    public class StoreSuppliers : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }

        public virtual string SupplierFullName { get; set; }
        public virtual string Description { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Website { get; set; }
        public virtual string Address { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string State { get; set; }
        public virtual string City { get; set; }


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
        public StoreSuppliers()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
