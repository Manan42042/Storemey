using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminEmailTemplates.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportAdminEmailTemplatesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string FromEmail { get; set; }
        public virtual string ToEmail { get; set; }
        public virtual string CCEmail { get; set; }
        public virtual string BCCEmail { get; set; }
        public virtual string EmailKey { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual string AttachedFile { get; set; }

        public virtual string Date { get; set; }

    }
}