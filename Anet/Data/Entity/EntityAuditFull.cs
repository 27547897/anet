﻿using System;

namespace Anet.Data.Entity
{
    public abstract class EntityAuditFull<TKey> : EntityAudit<TKey>, IEntityAuditFull<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey CreatedBy { get; set; }
        public virtual TKey UpdatedBy { get; set; }
    }

    public abstract class EntityAuditFull : EntityAuditFull<long>, IEntityAuditFull
    {
        public void SetId()
        {
            Id = IdGenerator.NewId();
        }
    }
}
