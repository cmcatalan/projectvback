﻿using AutoMapper;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Domain.Entities;

namespace ProjectVBack.Application.Services.Configuration
{
    public class GlobalAppProfile : Profile
    {
        public GlobalAppProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            //CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}