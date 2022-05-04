using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories;
using Application.Dishes;
using Application.Ingredients;
using Application.Orders;
using Application.Photos;
using Application.Portions;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<Dish, DishDTO<Guid>>();
            CreateMap<Category, CategoryDTO<Guid>>();
            CreateMap<Portion, PortionDTO<Guid>>()
                .ForMember(p => p.DishName, o => o.MapFrom(pp => pp.Dish.Name));
            CreateMap<Photo, PhotoDTO<string>>();
            CreateMap<Ingredient, IngredientDTO<Guid>>();
            CreateMap<Order, OrderDTO<Guid>>();
            CreateMap<User, ProfileDTO<string>>();
        }
    }
}