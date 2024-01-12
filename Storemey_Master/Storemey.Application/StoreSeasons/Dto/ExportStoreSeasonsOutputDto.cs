using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreSeasons.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreSeasonsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public string SeasonName { get; set; }

        public virtual string Date { get; set; }

        public virtual string Description { get; set; }


    }
}