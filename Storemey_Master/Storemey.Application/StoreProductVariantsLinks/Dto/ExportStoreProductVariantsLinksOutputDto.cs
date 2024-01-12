using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductVariantsLinks.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductVariantsLinksOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }


        public long? ProductId { get; set; }
        public long? VariantId { get; set; }
        public long? VariantValueId { get; set; }


        public virtual string Date { get; set; }

    }
}