using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreRoles.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreRolesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public string Name { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public virtual string Date { get; set; }

    }
}