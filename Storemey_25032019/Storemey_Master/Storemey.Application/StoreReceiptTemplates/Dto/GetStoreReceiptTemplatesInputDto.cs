using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreReceiptTemplates.Dto
{
    public class GetStoreReceiptTemplatesInputDto
    {

        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool PrintBarcode { get; set; }
        public virtual string Logo { get; set; }
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
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }

    }
}