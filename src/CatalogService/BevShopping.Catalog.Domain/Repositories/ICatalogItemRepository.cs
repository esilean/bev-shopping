using BevShopping.Catalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BevShopping.Catalog.Domain.Repositories
{
    public interface ICatalogItemRepository
    {
        Task<IEnumerable<CatalogItem>> FindAllManAsync();
        Task<IEnumerable<CatalogItem>> FindAllWomanAsync();

        Task<CatalogItem> FindAsync(Guid id);
        Task AddAsync(CatalogItem catalogItem);
        Task UpdateAsync(CatalogItem catalogItem);
        Task DeleteAsync(Guid id);
    }
}