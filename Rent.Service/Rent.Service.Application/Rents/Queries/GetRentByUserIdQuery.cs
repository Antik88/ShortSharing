﻿using MediatR;
using Rent.Service.Application.Model;

namespace Rent.Service.Application.Rents.Queries;
public record GetRentByUserIdQuery(Guid UserId) : IRequest<List<RentModel>>;