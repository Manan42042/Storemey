using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreRegisters.Dto
{
    public class GetStoreRegistersOutputDto
    {
        public virtual long Id { get; set; }
        public string RegisterName { get; set; }
        public long OutletId { get; set; }
        public long ReceiptTemplateId { get; set; }
        public string ReceiptNumber { get; set; }
        public string ReceiptPrefix { get; set; }
        public string ReceiptSuffix { get; set; }
        public bool SelectUserForNextSale { get; set; }
        public bool EmailReceipt { get; set; }
        public bool PrintReceipt { get; set; }
        public string AskForNote { get; set; }
        public bool PrintNoteOnReceipt { get; set; }
        public bool ShowDiscountOnReceipt { get; set; }

        public string OutletName { get; set; }
        public string ReceiptTemplateName { get; set; }


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