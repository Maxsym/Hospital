using AutoMapper;
using Hospital.API.Dtos.Category;
using Hospital.API.Dtos.Product;
using Hospital.API.Extensions;
using Hospital.BL.Enums;
using Hospital.DAL.Domains;

namespace Hospital.API.Automapper
{
    public class HospitalProfile : Profile
    {
        public HospitalProfile()
        {
            CreateMap<Product, ProductReadDto>().ForMember(src => src.UnitOfMeasurement,opt => opt.MapFrom(src => src.UnitOfMeasurement.GetDescriptionString()));
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>().ForMember(src => src.UnitOfMeasurement,
                opt => opt.MapFrom(src => (EUnitOfMeasurement)src.UnitOfMeasurement));
            
            CreateMap<ProductUpdateDto, Product>().ForMember(src => src.UnitOfMeasurement, opt => opt.MapFrom(src => (EUnitOfMeasurement)src.UnitOfMeasurement));
            CreateMap<Category, CategoryReadDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

        }
    }
}
