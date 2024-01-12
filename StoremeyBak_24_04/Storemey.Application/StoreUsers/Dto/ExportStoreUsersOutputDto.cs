using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreUsers.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreUsersOutputDto
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Note { get; set; }

       public virtual string Date { get; set; }

    }
}