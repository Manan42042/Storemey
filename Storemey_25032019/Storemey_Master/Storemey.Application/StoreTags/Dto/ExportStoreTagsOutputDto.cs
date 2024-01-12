using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreTags.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreTagsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public virtual string TagName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Note { get; set; }
        public virtual string Date { get; set; }

    }
}