using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreSaleItems.Dto
{
    public class GetStoreSaleItemsOutputDto
    {
        public virtual long Id { get; set; }


        public long? SaleId { get; set; }
        public string InvoiceId { get; set; }
        public long? ProductId { get; set; }
        public long? GiftcardId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemAmount { get; set; }


        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual int? recordsTotal { get; set; }
    }
}