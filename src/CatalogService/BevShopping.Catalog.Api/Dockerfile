FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/CatalogService/BevShopping.Catalog.Api/BevShopping.Catalog.Api.csproj", "src/CatalogService/BevShopping.Catalog.Api/"]
COPY ["src/CatalogService/BevShopping.Catalog.Application/BevShopping.Catalog.Application.csproj", "src/CatalogService/BevShopping.Catalog.Application/"]
COPY ["src/CatalogService/BevShopping.Catalog.Domain/BevShopping.Catalog.Domain.csproj", "src/CatalogService/BevShopping.Catalog.Domain/"]
COPY ["src/CatalogService/BevShopping.Catalog.Data/BevShopping.Catalog.Data.csproj", "src/CatalogService/BevShopping.Catalog.Data/"]

COPY ["shared/BevShopping.Shared.Core/BevShopping.Shared.Core.csproj", "shared/BevShopping.Shared.Core"]

RUN dotnet restore "src/CatalogService/BevShopping.Catalog.Api/BevShopping.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/src/CatalogService/BevShopping.Catalog.Api/"
RUN dotnet build "BevShopping.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BevShopping.Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BevShopping.Catalog.Api.dll"]