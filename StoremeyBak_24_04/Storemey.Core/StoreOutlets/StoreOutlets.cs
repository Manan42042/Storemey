using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreOutlets
{
    [Table("StoreOutlets")]
    public class StoreOutlets : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }

        public string OutletName { get; set; }
        public long WarehouseId { get; set; }
        public string OrderNumberPrefix { get; set; }
        public string OrderNumber { get; set; }
        public bool IsEnableNagativeInventory { get; set; }
        public string SupplierReturnPrefix { get; set; }
        public string SupplierReturnNumber { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Country { get; set; }
        public long CountryId { get; set; }
        public string State { get; set; }
        public long StateId { get; set; }

        public string City { get; set; }
        public long CityId { get; set; }
        public string ZipCode { get; set; }
        public string TimeZone { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string MobileNumber { get; set; }

        public long TimeZoneId { get; set; }


        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public StoreOutlets()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
