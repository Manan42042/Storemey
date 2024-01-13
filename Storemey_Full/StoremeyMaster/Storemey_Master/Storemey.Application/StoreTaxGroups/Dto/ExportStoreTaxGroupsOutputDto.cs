using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreTaxGroups.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreTaxGroupsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public virtual int Rate { get; set; }

        public virtual string TaxGroupName { get; set; }
        public virtual string TaxCommaseparated { get; set; }
        public virtual string Note { get; set; }

        public virtual string Date { get; set; }


    }
}