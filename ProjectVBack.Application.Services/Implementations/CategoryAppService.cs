﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectVBack.Infrastructure.Repositories;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;

namespace ProjectVBack.Application.Services.Implementations
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoryAppService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<bool> CreateCategoryAsync()
        {

            throw new NotImplementedException();
        }
    }
}