using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories;
using Application.Dishes;
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
        }
    }
}