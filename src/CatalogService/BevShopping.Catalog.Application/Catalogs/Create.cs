using BevShopping.Catalog.Domain.Enum;
using BevShopping.Catalog.Domain.Models;
using BevShopping.Catalog.Domain.Repositories;
using BevShopping.Shared.Core;
using BevShopping.Shared.Core.Commands;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BevShopping.Catalog.Application.Catalogs
{
    public class Create
    {
        public class Command : ICommand
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public string PictureUri { get; set; }
            public CatalogType CatalogType { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Price).GreaterThanOrEqualTo(1);
                RuleFor(x => x.PictureUri).NotEmpty();
                RuleFor(x => x.CatalogType).NotEmpty();
            }
        }

        public class Handler : ICommandHandler<Command>
        {
            private readonly ICatalogItemRepository _catalogItemRepository;

            public Handler(ICatalogItemRepository catalogItemRepository)
            {
                _catalogItemRepository = catalogItemRepository;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var catalogItem = Mapping.Map<Command, CatalogItem>(request);

                await _catalogItemRepository.AddAsync(catalogItem);

                return Unit.Value;
            }
        }
    }
}