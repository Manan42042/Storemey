using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductVariants.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductVariantsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public string VariantName { get; set; }

        public virtual string Date { get; set; }

    }
}