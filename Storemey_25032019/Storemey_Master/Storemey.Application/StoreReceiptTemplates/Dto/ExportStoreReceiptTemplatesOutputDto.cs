using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreReceiptTemplates.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreReceiptTemplatesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }
        public virtual string HeaderText { get; set; }
        public virtual string InvoiceNoPrefix { get; set; }
        public virtual string InoviceHeading { get; set; }
        public virtual string Date { get; set; }

    }
}