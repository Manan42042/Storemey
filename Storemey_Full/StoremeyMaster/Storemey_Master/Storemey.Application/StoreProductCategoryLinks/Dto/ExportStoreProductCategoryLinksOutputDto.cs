using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductCategoryLinks.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductCategoryLinksOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public long? CategoryId { get; set; }

        public virtual string Date { get; set; }

    }
}