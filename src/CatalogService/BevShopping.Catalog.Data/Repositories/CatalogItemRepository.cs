using BevShopping.Catalog.Data.Data;
using BevShopping.Catalog.Domain.Enum;
using BevShopping.Catalog.Domain.Models;
using BevShopping.Catalog.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BevShopping.Catalog.Data.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly CatalogDbContext _context;

        public CatalogItemRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CatalogItem>> FindAllManAsync()
        {
            return await _context.CatalogItems.Find(x => x.CatalogType == CatalogType.Man).ToListAsync();
        }

        public async Task<IEnumerable<CatalogItem>> FindAllWomanAsync()
        {
            return await _context.CatalogItems.Find(x => x.CatalogType == CatalogType.Woman).ToListAsync();
        }

        public async Task<CatalogItem> FindAsync(Guid id)
        {
            return await _context.CatalogItems.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(CatalogItem catalogItem)
        {
            await _context.CatalogItems.InsertOneAsync(catalogItem);
        }

        public async Task UpdateAsync(CatalogItem catalogItem)
        {
            await _context.CatalogItems.ReplaceOneAsync(x => x.Id == catalogItem.Id, catalogItem);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.CatalogItems.DeleteOneAsync(x => x.Id == id);
        }

    }
}