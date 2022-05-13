using AutoMapper;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Dtos.CategoryService;
using ProjectVBack.Domain.Entities;

namespace ProjectVBack.Application.Services.Configuration
{
    public class GlobalAppProfile : Profile
    {
        public GlobalAppProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, AddCategoryRequest>().ReverseMap();
            CreateMap<Category, EditCategoryRequest>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<Transaction, AddTransactionRequest>().ReverseMap();
            CreateMap<Transaction, EditTransactionRequest>().ReverseMap();
            CreateMap<User, RegisterRequest>().ReverseMap();
            CreateMap<UserDto, RegisterRequest>().ReverseMap();
        }
    }
}
