using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreBrands.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreBrandsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string BrandName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Date { get; set; }

    }
}