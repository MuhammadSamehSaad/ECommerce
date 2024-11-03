using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entites;
using static System.Net.WebRequestMethods;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnfDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, O => O.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
        }

    }
}
