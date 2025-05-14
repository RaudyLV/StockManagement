using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Features.Categories.Commands.CreateCategoryCommand;
using Application.Features.Products.Commands.CreateProductCommand;
using AutoMapper;
using Core.Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region  Dtos
            CreateMap<Product, ProductDto>();
            #endregion

            #region  Commands
            CreateMap<CreateProductCommand, Product>();
            CreateMap<CreateCategoryCommand, Category>();
            #endregion
        }
    }
}