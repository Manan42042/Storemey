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


namespace Storemey.AdminStores
{
    [Table("AdminStores")]
    public class AdminStores : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public string Name { get; set; }
        public string StoreName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string Currancy { get; set; }
        public long? CountryId { get; set; }
        public string State { get; set; }
        public long? StateId { get; set; }
        public string City { get; set; }
        public long? CityId { get; set; }
        public long? ZipCode { get; set; }
        public string TimeZone { get; set; }
        public string Language { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? PaidUser { get; set; }
        public long? PlanID { get; set; }
        public long? PlanAmount { get; set; }
        public DateTime? LastPaidDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ConnectionString { get; set; }
        public string TotalOutlets { get; set; }
        public string TotalRegisters { get; set; }

        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public AdminStores()
        {
            LastModificationTime = DateTime.UtcNow;
        }
    }
}
