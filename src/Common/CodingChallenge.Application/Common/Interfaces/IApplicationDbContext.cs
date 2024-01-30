using System.Threading;
using System.Threading.Tasks;
using CodingChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Application.Common.Interfaces
{
    /// <summary>
    /// application db context contract
    /// </summary>
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }

        DbSet<ProductBrand> ProductBrands { get; set; }

        DbSet<ProductType> ProductTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
