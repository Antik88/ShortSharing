﻿namespace ShortSharing.DAL.Abstractions
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
