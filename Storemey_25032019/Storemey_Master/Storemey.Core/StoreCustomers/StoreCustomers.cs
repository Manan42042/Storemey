using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreCustomers
{
    [Table("StoreCustomers")]
    public class StoreCustomers : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }


        public virtual string CustomerCode { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Gender { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Bill_Email { get; set; }
        public virtual string Bill_Phone { get; set; }
        public virtual string Bill_Address { get; set; }
        public virtual string Bill_Country { get; set; }
        public virtual string Bill_State { get; set; }
        public virtual string Bill_City { get; set; }
        public virtual string Bill_ZipCode { get; set; }
        public virtual int Creaditlimit { get; set; }
        public virtual string Ship_FirstName { get; set; }
        public virtual string Ship_LastName { get; set; }
        public virtual string Ship_Address { get; set; }
        public virtual string Ship_Country { get; set; }
        public virtual string Ship_State { get; set; }
        public virtual string Ship_City { get; set; }
        public virtual string Ship_Zipcode { get; set; }
        public virtual string Ship_Phone { get; set; }
        public virtual string Ship_Email { get; set; }



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
        public StoreCustomers()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
