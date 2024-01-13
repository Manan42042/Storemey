using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreWarehouses.Dto
{
    public class GetStoreWarehousesInputDto
    {
        public virtual long Id { get; set; }

        public string WarehouseName { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Country { get; set; }
        public long CountryId { get; set; }
        public string State { get; set; }
        public long StateId { get; set; }
        public string City { get; set; }
        public long CityId { get; set; }
        public string ZipCode { get; set; }
        public string ContactNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }

        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }

    }
}