using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductBrandLinks.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductBrandLinksOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public long? BrandId { get; set; }
        public virtual string Date { get; set; }

    }
}