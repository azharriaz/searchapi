using CodingChallenge.Domain.Entities;
using Mapster;

namespace CodingChallenge.Application.Dto
{
    public class ProductDto : IRegister
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductDto>()
            .Map(dest => dest.Brand,
                src => src.ProductBrand.Name)
            .Map(dest => dest.Type,
                src => src.ProductType.Name);
        }
    }
}
