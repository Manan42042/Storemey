using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCurrencies.Dto
{
    public class CreateStoreCurrenciesInputDto
    {

        public virtual long Id { get; set; }
        public virtual string Currency { get; set; }
        public virtual string Symbol { get; set; }
        public virtual string Digital_code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Country { get; set; }

        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }

    }
}