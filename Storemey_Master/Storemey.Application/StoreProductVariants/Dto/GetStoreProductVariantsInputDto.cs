﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductVariants.Dto
{
    public class GetStoreProductVariantsInputDto
    {
        public virtual long Id { get; set; }

        public string VariantName { get; set; }

        public virtual string Note { get; set; }
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