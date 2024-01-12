using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminBugTrackerComments.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportAdminBugTrackerCommentsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime CommentDate { get; set; }
        public virtual string CommentBy { get; set; }
        public virtual string AttachedFile { get; set; }
        public virtual string Date { get; set; }

    }
}