using AutoMapper;
using RhipeApi.Service.ModelDTOs;
using RhipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RhipeApi.Mapper
{


    //public class MemberValueResolver : IMemberValueResolver<object, object, decimal, decimal>
    //{
    //    private readonly IOptions<AppSettings> _settings;
    //    private readonly decimal priceMarkup;
    //    public MemberValueResolver(IOptions<AppSettings> settings)
    //    {
    //        _settings = settings;
    //        priceMarkup = _settings.Value.PriceMarkup;
    //    }
    //    public decimal Resolve(object source, object destination, decimal sourceMember, decimal destinationMember, ResolutionContext context)
    //    {
    //        return (sourceMember) + ((priceMarkup / 100m) * sourceMember);
    //    }
    //}


    public class ValueResolver : IValueResolver<ProductDTO, Product, decimal>
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly decimal priceMarkup;
        public ValueResolver(IOptions<AppSettings> settings)
        {
            _settings = settings;
            priceMarkup = _settings.Value.PriceMarkup;
        }
        public decimal Resolve(ProductDTO source, Product destination, decimal sourceMember, ResolutionContext context)
        {
            return (source.UnitPrice) + ((priceMarkup / 100m) * source.UnitPrice);
        }
    }

    public class ProductMapper : AutoMapper.Profile
    {
        public ProductMapper()
        {
            //CreateMap<ProductDTO, Product>()
            //    .ForMember(dest => dest.PriceWithMarkup, 
            //    opt => opt.MapFrom<MemberValueResolver, decimal>(src => src.UnitPrice));

            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.ProductPrice,
                opt => opt.MapFrom<ValueResolver>());


        }

    }

}
