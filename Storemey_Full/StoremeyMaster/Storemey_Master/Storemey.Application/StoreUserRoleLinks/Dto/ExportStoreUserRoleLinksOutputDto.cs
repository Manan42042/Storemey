using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreUserRoleLinks.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreUserRoleLinksOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public long? RoleId { get; set; }
        public long? UserId { get; set; }
        public bool IsAccesible { get; set; }

        public virtual string Date { get; set; }

    }
}