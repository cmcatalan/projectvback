﻿using ProjectVBack.Crosscutting.Utils;

namespace ProjectVBack.Application.Dtos
{
    public record AddCategoryRequest(string Name, string PictureUrl, string Description, CategoryType Type);
}