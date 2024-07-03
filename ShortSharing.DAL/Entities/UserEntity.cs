﻿using ShortSharing.DAL.Abstractions;

namespace ShortSharing.DAL.Entities
{
    public class UserEntity : IBaseEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public List<ThingEntity>? Things { get; set; }
        public List<RentEntity>? Rents { get; set; }
    }
}
