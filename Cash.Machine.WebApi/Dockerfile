FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Cash.Machine.WebApi/Cash.Machine.WebApi.csproj", "Cash.Machine.WebApi/"]
COPY ["Cash.Machine.Ioc/Cash.Machine.Ioc.csproj", "Cash.Machine.Ioc/"]
COPY ["Cash.Machine.Cross.Cutting/Cash.Machine.Cross.Cutting.csproj", "Cash.Machine.Cross.Cutting/"]
COPY ["Cash.Machine.Domain/Cash.Machine.Domain.csproj", "Cash.Machine.Domain/"]
COPY ["Cash.Machine.Application.DTO/Cash.Machine.Application.DTO.csproj", "Cash.Machine.Application.DTO/"]
COPY ["Cash.Machine.Repository/Cash.Machine.Repository.csproj", "Cash.Machine.Repository/"]
COPY ["Cash.Machine.Domain.Core/Cash.Machine.Domain.Core.csproj", "Cash.Machine.Domain.Core/"]
COPY ["Cash.Machine.Data/Cash.Machine.Data.csproj", "Cash.Machine.Data/"]
COPY ["Cash.Machine.Services/Cash.Machine.Services.csproj", "Cash.Machine.Services/"]
COPY ["Cash.Machine.Application/Cash.Machine.Application.csproj", "Cash.Machine.Application/"]
RUN dotnet restore "Cash.Machine.WebApi/Cash.Machine.WebApi.csproj"
COPY . .
WORKDIR "/src/Cash.Machine.WebApi"
RUN dotnet build "Cash.Machine.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cash.Machine.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cash.Machine.WebApi.dll"]