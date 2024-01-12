using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductVariantValues.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductVariantValuesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string BrandName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Date { get; set; }

    }
}