using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreStateMaster.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreStateMasterOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string StateName { get; set; }
        public virtual string Date { get; set; }

    }
}