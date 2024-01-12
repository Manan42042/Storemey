using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminUpdateAllDatabase.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportAdminUpdateAllDatabaseOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string Query { get; set; }
        public virtual string Discription { get; set; }
        public virtual string Date { get; set; }

    }
}