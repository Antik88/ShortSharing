﻿using Rent.Service.Application.Common.Mappings;
using Rent.Service.Domain.Entity;
using Rent.Service.Domain.Enums;

namespace Rent.Service.Application.Model;

public class RentModel : IMapFrom<RentEntity>
{
    public Guid Id { get; set; }
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
    public Guid ThingId { get; set; }
    public Guid TenantId { get; set; }
    public RentStatus Status { get; set; }
}
