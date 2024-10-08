﻿using ShortSharing.API.Dtos.CategoryDtos;
using ShortSharing.API.Dtos.ImageDtos;
using ShortSharing.API.Dtos.TypeDtos;

namespace ShortSharing.API.Dtos.ThingDtos; 

public record ThingDto
{
    public Guid Id { get ; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid OwnerId { get; set; }
    public required ShortCategoryDto Category { get; set; }
    public required ShortTypeDto Type { get; set; }
    public List<ImageDto> Images { get; set; } = [];
}
