using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCategories.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreCategoriesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public virtual string CategoryName { get; set; }
        public virtual string Image { get; set; }
        public virtual string Note { get; set; }
        public virtual string Date { get; set; }
        public virtual string Description { get; set; }


    }
}