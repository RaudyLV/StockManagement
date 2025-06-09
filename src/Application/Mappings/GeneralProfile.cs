
using Application.DTOs;
using Application.Features.Brands.Commands.CreateBrandCommand;
using Application.Features.Brands.Commands.DeleteBrandCommand;
using Application.Features.Brands.Commands.UpdateBrandCommand;
using Application.Features.Products.Commands.CreateProductCommand;
using Application.Features.Products.Commands.DeleteProductCommand;
using Application.Features.Products.Commands.UpdateProductCommand;
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
            CreateMap<InventoryMovements, InventoryMovementsDto>();
            CreateMap<Brand, BrandDto>();
            #endregion

            #region  Commands
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<DeleteProductCommand, Product>();

            CreateMap<CreateBrandCommand, Brand>();
            CreateMap<UpdateBrandCommand, Brand>();
            CreateMap<DeleteBrandCommand, Brand>();          
            #endregion 
        }
    }
}