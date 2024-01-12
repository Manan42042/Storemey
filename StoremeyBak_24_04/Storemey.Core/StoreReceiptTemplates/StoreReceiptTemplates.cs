using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;


namespace Storemey.StoreReceiptTemplates
{
    [Table("StoreReceiptTemplates")]
    public class StoreReceiptTemplates : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool PrintBarcode { get; set; }
        public virtual string Logo { get; set; }
        public virtual string TemplateType { get; set; }
        public virtual string HeaderText { get; set; }
        public virtual string InvoiceNoPrefix { get; set; }
        public virtual string InoviceHeading { get; set; }
        public virtual string ServedByLabel { get; set; }
        public virtual string DiscountLabel { get; set; }
        public virtual string SubTotalLabel { get; set; }
        public virtual string TaxLabel { get; set; }
        public virtual string ToPayLabel { get; set; }
        public virtual string TotalLabel { get; set; }
        public virtual string ChangeLable { get; set; }
        public virtual string FooterText { get; set; }


        /// <summary>
        /// commons
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
        public StoreReceiptTemplates()
        {
            LastModificationTime = DateTime.UtcNow;
        }

    }
}
